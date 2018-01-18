import { Component, OnInit, ViewChild, ElementRef, Inject, forwardRef } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubmissionService } from "../../services/submission.service";
import { DocumentService } from "../../services/document.service";
import { DocumenttypeService } from "../../services/documenttype.service";
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { ChangeDetectorRef } from '@angular/core';
import { Response }  from "@angular/http";

import { LinkedUploadedDocument } from "../../interfaces/documents.interface";
import { AllDocuments } from "../../interfaces/alldocuments.interface";

//Popup Service and Components
import { DialogService } from "ng2-bootstrap-modal";
import { AlertComponent } from '../../popup/alert.component';
import { ErrorComponent } from '../../popup/error.component';
import { ConfirmComponent } from '../../popup/confirm.component';
import { ViewerComponent } from '../../popup/viewer.component';

import { PurchaseOrderComponent } from '../purchase-order.component';
import { forEach } from '@angular/router/src/utils/collection';
import { Input } from '@angular/core/src/metadata/directives';

@Component({
    selector: 'upload-document',
    templateUrl: 'upload-document.component.html',
    styleUrls: ['upload-document.component.css'],
    providers: [SubmissionService, DocumentService, DocumenttypeService]

})
export class UploadDocumentComponent implements OnInit {

    @ViewChild('fileupload') uploader: ElementRef;
    @ViewChild('selectedDoc') selector: ElementRef;
    @ViewChild('othertypename') othertypename: ElementRef;

    public ParentModel: any = [];
    public doctypes: any = [];
    public uploadeddocs: any = [];
    public LinkedUploadedDocuments: any = [];
    public isDelete = false;

    public AllDocuments: any = [];
    public Document: any = [];

    docForm: FormGroup;

    public otherdoctype: any = [];
    public moreuploads = false;
    public isCategory = false;
    public isupload = false;

    public spinner = false;
    message = "Uploading... please wait";

    public MIMEtypes = localStorage.getItem("DocTypes").split(',');
    public DownloadURL = localStorage.getItem("DownloadUrl");

    typeid: number;
    file: File;

    public upload: string;
    public filename: string;
    public type: string;
    public size: string;

    constructor( @Inject(forwardRef(() => PurchaseOrderComponent)) private _parent: PurchaseOrderComponent,
        private fb: FormBuilder,
        private subService: SubmissionService,
        private docService: DocumentService,
        private dialogService: DialogService,
        private doctypeService: DocumenttypeService,
        public sanitizer: DomSanitizer,
        private ref: ChangeDetectorRef
    ) { }


    ngOnInit() {

        this.intForm();
    }

    intForm() {

        this.docForm = this.fb.group({
            InvoiceCat: ['', []],
            FileUpload: ['', []],
            FileUploadType: ['', []],

            InvoiceID: ['', []],
            POID: ['', []]
        });
    }


    LinkedDocuments(model, id) {

        this.ParentModel = model;

        this.uploadeddocs == false

        this.spinner = true
        this.doctypeService.getLinkedAndUnlinkedDoctypes(id).subscribe(
            result => {
                this.doctypes = result;
                this.mapInterface(this.doctypes, this.uploadeddocs)
                this.getAllDocuments(this.doctypes);
                this.spinner = false;
                this.isCategory = true;
            },
            error => {
                console.log(error);
                this.spinner = false;
            }
        );
    }

    getAllDocuments(alldocs) {

        let countlinked = alldocs.linked.length;
        let countunlinked = alldocs.unlinked.length;

        for (let i = 0; i < countlinked; i++) {

            let object: AllDocuments = {
                ID: alldocs.linked[i].ID,
                DocumentTypeID: alldocs.linked[i].DocumentTypeID,
                DocumentType: alldocs.linked[i].DocumentType,
                Mandatory: alldocs.linked[i].Mandatory,
                InvoiceCategoryID: alldocs.linked[i].InvoiceCategoryID
            };

            this.AllDocuments.push(object);
        }
        
        for (let i = 0; i < countunlinked; i++) {

            let object: AllDocuments = {
                ID: alldocs.unlinked[i].ID,
                DocumentTypeID: alldocs.unlinked[i].DocumentTypeID,
                DocumentType: alldocs.unlinked[i].DocumentType,
                Mandatory: alldocs.unlinked[i].Mandatory,
                InvoiceCategoryID: alldocs.unlinked[i].InvoiceCategoryID
            };

            this.AllDocuments.push(object);
        }
    }

    showFooter() {
        this.moreuploads = true;


    }

    hideFooter() {
        this.moreuploads = false;
    }


    selectedType(documenttypeid, doctype) {
        this.typeid = documenttypeid
        this.otherdoctype.push(doctype)
    }

    changeListner(event) {
        this.selectedfile(event)
    }

    selectedfile(event) {

        if (this.typeid != null) {

            let reader = new FileReader();
            if (event.target.files && event.target.files.length > 0) {
                let file: any = null
                let count = event.target.files.length

                file = event.target.files[count - 1];
                this.ref.detectChanges();

                reader.removeEventListener
                reader.readAsDataURL(file);
                reader.onload = () => {

                    this.upload = reader.result.split(',')[1]
                    this.filename = file.name,
                        this.type = file.type
                    this.size = file.size
                    this.fileLoaded(this.ParentModel, this.typeid)

                };

            }

            this.uploader.nativeElement.value = ''
            this.moreuploads = false;

        }

    }


    fileLoaded(model, documenttypeid) {

        if (this._parent.invForm.valid) {
            // model.FileUpload = this._parent.invForm.get('FileUpload').value
            model.FileUploadType = documenttypeid;

            if (this._parent.formSaved == false) {
                //Pre-save the invoice to get invoiceID for uploading file
                this.spinner = true;
                this.subService.postSubmission(model)
                    .subscribe(
                    response => {
                        this.ParentModel.InvoiceID = response,
                            this.postFile(response, documenttypeid),
                            this._parent.formSaved = true
                    },
                    error => {
                        this.dialogService.addDialog(ErrorComponent, {
                            title: "File Upload Failed",
                            message: error._body
                        }),
                            this.spinner = false
                            console.log(error._body)
                    })
            }
            else {
                this.postFile(this.ParentModel.InvoiceID, documenttypeid)
            }

        }
        else {
            this.dialogService.addDialog(AlertComponent, { title: 'Error', message: 'Please complete all required fields before uploading documents' })
        }


    }

    postFile(InvoiceID, documenttypeid) {
        this.uploader.nativeElement.value = ''
        console.log(" name " + this.filename)

        let formdata = new FormData()
        formdata.append("File", this.upload)
        formdata.append("FileName", this.filename)
        formdata.append("TypeOfFile", this.type)
        formdata.append("FileSize", this.size)
        formdata.append("FileTypeID", documenttypeid)
        formdata.append("InvoiceID", InvoiceID)
        formdata.append("PONumber", this.ParentModel.PONumber)


        this.message = "Uploading... please wait"
        this.spinner = true
        this.docService.postDocument(formdata, documenttypeid, InvoiceID)
            .subscribe(
            response => {
                this.uploadeddocs = response,
                    this.mapInterface(this.doctypes, this.uploadeddocs)
                this.ParentModel.FileUploadType = documenttypeid,
                    this.dialogService.addDialog(AlertComponent, {
                        title: 'File Uploaded',

                        message: 'File: ' + this.filename + ' was succesfully uploaded.',
                        bold: this.filename
                    }),
                    this.spinner = false
            },
            error  => {
                this.ngOnInit(),
                    this.resetuploadcontrols()
                this.dialogService.addDialog(ErrorComponent, {
                    title: "File Upload Failed",
                    message: error._body
                }),
                    this.spinner = false
                this.resetuploadcontrols
            })

        this._parent.lockCategory = true

    }

    deleteAttachment(id, type) {

        let stringToSplit = type;

        if (stringToSplit.split(" - ")[0] = 'Other') {
            let index = this.isInTypeList(stringToSplit.split(" - ")[1]);
            if (index != -1) {
                this.otherdoctype.splice(index, 1);
            }
        }

        this.uploadeddocs.length = 0;

        this.message = "Deleting..."
        this.spinner = true
        this.docService.deleteDocument(id)
            .subscribe(
            response => {
                this.mapInterface(this.doctypes, this.uploadeddocs),
                    this.loadGridView(this.ParentModel.InvoiceID),
                    this.dialogService.addDialog(AlertComponent, {
                        title: 'File Deleted',
                        message: response
                    })
                this.spinner = false
            },
            error => {
                this.dialogService.addDialog(ErrorComponent, {
                    title: "Delete File",
                    message: "Could not delete file. "
                }),
                    this.spinner = false
            }
            )

        if (this.uploadeddocs.length == 0)
            this._parent.lockCategory = false
    }


    loadGridView(invoiceid) {

        this.spinner = true
        this.docService.getAttachements(invoiceid)
            .subscribe(
            response => {
                this.uploadeddocs = response,
                    this.mapInterface(this.doctypes, this.uploadeddocs),
                    this.spinner = false
            },
            error => {
                this.dialogService.addDialog(ErrorComponent, {
                    title: "GridView Refresh",
                    message: "Error refreshing grid"
                }),
                    this.spinner = false
            })
    }

    resetuploadcontrols() {

        console.log(this.uploader.nativeElement.file)

        this.mapInterface(this.doctypes, this.uploadeddocs)

        console.log(this.uploader.nativeElement.file)
    }

    download(id) {
        this.message = "Fetching... please wait"
        this.spinner = true
        this.docService.downloadFiles(id)
            .subscribe(
            result => {
                this.preview(result),
                    this.spinner = false
            }),
            error => {
                console.log("Error downloading the file."),
                this.spinner = false
            },
            () => console.info("OK");
    }

    preview(data) {

        this.Document = JSON.parse(data);

        let mimetype: string[] = this.MIMEtypes;

        if (mimetype.find(x => x === this.Document.Type)) {
            let url = this.sanitizer.bypassSecurityTrustResourceUrl(this.DownloadURL + this.Document.Name);
            this.dialogService.addDialog(ViewerComponent, { title: this.Document.Name, NodeID: url })

        } else {
            let url = this.sanitizer.bypassSecurityTrustResourceUrl(this.DownloadURL + this.Document.Name);
            this.dialogService.addDialog(ViewerComponent, { title: this.Document.Name, NodeID: url })
            this.dialogService.addDialog(AlertComponent, { title: 'File Format', message: 'File type cannot be pre-viewed. File has been downloaded.' });

        }


    }

    mapInterface(doctypes, uploadeddocs) {


        this.spinner == true;

        if (uploadeddocs.length == 0 ) {



            this.LinkedUploadedDocuments.length = 0

            let count = this.doctypes.linked.length;

            for (let i = 0; i < count; i++) {

                if (doctypes.linked[i].Mandatory == true || doctypes.linked[i].Mandatory == "Yes") { doctypes.linked[i].Mandatory = 'Yes' }
                else if (doctypes.linked[i].Mandatory == false || doctypes.linked[i].Mandatory == "No") { doctypes.linked[i].Mandatory = 'No' }

                let object: LinkedUploadedDocument = {
                    id: this.LinkedUploadedDocuments.length + 1,
                    documentTypeID: doctypes.linked[i].DocumentTypeID,
                    documentType: doctypes.linked[i].DocumentType,
                    mandatory: doctypes.linked[i].Mandatory,
                    documentID: 0,
                    documentObjectID: 0,
                    documentName: ''
                };

                this.LinkedUploadedDocuments.push(object);
            }
        }
        else {



            let count = this.uploadeddocs.length;

            for (let i = 0; i < count; i++) {


                let index = this.isInList(uploadeddocs[i].DocTypeID);



                if (index != -1 && uploadeddocs[i].DocTypeName != 'Other') {



                    let currectobject: LinkedUploadedDocument = this.LinkedUploadedDocuments.find(value => value.documentTypeID == uploadeddocs[i].DocTypeID)

                    let object: LinkedUploadedDocument = {
                        id: this.LinkedUploadedDocuments.length + 1,
                        documentTypeID: currectobject.documentTypeID,
                        documentType: currectobject.documentType,
                        mandatory: currectobject.mandatory,
                        documentID: uploadeddocs[i].ID,
                        documentObjectID: uploadeddocs[i].ObjectID,
                        documentName: uploadeddocs[i].DocName
                    };


                    this.LinkedUploadedDocuments[index] = object;
                }
                else if (uploadeddocs[i].DocTypeName == "Other") {

                    console.log(uploadeddocs)
                    let index = this.isInOtherList(uploadeddocs[i].ID);

                    uploadeddocs[i].DocTypeMandatory = 'No'

                    let OtherObject: LinkedUploadedDocument = {
                        id: this.LinkedUploadedDocuments.length + 1,
                        documentTypeID: uploadeddocs[i].DocTypeID,
                        documentType: (this.otherdoctype) ? uploadeddocs[i].DocTypeName + " - " + this.otherdoctype[i] : uploadeddocs[i].DocTypeName,
                        mandatory: uploadeddocs[i].DocTypeMandatory,
                        documentID: uploadeddocs[i].ID,
                        documentObjectID: uploadeddocs[i].ObjectID,
                        documentName: uploadeddocs[i].DocName
                    };

                    if (index != -1) {
                        this.LinkedUploadedDocuments[index] = OtherObject;
                    } else {

                        this.LinkedUploadedDocuments.push(OtherObject)
                    }

                }
            }
        }

        this.spinner = false;
    }

    isInList(id: number) {
        let count = this.LinkedUploadedDocuments.length;
        for (let iter = 0; iter < count; iter++) {
            if (this.LinkedUploadedDocuments[iter].documentTypeID == id) {
                return iter;
            }
        }
        return -1;

    }

    isInOtherList(id: number) {
        let count = this.LinkedUploadedDocuments.length;
        for (let iter = 0; iter < count; iter++) {
            if (this.LinkedUploadedDocuments[iter].documentID == id) {
                return iter;
            }
        }
        return -1;

    }

    isInTypeList(type: string) {
        let count = this.otherdoctype.length;
        for (let iter = 0; iter < count; iter++) {
            if (this.otherdoctype[iter] == type) {
                return iter;
            }
        }
        return -1;

    }

}
