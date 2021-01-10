import { TestBed } from '@angular/core/testing';

import { InputInvoiceService } from './input-invoice.service';

describe('InputInvoiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InputInvoiceService = TestBed.get(InputInvoiceService);
    expect(service).toBeTruthy();
  });
});
