import { Component, OnInit, ViewChild, AfterViewInit, ElementRef} from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { ElementAst } from '@angular/compiler';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { WorkflowProcessService } from './../services/workflow-process.service';//
import { AddNotesService } from './../services/add-notes.service';
import { Location } from '@angular/common';
import { EmailService } from './../services/email.service';
import { WorkflowRouteService } from './../services/workflow-route-service';



export interface InvoiceApproveModel {
  title: string;
  message: string;
  bold?: string;
}

@Component({
  selector: 'alert',
  template: `<div class="modal-dialog">
                <div class="modal-content">
                   <div class="modal-header">
                   <h4 class="modal-title">{{ title || 'Alert' }} Invoice Request</h4>
                     <button type="button" class="close" (click)="close()" >&times;</button>
                   </div>
                   <form [formGroup]="form" (ngSubmit)="onSubmit(form.value)" >
                    <div class="modal-body">
                        <div class="form-group">
                          <p id="msg">Comment</p>
                          <textarea name="Note" formControlName="Note" class="form-control" rows="6" [(ngModel)]=mail.Message></textarea>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12">      
                          <div class="row">

                            <div class="col-md-6">    
                            </div>
                            <div class="col-md-6">
                              <div class="row">
                                <div class="col-sm-6 text-right"><button type="button" class="btn btn-primary btn-sm" style="width:80px;" (click)="close()" #setfocus>Cancel</button></div>
                                <div class="col-sm-6 text-left"><button [disabled]="!form.valid" (click)="onSubmit" class="btn btn-danger btn-sm" style="width:80px;">{{ title }}</button></div>
                              </div>
                            </div>
                          </div>
                        </div>      
                    </div>
                   </form>
                </div>
             </div>`
})
export class InvoiceApprovalComponent extends DialogComponent<InvoiceApproveModel, null> implements InvoiceApproveModel {
  title: string;
  message: string;
  bold?: string;
  WFinvoice: any = {};
  WFroutes: any = {};
  id: number;
  //adminOpt;
  supName = localStorage.getItem("supName");
  htitle = this.title + 'invoice request';

  form;

  notesdat: any = {
    "SupplierInvoiceID":1,
    "Notes":"Testings",
    "DateCreated": new Date(),
    "UserCreated": "2",
    "UserUpdated": 2,
    "DateUpdated":new Date()
  };

  mail: any = {
    "Address" : "napomd@hotmail.com",
    "Message" : ""
  }



  constructor(dialogService: DialogService,
              //private statusService: SupplierInvoiceUpdateService,
              private WFService: WorkflowProcessService,
              private notesService: AddNotesService,
              private location: Location,
              private mailService: EmailService,
              private routeservice:WorkflowRouteService) {
                super(dialogService);
  }

  ngOnInit(){ 
    
    if(this.bold)
    document.getElementById("msg").innerHTML = this.makeBold(this.message, this.bold);
    
    this.id = parseInt(localStorage.getItem("invid"));
    //this.adminOpt = localStorage.getItem("adminOpt");
    //this.mail.Address = localStorage.getItem("mailAdd");
    localStorage.removeItem("invid");
    /*localStorage.removeItem("adminOpt");
    localStorage.removeItem("supName");
    localStorage.removeItem("mailAdd");*/

    console.log(this.mail.Address);
    
    this.form = new FormGroup({
      Note: new FormControl("",Validators.required)
    });
    this.getWFInvoice(this.id);

    
  }

  @ViewChild('setfocus') private elementref: ElementRef
  public ngAfterViewInit(): void {
    this.elementref.nativeElement.focus();
  }

  makeBold(message, bold?) {
    return message.replace(new RegExp('('+bold+')', 'gi'),'<b>$1</b>')
  }

  private inputFocused = false;
  moveFocus() {
      this.inputFocused = true;
      setTimeout(() => {this.inputFocused = false});
  }

  onSubmit(obj:any) {
    console.log(obj.Note);
    this.WFroutes.forEach(element => {
      if (element.RoleFrom == this.WFinvoice.RoleID && element.Action == -1){
        console.log(this.WFinvoice.Status);
        this.WFinvoice.Status = false;
        this.WFService.update(this.WFinvoice).subscribe(result => {
          this.WFinvoice.Status = !this.WFinvoice.Status;
          this.WFinvoice.RoleID = element.RoleTo;
          this.WFService.add(this.WFinvoice).subscribe(res => {
            console.log("success");
            console.log(res);
            this.notesdat.SupplierInvoiceID = this.id;
            this.notesdat.Notes = obj.Note;
            this.notesService.add(this.notesdat).subscribe(result =>{
              console.log(result);
              this.SendMail();
              this.close();
              //display success...
              location.reload();
            });
          });
        });
      }
    });

    /*this.WFService.add(this.WFinvoice).subscribe(res => {
      console.log(res);
      
    }); */ 
  }

  getWFInvoice(invoiceid){
    this.WFService.getById(invoiceid).subscribe(results =>{
      this.WFinvoice = results;
      this.WFinvoice.Status = false;
      console.log(this.WFinvoice);


      this.routeservice.getRoutesbyRoleID(this.WFinvoice.RoleID).subscribe(result => {
        result.forEach(element => {
          delete element.Routes;
        });
        this.WFroutes = result;
      });
    });

    

  }

  SendMail(){
    this.mailService.SendMail(this.mail).subscribe(resp => {
      console.log(resp)
    });
  }  

  

}
