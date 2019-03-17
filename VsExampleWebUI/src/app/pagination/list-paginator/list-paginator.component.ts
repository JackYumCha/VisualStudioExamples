import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { IPage } from './page';

@Component({
  selector: 'app-list-paginator',
  templateUrl: './list-paginator.component.html',
  styleUrls: ['./list-paginator.component.scss']
})
export class ListPaginatorComponent implements OnInit {

  pages: IPage[] = [
    {
      content: '-',
      isLink: false,
      pageIndex: undefined
    }
  ];

  $numberOfPages: number;

  @Input()
  set numberOfPages(value: number){
    this.$numberOfPages = value;
    this.updatePages();
  }

  @Input()
  set selectedPage(value: number){
    this.$selectedPage = value;
    this.updatePages();
  }


  updatePages (){
    this.pages.splice(0, this.pages.length);

    let stage: number =0;
    let coverage = 4;

    if(this.$numberOfPages == 0) {
      this.pages.push({
        content: '-',
        isLink: false,
        pageIndex: undefined
      });
      return;
    }

    this.pages.push({
      content: '1',
      pageIndex: 0,
      isLink: this.$selectedPage != 0
    });

    
    if(this.$numberOfPages == 1) return;

    if(this.$selectedPage - 1 > coverage){
      this.pages.push({
        content: '...',
        isLink: false,
        pageIndex: undefined
      });
    }

    let fromPage = Math.max(1, this.$selectedPage - coverage);
    let toPage = Math.min(this.$selectedPage + coverage, this.$numberOfPages - 1);

    for(let i = fromPage; i <= toPage; i++){
      this.pages.push({
        content: `${i+1}`,
        pageIndex: i,
        isLink: this.$selectedPage != i
      });
    }

    //41 - 1 - 35 > 4
    if(this.$numberOfPages - 2 - this.selectedPage > coverage){
      this.pages.push({
        content: '...',
        isLink: false,
        pageIndex: undefined
      });
    }
    
    if(this.$selectedPage + coverage >= this.$numberOfPages -1) return;

    this.pages.push({
      content: `${this.$numberOfPages}`,
      pageIndex: this.$numberOfPages - 1,
      isLink: this.$selectedPage != this.$numberOfPages - 1
    });
    
  }

  $selectedPage: number = 0;

  get selectedPage():number {
    return this.$selectedPage;
  }

  @Output()
  pageChanged: EventEmitter<number> = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }

  pageClicked(index: number){
    this.pageChanged.emit(index);
    this.$selectedPage = index;
  }
}
