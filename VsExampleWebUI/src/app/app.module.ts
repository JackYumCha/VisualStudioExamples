import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Demo2Component } from './content/demo2/demo2.component';
import { Demo1Component } from './content/demo1/demo1.component';
import { DatetimeComponent } from './editor/datetime/datetime.component';
import { MsgService } from './msg.service';
import { InputTypesComponent } from './editor/input-types/input-types.component';
import { DashboardComponent } from './content/dashboard/dashboard.component';
import { LoginComponent } from './user/login/login.component';
import { ContainerComponent } from './container/container.component';
import { IncludesPipe } from './utils/includes.pipe';
import { UtilsModule } from './utils/utils.module';
import { AnimalListComponent } from './content/animal-list/animal-list.component';
import { AnimalViewComponent } from './content/animal-view/animal-view.component';
import { ContentListComponent } from './pagination/content-list/content-list.component';
import { ListPaginatorComponent } from './pagination/list-paginator/list-paginator.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { VxExamplesInterceptor } from './services/vsexamples.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    Demo2Component,
    Demo1Component,
    DatetimeComponent,
    InputTypesComponent,
    DashboardComponent,
    LoginComponent,
    ContainerComponent,
    AnimalListComponent,
    AnimalViewComponent,
    ContentListComponent,
    ListPaginatorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    UtilsModule,
    HttpClientModule
  ],
  providers: [
    MsgService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: VxExamplesInterceptor,
      multi: true
    }
  ],
  bootstrap: [ContainerComponent]
})
export class AppModule { }
