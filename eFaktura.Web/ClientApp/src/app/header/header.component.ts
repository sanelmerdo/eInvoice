import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';
import { HeaderService } from '../services/shared/header.service';
import { Client } from '../models/client';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  providers: [MessageService]
})
export class HeaderComponent implements OnInit {

  client: Client;

  constructor(private data: HeaderService) { }

  ngOnInit() {
    this.data.currentMessage.subscribe(result => this.client = result);
  }
}
