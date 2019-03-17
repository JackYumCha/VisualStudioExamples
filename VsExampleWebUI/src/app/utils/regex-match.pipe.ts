import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'regexMatch'
})
export class RegexMatchPipe implements PipeTransform {

  transform(value: any, pattern: string, flags?: string): any {
    let valueType = typeof value;
    if(valueType != 'string' && valueType != 'number') return false;
    let regex =  new RegExp(pattern, flags);
    // console.log('regex pipe:', value, pattern, ( new RegExp(pattern, flags)).test(value) );
    return regex.test(value);
  }

}
