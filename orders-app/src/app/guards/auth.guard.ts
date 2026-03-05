import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { KeycloakAuthGuard, KeycloakService } from 'keycloak-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard extends KeycloakAuthGuard {
  protected keycloakService: KeycloakService;

  constructor(
    router: Router,
    keycloak: KeycloakService
  ) {
    super(router, keycloak);
    this.keycloakService = keycloak;
  }

  public async isAccessAllowed(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {

    // 1. Si l'utilisateur n'est pas authentifié → redirection vers Keycloak
    if (!this.authenticated) {
      await this.keycloakService.login({
        redirectUri: window.location.origin + state.url
      });
      return false;
    }

    // 2. Vérification des rôles éventuels
    const requiredRoles = route.data['roles'];

    if (!requiredRoles || requiredRoles.length === 0) {
      return true;
    }

    const hasRole = requiredRoles.some(role => this.roles.includes(role));

    if (!hasRole) {
      this.router.navigate(['/forbidden']);
      return false;
    }

    return true;
  }
}
