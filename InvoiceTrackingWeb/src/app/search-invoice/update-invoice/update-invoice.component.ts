import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { StatusService } from './../../services/status.service';
import { SupplierInvoiceUpdateService } from './../../services/supplier-invoice-update.service';
import { AddNotesService } from './../../services/add-notes.service';
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ErrorComponent } from '../../popup/error.component';

@Component({
  selector: 'app-update-invoice',
  templateUrl: './update-invoice.component.html',
  styleUrls: ['./update-invoice.component.css']
})
export class UpdateInvoiceComponent implements OnInit {

    form;
    inv: any = {};
    stat: any = [];
    options = [];
    sub: any;
    lblHeading: string = "";
    userId: number = 2;
    direction: boolean = false;
    page: number = 1;
    pageSize = parseInt(localStorage.getItem("pageSize"));
    notesdat: any = {
        "SupplierInvoiceID":1,
        "Notes":"Testings",
        "DateCreated": new Date(),
        "UserCreated": "2",
        "UserUpdated": 2,
        "DateUpdated":new Date()
    };
    invID : number = 0;
    tableIndex : number = 0;
    notes: any = [];
    invlink: string; 
    isLoading: boolean = false;

    constructor( private service: SupplierInvoiceUpdateService,
        private statusService: StatusService, 
        private notesService: AddNotesService,
        private router: Router, 
        private  route: ActivatedRoute,
        private dialogService: DialogService) {
        this.readStatuses();
    }

    ngOnInit() {
        this.initForm();
        this.isLoading = true;
        this.sub = this.route.params.subscribe(params => {
            let id = params['id'];
            this.invID = id;
            this.service.getById(id).subscribe(result => {
                this.inv = result;
                this.lblHeading = "Update invoice number ";
                this.invlink = this.inv.InvoiceNumber.trim();

                (<FormGroup>this.form)
                    .setValue({
                        ID: this.inv.ID, InvoiceStatus: this.inv.Status, Note: "",
                    }, { onlySelf: true });
                this.isLoading = false;
            },error => {
                error = error.json();
                this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load the selected invoice`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
                this.isLoading = false;
                this.navigate();
            });
        });
        this.getOldNotes();
    }


    initForm() {
      this.form = new FormGroup({
          InvoiceStatus: new FormControl(this.inv.Status, Validators.compose([
              Validators.required, <any>Validators.maxLength(12),
              Validators.pattern('[\\w\-\\s\\/]+')
          ])),
          Note: new FormControl(this.inv.Description,Validators.required),
          ID: new FormControl(this.inv.ID)
      });
    }

    onSubmit(obj:any) {
        this.inv.Status = obj.InvoiceStatus;
        this.notesdat.Notes = obj.Note;
        this.inv.DateUpdated = new Date();
        this.inv.UserUpdated = this.userId;     
        this.notesdat.SupplierInvoiceID = this.invID;
        this.notesdat.DateCreated = new Date();
        this.notesdat.UserCreated = "2";
        this.notesdat.UserUpdated = 2;
        this.notesdat.DateUpdated = new Date();
        
        this.isLoading = true;
        this.service.update(this.inv).subscribe(result => {
            this.inv = result;
            this.notesService.add(this.notesdat).subscribe(result => {
                this.dialogService.addDialog(AlertComponent, { title: 'Update Invoice Status', message: `Invoice ${this.inv.InvoiceNumber} status updated succesfully.` });
                },error => {
                error = error.json();
                this.dialogService.addDialog(ErrorComponent, { title: 'Error', message: `Could not add notes for invoice ${this.inv.InvoiceCategory}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
                this.isLoading = false;
                this.navigate();
            });
            this.isLoading = false;
            this.navigate();
        },error => {
            error = error.json();
            this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not update status and notes for invoice ${this.inv.InvoiceCategory}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
            this.isLoading = false;
            this.navigate();
        });
    }

    navigate() {
      this.router.navigateByUrl("search-invoice-list/" + this.invlink);      
    }

    readStatuses(){
        this.isLoading = true;
        this.statusService.getAll().subscribe(result => {
        this.stat = result;
        this.stat.forEach(element => {
            this.options.push(element.Status);    
        });
        this.isLoading = false;
      });
    }

    getOldNotes(){
        this.isLoading = true;
        this.notesService.getById(this.invID).subscribe(result => {
            this.notes = result;
            this.isLoading = false;
        });
    }

    onSort() {
        this.direction = !this.direction;
    }
     
    onNext(pg){
        this.page = pg;
    }

    indexincr(){
        this.tableIndex++;
        return this.tableIndex;
    }
}