import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { Logger } from '@core';
import { NgxPermissionsService } from 'ngx-permissions';
import { CredentialsService } from './credentials.service';

const log = new Logger('AuthenticationGuard');

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  constructor(private router: Router,
    private credentialsService: CredentialsService, private permissionService: NgxPermissionsService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.credentialsService.isAuthenticated()) {
      const expectedRole = route.data.expectedRole;
      const token = this.credentialsService.credentials.token;
      const tokenPayload = JSON.parse(atob(token.split('.')[1]));;
      this.loadPermissions(tokenPayload.role);

      if (
        !this.credentialsService.isAuthenticated() ||
        (expectedRole && !tokenPayload.role.includes(expectedRole))) {
        this.router.navigate(['login']);
        return false;
      }
      return true;
    }

    log.debug('Not authenticated, redirecting and adding redirect url...');
    this.router.navigate(['/login'], { queryParams: { redirect: state.url }, replaceUrl: true });
    return false;
  }

  loadPermissions(data: any) {
    this.permissionService.flushPermissions();
    if (Array.isArray(data)) {
      this.permissionService.loadPermissions(data);
    }
    else {
      this.permissionService.loadPermissions([data]);
    }
  }

}

