import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpParamsOptions } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { TokenstorageService } from '@/diagnofirm/services/tokenstorage.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private HTTPCLIENT: HttpClient,
    private TOKEN: TokenstorageService,
    private ROUTER: Router
  ) { }


  public post(inputurl: string, input: any): Observable<object> {

    if (this.TOKEN.getToken() !== undefined && this.TOKEN.getToken() != null && this.TOKEN.getToken() !== '') {

        const headerToken = {
            headers: new HttpHeaders()
              .set('Authorization', `Bearer ${this.TOKEN.getToken()}`),
            withCredentials: true
          };
          return this.HTTPCLIENT.post(inputurl, input, headerToken);
    }
    else {
      const header = {
        headers: new HttpHeaders(),
        'Content-Type': 'application/json; charset = utf-8;',
        withCredentials: true
      };
      return this.HTTPCLIENT.post(inputurl, input, header);
    }

  }

  public PostSaveTrans(inputurl: string, input: any): Observable<object> {

    if (this.TOKEN.getToken() != undefined &&
      this.TOKEN.getToken() != null &&
      this.TOKEN.getToken() != '') {

      let header = {
        headers: new HttpHeaders()
          .set('Authorization', `Bearer ${this.TOKEN.getToken()}`)
          .set('methodx', 'ignore'),
      };

      return this.HTTPCLIENT.post(inputurl, input, header);
    }
    return this.HTTPCLIENT.post(inputurl, input);
  }


  public Get(inputurl:any): Observable<object> {

    if (this.TOKEN.getToken() != undefined &&
      this.TOKEN.getToken() != null &&
      this.TOKEN.getToken() != '') {

      let header = {
        headers: new HttpHeaders()
          .set('Authorization', `Bearer ${this.TOKEN.getToken()}`)
      };

      return this.HTTPCLIENT.get<any>(inputurl, header);
    }
    return this.HTTPCLIENT.get<any>(inputurl);
  }

  public getWithParam(inputurl:any, inputParams: any): Observable<object> {
    if (this.TOKEN.getToken() != undefined &&
      this.TOKEN.getToken() != null &&
      this.TOKEN.getToken() != '') {

      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.TOKEN.getToken()}`);

      const myObject: any = {
        // deliveryNumber: inputParams.deliveryNumber,
        languageCode: inputParams.languageCode,
      };
      const httpParams: HttpParamsOptions = { fromObject: myObject } as HttpParamsOptions;

      const options = { params: new HttpParams(httpParams), headers: headers };
      return this.HTTPCLIENT.get<any>(inputurl, options);
    }
    return this.HTTPCLIENT.get<any>(inputurl);
  }

}
