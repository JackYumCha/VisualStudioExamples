import { AnimalService } from './../../services/mvc-api/services/VsExample.AspAPI.Controllers.Animal.Service';
import { Animal } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';
import { MsgService } from './../../msg.service';
import { AppComponent } from './../../app.component';
import { DatetimeComponent } from './../../editor/datetime/datetime.component';
import { Component, OnInit, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

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

  animal: Animal;

  @ViewChildren(DatetimeComponent) datetimeComponents!: QueryList<DatetimeComponent>;

  constructor(public app: AppComponent, public msg: MsgService, 
    public animalService: AnimalService,
    public domSanitiozer: DomSanitizer) { }

  ngOnInit() {
    
  }

  getAnimal(){
    this.animalService.GetOneAnimal({
      _id: 'panda'
    })
      .subscribe(response => {
        console.log('response:', response);
        this.animal = response;
      });
  }
}
