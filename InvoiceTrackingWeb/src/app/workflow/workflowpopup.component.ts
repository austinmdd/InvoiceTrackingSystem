import { Component, OnInit, ViewChild, AfterViewInit, ElementRef} from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { ElementAst } from '@angular/compiler';
import { FormGroup, FormControl, Validators } from '@angular/forms'


export interface WorkFlowModel {
  title: string;
  message: string;
  bold?: string;
}


@Component({
  selector: 'arlet',
  template: `<div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                  <h4 class="modal-title">{{ title || 'Alert' }} Invoice Request</h4>
                    <button type="button" class="close" (click)="close()" >&times;</button>
                  </div>
                  <form [formGroup]="form" (ngSubmit)="onSubmit(form.value)" >
                    <div class="modal-body">
                        <div class="form-group">
                          <p id="msg">New Assignee Name</p>
                          <!--<textarea name="Note" formControlName="Note" class="form-control" rows="6"></textarea>-->
                          
                          <!--<select class="form-control input-sm" style="height:30px;font-size:14px;padding-top:1px;">
                            <option>David</option>
                            <option>Someone</option>
                          </select>-->

                          <div class="checkbox"><label><input type="checkbox" value="">Requirement 1</label></div>
                          <div class="checkbox"><label><input type="checkbox" value="">Requirement 2</label></div>
                          <div class="checkbox"><label><input type="checkbox" value="">Requirement 3</label></div>
                          <div class="checkbox"><label><input type="checkbox" value="">Requirement 4</label></div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12">      
                          <div class="row">

                            <div class="col-md-6">    
                            </div>
                            <div class="col-md-6">
                              <div class="row">
                                <div class="col-sm-6 text-right"><button type="button" class="btn btn-primary btn-sm" style="width:80px;" (click)="close()" #setfocus>Cancel</button></div>
                                <div class="col-sm-6 text-left"><button type="button" class="btn btn-success btn-sm" style="width:80px;">Save</button></div>
                              </div>
                            </div>
                          </div>
                        </div>      
                    </div>
                  </form>
                </div>
              </div>`
})
export class WorkflowpopupComponent extends DialogComponent<WorkFlowModel, null> implements WorkFlowModel {
  title: string;
  message: string;
  bold?: string;

  form;

  constructor(dialogService: DialogService) { 
    super(dialogService);
  }

  ngOnInit() {
    this.form = new FormGroup({
      Assignee: new FormControl("",Validators.required)
    });
  }

}
