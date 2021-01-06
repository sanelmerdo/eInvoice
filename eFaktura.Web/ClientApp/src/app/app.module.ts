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
import { TabViewModule } from 'primeng/tabview';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './modules/app-routing.module';
import { TableComponent } from './shared/components/table/table.component';
import { HeaderComponent } from './header/header.component';
import { Table2Component } from './shared/components/table2/table2.component';
import { CompanyComponent } from './company/company.component';
import { KifComponent } from './kif/kif.component';
import { KufComponent } from './kuf/kuf.component';
import { AutoCompleteDropdownComponent } from './shared/components/auto-complete-dropdown/auto-complete-dropdown.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TableComponent,
    HeaderComponent,
    Table2Component,
    CompanyComponent,
    KifComponent,
    KufComponent,
    AutoCompleteDropdownComponent
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
    CardModule,
    TabViewModule,
    DropdownModule,
    CalendarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
