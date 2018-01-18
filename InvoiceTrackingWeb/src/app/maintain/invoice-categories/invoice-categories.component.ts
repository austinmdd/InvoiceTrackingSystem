import { Component, OnInit } from '@angular/core';
import {InvoiceCategoryService} from './../../services/invoice-category.service';
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { SystemSettingsService } from './../../services/system-settings.service';
import { GlobalsService } from './../../services/globals.service';
import { ErrorComponent } from '../../popup/error.component';

@Component({
  selector: 'app-invoice-categories',
  templateUrl: './invoice-categories.component.html',
  styleUrls: ['./invoice-categories.component.css']
})

export class InvoiceCategoriesComponent implements OnInit {

  invoiceCat: any = [];
  isRemove: boolean = false;
  removeRef: any;
  page: number = 1;
  pageSize = parseInt(localStorage.getItem("pageSize"));
  message = "Loading...";

  criteria = "InvoiceCategory";
  direction: boolean = false;
  lblHeading: string = "";
  isLoading: boolean = false;

  constructor(private invcategory:InvoiceCategoryService,
              private dialogService: DialogService,
              private sysSettings : SystemSettingsService) {
  }

  ngOnInit() {
      this.onChangeHeandig();
      this.reload();
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
