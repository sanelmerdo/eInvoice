import { Company } from "./company";
import { Client } from "./client";

export class InputInvoice {
  id: number;
  clientId: number;
  taxPeriod: string;
  serialNumber: string;
  documentType: string;
  invoiceNumber: string;
  documentDate: string;
  documentReceivedDate: string;
  companyId: number;
  invoiceAmountWithoutPdv: number;
  invoiceAmountWithPdv: number;
  flatFeeAmount: number;
  inputPdvAmount: number;
  inputPdvWhichCanBeRefused: number;
  inputPdvWhichCannotBeRefused: number;
  inputPdv32: number;
  inputPdv33: number;
  inputPdv34: number;
  createDate: Date;
  modifiedDate: Date;

  company: Company;
  client: Client;
}
