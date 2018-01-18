import { Component, OnInit, ViewChild, AfterViewInit, ElementRef} from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { ElementAst } from '@angular/compiler';
//import { ElementRef } from '@angular/core/src/linker/element_ref';


export interface AlertModel {
  title: string;
  message: string;
  bold?: string;
}

@Component({
  selector: 'alert',
  template: `<div class="modal-dialog">
                <div class="modal-content">
                   <div class="modal-header">
                   <h4 class="modal-title">{{title || 'Alert!'}}</h4>
                     <button type="button" class="close" (click)="close()" >&times;</button>
                   </div>
                   <div class="modal-body">
                     <p id="msg">{{message || 'Something went wrong'}}</p>
                   </div>
                   <div class="modal-footer">
                     <button type="button" class="btn btn-primary" (click)="close()" #setfocus>OK</button>
                   </div>
                </div>
             </div>`
})
export class AlertComponent extends DialogComponent<AlertModel, null> implements AlertModel {
  title: string;
  message: string;
  bold?: string;
  constructor(dialogService: DialogService) {
    super(dialogService);
  }

  ngOnInit(){
    if(this.bold)
    document.getElementById("msg").innerHTML = this.makeBold(this.message, this.bold);
  }

  @ViewChild('setfocus') private elementref: ElementRef
  public ngAfterViewInit(): void {
    this.elementref.nativeElement.focus();
  }

  makeBold(message, bold?) {
    return message.replace(new RegExp('('+bold+')', 'gi'),'<b>$1</b>')
  }

  private inputFocused = false;
  moveFocus() {
      this.inputFocused = true;
      setTimeout(() => {this.inputFocused = false});
  }
}
