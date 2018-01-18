import { TestBed, inject } from '@angular/core/testing';

import { SupplierInvoiceService } from './supplier-invoice.service';

describe('SupplierInvoiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SupplierInvoiceService]
    });
  });

  it('should ...', inject([SupplierInvoiceService], (service: SupplierInvoiceService) => {
    expect(service).toBeTruthy();
  }));
});
