//Child Component
import { UploadDocumentComponent } from './upload/upload-document.component';

import { Component, OnInit, OnChanges, SimpleChanges, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PurchaseOrderService } from "../services/purchaseorder.service";
import { SubmissionService } from "../services/submission.service";
import { DocumentService } from "../services/document.service";
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
import { DatePipe, CurrencyPipe } from '@angular/common';
import {DomSanitizer,SafeResourceUrl,} from '@angular/platform-browser';

//Popup Components
import { AlertComponent } from '../popup/alert.component';
import { ErrorComponent } from '../popup/error.component';
import { ConfirmComponent } from '../popup/confirm.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ViewerComponent } from '../popup/viewer.component';




import { CurrencyFormat } from '../pipes/currencyFormat.pipe';

@Component({
    selector: 'purchase-order',
    templateUrl: 'purchase-order.component.html',
    styleUrls: ['./purchase-order.component.css'],
    providers: [PurchaseOrderService, SubmissionService, DocumentService, CurrencyFormat]
})


export class PurchaseOrderComponent implements OnInit, OnChanges {


    invForm: FormGroup;
    @ViewChild('uploader') uploader: ElementRef;
    @ViewChild('selectedDoc') selector: ElementRef;

    @ViewChild(UploadDocumentComponent) childcomponent: UploadDocumentComponent;

    public SupInvDTO: any = [];
    public Document: any = [];
    public formSaved: boolean = false

    public activeCategory = '';
    public isCategory: boolean = false;
    public lockCategory: boolean = false;

    public loading = false;

    public spinner = false;
    message = "Please wait...";

    public MIMEtypes = localStorage.getItem("DocTypes").split(',');
    public DownloadURL = localStorage.getItem("DownloadUrl");


    file: File;

    upload: string;
    filename: string;
    type: string;
    size: string;

    constructor(private service: PurchaseOrderService,
        private router: Router,
        private activRoute: ActivatedRoute,
        private subService: SubmissionService,
        private docService: DocumentService,
        private fb: FormBuilder,
        private dialogService: DialogService,
        public datepipe: DatePipe,
        public currpipe: CurrencyPipe,
        public currformat: CurrencyFormat,
        public sanitizer : DomSanitizer  
    ) {
    }

    ngOnInit() {
        //Get ID from url and pass it
        this.activRoute.params.subscribe((params: Params) => {
            this.getPObyID(params['id'])
        });

        //Initialise the form
        this.intForm();

    }

    ngOnChanges() {

    }

    getPObyID(id) {

        this.spinner = true;
        this.service.getPO(id)
            .subscribe(
            result => {
                this.SupInvDTO = result,
                    this.spinner = false;
            },
            error => {
                this.dialogService.addDialog(AlertComponent, { title: '' + error.code, message: 'Error loading PO : ' + error + '' }),
                    this.spinner = false;
            });


    }

    intForm() {

        this.invForm = this.fb.group({
            Name: [{ disabled: true }, [<any>Validators.required]],
            VendorCode: [{disabled:true }, [<any>Validators.required]],
            CSDNumber: [{ disabled: true }, [<any>Validators.required]],

            PONumber: [{ disabled: true }, [<any>Validators.required]],
            PODate: [{ disabled: true }, [<any>Validators.required]],
            POStatus: [{ disabled: true }, [<any>Validators.required]],
            POAmount: [{ disabled: true }, [<any>Validators.required]],
            DueAmount: [{ disabled: true }],

            InvoiceNumber: ['', [<any>Validators.required]],
            InvoiceAmount: ['', [<any>Validators.required]],
            InvoiceDate: [null, [<any>Validators.required]],
            Description: ['',],

            InvoiceCat: ['',[]],
            FileUpload: null,
            FileUploadType: ['', []],

            InvoiceID: ['', []],
            POID: ['', []]
        });
    }

    POST(model): void {

        if (this.formSaved === true) {
            this.dialogService.addDialog(AlertComponent, { title: 'Saved', message: 'Invoice successfully saved.' })
            
        }
        else {

            this.spinner = true;
            this.subService.postSubmission(model)
                .subscribe(
                response => {
                    this.dialogService.addDialog(AlertComponent, { title: 'Submitted', message: 'Invoice successfully submitted. Your submission number :' + response + '' }),
                        this.router.navigate(['submissions']),
                        this.spinner = false;
                },
                error => {
                    this.dialogService.addDialog(ErrorComponent, {
                        title: "Submission Failed!",
                        message: error.text(),
                        statuscode: error.statusText + ' ' + error.status,
                        url: error.url,
                        servermessage: error
                    }),
                        this.spinner = false
                })
        }
    }

    selectedCategory(model,catergoryid){
        
        if(this.lockCategory == false)
        {

            this.isCategory = true;
            this.childcomponent.LinkedDocuments(model, catergoryid)
            this.childcomponent.moreuploads = false;
            this.childcomponent.AllDocuments.length = 0
        }
        else
        {
            this.dialogService.addDialog(AlertComponent, {
                title: "Invoice Category",
                message: "Documents already uploaded. Please delete documents to change category."
            })
        }

    }


    backToPO(): any {

        if (this.formSaved = false)
        {
        this.dialogService.addDialog(ConfirmComponent, {
            title:'Cancel Invoice Capture', 
            message:'Are you sure you want to leave the page without saving the invoice details?'})
            .subscribe((isConfirmed)=>{
                //We get dialog result
                if(isConfirmed) {
                    this.router.navigate(['/search']);
                }
                else {
                   
                }
            });       
        }   
        else
        {
            this.router.navigate(['/search']);
        }     
      
    }


    viewPopUp(linkid: any)
    {

        let url = this.sanitizer.bypassSecurityTrustResourceUrl(linkid);
        this.dialogService.addDialog(ViewerComponent, { title: 'Name', NodeID: linkid })        

    }
    //////////////////////// Form Validations ////////////////////////////
    onBlurAmount(element) {

        let InvoiceAmount = this.invForm.get('InvoiceAmount').value
        let DueAmount = this.invForm.get('DueAmount').value

        this.amountValidator(InvoiceAmount, DueAmount);

       let formattedAmount = this.currformat.transform(parseFloat(InvoiceAmount));
       element.target.value = formattedAmount;
    }

    onBlurDate() {

        let InvoiceDate = this.invForm.get('InvoiceDate').value

        this.dateValidator(InvoiceDate);
    }

    amountValidator(InvoiceAmount, DueAmount) {

        if (InvoiceAmount > DueAmount) {
            this.invForm.controls.InvoiceAmount.reset();

            this.dialogService.addDialog(AlertComponent, { title: 'Error', message: 'Invoice amount cannot be more than amount due' });
            return;
        }
        // }else if(InvoiceAmount <= 0)
        // {
        //     this.invForm.controls.InvoiceAmount.reset();
            
        //     this.dialogService.addDialog(AlertComponent, { title: 'Error', message: 'Invoice amount must be greater than R 0.00' });
        //     return;
        // }
    }

    dateValidator(InvoiceDate): any {

        if (InvoiceDate == null) {
            this.invForm.controls.InvoiceDate.reset();

            this.dialogService.addDialog(AlertComponent, { title: 'Error', message: 'Invoice date cannot be NULL' });
            return;
        }
    }

}
