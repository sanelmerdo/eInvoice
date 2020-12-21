import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "../home/home.component";
import { NgModule } from "@angular/core";
import { CompanyComponent } from "../company/company.component";

const routes: Routes = [
  { path: 'clients', component: HomeComponent, pathMatch: 'full' },
  { path: 'companies', component: CompanyComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
