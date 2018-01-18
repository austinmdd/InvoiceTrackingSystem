import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { Headers, Http, RequestOptions } from '@angular/http';

import { StatusService } from "./../../services/status.service";

//Popup Components
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";

@Component({
  selector: 'app-statusmanage',
  templateUrl: './statusmanage.component.html',
  styleUrls: ['./statusmanage.component.css']
})
export class StatusmanageComponent implements OnInit {

    form;
    status: any = {};
    sub: any;
    lblHeading: string = "";
    btnActionLabel: string = "";
    isEdit: boolean;
    user: number = 1;
    isDup: boolean = false;

    public loading = false;

    constructor(private service: StatusService, 
        private router: Router, 
        private route: ActivatedRoute,
        private dialogService: DialogService) { }

    ngOnInit() {
       
        this.initForm();
        this.onChanges();
      this.sub = this.route.params.subscribe(params => {
          if (this.router.url.indexOf('edit') != -1) {
              this.isEdit = true;
              this.lblHeading = "Update a Status";
              this.btnActionLabel = "Update";
              let id = +params['id'];      
              this.loading = true;       
              this.service.getById(id).subscribe(result => {                 
                  this.status = result;
                  this.loading = false;
                  (<FormGroup>this.form)
                      .setValue({
                          StatusCode: this.status.StatusCode,
                          Status: this.status.Status,
                          Description: this.status.Description,
                          SupplierInvoiceYN: (this.status.SupplierInvoiceYN != null) ? this.status.SupplierInvoiceYN : false,
                          SubmissionYN: (this.status.SubmissionYN != null) ? this.status.SubmissionYN : false,
                          PurchaseOrderYN: (this.status.PurchaseOrderYN != null) ? this.status.PurchaseOrderYN : false
                      }, { onlySelf: true });
              });
          }
          else {
              this.isEdit = false;
              this.lblHeading = "Add a new Status";
              this.btnActionLabel = "Save";
          }


      });
  }


  initForm() {
      this.form = new FormGroup({
          Status: new FormControl(this.status.DocumentType, Validators.compose([Validators.required, <any>Validators.maxLength(12), Validators.pattern('[\\w\-\\s\\/]+')])),
          Description: new FormControl(this.status.Description, Validators.required),
          StatusCode: new FormControl(this.status.StatusCode),
          SupplierInvoiceYN: new FormControl(),
          SubmissionYN: new FormControl(),
          PurchaseOrderYN: new FormControl()
      });
  }

  ngOnDestroy() {
      this.sub.unsubscribe();
  }

  onSubmit(obj: any) {
      if (this.isDup) return;
      this.status.Status = obj.Status;
      this.status.Description = obj.Description;      
      this.status.PurchaseOrderYN = (obj.PurchaseOrderYN) ? obj.PurchaseOrderYN : false;
      this.status.SubmissionYN = (obj.SubmissionYN) ? obj.SubmissionYN : false;
      this.status.SupplierInvoiceYN = (obj.SupplierInvoiceYN) ? obj.SupplierInvoiceYN : false;
      if (this.isEdit) {
          this.status.DateUpdated = new Date();
          this.status.UserUpdated = this.user;
          this.loading = true;
          this.service.update(this.status).subscribe(result => {
              this.status = result;
              this.loading = false;
              this.dialogService.addDialog(AlertComponent, { title: 'Update a Status', message: `Status ${this.status.Status} updated succesfully.`, bold: `${this.status.Status}` });                                                
              this.navigate();
          });
      } else {          
          this.status.DateUpdated = this.status.DateCreated = new Date();
          this.status.UserCreated = this.status.UserUpdated = this.user;
         
          this.loading = true;
          this.service.add(this.status).subscribe(result => {
              this.status = result;
              this.loading = false;
              this.dialogService.addDialog(AlertComponent, { title: 'New Status', message: `New Status ${this.status.Status} added successfully.`, bold: `${this.status.Status}` });                                                
              this.navigate();
          });
      }
  }

  navigate() {
      this.router.navigateByUrl("statuses");
  }

  onDuplicates(control) {
      
      
  }

  onChanges(): void {
      this.form.get('Status').valueChanges.subscribe(val => {
          
          if (val.length > 3) {
              this.service.getByStatus(val).subscribe(result => {                 
                  this.isDup = (result.Status && !this.isEdit)?true:false;
              }, error => {                 
                  this.isDup = false;
                    
              });
          }
      });
  }



}
