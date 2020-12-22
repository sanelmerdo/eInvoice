import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Client } from '../../models/client';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  private messageSource = new BehaviorSubject(new Client());
  currentMessage = this.messageSource.asObservable();

  constructor() { }

  changeMessage(client: Client) {
    this.messageSource.next(client)
  }
}
