import { MsgService } from './msg.service';
import { Component, HostBinding } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'VsExampleWebUI2';

  // @HostBinding('attr.hidding') isHidding: boolean;

  constructor(public msg: MsgService, public router: Router){

  }
  bntClick(){
    console.log('click1');
  }

  bntClick2(){
    console.log('click1');
  }

  logout(){
    this.router.navigate(['login']);
  }
}
