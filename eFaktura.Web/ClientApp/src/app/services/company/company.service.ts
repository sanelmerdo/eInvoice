import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Company } from '../../models/company';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpService) { }

  /** Create company entity */
  public createCompany(company: Company): Observable<any> {
    return this.http.postRequest<any>("company/create", company);
  }

  /** get all companies as an observable array */
  public getAllCompanies(id: number) {
    return this.http.getRequest<Company[]>("company/client/" + id);
  }

  /** delete company */
  public deleteCompany(id: number): Observable<any> {
    return this.http.deleteRequest("company/delete/" + id);
  }

  public updateCompany(company: Company): Observable<any> {
    return this.http.putRequest("company/update", company);
  }

  public getCompanyById(id: number): Observable<any> {
    return this.http.getRequest<Company>("company/" + id);
  }
}
