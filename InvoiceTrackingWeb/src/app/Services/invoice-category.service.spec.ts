import { TestBed, inject } from '@angular/core/testing';

import { InvoiceCategoryService } from './invoice-category.service';

describe('InvoiceCategoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InvoiceCategoryService]
    });
  });

  it('should ...', inject([InvoiceCategoryService], (service: InvoiceCategoryService) => {
    expect(service).toBeTruthy();
  }));
});
