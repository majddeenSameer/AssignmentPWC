import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Complaint } from '../../models/complaint/complaint.vm';
import { PagedDataDto } from '../../models/paged-data-dto';
import { SearchPageDto } from '../../models/search-page-dto';
import { NoAuthUser } from '../../models/user/user.vm';
import { GenericHttpService } from '../generic-http.service';

@Injectable()
export class ComplaintService extends GenericHttpService<Complaint, Complaint> {
  model: ReplaySubject<Complaint> = new ReplaySubject<Complaint>();

  constructor(private client: HttpClient) {
    super("Complaint", client);
  }

  UserComplaints(search: SearchPageDto<Complaint>): Observable<PagedDataDto<Complaint>> {
    return this.http.post<PagedDataDto<Complaint>>(`${environment.serverUrl}${this.endpoint}/UserComplaints`, search);
  }

  ApproveComplaint(complaint: Complaint): Observable<Complaint> {
    return this.http.post<Complaint>(`${environment.serverUrl}${this.endpoint}/ApproveComplaint`, complaint);
  }

}
