import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternalRoutingModule } from './internal-routing.module';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../@shared';
import { ShellModule } from '../shell/shell.module';
import { RequestSearchComponent } from './request-search/request-search.component';



@NgModule({
  declarations: [RequestSearchComponent],
  imports: [
    CommonModule,
    InternalRoutingModule,
    FormsModule,
    TranslateModule,
    SharedModule,
    ShellModule
  ]
})
export class InternalModule { }
