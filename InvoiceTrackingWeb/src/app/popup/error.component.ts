import { Component, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';

export interface ErrorModel {
  title: string;
  message: string;
  statuscode: string;
  url: string;
  servermessage: string;
}

@Component({
  selector: 'error',
  template: `<div class="modal-dialog">
                <div class="modal-content" >
                   <div class="modal-header">
                   <h4 class="modal-title">{{title || 'Alert!'}}</h4>
                     <button type="button" class="close" (click)="close()" >&times;</button>
                   </div>
                   <div class="modal-body">
                     <p>{{message || 'TADAA-AM!'}}</p>
                     <button class="btn btn-sm btn-primary" data-toggle="collapse" data-target="#more" (click)="onClick()">{{btnTitle}}</button>
                        <div id="more" class="collapse">
                        <hr>
                        <h6><strong>Error Code : </strong>{{statuscode || 'Server Error'}}</h6>
                        <h6><strong>Url : </strong>{{url || 'http://its.co.za'}}</h6>
                        <p><strong>Error Message : </strong><br/>{{servermessage || 'Error'}}</p>
                        </div>
                    </div>
                   <div class="modal-footer">
                     <button type="button" class="btn btn-md btn-primary" style="width:25%;" (click)="close()" #setfocus>OK</button>
                   </div>
                </div>
             </div>`,
    styleUrls: ['./style.css']
})
export class ErrorComponent extends DialogComponent<ErrorModel, null> implements ErrorModel {
  title: string;
  message: string;
  statuscode: string;
  url: string;
  servermessage: string;
  btnTitle: string = 'Read More';
  constructor(dialogService: DialogService) {
    super(dialogService);
  }

  @ViewChild('setfocus') private elementref: ElementRef
  public ngAfterViewInit(): void {
    this.elementref.nativeElement.focus();
  }

  onClick() {
      this.btnTitle = (this.btnTitle === 'Read More') ? 'Read Less': 'Read More';
  }
}
