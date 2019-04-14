import { Animal } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';
import { ControlService } from './../../utils/control.service';
import { ListAnimalRequest } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.ListAnimalRequest';
import { Component, OnInit, ChangeDetectionStrategy, ViewChild, ElementRef } from '@angular/core';
import { ListAnimalResponse } from 'src/app/services/mvc-api/datatypes/VsExample.AspAPI.Dtos.ListAnimalResponse';
import { Router } from '@angular/router';
import { AnimalService } from 'src/app/services/mvc-api/services/VsExample.AspAPI.Controllers.Animal.Service';
import { ensureField } from 'src/app/utils/ensure-field.pipe';

@Component({
  selector: 'app-animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.scss'],
  providers: [ControlService]
})
export class AnimalListComponent implements OnInit {

  listAnimalRequest: ListAnimalRequest = { NumberPerPage: 2, PageIndex: 0};
  listAnimalResponse: ListAnimalResponse = {};

  animalAdding: Animal;
  
  ensureField = ensureField;

  @ViewChild('dialogAdd') dialogAdd: ElementRef<HTMLDialogElement>;


  constructor(
    public controlService: ControlService, 
    public animalService: AnimalService,
    public router: Router
    ) {


    }

  ngOnInit() {
    this.listAnimals();
  }

  listAnimals(){
    this.controlService.load(this.animalService.ListAnimal(this.listAnimalRequest), 'loading animals...', 'animals loaded.')
    .subscribe(response => {
      this.listAnimalResponse = response;
      console.log('list:', response);
    });
  }
  selectItem(item: Animal){
    this.router.navigate(['app', 'animals', 'view', item._id]);
  }

  addAnimal(item: Animal){
    this.controlService.load(this.animalService.CreateOneAnimal(item))
    .subscribe(response =>{
      console.log(response);
      this.listAnimalResponse.Items.splice(0, 0, response);
      this.listAnimalResponse.Items = [...this.listAnimalResponse.Items];
      this.dialogAdd.nativeElement.close();
    })
  }
}
