import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { InvoiceCategoryService } from "./../../services/invoice-category.service";
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ErrorComponent } from '../../popup/error.component';

@Component({
  selector: 'app-invoice-manage',
  templateUrl: './invoice-manage.component.html',
  styleUrls: ['./invoice-manage.component.css']
})
export class InvoiceManageComponent implements OnInit {

  form;
  inv: any = {};
  sub: any;
  lblHeading: string = "";
  btnActionLabel: string = "";
  isEdit: boolean;
  userId: number = 2;
  isDup: boolean = false;
  isLoading: boolean = false;
  message = "Loading...";

 constructor( private service: InvoiceCategoryService, 
     private router: Router, 
     private  route: ActivatedRoute,
     private dialogService: DialogService) { }

    ngOnInit() {
    this.initForm();
    this.onChanges();
    this.sub = this.route.params.subscribe(params => {
        if (this.router.url.indexOf('edit') != -1) {
            this.isEdit = true;
            this.lblHeading = "Update an Invoice Category";
            this.btnActionLabel = "Update";
            let id = +params['id'];
            this.isLoading = true; 
            this.service.getById(id).subscribe(result => {
                this.inv = result;
                (<FormGroup>this.form)
                    .setValue({
                        ID: this.inv.ID, InvoiceCategory: this.inv.InvoiceCategory, Description: this.inv.Description
                    }, { onlySelf: true }); 
                    this.isLoading = false;
            },error => {
                error = error.json();
                this.isLoading = false;
                this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load data for selected invoice`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
                this.navigate();
            });
        }
        else {
            this.isEdit = false;
            this.lblHeading = "Add a new Invoice Category";
            this.btnActionLabel = "Save";
        }

    });
    }


initForm() {      
   this.form = new FormGroup({
       InvoiceCategory: new FormControl(this.inv.InvoiceCategory, Validators.compose([
           Validators.required, <any>Validators.maxLength(12),
           Validators.pattern('[\\w\-\\s\\/]+')
       ])),
       Description: new FormControl(this.inv.Description,Validators.required),
       ID: new FormControl(this.inv.ID)
   });
}

ngOnDestroy() {
   this.sub.unsubscribe();
}

onSubmit(obj:any) {
   if (this.isDup) return;
   this.inv.InvoiceCategory = obj.InvoiceCategory;
   this.inv.Description = obj.Description;
   
   this.isLoading = true;
   if (this.isEdit) {
        this.inv.DateUpdated = new Date();
        this.inv.UserUpdated = this.userId;
       
        this.service.update(this.inv).subscribe(result => {
            this.inv = result;
            this.dialogService.addDialog(AlertComponent, { title: 'Update an Invoice Category', message: `Invoice Category ${this.inv.InvoiceCategory} updated succesfully.`, bold: `${this.inv.InvoiceCategory}` });
            this.isLoading = false;
            this.navigate();
        },error => {
            error = error.json();
            this.dialogService.addDialog(ErrorComponent, { title: 'Update error', message: `Could not update invoice ${this.inv.InvoiceCategory}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
            this.isLoading = false;
            this.navigate();
        });
   } else {
        this.inv.EnabledYN = 1;
        this.inv.DateUpdated = this.inv.DateCreated = new Date();
        this.inv.UserUpdated  = this.userId;
        this.inv.UserCreated = "Praxis";
        this.service.add(this.inv).subscribe(result => {
            this.inv = result;
            this.dialogService.addDialog(AlertComponent, { title: 'Add an Invoice Category', message: `Invoice Category ${this.inv.InvoiceCategory} added succesfully.`, bold: `${this.inv.InvoiceCategory}` });              
            this.isLoading = false;            
            this.navigate();
        },error => {
            error = error.json();
            this.dialogService.addDialog(ErrorComponent, { title: 'Update error', message: `Could not add invoice ${this.inv.InvoiceCategory}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
            this.isLoading = false;
            this.navigate();
        });
   }
}

navigate() {
   this.router.navigateByUrl("invoicecategory");      
}

onChanges(): void {
   this.form.get('InvoiceCategory').valueChanges.subscribe(val => {
      
       if (val.length > 3) {
           this.service.getByType(val).subscribe(result => {                  
               this.isDup = (result.Status && !this.isEdit) ? true : false;
           }, error => {                 
               this.isDup = false;
           });
       }
   });
}



  
 
}
