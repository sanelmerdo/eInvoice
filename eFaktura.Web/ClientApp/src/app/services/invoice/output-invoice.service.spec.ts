import { TestBed } from '@angular/core/testing';

import { OutputInvoiceService } from './output-invoice.service';

describe('OutputInvoiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OutputInvoiceService = TestBed.get(OutputInvoiceService);
    expect(service).toBeTruthy();
  });
});
