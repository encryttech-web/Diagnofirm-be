import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenstorageService {


  signOut(): void {
    window.sessionStorage.clear();
  }

  public getToken(): string | null {    
    return localStorage.getItem("WEB_TOKEN");
  }

  public saveToken(input:any): void | null {    
    return localStorage.setItem("WEB_TOKEN",input);
  }

  public getRefreshToken(): string | null {
    return window.sessionStorage.getItem("REFRESHTOKEN");
  }

  public saveRefreshToken(input:any): void | null {
    window.sessionStorage.setItem("REFRESHTOKEN", input);
  }

  public getIdToken(): string | null {
    return window.sessionStorage.getItem("IDTOKEN");
  }

  public saveIdToken(input:any): void {
    window.sessionStorage.setItem("IDTOKEN", input);
  }

}
