import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderComponent } from './loader/loader.component';
import { DatepickerControlComponent } from './datepicker-control/datepicker-control.component';
import { AutocompleteControlComponent } from './autocomplete-control/autocomplete-control.component';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelectModule } from '@ng-select/ng-select';
import { LookupListComponent } from './lookupList/lookup-list.component';
import { PaginationModule, PaginationComponent } from 'ngx-bootstrap/pagination';
import { AccordionModule, AccordionComponent, AccordionPanelComponent } from 'ngx-bootstrap/accordion';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxMaskModule, IConfig, MaskDirective, MaskPipe } from 'ngx-mask'
import { NgxIntlTelInputModule, NativeElementInjectorDirective } from 'ngx-intl-tel-input';
import { TranslateModule } from '@ngx-translate/core';
import { ControlValidatorComponent } from './validator/control-validator.component';


const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: false,
  };
};
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    NgbDatepickerModule,
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    NgxIntlTelInputModule,
    NgxMaskModule.forRoot(maskConfigFunction),
    AccordionModule.forRoot(),
    TranslateModule
  ],
  declarations: [
    LoaderComponent,
    ControlValidatorComponent,
    DatepickerControlComponent,
    AutocompleteControlComponent,
    LookupListComponent,
    
  ],
  exports: [
    LoaderComponent,
    ControlValidatorComponent,
    DatepickerControlComponent,
    AutocompleteControlComponent,
    LookupListComponent,
    PaginationComponent,
    AccordionComponent,
    AccordionPanelComponent,
    MaskDirective,
    MaskPipe,
    NativeElementInjectorDirective
  ]
})
export class SharedModule { }
