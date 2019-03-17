import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'includes'
})
export class IncludesPipe implements PipeTransform {

  transform(value: any[], item: any): any {
    return Array.isArray(value) && value.includes(item);
  }

}
