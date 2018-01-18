import { Component, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { DialogComponent, DialogService } from "ng2-bootstrap-modal";

export interface ConfirmModel {
  title:string;
  message:string;
}

@Component({
  selector: 'confirm',
  template: `<div class="modal-dialog">
                <div class="modal-content">
                   <div class="modal-header">
                      <h4 class="modal-title">{{title || 'Confirm'}}</h4>
                      <button type="button" class="close" (click)="close()" >&times;</button>
                   </div>
                   <div class="modal-body">
                     <p>{{message || 'Are you sure?'}}</p>
                   </div>
                   <div class="modal-footer">
                     <button type="button" class="btn btn-primary" (click)="confirm()" #setfocus>OK</button>
                     <button type="button" class="btn btn-default" (click)="cancel()">Cancel</button>
                   </div>
                 </div>
                </div>`
})
export class ConfirmComponent extends DialogComponent<ConfirmModel, boolean> implements ConfirmModel {
  title: string;
  message: string;
  constructor(dialogService: DialogService) {
    super(dialogService);
  }

  @ViewChild('setfocus') private elementref: ElementRef
  public ngAfterViewInit(): void {
    this.elementref.nativeElement.focus();
  }

  confirm() {
    // on click on confirm button we set dialog result as true,
    // ten we can get dialog result from caller code
    this.result = true;
    this.close();
  }
  cancel() {
    this.result = false;
    this.close();
  }
}
