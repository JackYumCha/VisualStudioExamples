/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "VsExample.AspAPI"
 * Source Type: "C:\Users\erris\Documents\GitHub\VisualStudioExamples\VsExample.AspAPI\bin\Debug\netcoreapp2.2\VsExample.AspAPI.dll"
 * Generated At: 2019-04-14 13:31:14.338
 */
import { GetAnimalRequest } from '../datatypes/VsExample.AspAPI.Dtos.GetAnimalRequest';
import { Animal } from '../datatypes/VsExample.AspAPI.Dtos.Animal';
import { ListAnimalRequest } from '../datatypes/VsExample.AspAPI.Dtos.ListAnimalRequest';
import { ListAnimalResponse } from '../datatypes/VsExample.AspAPI.Dtos.ListAnimalResponse';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({providedIn: 'root'})
export class AnimalService {
	constructor(private $httpClient: HttpClient) {{}}
	public $baseURL: string = '<VsExample.AspAPI>';
	public GetOneAnimal(getAnimalRequest: GetAnimalRequest): Observable<Animal> {
		return this.$httpClient.post<Animal>(this.$baseURL + 'Animal/GetOneAnimal', getAnimalRequest, {});
	}
	public CreateOneAnimal(animal: Animal): Observable<Animal> {
		return this.$httpClient.post<Animal>(this.$baseURL + 'Animal/CreateOneAnimal', animal, {});
	}
	public DeleteOneAnimalById(animal: Animal): Observable<Animal> {
		return this.$httpClient.post<Animal>(this.$baseURL + 'Animal/DeleteOneAnimalById', animal, {});
	}
	public ListAnimal(request: ListAnimalRequest): Observable<ListAnimalResponse> {
		return this.$httpClient.post<ListAnimalResponse>(this.$baseURL + 'Animal/ListAnimal', request, {});
	}
}
