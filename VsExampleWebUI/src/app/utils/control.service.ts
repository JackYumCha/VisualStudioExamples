import { map, filter, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Observable } from 'rxjs';
import { Injectable, OnInit, OnDestroy, NgZone, ChangeDetectorRef } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Injectable()
export class ControlService implements OnInit, OnDestroy {

  /** 宿主被注销时将被调用 */
  destroyed: Subject<boolean> = new Subject<boolean>();

  isLoading: boolean = false;
  status: string;

  /** 当页面路由导航结束时被呼叫 */
  onNavigationEnd: Observable<NavigationEnd>;

  constructor(private router: Router, private changeDetectorRef: ChangeDetectorRef) {
    this.onNavigationEnd = <Observable<NavigationEnd>> router.events
      .pipe(takeUntil(this.destroyed))
      .pipe(filter(e => e instanceof NavigationEnd));
  }

  ngOnInit(){ }

  /** 用来在页面当中进行异步调用 */
  load<T>(observable: Observable<T>, statusLoading?: string, statusLoaded?: string): Observable<T> {
    this.isLoading = true;
    this.status = statusLoading;
    return observable
    .pipe(takeUntil(this.destroyed))
    .pipe(map(obs => {
      this.isLoading = false;
      this.status = statusLoaded;
      return obs;
    }));
  }
  
  ngOnDestroy(): void {
    this.destroyed.next(true);
  }
}
