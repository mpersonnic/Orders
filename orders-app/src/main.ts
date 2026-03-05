import { bootstrapApplication } from '@angular/platform-browser';
import { APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';

import { KeycloakService } from 'keycloak-angular';
import { keycloakConfig } from './config/keycloak.config';

function initializeKeycloak(keycloak: KeycloakService) {
  return () => keycloak.init(keycloakConfig);
}

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [
    ...(appConfig.providers ?? []),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeKeycloak,
      deps: [KeycloakService],
      multi: true
    }
  ]
}).catch(err => console.error(err));
