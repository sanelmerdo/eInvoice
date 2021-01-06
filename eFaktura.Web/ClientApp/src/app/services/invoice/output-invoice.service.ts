import { Injectable, Output } from '@angular/core';
import { HttpService } from '../http/http.service';
import { OutputInvoice } from '../../models/outputInvoice';

@Injectable({
  providedIn: 'root'
})
export class OutputInvoiceService {

  constructor(private http: HttpService) { }

  public createOutputInvoice(outputInvoice: OutputInvoice) {
    return this.http.postRequest<any>("outputInvoice/create", outputInvoice);
  }

  public getAllOutputInvoicesPerPeriodAndClientId(clientId: number, taxPeriod: string) {
    return this.http.getRequest<OutputInvoice[]>("outputInvoice/client/" + clientId + "/taxperiod/" + taxPeriod);
  }
}
