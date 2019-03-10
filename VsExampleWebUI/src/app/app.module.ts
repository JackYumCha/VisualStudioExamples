import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Demo2Component } from './content/demo2/demo2.component';
import { Demo1Component } from './content/demo1/demo1.component';
import { DatetimeComponent } from './editor/datetime/datetime.component';
import { MsgService } from './msg.service';

@NgModule({
  declarations: [
    AppComponent,
    Demo2Component,
    Demo1Component,
    DatetimeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    MsgService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
