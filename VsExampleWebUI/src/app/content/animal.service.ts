import { Animal } from './../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';
import { delay } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { ListAnimalRequest } from './../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.ListAnimalRequest';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ListAnimalResponse } from '../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.ListAnimalResponse';

let animals: Animal[] = [
  {
    Name: 'Panda',
    ImageUrl: 'https://www.telegraph.co.uk/content/dam/news/2016/08/23/106598324PandawaveNEWS_trans_NvBQzQNjv4Bqeo_i_u9APj8RuoebjoAHt0k9u7HhRJvuo-ZLenGRumA.jpg?imwidth=450'
  },
  {
    Name: 'Goat',
    ImageUrl: 'https://images.mentalfloss.com/sites/default/files/styles/mf_image_16x9/public/iStock-177369626_0.jpg?itok=1Q7WLYKP&resize=1100x1100'
  },
  {
    Name: 'Lion',
    ImageUrl: 'http://www.krugerpark.co.za/images/1-lion-charge-gc590a.jpg'
  }
]

@Injectable({
  providedIn: 'root'
})
export class AnimalService {

  constructor() { }

  ListAnimals(request: ListAnimalRequest): Observable<ListAnimalResponse>{
    let defaultResult: ListAnimalResponse = {};
    if(environment.mockup){
      defaultResult.NumberOfPages = Math.ceil(animals.length / request.NumberPerPage);
      defaultResult.PageIndex = request.PageIndex;
      if(defaultResult.PageIndex < 0) defaultResult.PageIndex = 0;
      if(defaultResult.PageIndex > defaultResult.NumberOfPages) defaultResult.PageIndex = defaultResult.NumberOfPages;
      defaultResult.Items = animals.filter((item, i)=>{
        return i >= defaultResult.PageIndex * request.NumberPerPage && i < (defaultResult.PageIndex + 1)* request.NumberPerPage
      });
      return of(defaultResult).pipe(delay(1000));
    }
    else{

    }
    defaultResult.Items = [];
    defaultResult.NumberOfPages = 0;
    defaultResult.PageIndex = 0;
    return of(defaultResult).pipe(delay(1000));
  }

  GetAnimal(name: string): Observable<Animal>{
    if(environment.mockup){
      let found = animals.find(a => a.Name == name);
      console.log('found', animals, name,  found);
      return of(found);
    }
    return of({});
  }
}


