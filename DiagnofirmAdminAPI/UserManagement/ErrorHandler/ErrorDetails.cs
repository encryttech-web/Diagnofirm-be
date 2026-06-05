using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiagnofirmAdmin.ErrorHandler
{
    public class ErrorDetails
    {
        public string status { get; set; }
        public string code { get; set; }
        public int StatusCode { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
