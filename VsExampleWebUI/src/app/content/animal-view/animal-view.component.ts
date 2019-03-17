import { AnimalService } from './../animal.service';
import { ControlService } from './../../utils/control.service';
import { Animal } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-animal-view',
  templateUrl: './animal-view.component.html',
  styleUrls: ['./animal-view.component.scss'],
  providers: [ControlService]
})
export class AnimalViewComponent implements OnInit {

  animal: Animal = {};

  constructor(public controlService: ControlService, public route: ActivatedRoute, public animalService: AnimalService) { 

    this.controlService.onNavigationEnd.subscribe(this.onNavigationEnd);
  }

  onNavigationEnd =(end: NavigationEnd) => {
    let animalName = this.route.snapshot.paramMap.get('name');

    this.controlService.load(this.animalService.GetAnimal(animalName))
      .subscribe(a => {
        console.log('animal:', a);
        this.animal = a;
      });
  }

  ngOnInit() {
  }


}
