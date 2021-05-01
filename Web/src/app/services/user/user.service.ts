import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PagedDataDto } from '../../models/paged-data-dto';
import { SearchPageDto } from '../../models/search-page-dto';
import { User } from '../../models/user/user.vm';
import { GenericHttpService } from '../generic-http.service';

@Injectable()
export class UserService extends GenericHttpService<User, User> {
  constructor(private client: HttpClient) {
    super("user", client)
  }

  getUserById(id: string) {
    return this.http.post<User>(`${environment.serverUrl}${this.endpoint}/getUser?userName=${id}`,null);
  }

  search(search: SearchPageDto<User>): Observable<PagedDataDto<User>> {
    return this.http.post<PagedDataDto<User>>(`${environment.serverUrl}${this.endpoint}/search`, search);
  }

  deleteUser(userId:string): Observable<any> {
    return this.http.post<any>(`${environment.serverUrl}${this.endpoint}/deleteUser/?userId=${userId}`, null);
  }

  public approveUser(item: User) {
    return this.http.post<User>(`${environment.serverUrl}${this.endpoint}/approveUser`, item);
  }

  public rejectUser(item: User) {
    return this.http.post<User>(`${environment.serverUrl}${this.endpoint}/rejectUser`, item);
  }


  public exportUsers(item: SearchPageDto<User>) {
    return this.http.post<any>(`${environment.serverUrl}${this.endpoint}/exportUsers`, item);
  }
}
