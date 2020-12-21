import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';
import { HeaderService } from '../services/shared/header.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  providers: [MessageService]
})
export class HeaderComponent implements OnInit {

  message: string;

  constructor(private data: HeaderService) { }

  ngOnInit() {
    this.data.currentMessage.subscribe(message => this.message = message);
  }
}
