import { Component, forwardRef, Input, AfterViewChecked, EventEmitter, Output, OnChanges, SimpleChanges, OnInit, Self, ChangeDetectorRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, Validator, FormControl, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ElementRef } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'datepicker-control',
  templateUrl: './datepicker-control.component.html',
})
export class DatepickerControlComponent implements ControlValueAccessor, Validator, OnInit {
  /*
  * pass the input and out from the control directive
  * all available input you can wrap it https://github.com/valor-software/ngx-bootstrap/blob/development/src/datepicker/bs-daterangepicker.component.ts
  */

  ngOnInit() {
    this.controlDirective.control.setValidators([this.validate.bind(this)]);
    this.controlDirective.control.updateValueAndValidity();

    //this.cd.detectChanges();

    if (this.hasTime) {
      this._bsConfig.dateInputFormat = 'DD-MM-YYYY hh:mm a';
    }
    else {
      this._bsConfig.dateInputFormat = 'DD-MM-YYYY';

    }

    this._bsConfig.showWeekNumbers = false;
  }

  private _bsConfig: BsDatepickerConfig;
  private _disabled: boolean;

  innerValue: Date | string;
  class: string;

  OnChangeCallback: any = () => { };
  OnTouchCallback: any = () => { };
  OnValidationCallback: any = () => { };

  //To do datepicker with time
  @Input() hasTime: boolean = false;
  @Input() name: string;

  /**
  * Indicates whether datepicker content is enabled or not
  */
  @Input() set disabled(value: boolean) {
    this._disabled = value;
  }

  get disabled(): boolean {
    return this._disabled;
  }

  /**
  * Config object for datepicker
  */
  @Input()
  set bsConfig(value: BsDatepickerConfig) {
    this._bsConfig = value;
  }
  get innerBsConfig(): BsDatepickerConfig {
    return this._bsConfig;
  }

  set value(value: any) {

    if (!value) {
      this.innerValue = value;
    }
    else {
      if (value instanceof Date && this.innerValue && this.innerValue.toString() == value.toString())
        return;

      if (typeof value == 'string' && /([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))T\d{1,2}:\d{1,2}:\d{1,2}.\d{1,3}Z/.test(value)) {
        this.innerValue = new Date(value);
      }
      else
        this.innerValue = value;
    }

    this.onChange(this.innerValue);
  }

  /**
   * Placement of a datepicker. Accepts: "top", "bottom", "left", "right"
   */
  @Input() placement: 'top' | 'bottom' | 'left' | 'right' = 'bottom';

  /**
   * Specifies events that should trigger. Supports a space separated list of
   * event names.
   */
  @Input() triggers = 'click';

  /**
   * Close datepicker on outside click
   */
  @Input() outsideClick = true;

  /**
   * A selector specifying the element the datepicker should be appended to.
   */
  @Input() container = 'body';


  @Input() outsideEsc = true;

  /**
   * Returns whether or not the datepicker is currently being shown
   */
  @Input() isOpen: boolean;

  /**
   * Minimum date which is available for selection
   */
  @Input() minDate: Date;

  /**
   * Maximum date which is available for selection
   */
  @Input() maxDate: Date;

  /**
  * Emits an event when the datepicker is shown
  */
  @Output() onShown: EventEmitter<any>;
  onBsShown(event: any) {
    if (this.onShown)
      this.onShown.emit(event);
  }

  /**
  * Emits an event when the datepicker is hidden
  */
  @Output() onHidden: EventEmitter<any>;
  onBsHidden(event: any) {
    if (this.onHidden)
      this.onHidden.emit(event);
  }

  /**
   * Emits when datepicker value has been changed
   */
  @Output() bsValueChange: EventEmitter<Date[]> = new EventEmitter();
  onBsValueChange(event: Date[]) {
    if (this.bsValueChange)
      this.bsValueChange.emit(event)
  }

  constructor(el: ElementRef, @Self() private controlDirective: NgControl, private cd: ChangeDetectorRef) {
    this.controlDirective.valueAccessor = this;
    this.class = el.nativeElement.className;
    el.nativeElement.className = "";
    this._bsConfig = new BsDatepickerConfig();
    this._bsConfig.dateInputFormat = this._bsConfig.dateInputFormat;
  }

  onChange(value: string | Date) {
    if (value) {
      this.controlDirective.control.markAsTouched();
      let outValue = this.changeDateFormat(value, this._bsConfig.dateInputFormat, this._bsConfig.dateInputFormat);
      this.OnChangeCallback(outValue);
    }
    else {
      this.OnChangeCallback(value);
    }
  }

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.OnChangeCallback = fn;
  }

  registerOnTouched(fn: any): void {
    this.OnTouchCallback = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this._disabled = isDisabled;
  }

  validate(control: FormControl) {
    return this.OnValidationCallback(control);
  }

  /**
 * change date from spicific format to another in strnig 
 * @param value date value 
 * @param inputFormat input date value format
 * @param outputFormat return date string value in this format 
 */
  changeDateFormat(value: string | Date, inputFormat: string, outputFormat: string): string {

    const dateFormatParts = ["YYYY", "MM", "DD", "HH", "mm", "SS", "sss"];

    let date: { [x: string]: string | number; YYYY?: number; MM?: number; DD?: number; HH?: number; mm?: number; SS?: number; sss?: number; } = null;

    if (value instanceof Date) {
      date = {
        ["YYYY"]: value.getFullYear(),
        ["MM"]: value.getMonth() + 1,
        ["DD"]: value.getDate(),
        ["HH"]: value.getHours(),
        ["mm"]: value.getMinutes(),
        ["SS"]: value.getSeconds(),
        ["sss"]: value.getMilliseconds()
      }
    }
    else
      if (typeof value == 'string') {
        date = {};

        let dateFormatPartsIndexes: { part: string; index: number; }[] = [];

        dateFormatParts.forEach(part => {
          let partIndx = inputFormat.indexOf(part);
          if (partIndx != -1) {
            dateFormatPartsIndexes.push({ part: part, index: partIndx });
          }
        });

        dateFormatPartsIndexes.sort((a, b) => {

          if (a.index > b.index)
            return 1;
          else
            if (a.index < b.index)
              return -1;

          return 0;

        });

        let stringValue = value as any;

        dateFormatPartsIndexes.forEach((part, indx) => {
          while (isNaN(stringValue.slice(part.index, part.index + part.part.length))) {
            stringValue = stringValue.slice(0, part.index) + '0' + stringValue.slice(part.index);
          }
        });

        value = stringValue;

        dateFormatParts.forEach(part => {
          let partIndx = inputFormat.indexOf(part);
          if (partIndx != -1) {
            date[part] = stringValue.slice(partIndx, partIndx + part.length);
          }
          else
            date[part] = '';
        });

      }



    if (!date) throw new Error('unsupported value type:(');

    let result = new Date(+date["YYYY"], +date["MM"] - 1, +date["DD"], +date["HH"], +date["mm"], +date["SS"], +date["sss"]);

    var x = moment(result).format('YYYY-MM-DD HH:mm');

    return x;
  }

}
