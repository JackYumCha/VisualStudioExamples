import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate} from '@angular/router';
import { LoginSingleton } from './login-singleton.service';
import { map } from 'rxjs/operators';
import { GenericJwtToken } from '../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.GenericJwtToken';

class RouterGuardBase implements CanActivate {
  constructor(public login: LoginSingleton, public router: Router) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.login.token){
    
      let canAccess = this.check(this.login.token);
      if(!canAccess){
        console.log('navigate to login:', route.pathFromRoot);
        this.login.targetUrl = route.pathFromRoot.filter(p => p.routeConfig).map(p => p.url.map(u => u.path).join('/')).join('/');
        this.router.navigate(['login']);
      }
      return canAccess;
    }
    else{
      return this.login.fetchToken()
        .pipe(map(token=> {
      
          let canAccess = token && this.check(token);
          if(!canAccess){
            console.log('navigate to login:', route.pathFromRoot);
            this.login.targetUrl = route.pathFromRoot.filter(p => p.routeConfig).map(p => p.url.map(u => u.path).join('/')).join('/');
            this.router.navigate(['login']);
          }
          return canAccess;
        }));
    }
  }
  check = (token: GenericJwtToken) => false;
}

@Injectable({providedIn: 'root'})
export class AnyAuthGuard extends RouterGuardBase{
  constructor(public login: LoginSingleton, public router: Router) {
    super(login, router);
  }
  check = (token: GenericJwtToken): boolean => {
    console.log('user auth:', token);
    return token.Valid && Array.isArray(token.Roles) && token.Roles.length > 0;
  }
}

@Injectable({providedIn: 'root'})
export class AdministratorAuthGuard extends RouterGuardBase{
  constructor(public login: LoginSingleton, public router: Router) {
    super(login, router);
  }
  check = (token: GenericJwtToken): boolean => {
    return token.Valid && Array.isArray(token.Roles) && token.Roles.some(role => role == 'Administrator');
  }
}

@Injectable({providedIn: 'root'})
export class UserAuthGuard extends RouterGuardBase{
  constructor(public login: LoginSingleton, public router: Router) {
    super(login, router);
  }
  check = (token: GenericJwtToken): boolean => {
    return token.Valid && Array.isArray(token.Roles) && token.Roles.some(role => 
      role == 'User');
  }
}