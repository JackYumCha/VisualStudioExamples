import { ControlService } from './../../utils/control.service';
import { DateUtilsService } from './../../utils/date-utils.service';
import { Router } from '@angular/router';
import { AppComponent } from './../../app.component';
import { Component, OnInit, OnDestroy, ViewChildren, QueryList } from '@angular/core';
import { IsValidDirective } from 'src/app/utils/is-valid.directive';
import { LoginService } from '../login.service';
import { LoginRequest } from 'src/app/services/mvc-api/datatypes/VsExample.AspAPI.Dtos.LoginRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [ ControlService ]
})
export class LoginComponent implements OnInit, OnDestroy {

  username: string;
  email: string;
  dateOfBirth: string;
  password: string;
  minDOB: string;
  maxDOB: string;
  validationNumber: number;

  loginRequest: LoginRequest = {};

  @ViewChildren(IsValidDirective) inputs: QueryList<IsValidDirective>;

  constructor(public router: Router, public dateService: DateUtilsService, 
    public loginService: LoginService, public controlService: ControlService) { // public app: AppComponent, 

    // this.app.isHidding = true;

    this.maxDOB = this.dateService.getCurrentDate;
    this.minDOB = this.dateService.addYears(this.maxDOB, -40);
  }

  ngOnInit() {
  }

  ngOnDestroy(){
    // this.app.isHidding = undefined;
  }
  async login(){
    for(let input of this.inputs.toArray()){
      if(!input.isValid) {
        input.element.nativeElement.scrollIntoView();
        return;
      }
    }
    let success = await this.controlService.load(
      this.loginService.login(this.loginRequest),
      'Calling Login...'
      ).toPromise();
    this.controlService.status = success ? 'Login Succeeded.': 'Login Failed.';
    if(success) this.router.navigate(['']);
  }
}
