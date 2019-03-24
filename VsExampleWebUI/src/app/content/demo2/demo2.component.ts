import { DomSanitizer } from '@angular/platform-browser';
import { AnimalService } from './../../services/mvc-api/services/VsExample.AspAPI.Controllers.Animal.Service';
import { Component, OnInit } from '@angular/core';
import { Animal } from 'src/app/services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';

@Component({
  selector: 'app-demo2',
  templateUrl: './demo2.component.html',
  styleUrls: ['./demo2.component.scss']
})
export class Demo2Component implements OnInit {

  animal1: Animal;

  constructor(public animalService: AnimalService, public domSanitiozer: DomSanitizer) { }

  ngOnInit() {
    this.getAnimal();
  }

  getAnimal(){
    this.animalService.GetOneAnimal()
      .subscribe(response => {
        console.log('response:', response);
        this.animal1 = response;
      });
  }
  
}
