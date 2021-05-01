import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  constructor() { }

  private isShowLoader: boolean = false;
  private count = 0;
  showLoader(isApiCall: boolean) {
    if (isApiCall) {
      this.count++;
    }
    this.isShowLoader = true;
  }

  hideSpinner(isApiCall: boolean) {
    if (isApiCall) {
      this.count--;
    }
    if (this.count <= 0) {
      this.isShowLoader = false;

    }

  }

  getLoaderStatus() {
    return this.isShowLoader;
  }
}
