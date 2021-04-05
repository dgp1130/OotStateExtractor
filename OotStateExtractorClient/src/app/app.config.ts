import { InjectionToken } from '@angular/core';

/** Type representing the config. */
export interface Config {
    apiHost: string;
}

/** Hard-coded config data. */
export const config: Config = {
    apiHost: 'localhost:5000',
};

/** Token to inject the application config. */
export const APP_CONFIG = new InjectionToken<Config>('Config');
