import { LoginSingleton } from './../auth/login-singleton.service';
import { UserService } from './../services/mvc-api/services/VsExample.AspAPI.Controllers.User.Service';
import { Observable, of, timer } from 'rxjs';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.LoginRequest';
import { environment } from 'src/environments/environment';
import { MD5Salt } from '../services/mvc-api/enums/VsExample.AspAPI.Dtos.MD5Salt';
import { MD5 } from '../auth/md5';
import { timeout, delay, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(public userService: UserService, public loginSingleton: LoginSingleton) { }

  login(request: LoginRequest): Observable<boolean>{

    let cloned: LoginRequest = {...request};
    cloned.PasswordHash = MD5.encrypt(request.PasswordHash + MD5Salt.MD5Salt_47924720sfhosf);
    if(environment.mockup){
      if(cloned.Username == 'jack' && cloned.PasswordHash == MD5.encrypt('123456' + MD5Salt.MD5Salt_47924720sfhosf)){
        return of(true).pipe(delay(1000)) ;
      }
    }
    else{
      // todo call backend api for login
      return this.userService.Login(request)
        .pipe(map(token => {
          if(token && token.Valid){
            this.loginSingleton.token = token;
            return true;
          }
          return false;
        }));
    }
    return of(false).pipe(delay(1000));
  }
}
