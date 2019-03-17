import { Animal } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';
import { AnimalService } from './../animal.service';
import { ControlService } from './../../utils/control.service';
import { ListAnimalRequest } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.ListAnimalRequest';
import { Component, OnInit } from '@angular/core';
import { ListAnimalResponse } from 'src/app/services/mvc-api/datatypes/VsExample.AspAPI.Dtos.ListAnimalResponse';
import { Router } from '@angular/router';

@Component({
  selector: 'app-animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.scss'],
  providers: [ControlService]
})
export class AnimalListComponent implements OnInit {

  listAnimalRequest: ListAnimalRequest = { NumberPerPage: 2, PageIndex: 0};
  listAnimalResponse: ListAnimalResponse = {};

  constructor(public controlService: ControlService, 
    public animalService: AnimalService,
    public router: Router) { }

  ngOnInit() {
    this.listAnimals();
  }

  listAnimals(){
    this.controlService.load(this.animalService.ListAnimals(this.listAnimalRequest), 'loading animals...', 'animals loaded.')
    .subscribe(response => {
      this.listAnimalResponse = response;
      console.log('list:', response);
    });
  }
  selectItem(item: Animal){
    this.router.navigate(['app', 'animals', 'view', item.Name])
  }
}
