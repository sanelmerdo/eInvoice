import { Component, OnInit } from '@angular/core';
import { Month } from '../models/month';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Year } from '../models/year';
import { OridinalNumber } from '../models/oridinalNumber';
import { DocumentType } from '../models/documentType';
import { HeaderService } from '../services/shared/header.service';
import { Client } from '../models/client';
import { CompanyService } from '../services/company/company.service';
import { Company } from '../models/company';
import { MessageService } from 'primeng/components/common/messageservice';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TableColumn } from '../shared/components/table/tableColumn';
import { Router } from '@angular/router';
import { OutputInvoice } from '../models/outputInvoice';
import { DatePipe } from '@angular/common';
import { OutputInvoiceService } from '../services/invoice/output-invoice.service';

@Component({
  selector: 'app-kif',
  templateUrl: './kif.component.html',
  styleUrls: ['./kif.component.scss'],
  providers: [MessageService, DatePipe]
})
export class KifComponent implements OnInit {

  companyResults: string[] = [];
  disabled: boolean = true;
  submitted = false;
  client: Client;
  selectedMonth: Month;
  selectedYear: Year;
  selectedDocumentType: DocumentType;
  newKif: FormGroup;
  documentDate: Date;
  companies: Company[] = [];
  selectedCompany: Company;
  invoiceAmount: number;
  outputInvoice: OutputInvoice;
  outputInvoices: OutputInvoice[] = [];

  invoiceColumns: TableColumn[] = [
    { key: "id", title: "Id", field: "id", header: "Id", linkable: false, colWidth: 70 },
    { key: "taxPeriod", title: "Porezni period", field: "taxPeriod", header: "Porezni period", colWidth: 70 },
    { key: "serialNumber", title: "Redni broj", field: "serialNumber", header: "Redni broj", colWidth: 70 },
    { key: "documentType", title: "Tip dok", field: "documentType", header: "Tip dok", colWidth: 50 },
    { key: "invoiceNumber", title: "Broj fakture", field: "invoiceNumber", header: "Broj fakture", colWidth: 150 },
    { key: "documentDate", title: "Datum naloga", field: "documentDate", header: "Datum naloga", colWidth: 100 },
    { key: "invoiceAmount", title: "Vrijednost fakture", field: "invoiceAmount", header: "Vrijednost fakture", colWidth: 100 },
    { key: "internalInvoiceAmount", title: "Iznos Int.F", field: "internalInvoiceAmount", header: "Iznos Int.F", colWidth: 100 },
    { key: "exportDeliveryAmount", title: "Iznos Izv.F.", field: "invoiceNumber", header: "Iznos Izv.F.", colWidth: 100 },
    { key: "invoiceAmountForOtherDelivery", title: "IFzDI", field: "invoiceAmountForOtherDelivery", header: "IFzDI", colWidth: 100 },
    { key: "basisAmountForCalculation", title: "OzO Pdv-a", field: "basisAmountForCalculation", header: "OzO Pdv-a", colWidth: 100 },
    { key: "outputPDV", title: "Izl Pdv", field: "outputPDV", header: "Izl Pdv", colWidth: 100 },
    { key: "basicforCalulcationToNonRegisteredUser", title: "OzO Pdv-a nereg.koris.", field: "basicforCalulcationToNonRegisteredUser", header: "OzO Pdv-a nereg.koris.", colWidth: 100 },
    { key: "outputPDVToNonRegisteredUser", title: "IzlPdv nereg.koris", field: "outputPDVToNonRegisteredUser", header: "IzlPdv nereg.koris", colWidth: 100 },
    { key: "outputPDV32", title: "Pdv 32", field: "outputPDV32", header: "Pdv 32", colWidth: 100 },
    { key: "outputPDV33", title: "Pdv 33", field: "outputPDV33", header: "Pdv 33", colWidth: 100 },
    { key: "outputPDV34", title: "Pdv 34", field: "outputPDV34", header: "Pdv 34", colWidth: 100 }
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
    { name: '2020', code: '20' },
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
    { name: 'Izlazna faktura za robu i usluge iz zemlje', code: '01' },
    { name: 'Izlazna faktura za vlastitu potrošnju (Vanposlovne svrhe)', code: '02' },
    { name: 'Izlazna faktura - avansna (Primljeni avansi)', code: '03' },
    { name: 'JCI (Izvoz)', code: '04' },
    { name: 'Ostalo (Fakture za usluge izvršene stranom licu itd)', code: '05' }
  ]
  constructor(private fb: FormBuilder,
              private data: HeaderService,
              private companyService: CompanyService,
              private outputInvoiceSevice: OutputInvoiceService,
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
    
    this.newKif = this.fb.group({
      selectedMonth: [''],
      selectedYear: [''],
      selectedOridinalNumber: [''],
      selectedDocumentType: [''],
      serialNumber: [''],
      invoiceNumber: [''],
      documentDate: [''],
      company: [''],
      invoiceAmount: [''],
      internalInvoiceAmount: [''],
      exportDeliveryAmount: [''],
      invoiceAmountForOtherDelivery: [''],
      basisAmountForCalculation: [''],
      outputPDV: [''],
      basicforCalulcationToNonRegisteredUser: [''],
      outputPDVToNonRegisteredUser: [''],
      outputPDV32: ['0.00'],
      outputPDV33: [''],
      outputPDV34: ['']
    });

    this.data.currentMessage.subscribe(result => this.client = result);
    this.loadMonths();
    this.loadYears();
    this.loadOridinalNumbers();
    this.loadDocumentTypes();
    this.loadCompanies();

    this.validateDropdDown();
  }

  validateDropdDown() {
    if (this.selectedMonth == null || this.selectedMonth === undefined
        || this.selectedYear == null || this.selectedYear === undefined)
      this.disabled = true;
    else
      this.disabled = false;
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

  loadOutputInvoices() {
    let taxPeriod = this.selectedYear.code + this.selectedMonth.code;
    this.outputInvoiceSevice.getAllOutputInvoicesPerPeriodAndClientId(this.client.id, taxPeriod).subscribe(result => {
      this.outputInvoices = result;
    });
  }

  reviewOutputInvoices() {
    this.loadOutputInvoices();
  }

  onSubmit() {
    this.outputInvoice = new OutputInvoice();
    this.outputInvoice.clientId = this.client.id;
    this.outputInvoice.taxPeriod = this.selectedYear.code + this.selectedMonth.code;
    this.outputInvoice.serialNumber = this.newKif.value["serialNumber"];
    this.outputInvoice.documentType = this.selectedDocumentType.code;
    this.outputInvoice.invoiceNumber = this.newKif.value["invoiceNumber"];
    this.outputInvoice.documentDate = this.newKif.value["documentDate"];
    this.outputInvoice.documentDate = this.datepipe.transform(this.outputInvoice.documentDate, 'yyyy-MM-dd');
    this.outputInvoice.companyId = this.selectedCompany.id;
    this.outputInvoice.invoiceAmount = this.checkIfNumberUndefined(this.newKif.value["invoiceAmount"]);
    this.outputInvoice.internalInvoiceAmount = this.checkIfNumberUndefined(this.newKif.value["internalInvoiceAmount"]);
    this.outputInvoice.exportDeliveryAmount = this.checkIfNumberUndefined(this.newKif.value["exportDeliveryAmount"]);
    this.outputInvoice.invoiceAmountForOtherDelivery = this.checkIfNumberUndefined(this.newKif.value["invoiceAmountForOtherDelivery"]);
    this.outputInvoice.basisAmountForCalculation = this.checkIfNumberUndefined(this.newKif.value["basisAmountForCalculation"]);
    this.outputInvoice.outputPDV = this.checkIfNumberUndefined(this.newKif.value["outputPDV"]);
    this.outputInvoice.basicforCalulcationToNonRegisteredUser = this.checkIfNumberUndefined(this.newKif.value["basicforCalulcationToNonRegisteredUser"]);
    this.outputInvoice.outputPDVToNonRegisteredUser = this.checkIfNumberUndefined(this.newKif.value["outputPDVToNonRegisteredUser"]);
    this.outputInvoice.outputPDV32 = this.checkIfNumberUndefined(this.newKif.value["outputPDV32"]);
    this.outputInvoice.outputPDV33 = this.checkIfNumberUndefined(this.newKif.value["outputPDV33"]);
    this.outputInvoice.outputPDV34 = this.checkIfNumberUndefined(this.newKif.value["outputPDV34"]);

    this.outputInvoiceSevice.createOutputInvoice(this.outputInvoice).subscribe(result => {
      this.messageService.add({ severity: 'success', summary: 'Izlazna faktura', detail: 'Uspijesno ste novi red!' });
      this.clearFields();
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Izlazna faktura', detail: 'Doslo je do greske!' });
      });
  }

  onSubmitNewKif() {

    if (this.newKif.invalid) {
      return;
    }

    console.log(this.selectedMonth);
  }

  closeDialog() {
    this.submitted = false;
    this.newKif.reset();
    this.modalService.dismissAll();
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
  get f() { return this.newKif.controls; }

  onKeyTab(event) {
    if (event === undefined)
      return;
    
    event.target.value = parseFloat(event.target.value).toFixed(2);
  }

  onOptionSelected(event) {
    this.validateDropdDown();
  }

  checkIfNumberUndefined(input: any) {
    if (input == null || input === undefined)
      return 0;

    return input;
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

  clearFields() {
    this.newKif.reset();
  }
}
