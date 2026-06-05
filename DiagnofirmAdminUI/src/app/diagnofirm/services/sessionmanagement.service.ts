import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionmanagementService {

  public setuserinfo(input: any[]): void {
    window.sessionStorage.removeItem('USERINFO');
    window.sessionStorage.setItem('USERINFO', input.toString());
  }

  public getuserinfo(): string {
    return window.sessionStorage.getItem('USERINFO');
  }

}
