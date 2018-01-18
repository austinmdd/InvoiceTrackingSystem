import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomecompliancechecklistComponent } from './homecompliancechecklist.component';

describe('HomecompliancechecklistComponent', () => {
  let component: HomecompliancechecklistComponent;
  let fixture: ComponentFixture<HomecompliancechecklistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomecompliancechecklistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomecompliancechecklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
