import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';

import { DocumenttypeService } from "./../../services/documenttype.service"

//Popup Components
import { AlertComponent } from '../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";

@Component({
  selector: 'documentmanage',
  templateUrl: './documentmanage.component.html',
  styleUrls: ['./documentmanage.component.css']
})
export class DocumentmanageComponent implements OnInit {
     form;
     doc: any = {};
     sub: any;
     lblHeading: string = "";
     btnActionLabel: string = "";
     isEdit: boolean;
     userId: number = 2;
     isDup: boolean = false;

    constructor( private service: DocumenttypeService, 
        private router: Router, 
        private  route: ActivatedRoute,
        private dialogService: DialogService) { }

  ngOnInit() {
      this.initForm();
      this.onChanges();
      this.sub = this.route.params.subscribe(params => {
          if (this.router.url.indexOf('edit') != -1) {
              this.isEdit = true;
              this.lblHeading = "Update a Document Type";
              this.btnActionLabel = "Update";
              let id = +params['id'];             
              this.service.getById(id).subscribe(result => {                 
                  this.doc = result;
                  (<FormGroup>this.form)
                      .setValue({
                          ID: this.doc.ID, DocumentType: this.doc.DocumentType, Description: this.doc.Description
                      }, { onlySelf: true }); 
              });
          }
          else {
              this.isEdit = false;
              this.lblHeading = "Add a new Document Type";
              this.btnActionLabel = "Save";
          }


      });
  }


  initForm() {      
      this.form = new FormGroup({
          DocumentType: new FormControl(this.doc.DocumentType, Validators.compose([
              Validators.required, <any>Validators.maxLength(12),
              Validators.pattern('[\\w\-\\s\\/]+')
          ])),
          Description: new FormControl(this.doc.Description,Validators.required),
          ID: new FormControl(this.doc.ID)
      });
  }

  ngOnDestroy() {
      this.sub.unsubscribe();
  }

  onSubmit(obj:any) {
      if (this.isDup) return;
      this.doc.DocumentType = obj.DocumentType;
      this.doc.Description = obj.Description;
      
      if (this.isEdit) {
          this.doc.DateUpdated = new Date();
          this.doc.UserUpdated = this.userId;
          
          this.service.update(this.doc).subscribe(result => {
              this.doc = result;
              this.dialogService.addDialog(AlertComponent, { title: 'Update a Document Type', message: `Document Type ${this.doc.DocumentType } updated succesfully.`, bold: `${this.doc.DocumentType }` });
              this.navigate();
          });
      } else {
          this.doc.EnabledYN = 1;
          this.doc.DateUpdated = this.doc.DateCreated = new Date();
          this.doc.UserUpdated = this.doc.UserCreated = this.userId;         
          this.service.add(this.doc).subscribe(result => {
              this.doc = result;
              this.dialogService.addDialog(AlertComponent, { title: 'Add a Document Type', message: `Document Type ${this.doc.DocumentType} added succesfully.`, bold: `${this.doc.DocumentType }` });              
              this.navigate();
          });
      }
  }

  navigate() {
      this.router.navigateByUrl("doctypes");      
  }

  onChanges(): void {
      this.form.get('DocumentType').valueChanges.subscribe(val => {
         
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
