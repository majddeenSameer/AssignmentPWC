import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AuthModule } from '@app/auth';
import { ShellComponent } from './shell.component';
import { SharedModule } from '../@shared';

@NgModule({
  imports: [
    CommonModule,
    TranslateModule,
    NgbModule,
    AuthModule,
    RouterModule,
    SharedModule
  ],
  declarations: [
    ShellComponent,
  ]
})
export class ShellModule {
}
