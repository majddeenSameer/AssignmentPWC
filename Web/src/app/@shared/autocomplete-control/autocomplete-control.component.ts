import { Component, OnInit, Input, OnChanges, SimpleChange, forwardRef, Output, EventEmitter, ChangeDetectorRef, Self } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, FormControl, NgControl } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Subject } from 'rxjs';
import { debounceTime, switchMap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: './app-autocomplete-control',
  styleUrls: ['./autocomplete-control.component.css'],
  templateUrl: './autocomplete-control.component.html',

})
export class AutocompleteControlComponent implements ControlValueAccessor, Validator, OnChanges {
  //The internal data model
  private innerValue: any;

  //Placeholders for the callbacks which are later provided
  //by the Control Value Accessor
  private onTouchedCallback: () => {};
  private onChangeCallback: (_: any) => {};
  OnValidationCallback: any = () => { };

  //get accessor
  get value(): any {
    return this.innerValue;
  };

  //set accessor including call the onchange callback
  set value(v: any) {
    if (v !== this.innerValue) {

      this.innerValue = v;

      this.onChangeCallback(v);
    }
  }

  //Set touched on blur
  onBlur() {
    this.onTouchedCallback();
  }

  //From ControlValueAccessor interface
  writeValue(value: any) {
    //if (value !== this.innerValue) {
    //  if (this.items && !this.items.map(x => x.id).includes(value)) {
    //    this.items = [...this.items, value];
    //  }
    //  else
    if (value == null)
      this.innerValue = null;
    if (value != null && (value.id || value.length > 0)) {
      this.innerValue = value;
    }

    //}
  }

  //From ControlValueAccessor interface
  registerOnChange(fn: any) {
    this.onChangeCallback = fn;
  }

  //From ControlValueAccessor interface
  registerOnTouched(fn: any) {
    this.onTouchedCallback = fn;
  }
  @Input() items: any[];
  @Input() bindLabel: string;
  @Input() bindValue: string;
  @Input() autoCompleteAPI: string;
  @Input() bindText: any;
  @Input() disabled: boolean = false;
  @Input() multiple: boolean = false;
  @Input() isExternalUrl: string = "false";
  @Input() clearable: boolean = true;
  @Output() change: EventEmitter<any> = new EventEmitter<any>();
  @Input() showAddLookupDialog: boolean = false;
  @Input() domainCode: string;
  @Input() placeholder = '...';
  @Input() excluded?: number[] = [];

  typeahead = new Subject<string>();
  isFinished = true;

  constructor(private apiHelper: HttpClient, private cd: ChangeDetectorRef, @Self() private controlDirective: NgControl, private translateService: TranslateService) {
    this.controlDirective.valueAccessor = this;
  }

  onChange($event: any) {
    this.controlDirective.control.markAsTouched();
    this.change.emit($event);
  }

  validate(control: FormControl) {
    return this.OnValidationCallback(control);
  }
  registerOnValidatorChange?(fn: () => void): void {
  }
  setDisabledState?(isDisabled: boolean): void {
  }

  ngOnInit() {
    this.controlDirective.control.setValidators([this.validate.bind(this)]);
    this.controlDirective.control.updateValueAndValidity();

    this.cd.detectChanges();
    if (this.autoCompleteAPI) {
      this.typeahead
        .pipe(
          debounceTime(200),
          switchMap((term: string) => this.getAutoCompleteResult(term)
          ))
        .subscribe(resultItems => {
          this.items = resultItems;
          if (this.excluded.length > 0 && this.items.length > 0) {
            this.items = this.items.filter(x => !this.excluded.includes(x.id))
          }
          return this.items;
        }, (error) => {
          console.log(error);
        });
    }
  }

  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    if (changes.autoCompleteAPI) {
      this.typeahead
        .pipe(
          switchMap(() => this.getAutoCompleteResult('')
          ))
        .subscribe(resultItems => {
          this.items = resultItems;
          if (this.excluded.length > 0 && this.items.length > 0) {
            this.items = this.items.filter(x => !this.excluded.includes(x.id))
          }
          return this.items;
        }, (error) => {
          console.log(error);
        });

      this.typeahead.next();
      this.typeahead.observers.pop();
    }
  }

  getAutoCompleteResult(text: string, bindFromDb = false) {
    let params = new HttpParams();
    if (text == null) text = '';
    params = params.append('text', text);
    params = params.append('lang', this.getCurrentLang());
    let headers = new HttpHeaders();
    headers = headers.append('no-loader', 'true');
    if (this.isExternalUrl == "true") {
      return this.apiHelper.get<any>(this.autoCompleteAPI, { params: params, headers: headers });

    }
    else {
      return this.apiHelper.get<any>(environment.serverUrl + this.autoCompleteAPI, { params: params, headers: headers });
    }
  }

  onSelect(event: any) {
    this.value = event;
  }

  getCurrentLang() {
    return this.translateService.store.currentLang == "ar-AR" ? "Arabic" : "English";
  }

  private stringToBoolean(str: string) {
    switch (str.toLowerCase().trim()) {
      case "true": case "yes": case "1": return true;
      case "false": case "no": case "0": case null: return false;
      default: return Boolean(str);
    }
  }
}
