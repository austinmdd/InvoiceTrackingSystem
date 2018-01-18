import { Component, OnInit } from '@angular/core';
import { WorkflowStatusservice } from './../../services/workflow-status-service';
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { SystemSettingsService } from './../../services/system-settings.service';
import { GlobalsService } from './../../services/globals.service';
import { ErrorComponent } from '../../popup/error.component';
import {NewInvoiceSubmittedsservice} from './../../services/submit-invoicesforwork.service';

import { InvoiceCategoryService } from './../../services/invoice-category.service';



@Component({
  selector: 'app-workflowprocess',
  templateUrl: './workflowprocess.component.html',
  styleUrls: ['./workflowprocess.component.css']
})
export class WorkflowprocessComponent implements OnInit {
  WorkflowOptions = [
    {id:1,name: "Forward"},
    {id:2,name: "Reject" },
    {id:3,name:"View"   }

  ];
  selectedValue = null;
  invoiceCat: any = [];
  flowStatus: any =[];
  categories: any =[];
  isRemove: boolean = false;
  removeRef: any;
  page: number = 1;
  pageSize = parseInt(localStorage.getItem("pageSize"));
  message = "Loading...";

  criteria = "InvoiceCategory";
  direction: boolean = false;
  lblHeading: string = "";
  isLoading: boolean = false;

  constructor(private invcategory:NewInvoiceSubmittedsservice,
              private workflowstatus:WorkflowStatusservice,
              private dialogService: DialogService,
              private sysSettings : SystemSettingsService,
              private categoryService: InvoiceCategoryService) { }

  ngOnInit() {
    this.onChangeHeandig();
    this.reload();
    this.LoadWorkflowStatuses();
    this.onLoadInvoiceCategories();
  }
  onLoadInvoiceCategories() {
    this.isLoading = true;
    this.categoryService.getAll().subscribe(
        result => {              
            this.categories = result;
            this.isLoading = false;
        },
        error => {
            console.log(error);
            this.isLoading = false;
        });
  }
  LoadWorkflowStatuses()
  {
    this.isLoading = true;
    this.workflowstatus. getAllStatus(this.page,this.pageSize).subscribe(result => {
      this.flowStatus = result;
      this.isLoading = false;
    },
    error => {
   console.log(error);
   this.isLoading = false;

    });
  }
  reload() {
    this.isLoading = true;
    this.invcategory.getAllFixed(this.page, this.pageSize).subscribe(result => {
        //console.log(result);
        this.invoiceCat = result;
        this.isLoading = false;
    },error => {
        error = error.json();
        this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load invoice categories`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
        this.isLoading = false;
  });
}

onDelete(id: any) {
  this.isLoading = true;
  this.invcategory.delete(id).subscribe(result => {
    this.onChangeHeandig();
    this.dialogService.addDialog(AlertComponent, { title: 'Delete an Invoice Category', message: `Invoice Category ${this.removeRef.InvoiceCategory} deleted.`, bold: `${this.removeRef.InvoiceCategory}` });                        
    if (result != null) {           
        this.onNo();
        this.reload();
    }
    this.isLoading = false;
}, error => {
    error = error.json();
    this.dialogService.addDialog(ErrorComponent, { title: 'Delete Invoice Category', message: `Invoice Category ${this.removeRef.InvoiceCategory} was not deleted.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
    this.isLoading = false;
    });
}
onSort() {
  this.direction = !this.direction;
}

onConfirm(inv: any) {
  this.removeRef = inv;     
  this.isRemove = true;
  this.onChangeHeandig(false);
}

onNo() {
    this.isRemove = false;
    this.onChangeHeandig();
}

onNext(pg){
  this.page = pg;
  this.reload();
}

onChangeHeandig(change: boolean = true) {
    this.lblHeading = (change) ? "Invoice Categories" : "Delete Invoice Category";
}


}
