import { Component, OnInit } from '@angular/core';
import { TableColumn } from '../shared/components/table/tableColumn';
import { Client } from '../models/client';
import { ClientService } from '../services/client/client.service';
import { MessageService } from 'primeng/components/common/messageservice';
import { HeaderService } from '../services/shared/header.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [MessageService]
})
export class HomeComponent implements OnInit {

  submitted = false;
  clients: Client[] = [];
  client: Client = new Client();
  displayDialog: boolean;
  clientName: string;
  editProfileForm: FormGroup;

  cols: TableColumn[] = [
    { key: "id", title: "Id", field: "id", colWidth: "50", linkable: false },
    { key: "name", title: "Ime komitenta", field: "name", colWidth: "150", linkable: true },
    { key: "pdvNumber", title: "PDV Broj", field: "pdvNumber", colWidth: "50", linkable: false }
  ];

  clientColumns: TableColumn[] = [
    { key: "id", title: "Id", field: "id", header: "Id", colWidth: "100", linkable: false },
    { key: "name", title: "Ime komitenta", field: "name", header: "Ime komiteta", colWidth: "200", linkable: true },
    { key: "pdvNumber", title: "PDV Broj", field: "pdvNumber", header: "PDV broj", colWidth: "200", linkable: false }
  ];

  constructor(private clientService: ClientService,
    private messageService: MessageService,
    private data: HeaderService,
    private fb: FormBuilder,
    private modalService: NgbModal) {
  }

  ngOnInit(): void {
    this.loadClients();
    this.editProfileForm = this.fb.group({
      companyName: ['', Validators.required],
      idNumber: ['', Validators.required]
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
    this.data.changeMessage(selectedValue['name']);
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

  change() {
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

    if (this.editProfileForm.invalid) {
      return;
    }

    this.client.name = this.editProfileForm.value["companyName"];
    this.client.pdvNumber = this.editProfileForm.value["idNumber"];

    this.clientService.createClientEntity(this.client).subscribe(result => {
      this.messageService.add({ severity: 'success', summary: 'Komitent', detail: 'Uspijesno ste dodali komintenta!' });
    },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Komitent', detail: 'Doslo je do greske!' })
      });

    this.closeDialog();

    this.loadClients();
  }

  // convenience getter for easy access to form fields
  get f() { return this.editProfileForm.controls; }

  closeDialog() {
    this.submitted = false;
    this.editProfileForm.reset();
    this.modalService.dismissAll();
  }
}
