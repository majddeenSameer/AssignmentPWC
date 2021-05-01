import { ToastrService } from 'ngx-toastr';
import { environment } from '../environments/environment';
import { LocatorService } from './services/locator.service';
import { TranslateService } from '@ngx-translate/core';
//import { SweetAlertService } from '../services/sweet-alert.service';
import * as enums from './models/enums';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, ReplaySubject } from 'rxjs';
import { finalize, take } from 'rxjs/operators';
//import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

export class BaseComponent {
  toastrService: ToastrService;
  translateService: TranslateService;
  //sweetAlertService: SweetAlertService;
  isInstalledLocalMachineService = true;
  dateTimeFormat = 'dd/MM/yyyy h:mm a';
  dateFormat = 'dd/MM/yyyy';
  dateFomratInput = 'yyyy-MM-dd';
  emiratesIdFormat: string = "000-0000-0000000-0";
  mobileNumberFormat: string = "000-0000000";
  enums = enums;
  environment = environment;
  currentDate = new Date();
  modalRef: BsModalRef;
  modalService: BsModalService;
  gridMaxVisiblepages: number;

  constructor() {

    this.toastrService = LocatorService.injector.get(ToastrService);
    this.translateService = LocatorService.injector.get(TranslateService);
    //this.sweetAlertService = LocatorService.injector.get(SweetAlertService);
    this.modalService = LocatorService.injector.get(BsModalService);
    this.gridMaxVisiblepages = this.environment.gridMaxVisiblepages;

  }

  isArabicLang() {
    return this.translateService.store.currentLang == "ar-AR";
  }

  getCurrentLang() {
    return this.translateService.store.currentLang == "ar-AR" ? enums.Language.Arabic : enums.Language.English;
  }

  translate(key: string): string {
    return this.translateService.instant(key);
  }

  public base64ToArrayBuffer(base64: string) {
    const binaryString = atob(base64);
    const len = binaryString.length;
    var bytes: Uint8Array = new Uint8Array(len);
    for (var i = 0; i < len; i++)
      bytes[i] = binaryString.charCodeAt(i);
    return bytes.buffer;
  }




  ViewPDF(base64String: string, hideDownloadButton = false, openNewWindow: boolean) {
    let blob = new Blob([new Uint8Array(this.base64ToArrayBuffer(base64String))], { type: "application/pdf" });
    var fileURL = URL.createObjectURL(blob);
    if (openNewWindow) {
      window.open(fileURL, "_blank");
    }

  }

}


