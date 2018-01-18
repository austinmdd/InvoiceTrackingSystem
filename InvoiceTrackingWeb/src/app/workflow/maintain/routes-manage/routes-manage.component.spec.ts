import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutesManageComponent } from './routes-manage.component';

describe('RoutesManageComponent', () => {
  let component: RoutesManageComponent;
  let fixture: ComponentFixture<RoutesManageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoutesManageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoutesManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
