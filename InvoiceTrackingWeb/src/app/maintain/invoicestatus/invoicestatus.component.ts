import { Component, OnInit } from '@angular/core';
import { StatusService } from "./../../services/status.service";

//Popup Components
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ErrorComponent } from '../../popup/error.component';

@Component({
  selector: 'app-invoicestatus',
  templateUrl: './invoicestatus.component.html',
  styleUrls: ['./invoicestatus.component.css']
})
export class InvoicestatusComponent implements OnInit {

    statuses: any = [];
    isRemove: boolean = false;
    removeRef: any;
    criteria = "Status";
    direction: boolean = false;
    page: number = 1;
    lblHeading: string = "";

    public loading = false;

    constructor(private service: StatusService, private dialogService: DialogService) { }

    ngOnInit() {
      this.onChangeHeandig();
      this.reload();
  }

  reload() {
    this.loading = true;
      this.service.getAll().subscribe(result => {
          this.statuses = result;  
          this.loading = false;        
      });
  }

  onDelete(id: any) {      
      this.service.delete(id).subscribe(result => {
          
          this.dialogService.addDialog(AlertComponent, { title: 'Delete a Status', message: `Status ${this.removeRef.Status} deleted.`, bold: `${this.removeRef.Status}` });                                  
          if (result != null) {             
              this.onNo();
              this.reload();             
          }
          this.onChangeHeandig();
      }, error => {                  
          error = error.json();          
          this.dialogService.addDialog(ErrorComponent, { title: 'Delete Status', message: `Status ${this.removeRef.Status} was not deleted.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message });          
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
      this.lblHeading = (change) ? "Statuses" : "Delete Status";
  }

}
