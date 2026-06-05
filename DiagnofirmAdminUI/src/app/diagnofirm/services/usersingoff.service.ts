import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
// import { LockOrderService } from '../modules/warehouse/warehouse-services/lock-order.service';
// import { ApiLoginJsonResponseKeys, JsonFlags } from '../Shared/global.constant';
// import { SessionmanagementService } from '../Shared/statemanagement/statemanagement/sessionmanagement.service';
// import { TokenstorageService } from '../Shared/statemanagement/tokenstorage/tokenstorage.service';
// import { HttpService } from './http.service';
//import { DevicefinderService } from 'src/app/service/devicefinder.service';
@Injectable({
  providedIn: 'root'
})
export class UsersingoffService {

  constructor(
    // private SESSION: SessionmanagementService,
    // private TOKENSTORAGE: TokenstorageService,
    private ROUTE: Router,
    private HTTPCLIENT: HttpClient,
    // private HTTPSERVICE: HttpClient,
    // private LOCKORDER: LockOrderService,
    //private _devicefinderService : DevicefinderService
  ) { }

  userlogout(): void {
    this.adminLogOut();
  }

  private adminLogOut(): void {
    // if (this.SESSION.getAdminMiddleware() !== null) {
    //   let input = {
    //     lang: this.SESSION.getLanguage()
    //   };
    //   let url = (this.SESSION.getAdminMiddleware()).trim() + 'UserSiteAccess/InsertUserSingOff';
    //   this.HTTPSERVICE.post(url, input).subscribe(res => {
    //     console.log('Response:' + res);
    //   });
    // }
    this.logout();
  }

  logout(): void {
    this.userlogout();
    //this.LOCKORDER.releaseLocks();
    this.ROUTE.navigateByUrl('/logout');
  }

  public clearStorage(): void {
    window.sessionStorage.clear();
    window.localStorage.clear();
  }

  clearUserStorage(): void {
    window.sessionStorage.clear();
    window.localStorage.clear();
    localStorage.setItem('signOut', 'true');
  }

//   assingToken(response: any): void {
//     this.TOKENSTORAGE.saveToken(response['jwttoken']);
//     this.TOKENSTORAGE.saveRefreshToken(response['refreshtoken']);
//     this.TOKENSTORAGE.saveIdToken(response['id_token']);
//   }

//   refreshToken(): void {

//     let refreshToken = this.TOKENSTORAGE.getRefreshToken();

//     const input = {
//       SGID: this.SESSION.getSgid(),
//       token: refreshToken,
//       LANG: this.SESSION.getLanguage()
//     };

//     this.HTTPCLIENT.post(this.SESSION.getAdminMiddleware() + "Login/ValidateToken", input).subscribe(response => {
//       let loginstatus = response[ApiLoginJsonResponseKeys.ApiStatus] === JsonFlags.Success ? true : false;
//       if (loginstatus) {
//         this.assingToken(response);
//       }
//       else {
//         this.clearUserStorage();
//       }
//     },
//       (error: any) => {

//         this.clearUserStorage();
//         const webRequestError = {
//           error: 'SGWebRequest',
//           status: error.status,
//           Message: 'Unauthorized Request...'
//         };
//         return throwError(webRequestError);

//       },
//       () => {
//         window.location.reload();
//       });
//   }

//   saveTokenExpiryUserLog() {
//     const input = {
//       PlantCode : "",
//       UserId: this.SESSION.getSgid(),
//       DeviceType : this._devicefinderService.findDeviceType()
//     };
//     const url = encodeURI(this.SESSION.getAdminMiddleware().trim() + 'UserManagement/CreateLogWhenSessionExpiry');    
//     return this.HTTPSERVICE.post(url, input);
//   }

}
