import { KeycloakOptions } from 'keycloak-angular';

export const keycloakConfig: KeycloakOptions = {
  config: {
    url: 'http://localhost:8080/',
    realm: 'orders-realm',
    clientId: 'orders-app',
  },
  initOptions: {
    onLoad: 'login-required',
    checkLoginIframe: false
  },
  bearerExcludedUrls: ['/assets', '/public']
};
