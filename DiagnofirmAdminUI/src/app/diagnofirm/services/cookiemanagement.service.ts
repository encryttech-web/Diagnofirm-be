import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CookiemanagementService {
 
  public setUser(input:any):void{
    window.localStorage.setItem('SG_ID','');
    window.localStorage.setItem('SG_ID',input);
  }

  public getSgid(): string|null{
    return window.localStorage.getItem('SG_ID');
  }

}
