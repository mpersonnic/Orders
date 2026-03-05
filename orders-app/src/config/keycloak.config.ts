import { KeycloakConfig, KeycloakInitOptions } from 'keycloak-js';

export const keycloakConfig: KeycloakConfig = {
  url: 'http://localhost:8080',
  realm: 'OrderRealm',
  clientId: 'orders-app'
};

export const keycloakInitOptions: KeycloakInitOptions = {
  onLoad: 'login-required',
  checkLoginIframe: false,
  redirectUri: 'http://localhost:4200'
};

