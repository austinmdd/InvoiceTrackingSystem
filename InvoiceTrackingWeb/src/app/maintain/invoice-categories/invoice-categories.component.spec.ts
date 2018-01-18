import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceCategoriesComponent } from './invoice-categories.component';

describe('InvoiceCategoriesComponent', () => {
  let component: InvoiceCategoriesComponent;
  let fixture: ComponentFixture<InvoiceCategoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InvoiceCategoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvoiceCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
