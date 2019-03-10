import { MsgService } from './../../msg.service';
import { AppComponent } from './../../app.component';
import { DatetimeComponent } from './../../editor/datetime/datetime.component';
import { Component, OnInit, ViewChild, ViewChildren, QueryList } from '@angular/core';

@Component({
  selector: 'app-demo1',
  templateUrl: './demo1.component.html',
  styleUrls: ['./demo1.component.scss']
})
export class Demo1Component implements OnInit {

  myDateTime: string = '2019-03-10 12:44:12';
  myDateTime2: string = '2019-04-10 12:44:12';

  // @ViewChild('#dt1') datetime1: DatetimeComponent;
  // @ViewChild('#dt2') datetime2: DatetimeComponent;

  @ViewChildren(DatetimeComponent) datetimeComponents!: QueryList<DatetimeComponent>;

  constructor(public app: AppComponent, public msg: MsgService) { }

  ngOnInit() {
    
  }

}
