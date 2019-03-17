import { Component, OnInit, Input, TemplateRef, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {

  @Input() items: any[];
  @Input() itemTemplate: TemplateRef<any>

  @Input() numberOfPages: number = 1;
  @Input() selectedPageIndex: number = 0;
  @Output() pageSelected: EventEmitter<number> = new EventEmitter<number>();
  @Output() itemSelected: EventEmitter<any> = new EventEmitter<any>();

  itemIndex: number = -1;
  constructor() { }

  ngOnInit() {
  }

  onItemSelected(i: number){
    console.log('clicked:', i);
    // if(!Array.isArray(this.items) || i<0 || i >= this.items.length ){
    //   this.itemIndex = -1;
    //   this.itemSelected.emit(null);
    // }
     this.itemIndex = i;
     this.itemSelected.emit(this.items[i]);
  }
}
