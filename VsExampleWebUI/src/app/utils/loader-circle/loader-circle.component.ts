import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-loader-circle',
  templateUrl: './loader-circle.component.html',
  styleUrls: ['./loader-circle.component.scss']
})
export class LoaderCircleComponent implements OnInit {

  @Input() size: number = 32;

  constructor() { }

  ngOnInit() {
  }

}
