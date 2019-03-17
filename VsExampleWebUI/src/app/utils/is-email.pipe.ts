import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'isEmail'
})
export class IsEmailPipe implements PipeTransform {

  transform(value: string): any {
    return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
  }

}
