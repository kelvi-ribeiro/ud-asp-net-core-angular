import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http'
import { BsDropdownModule, TooltipModule, ModalModule } from "ngx-bootstrap";

import { EventosComponent } from './eventos/eventos.component';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';


import { DateTimeFormatTimePipe } from './helps/DateTimeFormatTime.pipe';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatTimePipe
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
