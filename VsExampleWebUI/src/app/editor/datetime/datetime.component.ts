import { Component, OnInit, Input, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-datetime',
  templateUrl: './datetime.component.html',
  styleUrls: ['./datetime.component.scss']
})
export class DatetimeComponent implements OnInit {

  date: string;
  hour: number; // 0-23
  minute: number; //0-59
  second: number; //0-59
  @Input()
  set dateTime(value : string){
    var matches = /(\d{4}-\d{1,2}-\d{1,2})\s+(\d{2}):(\d{1,2}):(\d{1,2})/ig.exec(value);
    this.date = matches[1];
    this.hour = Number(matches[2]);
    this.minute = Number(matches[3]);
    this.second = Number(matches[4]);
  }

  @Output()
  dateTimeChange: EventEmitter<string> = new EventEmitter<string>();

  get dateTime(): string{
    return `${this.date} ${this.hour.toString().padStart(2, '0')}:${this.minute.toString().padStart(2, '0')}:${this.second.toString().padStart(2, '0')}`;
  }

  constructor() { }

  ngOnInit() {
  }

}
