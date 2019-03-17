import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-input-types',
  templateUrl: './input-types.component.html',
  styleUrls: ['./input-types.component.scss']
})
export class InputTypesComponent implements OnInit {

  constructor() { }

  singleOption: string;
  options:{name: string, value: string}[] = [
    {
      name: 'Option 1',
      value: 'cat'
    },
    {
      name: 'Option 2',
      value: 'dog'
    },   
    {
      name: 'Option 3',
      value: 'pig'
    }
];

  multipleOptions: string[] = ['cat'];

  text: string = 'text value';
  numberValue: number = 2434242;
  date: string = '2019-03-24';
  time: string = '14:28:33';


  @ViewChild('mydialog') myDialog: ElementRef<HTMLDialogElement>;
  ngOnInit() {
  }

}
