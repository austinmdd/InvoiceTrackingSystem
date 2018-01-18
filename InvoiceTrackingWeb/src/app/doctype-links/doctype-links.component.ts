import { Component, OnInit } from '@angular/core';
import { InvoiceCategoryService } from './../services/invoice-category.service';
import { DocumenttypeService } from "./../services/documenttype.service";
import { DialogService } from "ng2-bootstrap-modal";
import { AlertComponent } from './../popup/alert.component';
import { Linked } from './../interfaces/linked'


@Component({
  selector: 'app-doctype-links',
  templateUrl: './doctype-links.component.html',
  styleUrls: ['./doctype-links.component.css']
})
export class DoctypeLinksComponent implements OnInit {
    categories: any = [];
    selectedCat: any = {};
    doctypes: any = {};
    linked: any = [];
    isChecked: boolean = false;
    refLink: any = {};
    isEdit: boolean = false;
    doctype: string = "";
    message: string = "Loading invoice categories, please wait...";
    isActivated: boolean = false;
    selected = true;

    constructor(private categoryService: InvoiceCategoryService, private doctypeService: DocumenttypeService,  private dialogService: DialogService) { }

    ngOnInit() {
        this.onLoadInvoiceCategories();
    }

    onLoadInvoiceCategories() {
      this.isActivated = true;
      this.categoryService.getAll().subscribe(
          result => {              
              this.categories = result;
              this.isActivated = false;
          },
          error => {
              console.log(error);
              this.isActivated = false;
          });
    }

    onChange() {
              
        this.linked = [];
        this.isChecked = false;
        if (this.selectedCat.ID> 0)
            this.onReloadLinks(this.selectedCat.ID);
    }

    onReloadLinks(id: number, show: boolean=true) {
        this.message = "Loading document types, please wait...";
        this.isActivated = show;
        this.doctypeService.getLinkedAndUnlinkedDoctypes(id).subscribe(
            result => {                
                this.doctypes = result;
                this.isActivated = false;
            },
            error => {
                console.log(error);
                this.isActivated = false;
            }
        );
    }

    onCheck(id, link) {
        let val: Linked = {
            id: undefined,
            documentTypeID: id,
            invoiceCategoryID: this.selectedCat.ID,
            mandatory: (link == 1) ? true : false,
            optional: (link == 2) ? true : false,
            dateCreated: new Date(),
            userCreated: 'Praxis',//To be changed
            userUpdated: 1,//To be changed
            dateUpdated: new Date()
        };
        this.isChecked = true;
        let index = this.isInList(id);
        if (index == -1) this.linked.push(val);
        else this.linked[index] = val;               
    }

    isInList(id: number) {
        let count = this.linked.length;
        for (let iter = 0; iter < count; iter++) {
            if (this.linked[iter].id == id) return iter;
        }
        return -1;
    }

    OnSaveLinks() {
        this.message = "Linking selected document type(s), please wait...";
        this.isActivated = true;
        this.doctypeService.addToLinks(this.linked).subscribe(
            result => {              
                this.isActivated = false;
                this.dialogService.addDialog(AlertComponent, { title: 'Linking Document Type(s)', message: `Document type(s) linked to ${this.selectedCat.InvoiceCategory} succesfully.`, bold: `${this.selectedCat.InvoiceCategory}` });
                this.onReloadLinks(this.selectedCat.ID, false);
                this.linked = [];
            },
            error => {
                console.log(error);
                this.isActivated = false;
                this.linked = [];
            }
        );

    }

    onDelete(id: number, doctype: string) {
        this.message = `Unlinking ${this.doctype}, please wait...`;
        this.isActivated = true;
        this.doctypeService.deleteLink(id).subscribe(
            result => {
                this.isActivated = false;
                this.onReloadLinks(this.selectedCat.ID, false);
                this.dialogService.addDialog(AlertComponent, { title: 'Unlink Document Type', message: `Document type ${doctype} linked to ${this.selectedCat.InvoiceCategory} unlinked succesfully.`, bold: `${this.selectedCat.InvoiceCategory}` });
            },
            error => {
                this.isActivated = false;
            }
        );
    }
    onEdit(id,doctype) {        
        this.doctype = doctype;
        
        this.isEdit = true;
        this.doctypeService.getLinkById(id).subscribe(
            result => {
                this.refLink = result;
                this.refLink.DocumentType = doctype;                               
            },
            error => {
                console.log(error);
            }
        );
    }

    onUpdateCheck(id){
        console.log(this.refLink);        
            this.refLink.Mandatory = (id == 1)? true:false;
            this.refLink.Optional = (id != 1) ? true : false;            
            this.isChecked = true;
    }
    onUpdateLink() {
        this.message = `Update link between ${this.doctype} and ${this.selectedCat.InvoiceCategory}, please wait...`;
        this.isActivated = true;
        let link: Linked = {
            id: this.refLink.ID,
            documentTypeID: this.refLink.DocumentTypeID,
            invoiceCategoryID: this.selectedCat.ID,
            mandatory: this.refLink.Mandatory,
            optional: this.refLink.Optional,
            dateCreated: this.refLink.DateCreated,
            userCreated: this.refLink.UserCreated,//To be changed
            userUpdated: 2,//To be changed
            dateUpdated: new Date()
        };
        console.log(link);

        this.doctypeService.updateLink(link).subscribe(
            result => {
                this.onReloadLinks(this.selectedCat.ID, false);
                this.isEdit = this.isActivated = false;
                this.dialogService.addDialog(AlertComponent, { title: 'Update Document Type Link', message: `Document type ${this.doctype} linked to ${this.selectedCat.InvoiceCategory} updated succesfully.`, bold: `${this.selectedCat.InvoiceCategory}` });
            },
            error => {
                console.log(error);
                this.isEdit = this.isActivated = false; 
            }
        );
    }

    onCancel() {
        this.isEdit = false;        
    }

}
