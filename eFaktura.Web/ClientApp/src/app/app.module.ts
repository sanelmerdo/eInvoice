import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { CardModule } from 'primeng/card';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './modules/app-routing.module';
import { TableComponent } from './shared/components/table/table.component';
import { HeaderComponent } from './header/header.component';
import { AutoCompleteDropdownComponent } from './shared/components/auto-complete-dropdown/auto-complete-dropdown.component';
import { Table2Component } from './shared/components/table2/table2.component';
import { CompanyComponent } from './company/company.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TableComponent,
    HeaderComponent,
    AutoCompleteDropdownComponent,
    Table2Component,
    CompanyComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    TableModule,
    AutoCompleteModule,
    PanelModule,
    ButtonModule,
    NgbModule,
    ReactiveFormsModule,
    ToastModule,
    DialogModule,
    CardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
