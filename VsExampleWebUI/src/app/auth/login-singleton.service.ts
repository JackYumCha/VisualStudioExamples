import { Router } from '@angular/router';
import { Injectable } from "@angular/core";
import { Subject, of } from "rxjs";
import { map, catchError } from 'rxjs/operators';
import { GenericJwtToken } from '../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.GenericJwtToken';
import { UserService } from '../services/mvc-api/services/VsExample.AspAPI.Controllers.User.Service';

@Injectable({
    providedIn: 'root'
})
export class LoginSingleton{
    token: GenericJwtToken;
    targetUrl: string;
    // private tokenHolder: Subject<GenericJwtToken> = new Subject<GenericJwtToken>();
    constructor(private user: UserService, private router: Router){
    }
    fetchToken(){
        return this.user.Renew()
            .pipe(map(token => {
                this.token = token;
                return token;
            }))
            .pipe(catchError((err: any) =>{
                console.warn('login singleton error 404:', err);
                this.token = {};
                this.router.navigate(['login']);
                return of(<GenericJwtToken>{});
            }));
    }
}