import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiHelperService {

  private baseUrl = environment.serverUrl;

  constructor(private http: HttpClient) { }

  post<TData, TResponse>(uri: string, data: TData, params = new HttpParams(), showSpinner = true, externalCall = false): Observable<TResponse> {
    let headers = new HttpHeaders();
    if (!showSpinner) {
      headers = headers.append('no-spinner', 'true');
    }
    let url = externalCall ? uri : this.baseUrl + uri;
    return this.http.post(url, data, { params: params, headers: headers }).
      pipe(
        map(
          (res: TResponse) => {

            return this.handleSuccess(res)
          }),
        catchError(
          (error: HttpErrorResponse) => {

            return this.handelError(error)
          }),
        finalize(
          () => {

            this.handleFinally()
          }));
  }

  postFile(formData: FormData, options: {
    headers?: HttpHeaders | {
      [header: string]: string | string[];
    };
    observe?: 'body';
    params?: HttpParams | {
      [param: string]: string | string[];
    };
    reportProgress?: boolean;
    responseType: 'arraybuffer';
    withCredentials?: boolean;
  }): Observable<ArrayBuffer> {
    return this.http.post(this.baseUrl + 'attachment', formData, options);
  }

  put<TData, TResponse>(uri: string, data: TData, params = new HttpParams(), showSpinner = true, externalCall = false): Observable<TResponse> {
    let headers = new HttpHeaders();
    if (!showSpinner) {
      headers = headers.append('no-spinner', 'true');
    }
    let url = externalCall ? uri : this.baseUrl + uri;
    return this.http.put(url, data, { params: params, headers: headers }).
      pipe(
        map(
          (res: TResponse) => {

            return this.handleSuccess(res)
          }),
        catchError(
          (error: HttpErrorResponse) => {

            return this.handelError(error)
          }),
        finalize(
          () => {

            this.handleFinally()
          }));
  }

  get<TResponse>(uri: string, params = new HttpParams(), showSpinner = true, externalCall = false): Observable<TResponse> {
    let headers = new HttpHeaders();
    if (!showSpinner) {
      headers = headers.append('no-spinner', 'true');
    }
    let url = externalCall ? uri : this.baseUrl + uri;
    return this.http.get(url, { params: params, headers: headers }).
      pipe(map((res: TResponse) => { return this.handleSuccess(res); })
        , catchError((error: HttpErrorResponse) => { return this.handelError(error); })
        , finalize(() => { this.handleFinally(); }));
  }

  delete<TResponse>(uri: string, params = new HttpParams(), showSpinner = true, externalCall = false): Observable<TResponse> {
    let headers = new HttpHeaders();
    if (!showSpinner) {
      headers = headers.append('no-spinner', 'true');
    }
    let url = externalCall ? uri : this.baseUrl + uri;
    return this.http.delete(url, { params: params, headers: headers }).
      pipe(
        map(
          (res: TResponse) => {

            return this.handleSuccess(res)
          }),
        catchError(
          (error: HttpErrorResponse) => {

            return this.handelError(error)
          }),
        finalize(
          () => {

            this.handleFinally()
          }));
  }


  getBlob(uri: string, showSpinner = true, externalCall = false) {
    let headers = new HttpHeaders();

    if (!showSpinner) {
      headers = headers.append('no-spinner', 'true');
    }

    let url = externalCall ? uri : this.baseUrl + uri;
    return this.http.get(url, { responseType: 'blob', headers: headers });
  }


  private handleSuccess<TResponse>(res: TResponse) { return res; }
  private handelError(error: HttpErrorResponse) {

    if (error.message != undefined) {
      console.log('Inner Exception: ' + error.message);
    }

    return throwError(error);
  }
  private handleFinally() { }


}


