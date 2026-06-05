import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class DevicefinderService {

  viewportSizes = [
    Breakpoints.Handset,
    Breakpoints.Tablet,
    Breakpoints.Web,
    Breakpoints.TabletLandscape,
    Breakpoints.TabletPortrait,
    Breakpoints.WebLandscape,
    Breakpoints.WebPortrait,
  ];

  isDeviceHandsetFlag: boolean;
  isDeviceTabletFlag: boolean;
  isDeviceWebFlag: boolean;

  private isDeviceHandset = new BehaviorSubject<boolean>(false);
  private isDeviceTablet = new BehaviorSubject<boolean>(false);
  private isDeviceWeb = new BehaviorSubject<boolean>(false);

  tabDevice = ['IPAD', 'TAB', 'TABLET', 'VM1A', 'SM-T505', 'RT10A', 'RT10'];
  mobileDevice = ['CT40', 'CK65', 'CT45', 'CT60'];
  windowDevice = ['WIN32', 'WINDOWS'];

  constructor(
    private breakpointObserver: BreakpointObserver,
  ) {
    this.loadDevices();
  }

  loadDevices(): void {
    this.breakpointObserver.observe([
      Breakpoints.XSmall,
      Breakpoints.Small,
      Breakpoints.Medium
    ]).subscribe(() => {

      this.isDeviceHandsetFlag = this.breakpointObserver.isMatched(Breakpoints.Handset);
      this.isDeviceTabletFlag = this.breakpointObserver.isMatched(Breakpoints.Tablet);
      this.isDeviceWebFlag = this.breakpointObserver.isMatched(Breakpoints.Web);
      if (this.isValidTabDevice()) {
        this.isDeviceHandset.next(false);
        this.isDeviceTablet.next(true);
        this.isDeviceWeb.next(false);
      }
      else if (this.isValidMobileDevice()) {
        this.isDeviceHandset.next(true);
        this.isDeviceTablet.next(false);
        this.isDeviceWeb.next(false);
      }
      else if (this.isValidWindowsDevice()) {
        this.isDeviceHandset.next(false);
        this.isDeviceTablet.next(false);
        this.isDeviceWeb.next(true);
      }
      else {
        if (this.isDeviceHandsetFlag) {
          this.isDeviceHandset.next(true);
          this.isDeviceTablet.next(false);
        }
        else {
          this.isDeviceHandset.next(false);
          this.isDeviceTablet.next(true);
        //   if (this._configureService.getApi('APP_ENV') !== undefined && this._configureService.getApi('APP_ENV').toUpperCase() === 'QAS') {
        //     console.log(this._configureService.getApi('APP_ENV'));
        //     this.isDeviceWeb.next(this.isDeviceWebFlag);
        //   }
        }
      }
    });
  }

  isValidTabDevice(): boolean {
    const userAgent = window.navigator.userAgent;
    let status = false;
    for (let deviceName of this.tabDevice) {
      let mobileData = userAgent.toUpperCase().search(deviceName);
      if (mobileData >= 0) {
        status = true;
        break;
      }
    }
    return status;
  }

  isValidMobileDevice(): boolean {
    const userAgent = window.navigator.userAgent;
    let status = false;
    for (let deviceName of this.mobileDevice) {
      let tabData = userAgent.toUpperCase().search(deviceName);
      if (tabData >= 0) {
        status = true;
        break;
      }
    }
    return status;
  }

  isValidWindowsDevice(): boolean {
    const userAgent = window.navigator.userAgent;
    let status = false;
    for (let deviceName of this.windowDevice) {
      let tabData = userAgent.toUpperCase().search(deviceName);
      if (tabData >= 0) {
        status = true;
        break;
      }
    }
    return status;
  }

  isTablet(): boolean {
    return this.isDeviceTablet.getValue();
  }

  isDesktop(): boolean {
    return this.isDeviceWeb.getValue();
  }

  isMobile(): boolean {
    return this.isDeviceHandset.getValue();
  }

  findDeviceType(): string {
    if (this.isDesktop()) {
      return 'DES';
    }
    if (this.isTablet()) {
      return 'VM';
    }
    if (this.isMobile()) {
      return 'MOB';
    }
    return 'DES';
  }

}
