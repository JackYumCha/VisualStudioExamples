import { MsgService } from './msg.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'VsExampleWebUI2';

  constructor(public msg: MsgService){

  }
  bntClick(){
    console.log('click1');
  }

  bntClick2(){
    console.log('click1');
  }
}
