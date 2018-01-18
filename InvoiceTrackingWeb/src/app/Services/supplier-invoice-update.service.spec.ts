import { TestBed, inject } from '@angular/core/testing';

import { SupplierInvoiceUpdateService } from './supplier-invoice-update.service';

describe('SupplierInvoiceUpdateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SupplierInvoiceUpdateService]
    });
  });

  it('should ...', inject([SupplierInvoiceUpdateService], (service: SupplierInvoiceUpdateService) => {
    expect(service).toBeTruthy();
  }));
});
