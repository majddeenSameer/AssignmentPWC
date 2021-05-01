import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Lookup } from '../models/lookup.model';

@Injectable({
  providedIn: 'root',
})
export class LookupService {


  constructor(private client: HttpClient, private translateService: TranslateService) {
  }

  List(lookupUrl: string, showSpinner = true, externalCall = false): Observable<Array<Lookup>> {
    let headers = new HttpHeaders();
    headers = headers.append('no-loader', 'true');
    return this.client.get<Array<Lookup>>(externalCall ? lookupUrl : `${environment.serverUrl}Lookups/${lookupUrl}/${this.getCurrentLang()}`, { headers: headers });
  }

  getCurrentLang() {
    return this.translateService.store.currentLang == "ar-AR" ? "Arabic" : "English";
  }
}
