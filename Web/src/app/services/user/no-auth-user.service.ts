import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PagedDataDto } from '../../models/paged-data-dto';
import { SearchPageDto } from '../../models/search-page-dto';
import { NoAuthUser } from '../../models/user/user.vm';
import { GenericHttpService } from '../generic-http.service';

@Injectable()
export class NoAuthUserService extends GenericHttpService<NoAuthUser, NoAuthUser> {
  model: ReplaySubject<NoAuthUser> = new ReplaySubject<NoAuthUser>();

  constructor(private client: HttpClient) {
    super("NoAuthUser", client);
  }

  login(search: SearchPageDto<NoAuthUser>): Observable<PagedDataDto<NoAuthUser>> {
    return this.http.post<PagedDataDto<NoAuthUser>>(`${environment.serverUrl}${this.endpoint}/login`, search);
  }
}
