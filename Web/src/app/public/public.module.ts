import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NgWizardConfig, NgWizardModule, THEME } from 'ng-wizard';
import { SharedModule } from '../@shared';
import { ShellModule } from '../shell/shell.module';
import { AddUesrComponent } from '../user/manage-user/add-user/add-user.component';
import { RegisterComponent } from '../user/register/register.component';
import { PublicRoutingModule } from './public-routing.module';

const ngWizardConfig: NgWizardConfig = {
  theme: THEME.dots,
  toolbarSettings: {
    showNextButton: false,
    showPreviousButton: false,
  }
};

@NgModule({
  declarations: [ RegisterComponent, AddUesrComponent],
  imports: [
    CommonModule,
    PublicRoutingModule,
    FormsModule,
    TranslateModule,
    SharedModule,
    ShellModule,
    NgWizardModule.forRoot(ngWizardConfig)
  ]
})
export class PublicModule { }
