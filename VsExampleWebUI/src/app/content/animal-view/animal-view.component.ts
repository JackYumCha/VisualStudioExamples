import { AnimalListComponent } from './../animal-list/animal-list.component';
import { ControlService } from './../../utils/control.service';
import { Animal } from './../../services/mvc-api/datatypes/VsExample.AspAPI.Dtos.Animal';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NavigationEnd, ActivatedRoute, Router } from '@angular/router';
import { AnimalService } from 'src/app/services/mvc-api/services/VsExample.AspAPI.Controllers.Animal.Service';

@Component({
  selector: 'app-animal-view',
  templateUrl: './animal-view.component.html',
  styleUrls: ['./animal-view.component.scss'],
  providers: [ControlService]
})
export class AnimalViewComponent implements OnInit {

  animal: Animal = {};

  @ViewChild('dialogDeleting') dialogDeleting: ElementRef<HTMLDialogElement>;

  constructor(
    public controlService: ControlService, 
    public route: ActivatedRoute, 
    public animalService: AnimalService,
    public router: Router,
    public animalListComponent: AnimalListComponent
    ) { 

    this.controlService.onNavigationEnd.subscribe(this.onNavigationEnd);
  }

  onNavigationEnd =(end: NavigationEnd) => {
    let id = this.route.snapshot.paramMap.get('id');

    this.controlService.load(this.animalService.GetOneAnimal({
      _id: id
    }))
      .subscribe(a => {
        console.log('animal:', a);
        this.animal = a;
      });
  }

  ngOnInit() {
  }


  delete(item: Animal){
    this.controlService.load(this.animalService.DeleteOneAnimalById(item))
    .subscribe(response=>{
      console.log('deleted:', response);
      this.dialogDeleting.nativeElement.close();
      this.animalListComponent.listAnimals();
      this.router.navigate(['app', 'animals']);
    })
  }
}
