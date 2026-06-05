import { HttpClient } from '@angular/common/http';
import { Injectable, APP_INITIALIZER } from '@angular/core';

interface AppConfig {
    ApplicationSettings: {
        [key: string]: string;
    };

    [key: string]: any;
}

@Injectable()
export class ConfigService {

    private _config!: AppConfig;

    constructor(private _http: HttpClient) { }

    load(): Promise<boolean> {
        return new Promise((resolve, reject) => {

            this._http.get<AppConfig>('./assets/config/app.settings.json')
                .subscribe(
                    (data) => {
                        this._config = data;
                        resolve(true);
                    },
                    (error: any) => {
                        console.error(error);
                        reject(error);
                    }
                );
        });
    }

    // Gets API route based on the provided key
    getApi(key: string): string {
        return this._config.ApplicationSettings[key];
    }

    // Gets a value of specified property in the configuration file
    get<T = any>(key: string): T {
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
    };
}

const ConfigModule = {
    init: init
};

export { ConfigModule };