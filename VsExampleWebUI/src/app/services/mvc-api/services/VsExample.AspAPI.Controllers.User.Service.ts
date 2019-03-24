/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "VsExample.AspAPI"
 * Source Type: "C:\Users\erris\Documents\GitHub\VisualStudioExamples\VsExample.AspAPI\bin\Debug\netcoreapp2.2\VsExample.AspAPI.dll"
 * Generated At: 2019-03-24 17:14:25.566
 */
import { LoginRequest } from '../datatypes/VsExample.AspAPI.Dtos.LoginRequest';
import { GenericJwtToken } from '../datatypes/VsExample.AspAPI.Dtos.GenericJwtToken';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({providedIn: 'root'})
export class UserService {
	constructor(private $httpClient: HttpClient) {{}}
	public $baseURL: string = '<VsExample.AspAPI>';
	public Login(loginRequest: LoginRequest): Observable<GenericJwtToken> {
		return this.$httpClient.post<GenericJwtToken>(this.$baseURL + 'User/Login', loginRequest, {});
	}
	public Renew(): Observable<GenericJwtToken> {
		return this.$httpClient.post<GenericJwtToken>(this.$baseURL + 'User/Renew', null, {});
	}
	public Logout(): Observable<GenericJwtToken> {
		return this.$httpClient.post<GenericJwtToken>(this.$baseURL + 'User/Logout', null, {});
	}
}
