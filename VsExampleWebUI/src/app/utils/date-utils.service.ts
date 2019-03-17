import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateUtilsService {

  constructor() { }

  /** return current date as yyyy-MM-dd */
  get getCurrentDate(): string{
    let now = new Date(Date.now());
    return this.toDateString(now);
  }

  toDateString(value: Date|number|string): string{
    let date = new Date(value);
    return `${date.getFullYear()}-${(date.getMonth()+1).toString().padStart(2,'0')}-${date.getDate().toString().padStart(2,'0')}`;
  }
  asDateString(year: number, month: number, date: number): string{
    return `${year}-${(month+1).toString().padStart(2,'0')}-${date.toString().padStart(2,'0')}`;
  }
  addYears(
    /** format: yyyy-MM-dd */
    date: string|number|Date, 
    years: number): string{
      let current = new Date(date);
      let resultDate = new Date(this.asDateString(current.getFullYear()+years, current.getMonth(), current.getDate()));
      return this.toDateString(resultDate);
  }
}
