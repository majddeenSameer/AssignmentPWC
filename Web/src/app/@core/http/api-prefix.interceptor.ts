import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { LoaderService } from '../../@shared/loader/loader.service';
import { tap } from 'rxjs/operators';

/**
 * Prefixes all requests not starting with `http or https` with `environment.serverUrl`.
 */
export interface ICredentials {
  // Customize received credentials here
  username: string;
  token: string;
}
@Injectable({
  providedIn: 'root'
})
export class ApiPrefixInterceptor implements HttpInterceptor {

  constructor(
    private loaderService: LoaderService

  ) {

  }
  private _credentials: ICredentials | null = null;

  environment = environment;
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //debugger;

    if (!request.headers.has("no-loader") || request.headers.get("no-loader") == "false") {
      this.loaderService.showLoader(true);
    }

    if (!/^(http|https):/i.test(request.url)) {
      request = request.clone({ url: environment.serverUrl + request.url });
    }


    const savedCredentials = sessionStorage.getItem('credentials') || localStorage.getItem('credentials');
    if (savedCredentials) {
      this._credentials = JSON.parse(savedCredentials);
    }

    if (!!this._credentials) {
      request = this.addAuthenticationHeader(request, this._credentials.token);
    }
    return next.handle(request).pipe(
      tap(
        event => this.handleResponse(request, event)
      ));
  }

  handleResponse(request: HttpRequest<any>, event: any) {
    if ((event instanceof HttpResponse) == false) return;
    if (!request.headers.has("no-loader") || request.headers.get("no-loader") == "false") {
      this.loaderService.hideSpinner(true);
    }
  }

  escapeRegExp(str: string) {
    return str.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
  }

  addAuthenticationHeader(req: any, token: string) {

    return req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      }
    });
  }

}
