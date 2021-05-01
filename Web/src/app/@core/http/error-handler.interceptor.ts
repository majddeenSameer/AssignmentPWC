import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { environment } from '@env/environment';
import { Logger } from '../logger.service';
import { LoaderService } from '../../@shared/loader/loader.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';

const log = new Logger('ErrorHandlerInterceptor');

/**
 * Adds a default error handler to all requests.
 */
@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerInterceptor implements HttpInterceptor {

  constructor(
    private loaderService: LoaderService,
    private toastrService: ToastrService,
    private translateService: TranslateService,
    private router: Router,

  ) {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => this.errorHandler(request, error)));
  }

  // Customize the default error handler here if needed
  private errorHandler(request: HttpRequest<any>, response: HttpEvent<any>): Observable<HttpEvent<any>> {
    //var re = new RegExp(this.escapeRegExp(environment.serverUrl), "i");
    if (response instanceof HttpErrorResponse ) {
      if (!request.headers.has("no-loader") || request.headers.get("no-loader") == "false") {

        this.loaderService.hideSpinner(true);

      }

      if (response.status === 401 || response.status === 403) {
        this.toastrService.info(this.translateService.instant("notAuthorized"))
        this.router.navigate(['/login'], { replaceUrl: true });
      }

      else if (response.status === 422) {
        this.toastrService.error(response.error, "Error");
      }
      else if (response.status === 0) {
        return throwError("connection-Refused");

      }
      else {
        this.toastrService.error(this.translateService.instant("GeneralErrorMessage"), "Error");
      }
    }

    if (!environment.production) {
      // Do something with the error
      log.error('Request error', response);
    }
    throw response;
  }


  escapeRegExp(str: string) {
    return str.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
  }

}
