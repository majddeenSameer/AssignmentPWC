import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../base.component';
import { Complaint } from '../../models/complaint/complaint.vm';
import { UserType } from '../../models/enums';
import { PagedDataDto } from '../../models/paged-data-dto';
import { SearchPageDto } from '../../models/search-page-dto';
import { NoAuthUser } from '../../models/user/user.vm';
import { ComplaintService } from '../../services/complaint/complaint.service';
import { NoAuthUserService } from '../../services/user/no-auth-user.service';

@Component({
  selector: 'app-request-search',
  templateUrl: './request-search.component.html',
  styleUrls: ['./request-search.component.scss'],
  providers: [NoAuthUserService, ComplaintService]
})
export class RequestSearchComponent extends BaseComponent implements OnInit {
  isAdmin: boolean;
  model: Complaint;
  userId: number;

  constructor(private noAuthUserService: NoAuthUserService, private complaintService: ComplaintService) {
    super();
    this.noAuthUserService.model.subscribe((res) => {
      if (res.userType.id == UserType.User) {
        this.userId = res.id
        this.isAdmin = false
      } else if (res.userType.id == UserType.Admin) {
        this.userId = res.id
        this.isAdmin = true
      }
    });
  }
  result: PagedDataDto<NoAuthUser>;
  search = new SearchPageDto<NoAuthUser>();
  ngOnInit(): void {

  }

  getData() {
    this.search.page = (!this.search.page || this.search.page <= 1) ? 1 : this.search.page;
    this.noAuthUserService.search(this.search).subscribe(res => {
      this.result = res;
      if (!(res.totalCount > 0)) {
        this.toastrService.warning("No results");
      }
    });
  }

  onPageChange(event: any) {
    this.search.page = event.page;
    this.getData();
  };

  Approve(row: Complaint) {
    this.complaintService.ApproveComplaint(row).subscribe(res => {
      this.toastrService.warning("successfully approved");
    });
  }

  Add() {
    this.model.noAuthUser.id = this.userId
    this.model.requestStatus.id = 2;
      this.complaintService.create(this.model).subscribe(res => {
        this.toastrService.warning("successfully added");
      });
  }

}
