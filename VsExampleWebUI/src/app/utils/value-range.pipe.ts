import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'valueRange'
})
export class ValueRangePipe implements PipeTransform {

  transform(value: number | string, min?: number|string|undefined, max?: number|string|undefined): any {
    let valueType = typeof value;
    if(valueType != 'string' && valueType != 'number') return false;
    let minType = typeof min;
    if((minType == 'string' || minType == 'number') && value < min) return false;
    let maxType = typeof max;
    if((maxType == 'string' || maxType == 'number') && value > max) return false;
    return true;
  }

}
