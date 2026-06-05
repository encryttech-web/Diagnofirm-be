using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DiagnofirmAdmin.Contaxdb;
using System;
using System.Collections.Generic;
using System.Text;
using DiagnofirmAdmin.Handler;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DiagnofirmAdmin.ErrorHandler;
using Microsoft.OpenApi.Any;
using DiagnofirmAdmin.Middleware;
using DiagnofirmAdmin.Filter;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Collections.Specialized;
using Serilog.Events;

namespace DiagnofirmAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }
        readonly string _specificOrigin = "_specificOrigin";
        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<OidcSettings>(Configuration.GetSection("Oidc"));

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("NoCache",
                    new CacheProfile
                    {
                        Duration = 0,
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problems = new CustomBadRequest(context);
                    return new BadRequestObjectResult(problems);
                };
            });

            var domainWhiteList = Configuration["ApplicationSettings:DOMAINLIST"];             

            string[] whiteList = null;
            if(domainWhiteList != null && domainWhiteList != "")
            {
                whiteList = domainWhiteList.Split(',');
            }
            else
            {
                whiteList = "https://*.saint-gobain.com".Split(",");
            }

            #region Elk Logs
            
            var environment = Configuration["ApplicationSettings:Env"];

            var logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .Enrich.WithMachineName()
                            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);

            string sLogIndexFormat = "GIMAPI-USER-" + environment + "-{0:ddMMyyyy}";
            if (Configuration["ApplicationSettings:LogMethod"].Contains("ElasticSearch", StringComparison.OrdinalIgnoreCase))
            {
                logger.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = sLogIndexFormat,
                    TypeName = null,
                    BatchAction = ElasticOpType.Create,
                    ModifyConnectionSettings = (c) =>
                    {
                        c.ServerCertificateValidationCallback((o, certificate, arg3, arg4) =>
                        {
                            return true;
                        });
                        c.GlobalHeaders(new NameValueCollection { { "Authorization", $"ApiKey {Configuration["ElasticConfiguration:Token"]}" } });
                        return c;
                    }
                });
            }

            Log.Logger = logger.Enrich.WithProperty("Environment", environment)
                          .ReadFrom.Configuration(Configuration)
                          .CreateLogger();
            #endregion

            services.AddCors(o =>
            {
                o.AddPolicy("_specificOrigin",
                    policy => policy.WithOrigins(whiteList)
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Log Register API",
                    //Version = Configuration["ApplicationSettings:Version"].ToString(),
                    Version = "v1",
                    Description = "API for Log register",
                });
                c.OperationFilter<CustomHeaderSwaggerAttribute>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                                }
                            });
                c.CustomSchemaIds(type => type.ToString());
            });

            services.AddDbContext<AuthenticationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            var authMode = Configuration["ApplicationSettings:AuthMode"].ToString();
            //Configure Key for SSO and CloudSSO Change
            if (string.Equals(authMode, "KEYCLOAK"))
            {
                KeyCloakValidationAttributes(services);
            }
            else
            {
                CloudSSOValidationAttributes(services);
            }

        }

        private void KeyCloakValidationAttributes(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = this.Configuration["Oidc:Authority"];
                options.ClaimsIssuer = this.Configuration["Oidc:Authority"];
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = this.Configuration["Oidc:Authority"],
                    ValidateLifetime = true,
                    ClockSkew = System.TimeSpan.Zero,
                    RequireSignedTokens = false,
                    RequireAudience = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = (new JsonWebKeySet(OidcIssuer.sIssuingKey)).Keys
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });
        }

        private void CloudSSOValidationAttributes(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<AnyType> logger)
        {
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("X-Frame-Options", "DENY");
                ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always,
                    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_specificOrigin);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var basepath = Configuration["ApplicationSettings:PathBase"].ToString();
            //app.UseSwagger();
            //app.UseSwaggerAuthorized();
            app.UseSwagger(c =>
            {

                if (basepath.Length != 0)
                {
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basepath}" } };
                    });
                }
            });
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Log register API " + env.EnvironmentName);
                c.RoutePrefix = string.Empty;
            });
        }
    }

    public static class SwaggerBuilder
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }

    public static class OidcIssuer
    {
        private static string issureKey = "{" +
    "\"keys\": [" +
      "{" +
        "\"kid\": \"1v2dDqGrAoZ4_MyER2bSsAV6mUEZKKEWrY-18EgFIhk\"," +
        "\"kty\": \"RSA\"," +
        "\"alg\": \"RSA-OAEP\"," +
        "\"use\": \"enc\"," +
        "\"n\": \"i5Vk23HeQPRuR6ERBV67wfqHi7But2ga9BZftzVhzHb8OpNmJiirkRbzvEDYnCb4AyRTbWV77nz52rMehlZX0BmhoPlL2FN9wFsmZ9spFVh7RcLGLSjLVIQQFQ0rBTxJLm1nUTftmysRg4UyWccEtD3fMT8vxUJxA6xDp2UnsE2ierMIUuipN5qJpY9miqYEI-6oB319_Z3cVLjNnivVIZuS3GlKTqMZ9edTWhf07-ljHkGFcoMkvasCjdPQyRnkKheptzbY9DBjwwcqmNyJe8XEyXMe8bwUvH3Lq-ai8uF6CFi3PsN8hPe6hfB8m6JUOlKo16oxxrvFpSnOxRafDQ\"," +
        "\"e\": \"AQAB\"," +
        "\"x5c\": [" +
          "\"MIICpTCCAY0CBgGAvQ2xzjANBgkqhkiG9w0BAQsFADAWMRQwEgYDVQQDDAtQb2xhcmlzLVBvQzAeFw0yMjA1MTMxMDUyMTJaFw0zMjA1MTMxMDUzNTJaMBYxFDASBgNVBAMMC1BvbGFyaXMtUG9DMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAi5Vk23HeQPRuR6ERBV67wfqHi7But2ga9BZftzVhzHb8OpNmJiirkRbzvEDYnCb4AyRTbWV77nz52rMehlZX0BmhoPlL2FN9wFsmZ9spFVh7RcLGLSjLVIQQFQ0rBTxJLm1nUTftmysRg4UyWccEtD3fMT8vxUJxA6xDp2UnsE2ierMIUuipN5qJpY9miqYEI+6oB319/Z3cVLjNnivVIZuS3GlKTqMZ9edTWhf07+ljHkGFcoMkvasCjdPQyRnkKheptzbY9DBjwwcqmNyJe8XEyXMe8bwUvH3Lq+ai8uF6CFi3PsN8hPe6hfB8m6JUOlKo16oxxrvFpSnOxRafDQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQBp+7+aHnQhy9YI0oZTMdIseldTxS8heQzeNenjhNTL1lLBpAuURZStOI+EwHA7RhUghe1VGaKe4fdnhs6poe94yLQOblWNnBFm6kWPOnYmcn/PJdmr2SeiRvy//TEb9owB0y+tVO2aC8SV777qx5HYwi9f0vk+SFLZE0vdqwF3aNRkMU7IjAY1jv/MaWPU8H6hZjAqh6Oa6HtiFC7klXkL96FZwW0oMkj+FDAwdGUOGt8749oWlJca783NesIktTZjBKvxugi6G+/7yJxU9TUEg4L1sYdMf3PZi6VjScuSXRsDrF7JAMWT+qLOZeq0iHh3kRxNe1K8wsOX+W7LKrS3\"" +
        "]," +
        "\"x5t\": \"A8rv_z78eOnuUnKr7QXzegcVzmk\"," +
        "\"x5t#S256\": \"Ahb7JwEa1gnXgv9hXIQS2oXVXFaNAn344TP77xXBpNA\"" +
      "}," +
      "{" +
        "\"kid\": \"fdqE5roJ00h1h6krvSMwfC3C_s03X0VgFpea8gGsKqQ\"," +
        "\"kty\": \"RSA\"," +
        "\"alg\": \"RS256\"," +
        "\"use\": \"sig\"," +
        "\"n\": \"t7yxkCVxVunbjq5kNj2YX4EOBPkRDiz57tbQ3mdXx6rOoaHxPqPErOD-rkhGZUF-lgYdy7U7ts9DdEHpqD9eUtJYpnQ3o01ALB3XdVEnRzNMqWpg3nY36-20XyODufTY4EuOWg7aPaGvClLAy1k_w-wQZLsrUYW8NcDD9Xj7Ry4DdmaQdKomif7NGPyaV9KT3gscN0eht9_6SoeuSJbJrwyN79d_OuXyED7rRNQHW_ii7wVmt-kCQpYLBRcDjXYHoTNawDL1N8Cvq7Q6nOCWfbJQZD73SPc_iVvk2vPfVAJFPYSxG8rpr2uVeF1kseEt8pVHmAk5hZf11R69gxnD9w\"," +
        "\"e\": \"AQAB\"," +
        "\"x5c\": [" +
          "\"MIICpTCCAY0CBgGAvQ2wWTANBgkqhkiG9w0BAQsFADAWMRQwEgYDVQQDDAtQb2xhcmlzLVBvQzAeFw0yMjA1MTMxMDUyMTJaFw0zMjA1MTMxMDUzNTJaMBYxFDASBgNVBAMMC1BvbGFyaXMtUG9DMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAt7yxkCVxVunbjq5kNj2YX4EOBPkRDiz57tbQ3mdXx6rOoaHxPqPErOD+rkhGZUF+lgYdy7U7ts9DdEHpqD9eUtJYpnQ3o01ALB3XdVEnRzNMqWpg3nY36+20XyODufTY4EuOWg7aPaGvClLAy1k/w+wQZLsrUYW8NcDD9Xj7Ry4DdmaQdKomif7NGPyaV9KT3gscN0eht9/6SoeuSJbJrwyN79d/OuXyED7rRNQHW/ii7wVmt+kCQpYLBRcDjXYHoTNawDL1N8Cvq7Q6nOCWfbJQZD73SPc/iVvk2vPfVAJFPYSxG8rpr2uVeF1kseEt8pVHmAk5hZf11R69gxnD9wIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQBONd2zq/iSHnpyWy0g1PAmkuj4wCxa0k/hoALgfXGBWo27kHAC+S22ZylhOVBj/3rZP+jOm9BHC4XejPaZNUDaaqudnoBPY+x35PMuZVpih2dk8yUBL4t74RtGTZm+6UBWnPlQuRmLKeeQg78w2X+K9LyiV/Du7B6rGV08dRfxs6b856UZHxKyEBwOqhFThgsUn6pQuyxEDU5gADYTpJeiFUJI5b4O9IpL6zYj3+SClhRWKpfUlURpEMqmafCNi33r4PICslv8dHGQXs3q+kD8RRwLMK2tQoBzBBEQcMqjQ42nk3WoBdXDaSvPZi7rS5t2mMILdV1lFMNjnPgIu0ZW\"" +
        "]," +
        "\"x5t\": \"xFSxpOVXUjaK87ysslEhigZfnHw\"," +
        "\"x5t#S256\": \"BxOWHHgInnNMrMxF32jd42q6QQrQLI2citUx17xicWM\"" +
      "}," +
      "{" +
    "\"kid\": \"woZrGrRM86BwhFK9NLwl099Ke_bJ80otD5pawe-AS4E\"," +
        "\"kty\": \"EC\"," +
        "\"alg\": \"ES256\"," +
        "\"use\": \"sig\"," +
        "\"crv\": \"P-256\"," +
        "\"x\": \"vr6L7g-sxcA74ESQkK4RoIvRxFFqCl-LLBhGDDsRQkA\"," +
        "\"y\": \"MTX_S6W7lSEqv0T0G2uigqI6GsL9KfSCGgeojikP00A\"" +
      "}" +
    "]" +
  "}";

        public static string sIssuingKey { get { return issureKey; } }

    }
}
