import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkflowprocessComponent } from './workflowprocess.component';

describe('WorkflowprocessComponent', () => {
  let component: WorkflowprocessComponent;
  let fixture: ComponentFixture<WorkflowprocessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkflowprocessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkflowprocessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
