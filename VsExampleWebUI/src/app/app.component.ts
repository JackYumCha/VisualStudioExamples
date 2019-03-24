import { LoginSingleton } from 'src/app/auth/login-singleton.service';
import { ControlService } from './utils/control.service';
import { UserService } from './services/mvc-api/services/VsExample.AspAPI.Controllers.User.Service';
import { MsgService } from './msg.service';
import { Component, HostBinding } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [ControlService]
})
export class AppComponent {
  title = 'VsExampleWebUI2';

  // @HostBinding('attr.hidding') isHidding: boolean;

  constructor(
    public msg: MsgService, 
    public router: Router,
    public userService: UserService,
    public controlService: ControlService,
    public loginSingleton: LoginSingleton
    ){

  }
  bntClick(){
    console.log('click1');
  }

  bntClick2(){
    console.log('click1');
  }

  logout(){
    this.controlService.load(this.userService.Logout())
      .subscribe(() => {
        this.router.navigate(['login']);
      });
    
  }
}
