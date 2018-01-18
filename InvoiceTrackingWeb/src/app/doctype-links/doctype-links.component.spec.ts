import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctypeLinksComponent } from './doctype-links.component';

describe('DoctypeLinksComponent', () => {
  let component: DoctypeLinksComponent;
  let fixture: ComponentFixture<DoctypeLinksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctypeLinksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctypeLinksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
