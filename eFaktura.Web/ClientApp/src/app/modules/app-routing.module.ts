import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "../home/home.component";
import { NgModule } from "@angular/core";
import { CompanyComponent } from "../company/company.component";
import { KifComponent } from "../kif/kif.component";
import { KufComponent } from "../kuf/kuf.component";

const routes: Routes = [
  { path: 'clients', component: HomeComponent },
  { path: 'companies', component: CompanyComponent },
  { path: 'kif', component: KifComponent },
  { path: 'kuf', component: KufComponent },
  { path: '', redirectTo: '/clients', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
