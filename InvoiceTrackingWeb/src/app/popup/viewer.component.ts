import { Component, OnInit, ViewChild, AfterViewInit, ElementRef} from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { ElementAst } from '@angular/compiler';

import { DocumentService } from "../services/document.service";


export interface ViewerModel {
  title: string;
  NodeID: string;
}



@Component({
  selector: 'viewer',
  template: `<div class="modal-dialog modal-lg" style="width:70%">
                <div class="modal-content">
                   <div class="modal-header">
                      <h4 class="modal-title">{{title}}</h4>
                      <button type="button" class="close" (click)="close()" >&times;</button>
                   </div>
                   <div class="modal-body">
                   <iframe [src]= 'NodeID' style="width:100%; height:350px;" frameborder="0">
                   <h2>The requested document cannot be viewed and has be downloaded. Please check you downloads</h2>
                   </iframe>
                   </div>
                   <div class="modal-footer">
                     <button type="button" class="btn btn-primary" (click)="destroy(title)" #setfocus>Close</button>
                   </div>
                </div>
             </div>`,
             providers: [ DocumentService]
})
export class ViewerComponent extends DialogComponent<ViewerModel, null> implements ViewerModel {
  title: string;
  NodeID: string;
  constructor(dialogService: DialogService, private docService: DocumentService) {
    super(dialogService);
  }

  public Doc = { Name : String };

  ngOnInit(){

  }

  destroy(documentname){

    this.Doc = {Name : documentname};

    this.docService.DeleteFromTemp(this.Doc)
    .subscribe(
        result => result), 
    error => console.log("Error cleaning teamp documents."),
    () => console.info("OK");

    this.close();
  }
}
