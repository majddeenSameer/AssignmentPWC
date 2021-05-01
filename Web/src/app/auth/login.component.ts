import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';

import { environment } from '@env/environment';
import { Logger, UntilDestroy, untilDestroyed } from '@core';
import { AuthenticationService } from './authentication.service';
import { CredentialsService } from '.';
import { NoAuthUserService } from '../services/user/no-auth-user.service';
import { NoAuthUser } from '../models/user/user.vm';
import { SearchPageDto } from '../models/search-page-dto';

const log = new Logger('Login');

@UntilDestroy()
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [NoAuthUserService],
})
export class LoginComponent implements OnInit {
  version: string | null = environment.version;
  error: string | undefined;
  loginForm!: FormGroup;
  isLoading = false;
  loadPage: boolean = true;
  search = new SearchPageDto<NoAuthUser>();

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private noAuthUserService: NoAuthUserService
  ) {
    this.createForm();
  }

  ngOnInit() {

  }

  login() {
    this.isLoading = true;
    this.search.page = 1;
    this.search.criteria.emailAddress = this.loginForm.controls.username.value
    this.search.criteria.password = this.loginForm.controls.password.value
    this.noAuthUserService.login(this.search).subscribe(x => {
      if (x.totalCount > 0) {
        this.noAuthUserService.model.next(x.data[0]);
        this.router.navigate(['/internal/request']);
      }
    })
  }

  private createForm() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      remember: true
    });
  }

}
