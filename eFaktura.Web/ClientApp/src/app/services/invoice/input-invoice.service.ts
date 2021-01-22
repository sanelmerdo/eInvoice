import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { InputInvoice } from '../../models/inputInvoice';

@Injectable({
  providedIn: 'root'
})
export class InputInvoiceService {

  constructor(private http: HttpService) { }

  public createInputInvoice(inputInvoice: InputInvoice) {
    return this.http.postRequest<any>("inputInvoice/create", inputInvoice);
  }

  public getAllInputInvoicesPerPeriodAndClientId(clientId: number, taxPeriod: string) {
    return this.http.getRequest<InputInvoice[]>("inputInvoice/client/" + clientId + "/taxperiod/" + taxPeriod);
  }

  public updateInputInvoice(inputInvoice: InputInvoice) {
    return this.http.putRequest<any>("inputInvoice/update/", inputInvoice);
  }

  public deleteInputInvoice(id: number) {
    return this.http.deleteRequest<any>("inputInvoice/delete/" + id);
  }

  public generateCsv(clientId: number, taxNumber: string) {
    return this.http.getRequest<any>("csvgenerator/input/clientId/" + clientId + "/taxPeriod/" + taxNumber);
  }
}
