import { Client } from "./client";

export class Company {
  id: number;
  name: string;
  pdvNumber: string;
  idNumber: string;
  clientId: number;
  address: string;

  client: Client;
}
