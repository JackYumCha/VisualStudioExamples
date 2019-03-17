import { Component, OnInit, Input, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-datetime',
  templateUrl: './datetime.component.html',
  styleUrls: ['./datetime.component.scss']
})
export class DatetimeComponent implements OnInit {

  date: string;
  // hour: number; // 0-23
  // minute: number; //0-59
  // second: number; //0-59
  time: string;
  @Input()
  set dateTime(value : string){
    // var matches = /(\d{4}-\d{1,2}-\d{1,2})\s+(\d{2}):(\d{1,2}):(\d{1,2})/ig.exec(value);

    var matches = /(\d{4}-\d{1,2}-\d{1,2})\s+(\d{2}:\d{1,2}:\d{1,2})/ig.exec(value);
    this.date = matches[1];
    // this.hour = Number(matches[2]);
    // this.minute = Number(matches[3]);
    // this.second = Number(matches[4]);

    this.time = matches[2];
  }

  @Output()
  dateTimeChange: EventEmitter<string> = new EventEmitter<string>();

  get dateTime(): string{
    return `${this.date} ${this.time}`;
  }

  constructor() { }

  ngOnInit() {
  }

}
