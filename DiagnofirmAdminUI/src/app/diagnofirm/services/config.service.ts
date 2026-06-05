import { HttpClient } from '@angular/common/http';
import { Injectable, APP_INITIALIZER } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ConfigService {

    //     private configSubject = new BehaviorSubject<any>(null); 
    //     private configUrl = './assets/config/app.settings.json';  

    //     constructor(private http: HttpClient) { }

    //     // Returns the config as an observable
    //     get config$(): Observable<any> {
    //         return this.configSubject.asObservable();
    //     }

    //     // Loads the configuration (e.g., from a file or API)
    //     load(): Observable<any> {
    //         return this.http.get<any>(this.configUrl).pipe(
    //             tap((config) => {
    //                 this.setConfig(config); // Once loaded, set the config
    //             }),
    //             catchError((error) => {
    //                 console.error('Error loading config', error);
    //                 return []; // Return empty array or fallback data in case of error
    //             })
    //         );
    //     }

    //     // Sets the loaded config into the BehaviorSubject
    //     setConfig(config: any): void {
    //         this.configSubject.next(config);
    //     }

    //      //Gets API route based on the provided key
    //      getApi(key: string): string {
    //         const config = this.configSubject.getValue(); // Get the current config
    //         if (config && config.ApplicationSettings && key in config.ApplicationSettings) {
    //           return config.ApplicationSettings[key];
    //         } else {
    //           console.warn(`Config key '${key}' not found in 'ApplicationSettings'`);
    //           return ''; // Return an empty string or a default value
    //         }
    //       }

    // }


    private _config: Object | any

    private configSubject = new BehaviorSubject<any>(null);
    private configUrl = './assets/config/app.settings.json';  

    constructor(private _http: HttpClient) { }

    setConfig(config: any) {
        this.configSubject.next(config);
    }

    load(): Observable<any> {
        return this._http.get(this.configUrl).pipe(
            tap(config => this.setConfig(config))
        );
    }

    

    // load() {
    //     return new Promise((resolve, reject) => {

    //         this._http.get('../../../assets/config/app.settings.json')
    //             .subscribe((data) => {

    //                 this._config = data;
    //                 resolve(true);
    //             },
    //                 (error: any) => {
    //                     console.error(error);
    //                     //return Observable.throw(error.json().error || 'Server error');
    //                 });
    //     });
    // }

    // Gets API route based on the provided key
    getApi(key: string): string {
        if (!this._config?.ApplicationSettings) {
            console.warn('Config not loaded or invalid');
            return '';
        }

        return this._config.ApplicationSettings[key] || '';
    }
    // Gets a value of specified property in the configuration file
    get(key: any) {
        return this._config[key];
    }
}

export function ConfigFactory(config: ConfigService) {
    return () => config.load();
}

export function init() {
    return {
        provide: APP_INITIALIZER,
        useFactory: ConfigFactory,
        deps: [ConfigService],
        multi: true
    }
}

const ConfigModule = {
    init: init
}

export { ConfigModule };