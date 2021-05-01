import { Component, ViewChild, Output, EventEmitter, Input, forwardRef, OnChanges, SimpleChanges, Self } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl, Validator, NgControl } from '@angular/forms';
import { NgSelectComponent } from '@ng-select/ng-select';
import { Lookup } from '../../models/lookup.model';
import { LookupService } from '../../services/lookup.service';

@Component({
  selector: 'lookup',
  templateUrl: './lookup-list.component.html',
  styleUrls: ['./lookup-list.component.css'],
  providers: [
  ]
})
export class LookupListComponent implements ControlValueAccessor, Validator, OnChanges {
  @ViewChild("ddl", { static: false }) ddl: NgSelectComponent;
  innerValue: Lookup;

  OnChangeCallback: any = () => { };
  OnTouchCallback: any = () => { };
  OnValidationCallback: any = () => { };

  @Input()
  excludedLookups?: number[] = [];

  @Input()
  lookupName: string;

  @Input()
  lookupFullUrl: string;

  @Input()
  lookups: Lookup[];

  @Input()
  selfLoad: boolean = true;

  @Input()
  readonly: boolean = false;

  @Input()
  clearable: boolean = true;

  @Input() set
    parentId(parentId: number) {
    if (parentId)
      this.currentparentId = parentId;
    // this.ListLookup();
  }
  @Input() set
    secondParentId(secondParentId: number) {
    if (secondParentId)
      this.currentsecondParentId = secondParentId;
    //this.ListLookup();
  }
  //@Input()
  currentparentId: number = null;
  //@Input()
  currentsecondParentId: number = null;

  @Input() bindLabel = "description";

  @Input()
  multiple: boolean = false;

  @Output()
  OnChange: EventEmitter<Lookup> = new EventEmitter<Lookup>();

  constructor(protected svcLookup: LookupService, @Self() private controlDirective: NgControl) {
    this.controlDirective.valueAccessor = this;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.readonly && changes.readonly.currentValue != changes.readonly.previousValue)
      this.ListLookup();
  }

  ngOnInit() {
    this.controlDirective.control.setValidators([this.validate.bind(this)]);
    this.controlDirective.control.updateValueAndValidity();
    if (this.selfLoad == true && (this.readonly == false || this.readonly == undefined) && (this.lookupName || this.lookupFullUrl)) {
      this.ListLookup();
    }
  }

  writeValue(value: any): void {
    this.value = value;
  }

  get value(): any {
    return this.innerValue;
  };

  set value(value: any) {
    if (value !== this.innerValue) {
      this.innerValue = value;
      this.OnChangeCallback(this.innerValue);
      this.OnChange.emit(this.innerValue);

      if (this.selfLoad == true && this.lookupName && this.readonly == false) {
        // this.ListLookup();
      }
    }
  }

  registerOnChange(callback: any): void {
    this.OnChangeCallback = callback;
  }
  registerOnTouched(fn: any): void {
    this.OnTouchCallback = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    //this.ddl.disabled = isDisabled;
  }

  change() {
    this.controlDirective.control.markAsTouched();
  }

  validate(control: FormControl) {

    return this.OnValidationCallback(control);
  }

  ListLookup() {
    if (!this.readonly) {
      if (this.lookupFullUrl) {
        this.svcLookup.List(this.lookupFullUrl, true).subscribe(result => {
          this.lookups = result;
          if (this.excludedLookups.length > 0 && this.lookups.length > 0) {
            this.lookups = this.lookups.filter(x => !this.excludedLookups.includes(x.id))
          }
        });
      }
      else {
        this.svcLookup.List(this.lookupName).subscribe(result => {
          this.lookups = result;
          if (this.excludedLookups.length > 0 && this.lookups.length > 0) {
            this.lookups = this.lookups.filter(x => !this.excludedLookups.includes(x.id))
          }
        });
      }
    }
  }
}
