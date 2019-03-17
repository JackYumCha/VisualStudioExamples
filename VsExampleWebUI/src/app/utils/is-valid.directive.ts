import { Directive, Input, HostBinding, HostListener, ElementRef } from '@angular/core';
import { ObjectOrientedRenderer3 } from '@angular/core/src/render3/interfaces/renderer';

@Directive({
  selector: '[isValid]'
})
export class IsValidDirective {

  constructor(public element: ElementRef<HTMLElement>) { }
  
  @HostBinding('attr.has-error') hasError: boolean;

  isTouched: boolean = false;
  @HostListener('blur', ['event']) onBlue(){
    this.isTouched = true;
    this.element.nativeElement['touched'] = true;
    this.hasError = this.isTouched ? (this.currentValue? undefined: true): undefined;
  }

  private currentValue: boolean;
  @Input('isValid') 
  set isValid(value: boolean){
    this.hasError = this.isTouched ? (value? undefined: true): undefined;
    this.currentValue = value;
  }

  get isValid(): boolean{
    return this.currentValue;
  }

}
