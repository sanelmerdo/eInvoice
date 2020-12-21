import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Client } from '../../models/client';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private http: HttpService) { }

  /** Create client entity */
  public createClientEntity(client: Client): Observable<any> {
    return this.http.postRequest<any>("client/create", client);
  }

  /** get all clients as an observable array */
  public getAllClients() {
    return this.http.getRequest<Client[]>("client/all");
  }

  /** delete client */
  public deleteClient(pdvNumber: string): Observable<any> {
    return this.http.deleteRequest("client/delete/" + pdvNumber);
  }

  public updateClient(client: Client): Observable<any> {
    return this.http.putRequest("client/update", client);
  }
}
