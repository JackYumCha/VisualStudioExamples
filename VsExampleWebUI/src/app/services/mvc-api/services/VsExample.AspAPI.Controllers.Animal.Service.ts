/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "VsExample.AspAPI"
 * Source Type: "C:\Users\erris\Documents\GitHub\VisualStudioExamples\VsExample.AspAPI\bin\Debug\netcoreapp2.2\VsExample.AspAPI.dll"
 * Generated At: 2019-03-24 16:06:02.091
 */
import { Animal } from '../datatypes/VsExample.AspAPI.Dtos.Animal';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({providedIn: 'root'})
export class AnimalService {
	constructor(private $httpClient: HttpClient) {{}}
	public $baseURL: string = '<VsExample.AspAPI>';
	public GetOneAnimal(): Observable<Animal> {
		return this.$httpClient.post<Animal>(this.$baseURL + 'Animal/GetOneAnimal', null, {});
	}
}
