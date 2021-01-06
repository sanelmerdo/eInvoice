import { Company } from "./company";
import { Client } from "./client";

export class OutputInvoice {
  id: number;
  clientId: number;
  taxPeriod: string;
  serialNumber: string;
  documentType: string;
  invoiceNumber: string;
  documentDate: string;
  companyId: number;
  invoiceAmount: number;
  internalInvoiceAmount: number;
  exportDeliveryAmount: number;
  invoiceAmountForOtherDelivery: number;
  basisAmountForCalculation: number;
  outputPDV: number;
  basicforCalulcationToNonRegisteredUser: number;
  outputPDVToNonRegisteredUser: number;
  outputPDV32: number;
  outputPDV33: number;
  outputPDV34: number;
  createDate: Date;
  modifiedDate: Date;

  company: Company;
  client: Client;
}
