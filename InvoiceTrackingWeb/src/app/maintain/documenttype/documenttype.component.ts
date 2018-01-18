import { Component, OnInit } from '@angular/core';
import { DocumenttypeService } from "./../../services/documenttype.service";

//Popup Components
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ErrorComponent } from '../../popup/error.component';

@Component({
  selector: 'app-documenttype',
  templateUrl: './documenttype.component.html',
  styleUrls: ['./documenttype.component.css']
})
export class DocumenttypeComponent implements OnInit {
     doctypes: any = [];
     isRemove: boolean = false;
     removeRef: any;
     page: number = 1;
     pageSize = parseInt(localStorage.getItem("pageSize"));
     lblHeading: string = "";
     criteria = "DocumentType";
     direction: boolean = false;

    constructor(private service: DocumenttypeService, private dialogService: DialogService) { }

    ngOnInit() {
        this.onChangeHeandig();
        this.reload();
  }

  reload() {
      this.service.getAllFixed(this.page, this.pageSize).subscribe(result => {
          this.doctypes = result;        
      });
    }


  onDelete(id: any) {     
      this.service.delete(id).subscribe(result => {
          this.onChangeHeandig();
          this.dialogService.addDialog(AlertComponent, { title: 'Delete document type', message: `Document type ${this.removeRef.DocumentType} deleted.`, bold:`${this.removeRef.DocumentType}` });                        
          if (result != null) {
              this.reload();
              this.onNo();
          }
      }, error => {
          error = error.json(); 
          this.dialogService.addDialog(ErrorComponent, { title: 'Delete document type', message: `Document type ${this.removeRef.DocumentType} was not deleted.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message });          
         });
  }

  onSort() {
      this.direction = !this.direction;
  }
  
  onConfirm(doc: any) {
      this.removeRef = doc;     
      this.isRemove = true;
      this.onChangeHeandig(false);
  }

  onNo() {
      this.isRemove = false;
      this.onChangeHeandig();
  }

  onChangeHeandig(change: boolean = true) {
      this.lblHeading = (change) ? "Document Types" : "Delete Document Type";
  }


}
