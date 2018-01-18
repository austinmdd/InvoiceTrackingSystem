import { TestBed, inject } from '@angular/core/testing';

import { AddNotesService } from './add-notes.service';

describe('AddNotesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AddNotesService]
    });
  });

  it('should ...', inject([AddNotesService], (service: AddNotesService) => {
    expect(service).toBeTruthy();
  }));
});
