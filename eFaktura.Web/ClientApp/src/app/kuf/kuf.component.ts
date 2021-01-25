import { Component, OnInit } from '@angular/core';
import { Month } from '../models/month';
import { Year } from '../models/year';
import { OridinalNumber } from '../models/oridinalNumber';
import { DocumentType } from '../models/documentType';
import { MessageService } from 'primeng/components/common/messageservice';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HeaderService } from '../services/shared/header.service';
import { CompanyService } from '../services/company/company.service';
import { InputInvoiceService } from '../services/invoice/input-invoice.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { Client } from '../models/client';
import { TableColumn } from '../shared/components/table/tableColumn';
import { InputInvoice } from '../models/inputInvoice';
import { Company } from '../models/company';

@Component({
  selector: 'app-kuf',
  templateUrl: './kuf.component.html',
  styleUrls: ['./kuf.component.css'],
  providers: [MessageService, DatePipe]
})
export class KufComponent implements OnInit {

  client: Client;
  selectedMonth: Month;
  selectedYear: Year;
  disabled: boolean = true;
  inputInvoice: InputInvoice = new InputInvoice();
  inputInvoices: InputInvoice[] = [];
  documentDate: Date;
  documentReceivedDate: Date;
  selectedCompany: Company;
  displayDialog: boolean;
  newKuf: FormGroup;
  companies: Company[] = [];
  submitted = false;
  companyResults: string[] = [];
  selectedDocumentType: DocumentType;

  invoiceColumns: TableColumn[] = [
    { key: "id", title: "Id", field: "id", header: "Id", linkable: false, colWidth: 70 },
    { key: "taxPeriod", title: "Porezni period", field: "taxPeriod", header: "Porezni period", colWidth: 70 },
    { key: "serialNumber", title: "Redni broj", field: "serialNumber", header: "Redni broj", colWidth: 70 },
    { key: "documentType", title: "Tip dok", field: "documentType", header: "Tip dok", colWidth: 70 },
    { key: "invoiceNumber", title: "Broj fakture", field: "invoiceNumber", header: "Broj fakture", colWidth: 150 },
    { key: "documentDate", title: "Datum fakture", field: "documentDate", header: "Datum fakture", colWidth: 100 },
    { key: "documentReceivedDate", title: "Datum prijema fakture", field: "documentReceivedDate", header: "Datum prijema fakture", colWidth: 100 },
    { key: "invoiceAmountWithoutPdv", title: "Iznos fakture bez PDV-a", field: "invoiceAmountWithoutPdv", header: "Iznos fakture bez PDV-a", colWidth: 100, decimalPipe: true },
    { key: "invoiceAmountWithPdv", title: "Iznos fakture sa PDV-om", field: "invoiceAmountWithPdv", header: "Iznos fakture sa PDV-om", colWidth: 100, decimalPipe: true },
    { key: "flatFeeAmount", title: "Iznos pausalne naknade", field: "flatFeeAmount", header: "Iznos pausalne naknade", colWidth: 100, decimalPipe: true },
    { key: "inputPdvAmount", title: "Iznos ulaznog PDV-a", field: "inputPdvAmount", header: "Iznos ulaznog PDV-a", colWidth: 100, decimalPipe: true },
    { key: "inputPdvWhichCanBeRefused", title: "Ulazni PDV koji se moze odbiti", field: "inputPdvWhichCanBeRefused", header: "Ulazni PDV koji se moze odbiti", colWidth: 100, decimalPipe: true },
    { key: "inputPdvWhichCannotBeRefused", title: "Ulazni PDV koji se ne moze odbiti", field: "inputPdvWhichCannotBeRefused", header: "Ulazni PDV koji se ne moze odbiti", colWidth: 100, decimalPipe: true },
    { key: "inputPdv32", title: "Pdv 32", field: "inputPdv32", header: "Pdv 32", colWidth: 100, decimalPipe: true },
    { key: "inputPdv33", title: "Pdv 33", field: "inputPdv33", header: "Pdv 33", colWidth: 100, decimalPipe: true },
    { key: "inputPdv34", title: "Pdv 34", field: "inputPdv34", header: "Pdv 34", colWidth: 100, decimalPipe: true }
  ];

  months: Month[] = [
    { name: 'Januar', code: '01' },
    { name: 'Februar', code: '02' },
    { name: 'Mart', code: '03' },
    { name: 'April', code: '04' },
    { name: 'Maj', code: '05' },
    { name: 'Juni', code: '06' },
    { name: 'Juli', code: '07' },
    { name: 'Avgust', code: '08' },
    { name: 'Septembar', code: '09' },
    { name: 'Oktobar', code: '10' },
    { name: 'Novembar', code: '11' },
    { name: 'Decembar', code: '12' }
  ]

  years: Year[] = [
    { name: '2021', code: '21' },
    { name: '2022', code: '22' },
    { name: '2023', code: '23' },
    { name: '2024', code: '24' },
    { name: '2025', code: '25' },
    { name: '2026', code: '26' },
    { name: '2027', code: '27' },
    { name: '2028', code: '28' }
  ]

  oridinalNumbers: OridinalNumber[] = [
    { value: '01' },
    { value: '02' },
    { value: '03' },
    { value: '04' },
    { value: '05' }
  ]

  documentTypes: DocumentType[] = [
    { name: 'Ulazne fakture za robu i usluge iz zemlje', code: '01' },
    { name: 'Ulazna faktura za vlastitu potrošnju (Vanposlovne svrhe)', code: '02' },
    { name: 'Ulazna faktura - avansna (Dati avansi)', code: '03' },
    { name: 'JCI (Uvoz)', code: '04' },
    { name: 'Ostalo (Fakture za usluge primljene iz inostranstva itd)', code: '05' }
  ]

  constructor(private fb: FormBuilder,
    private data: HeaderService,
    private companyService: CompanyService,
    private inputInvoiceSevice: InputInvoiceService,
    private messageService: MessageService,
    private modalService: NgbModal,
    private router: Router,
    public datepipe: DatePipe) {
    this.data.currentMessage.subscribe(result => this.client = result);
    if (this.client == null || this.client === undefined || this.client.id === undefined) {
      this.router.navigateByUrl('/clients');
    }
  }

  ngOnInit() {

    this.newKuf = this.fb.group({
      selectedMonth: [''],
      selectedYear: [''],
      selectedOridinalNumber: [''],
      selectedDocumentType: [''],
      serialNumber: [''],
      invoiceNumber: [''],
      documentDate: [''],
      documentReceivedDate: [''],
      company: [''],
      invoiceAmountWithoutPdv: [''],
      invoiceAmountWithPdv: [''],
      flatFeeAmount: [''],
      inputPdvAmount: [''],
      inputPdvWhichCanBeRefused: [''],
      inputPdvWhichCannotBeRefused: [''],
      inputPdv32: [''],
      inputPdv33: [''],
      inputPdv34: ['']
    });

    this.data.currentMessage.subscribe(result => this.client = result);
    this.loadMonths();
    this.loadYears();
    this.loadOridinalNumbers();
    this.loadDocumentTypes();
    this.loadCompanies();

    this.validateDropdDown();
  }

  loadMonths() {
    return this.months;
  }

  loadYears() {
    return this.years;
  }

  loadOridinalNumbers() {
    return this.oridinalNumbers;
  }

  loadDocumentTypes() {
    return this.documentTypes;
  }

  loadCompanies() {
    this.companyService.getAllCompanies(this.client.id).subscribe(result => {
      this.companies = result;
    });
  }

  onOptionSelected(event) {
    this.validateDropdDown();
  }

  validateDropdDown() {
    if (this.selectedMonth == null || this.selectedMonth === undefined
      || this.selectedYear == null || this.selectedYear === undefined)
      this.disabled = true;
    else
      this.disabled = false;
  }

  reviewInputInvoices() {
    this.loadInputInvoices();
  }

  loadInputInvoices() {
    let taxPeriod = this.selectedYear.code + this.selectedMonth.code;
    this.inputInvoiceSevice.getAllInputInvoicesPerPeriodAndClientId(this.client.id, taxPeriod).subscribe(result => {
      this.inputInvoices = result;
    });
  }

  selectedDataMethod(selectedData: any) {
    this.inputInvoice = this.cloneOutputInvoice(selectedData);
    this.documentDate = new Date(this.inputInvoice.documentDate);
    this.companyService.getCompanyById(this.inputInvoice.companyId).subscribe(result => {
      this.selectedCompany = result;
    });

    this.displayDialog = true;
  }

  cloneOutputInvoice(outputInvoiceinput: InputInvoice): InputInvoice {
    let outputInvoice: InputInvoice = new InputInvoice();
    Object.entries(outputInvoiceinput).forEach(

      ([key, value]) => {
        outputInvoice[key] = value;
      }
    );

    return outputInvoice;
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' }).result.then((result) => {
    }, (reason) => {

    });
  }

  openModal(targetModal, user) {
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.newKuf.controls; }

  closeDialog() {
    this.submitted = false;
    this.newKuf.reset();
    this.modalService.dismissAll();
  }

  onSubmit() {
    this.inputInvoice = new InputInvoice();
    this.inputInvoice.clientId = this.client.id;
    this.inputInvoice.taxPeriod = this.selectedYear.code + this.selectedMonth.code;
    this.inputInvoice.serialNumber = this.newKuf.value["serialNumber"];
    this.inputInvoice.documentType = this.selectedDocumentType.code;
    this.inputInvoice.invoiceNumber = this.newKuf.value["invoiceNumber"];
    this.inputInvoice.documentDate = this.newKuf.value["documentDate"];
    this.inputInvoice.documentDate = this.datepipe.transform(this.inputInvoice.documentDate, 'yyyy-MM-dd');
    this.inputInvoice.documentReceivedDate = this.newKuf.value["documentReceivedDate"];
    this.inputInvoice.documentReceivedDate = this.datepipe.transform(this.inputInvoice.documentReceivedDate, 'yyyy-MM-dd');
    this.inputInvoice.companyId = this.selectedCompany.id;
    this.inputInvoice.invoiceAmountWithoutPdv = this.checkIfNumberUndefined(this.newKuf.value["invoiceAmountWithoutPdv"]);
    this.inputInvoice.invoiceAmountWithPdv = this.checkIfNumberUndefined(this.newKuf.value["invoiceAmountWithPdv"]);
    this.inputInvoice.flatFeeAmount = this.checkIfNumberUndefined(this.newKuf.value["flatFeeAmount"]);
    this.inputInvoice.inputPdvAmount = this.checkIfNumberUndefined(this.newKuf.value["inputPdvAmount"]);
    this.inputInvoice.inputPdvWhichCanBeRefused = this.checkIfNumberUndefined(this.newKuf.value["inputPdvWhichCanBeRefused"]);
    this.inputInvoice.inputPdvWhichCannotBeRefused = this.checkIfNumberUndefined(this.newKuf.value["inputPdvWhichCannotBeRefused"]);
    this.inputInvoice.inputPdv32 = this.checkIfNumberUndefined(this.newKuf.value["inputPdv32"]);
    this.inputInvoice.inputPdv33 = this.checkIfNumberUndefined(this.newKuf.value["inputPdv33"]);
    this.inputInvoice.inputPdv34 = this.checkIfNumberUndefined(this.newKuf.value["inputPdv34"]);

    this.inputInvoiceSevice.createInputInvoice(this.inputInvoice).subscribe(result => {
      this.messageService.add({ severity: 'success', summary: 'Ulazna faktura', detail: 'Uspijesno ste dodali novi red!' });
      this.clearFields();
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Ulazna faktura', detail: 'Doslo je do greske!' });
      });
  }

  clearFields() {
    this.newKuf.reset();
  }

  searchCompany(event) {
    let query = event.query;
    this.companyResults = this.filterCompany(query, this.companies);
  }

  filterCompany(query, companies: any[]): any[] {
    //in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    let filtered: any[] = [];
    for (let i = 0; i < companies.length; i++) {
      let company = companies[i];
      if (company.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(company);
      }
    }
    return filtered;
  }

  checkIfNumberUndefined(input: any) {
    if (input == null || input === undefined)
      return 0;

    return input;
  }

  onKeyTab(event) {
    if (event === undefined)
      return;

    event.target.value = parseFloat(event.target.value).toFixed(2);
  }

  delete() {
    this.inputInvoiceSevice.deleteInputInvoice(this.inputInvoice.id).subscribe(result => {
      this.loadInputInvoices();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno izbrisana ulazna faktura' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom brisanja ulazne fakture' });
      });
    this.displayDialog = false;
  }

  change() {
    if (this.selectedDocumentType !== undefined)
      this.inputInvoice.documentType = this.selectedDocumentType.code;
    this.inputInvoice.companyId = this.selectedCompany.id;
    this.inputInvoice.documentDate = this.datepipe.transform(this.documentDate, 'yyyy-MM-dd');
    this.inputInvoice.documentReceivedDate = this.datepipe.transform(this.documentReceivedDate, 'yyyy-MM-dd');
    this.inputInvoiceSevice.updateInputInvoice(this.inputInvoice).subscribe(result => {
      this.loadInputInvoices();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno je izmijenjena ulazna faktura' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom izmijene ulazne fakture' });
      });

    this.displayDialog = false;
  }

  exportInputInvoice() {
    let taxPeriod = this.selectedYear.code + this.selectedMonth.code;
    this.inputInvoiceSevice.generateCsv(this.client.id, taxPeriod).subscribe(result => {
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno je kreiran csv fajl' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom kreiranja csv fajla' });
      });
  }
}
