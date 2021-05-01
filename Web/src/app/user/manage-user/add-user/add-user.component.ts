import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../base.component';
import { UserType } from '../../../models/enums';
import { SearchPageDto } from '../../../models/search-page-dto';
import { Address } from '../../../models/user/address.vm';
import { NoAuthUser, User } from '../../../models/user/user.vm';
import { NoAuthUserService } from '../../../services/user/no-auth-user.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss'],
  providers: [NoAuthUserService],

})
export class AddUesrComponent extends BaseComponent implements OnInit {

  model: NoAuthUser = new NoAuthUser;
  modelAddress: Address = new Address;
  id: string
  isPasswordMatch: boolean = false;
  regex: RegExp;


  constructor(private noAuthUserService: NoAuthUserService) {
    super();
  }

  ngOnInit(): void {

  }

  save() {
    if ((this.model.password != this.model.confirmPassword)) {
      this.toastrService.warning(this.translateService.instant("password does Not Match"));
      return;
    } else {
      if (this.model.userType?.id == UserType.Admin) {
        this.noAuthUserService.create(this.model).subscribe(x => {
          this.toastrService.success(this.translateService.instant("Registered Successfully"));
        })
        // add admin 
      } else {
        this.noAuthUserService.create(this.model).subscribe(x => {
          this.toastrService.success(this.translateService.instant("Registered Successfully"));
        })
        // user
      }
    }
  }



  checkPassword() {
    this.regex = new RegExp(/^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\d)(?=.*[!#$@%&? "]).*$/g);
    this.isPasswordMatch = this.regex.test(this.model.password);
  }

}

