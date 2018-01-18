import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusmanageComponent } from './statusmanage.component';

describe('StatusmanageComponent', () => {
  let component: StatusmanageComponent;
  let fixture: ComponentFixture<StatusmanageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatusmanageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusmanageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
