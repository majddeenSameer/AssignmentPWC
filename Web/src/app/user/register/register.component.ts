import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../base.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent implements OnInit {
  isPasswordMatch: any;
    regex: RegExp;

  constructor() { super(); }

  ngOnInit(): void {
  }
  save() { }

  checkPassword() {
    this.regex = new RegExp(/^.*(?=.{10,})(?=.*[a-zA-Z])(?=.*\d)(?=.*[!#$@%&? "]).*$/g);
    //this.isPasswordMatch = this.regex.test(this.model.password);
  }

}
