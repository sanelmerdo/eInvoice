import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { TableColumn } from '../shared/components/table/tableColumn';
import { CompanyService } from '../services/company/company.service';
import { Company } from '../models/company';
import { MessageService } from 'primeng/components/common/messageservice';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder, NgForm } from '@angular/forms';
import { HeaderService } from '../services/shared/header.service';
import { Client } from '../models/client';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css'],
  providers: [MessageService]
})
export class CompanyComponent implements OnInit {
  client: Client;
  submitted = false;
  parameter: string;
  companies: Company[] = [];
  company: Company = new Company();
  editProfileForm: FormGroup;
  displayDialog: boolean;
  @ViewChild('companyForm', null) companyForm: NgForm;


  companyColumns: TableColumn[] = [
    { key: "id", title: "Id", field: "id", header: "Id", colWidth: 100, linkable: false },
    { key: "name", title: "Ime komitenta", field: "name", header: "Ime firme", colWidth: 200, linkable: false },
    { key: "pdvNumber", title: "PDV Broj", field: "pdvNumber", header: "PDV broj", colWidth: 200, linkable: false },
    { key: "idNumber", title: "ID broj", field: "idNumber", header: "ID broj", colWidth: 200, linkable: false },
    { key: "address", title: "Adresa", field: "address", header: "Adresa", colWidth: 200, linkable: false }
  ];

  constructor(private companyService: CompanyService,
              private messageService: MessageService,
              private fb: FormBuilder,
              private modalService: NgbModal,
              private data: HeaderService,
              private router: Router) {
    this.data.currentMessage.subscribe(result => this.client = result);
    if (this.client == null || this.client === undefined || this.client.id === undefined) {
      this.router.navigateByUrl('/clients');
    }
  }
  
  ngOnInit() {
    this.loadCompanies();
    this.editProfileForm = this.fb.group({
      companyName: ['', Validators.required],
      pdvNumber: ['', Validators.required],
      address: ['']
    });
  }

  selectedDataMethod(selectedData: any) {
    this.company = this.cloneCompany(selectedData);
    this.displayDialog = true;
  }

  selectedValues(selectedValue: any) {
    this.data.changeMessage(selectedValue['name']);
  }

  cloneCompany(companyInput: Company): Company {
    let company: Company = new Company();
    Object.entries(companyInput).forEach(

      ([key, value]) => {
        company[key] = value;
      }
    );

    return company;
  }

  loadCompanies() {
    this.companyService.getAllCompanies(this.client.id).subscribe(result => {
      this.companies = result;
    });
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

  onSubmit() {
    this.submitted = true;

    if (this.editProfileForm.invalid) {
      return;
    }

    this.company.name = this.editProfileForm.value["companyName"];
    this.company.pdvNumber = this.editProfileForm.value["pdvNumber"];
    this.company.idNumber = "4" + this.company.pdvNumber;
    this.company.clientId = this.client.id;

    this.companyService.createCompany(this.company).subscribe(result => {
      this.messageService.add({ severity: 'success', summary: 'Kompanija', detail: 'Uspijesno ste dodali firmu!' });
      this.loadCompanies();
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Kompanija', detail: 'Doslo je do greske!' });
        this.loadCompanies();
      });

    this.closeDialog();
  }

  // convenience getter for easy access to form fields
  get f() { return this.editProfileForm.controls; }

  closeDialog() {
    this.submitted = false;
    this.editProfileForm.reset();
    this.modalService.dismissAll();
  }

  delete() {
    this.companyService.deleteCompany(this.company.id).subscribe(result => {
      this.loadCompanies();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno izbrisana kompanija' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom brisanja kompanije' });
      });
    this.displayDialog = false;
  }

  change() {
    this.companyService.updateCompany(this.company).subscribe(result => {
      this.loadCompanies();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno je izmijenjena kompanija' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom izmijene kompanije' });
      });

    this.displayDialog = false;
  }

  onChangeSubmit() {
    if (this.companyForm.invalid)
      return;

    this.company.idNumber = "4" + this.company.pdvNumber;

    this.companyService.updateCompany(this.company).subscribe(result => {
      this.loadCompanies();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno je izmijenjena kompanija' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom izmijene kompanije' });
      });

    this.displayDialog = false;
  }
}
