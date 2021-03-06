import { Component, OnInit, ViewChild } from '@angular/core';
import { TableColumn } from '../shared/components/table/tableColumn';
import { Client } from '../models/client';
import { ClientService } from '../services/client/client.service';
import { MessageService } from 'primeng/components/common/messageservice';
import { HeaderService } from '../services/shared/header.service';
import { FormGroup, Validators, FormBuilder, NgForm } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [MessageService]
})
export class HomeComponent implements OnInit {

  submitted = false;
  changeSubmitted = false;
  clients: Client[] = [];
  client: Client = new Client();
  displayDialog: boolean;
  clientName: string;
  addNewCompany: FormGroup;
  @ViewChild('clientForm', null) clientForm: NgForm;

  clientColumns: TableColumn[] = [
    { key: "id", title: "Id", field: "id", header: "Id", colWidth: 100, linkable: true },
    { key: "name", title: "Ime komitenta", field: "name", header: "Ime komiteta", colWidth: 200, linkable: false },
    { key: "pdvNumber", title: "PDV Broj", field: "pdvNumber", header: "PDV broj", colWidth: 200, linkable: false },
    { key: "idNumber", title: "ID broj", field: "idNumber", header: "ID broj", colWidth: 200, linkable: false }
  ];

  constructor(private clientService: ClientService,
    private messageService: MessageService,
    private data: HeaderService,
    private fb: FormBuilder,
    private modalService: NgbModal) {
  }

  ngOnInit(): void {
    this.loadClients();
    
    this.addNewCompany = this.fb.group({
      companyName: ['', Validators.required],
      //idNumber: ['', Validators.required],
      pdvNumber: ['', Validators.required]
    });
  }

  loadClients() {
    this.clientService.getAllClients().subscribe(results => {
      this.clients = results
    });
  }

  selectedDataMethod(selectedData: any) {
    this.client = this.cloneClient(selectedData);
    this.displayDialog = true;
  }

  selectedValues(selectedValue: any) {
    let client = new Client();
    client.name = selectedValue['name'];
    client.id = selectedValue['id'];
    this.data.changeMessage(client);
  }

  cloneClient(clientInput: Client): Client {
    let client: Client = new Client();
    Object.entries(clientInput).forEach(
      
      ([key, value]) => {
        client[key] = value;
      }
    );

    return client;
  }

  delete() {
    this.clientService.deleteClient(this.client.pdvNumber).subscribe(result => {
      this.loadClients();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno izbrisan klijent' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom brisanja klijenta' });
      });
    this.displayDialog = false;
  }

  onChangeSubmit() {
    if (this.clientForm.invalid)
      return;

    this.client.idNumber = "4" + this.client.pdvNumber;

    this.clientService.updateClient(this.client).subscribe(result => {
      this.loadClients();
      this.messageService.add({ severity: 'success', summary: 'Uspijesno', detail: 'Uspijesno je izmijenjen klijent' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Greska', detail: 'Doslo je do greske prilikom izmijene klijenta' });
      });

    this.displayDialog = false;
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

    if (this.addNewCompany.invalid) {
      return;
    }

    this.client.name = this.addNewCompany.value["companyName"];
    this.client.pdvNumber = this.addNewCompany.value["pdvNumber"];
    this.client.idNumber = "4" + this.client.pdvNumber;

    this.clientService.createClientEntity(this.client).subscribe(result => {
      this.messageService.add({ severity: 'success', summary: 'Komitent', detail: 'Uspijesno ste dodali komintenta!' });
      this.loadClients();
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Komitent', detail: 'Doslo je do greske!' });
        this.loadClients();
      });

    this.closeDialog();
  }

  // convenience getter for easy access to form fields
  get f() { return this.addNewCompany.controls; }

  closeDialog() {
    this.submitted = false;
    this.addNewCompany.reset();
    this.modalService.dismissAll();
  }

  onKeyTab(event) {
    if (event === undefined)
      return;

    this.addNewCompany.value["idNumber"] = "4" + event.target.value;
  }
}
