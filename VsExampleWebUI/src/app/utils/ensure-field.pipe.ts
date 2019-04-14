import { Pipe, PipeTransform } from '@angular/core';

export function ensureField(value: any, field: string, defaultValue: any): any{
  if(!value) return defaultValue;
    let result = value[field];
    if(result === undefined || result === null) return defaultValue;
    return result;
}

@Pipe({
  name: 'ensureField'
})
export class EnsureFieldPipe implements PipeTransform {

  transform(value: any, field: string, defaultValue: any): any {
    return ensureField(value, field, defaultValue);
  }

}


