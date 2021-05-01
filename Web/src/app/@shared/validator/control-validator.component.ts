import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, AbstractControlDirective, NgForm } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'control-validator',
  template: `
    <ul *ngIf="shouldShowErrors()" class="ul-{{translation.currentLang}}">
      <li *ngFor="let error of listOfErrors()" translate>{{error}}</li>
    </ul>
  `,
  styleUrls: ['./control-validator.component.css'],
})

export class ControlValidatorComponent implements OnInit {

  constructor(public translation: TranslateService) {
  }

  ngOnInit() {
  }

  @Input()
  private control: AbstractControlDirective | AbstractControl;

  @Input()
  private forms: NgForm;
  @Input()
  private controlName: string;
  shouldShowErrors(): boolean {
    if (this.control == undefined && this.forms != undefined) {
      this.control = this.forms.controls[this.controlName];
    }
    return this.control &&
      this.control.errors &&
      (this.control.dirty && this.control.touched);

  }

  listOfErrors(): string[] {
    return Object.keys(this.control.errors)
      .map(field => this.getMessage(field, this.control.errors[field]));
  }

  private getMessage(type: string, params: any) {
    if (params.messageKey != undefined)
      return this.translation.instant(params.messageKey);
    else if (params.message != undefined) {
      return this.translation.instant(params.message);
    }
    else
      return this.translation.instant(type + this.getParam(params)); 
  }

  private getParam(params: any) {
    if (params.requiredLength != undefined) {
      return params.requiredLength;
    }
    else if (params.requiredPattern != undefined) {
      return params.requiredPattern;
    }
    else if (params.message != undefined) {
      return params.message;
    }
    else {
      return '';
    }
  }
}
