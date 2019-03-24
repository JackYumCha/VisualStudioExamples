import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http/src/interceptor';
import { HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http/src/backend';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { catchError, retry } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LoginSingleton } from 'src/app/auth/login-singleton.service';
import { ErrorLog } from '../utils/error-log';

const API_SERVER = '<VsExample.AspAPI>';

@Injectable()
export class VxExamplesInterceptor implements HttpInterceptor {
    constructor(private router: Router, private loginSinglton: LoginSingleton) {
    }

    /** this will add the authorization to the header */
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if(req.url.includes(API_SERVER)){
            req = req.clone({
                url: req.url.replace(API_SERVER, environment.baseUrl),
                withCredentials: true
            });
        }
        return next.handle(req)
        .pipe(retry(0))
        .pipe(catchError((err: any, httpError) => {
            if(err instanceof HttpErrorResponse){
                console.error('Http Error: ', err.status, err.statusText);
                console.log(`%cApi Url:`, 'color: grey; padding: 0px 0px;');
                console.log(`%c${err.url}`, 'color: blue; padding: 0px 20px;');
                ErrorLog.log(err.error);
                throw 'Http Error Response';
            }
            else{
                console.warn('Error:', err, httpError);
                throw 'Http Request Failure';
            }
        }));
    }
}
