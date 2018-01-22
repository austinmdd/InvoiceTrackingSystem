import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertComponent } from '../../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ErrorComponent } from '../../../popup/error.component';
import { WorkflowRuleService } from './../../../services/workflow-rule.service';

@Component({
  selector: 'app-rule-manage',
  templateUrl: './rule-manage.component.html',
  styleUrls: ['./rule-manage.component.css']
})
export class RuleManageComponent implements OnInit {
  form;
  rule: any = {};
  sub: any;
  lblHeading: string = "";
  btnActionLabel: string = "";
  isEdit: boolean;
  userId: number = 1;
  isDup: boolean = false;
  isLoading: boolean = false;
  message = "Loading...";
  userCreated: string = "Praxis";
  
  constructor(private ruleservice: WorkflowRuleService, 
    private router: Router, 
    private  route: ActivatedRoute,
    private dialogService: DialogService) { }

  ngOnInit() {
    this.initForm();
    this.onChanges();
    this.sub = this.route.params.subscribe(params => {
      if (this.router.url.indexOf('edit') != -1) {
          this.isEdit = true;
          this.lblHeading = "Update Compliance Checklist";
          this.btnActionLabel = "Update";
          let id = +params['id'];
          this.isLoading = true; 
          this.ruleservice.getById(id).subscribe(result => {
              this.rule = result;
              (<FormGroup>this.form)
                  .setValue({
                    ID: this.rule.ID, ChecklistName: this.rule.ChecklistName
                  }, { onlySelf: true }); 
                  this.isLoading = false;
          },error => {
              error = error.json();
              this.isLoading = false;
              this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load data for selected Compliance Checklist`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
              this.navigate();
          });
      }
      else {
          this.isEdit = false;
          this.lblHeading = "Add a new Compliance Checklist";
          this.btnActionLabel = "Save";
      }

  });
  }
  navigate() {
    this.router.navigateByUrl("checklist");      
 }
 initForm() {      
  this.form = new FormGroup({
    ChecklistName: new FormControl(this.rule.ChecklistName, Validators.compose([
          Validators.required, <any>Validators.maxLength(50),
          Validators.pattern('[\\w\-\\s\\/]+')
      ])),
      ID: new FormControl(this.rule.ID)
  });
}
ngOnDestroy() {
  this.sub.unsubscribe();
}
onSubmit(obj:any) {
  if (this.isDup) return;
  this.rule.ChecklistName = obj.ChecklistName;
  this.isLoading = true;
  if (this.isEdit) {
      this.rule.Enabled = true;
      this.rule.UserUpdated = this.userId;
      this.rule.UserCreated = this.userCreated;
       this.rule.DateUpdated = new Date();
       this.ruleservice.update(this.rule).subscribe(result => {
           this.rule = result;
           this.dialogService.addDialog(AlertComponent, { title: 'Update a Compliance Checklist', message: `Compliance Checklist ${this.rule.ChecklistName} updated succesfully.`, bold: `${this.rule.ChecklistName}` });
           this.isLoading = false;
           this.navigate();
       },error => {
           error = error.json();
           this.dialogService.addDialog(ErrorComponent, { title: 'Update error', message: `Could not update Compliance Checklist ${this.rule.ChecklistName}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
           this.isLoading = false;
           this.navigate();
       });
  } else {
       this.rule.Enabled = true;
       this.rule.DateUpdated = this.rule.DateCreated = new Date();
       this.rule.UserUpdated  = this.userId;
       this.rule.UserCreated = this.userCreated;
      this.ruleservice.add(this.rule).subscribe(result => {
           this.rule = result;
           this.dialogService.addDialog(AlertComponent, { title: 'Add Compliance Checklist', message: `Compliance Checklist ${this.rule.ChecklistName} added succesfully.`, bold: `${this.rule.ChecklistName}` });              
           this.isLoading = false;            
           this.navigate();
       },error => {
           error = error.json();
           this.dialogService.addDialog(ErrorComponent, { title: 'Update error', message: `Could not add Compliance Checklist ${this.rule.ChecklistName}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
           this.isLoading = false;
           this.navigate();
       });
  }
}

onChanges(): void {
  this.form.get('ChecklistName').valueChanges.subscribe(val => {
     
      if (val.length > 3) {
          this.ruleservice.getByType(val).subscribe(result => {                  
              this.isDup = (result.Status && !this.isEdit) ? true : false;
          }, error => {                 
              this.isDup = false;
          });
      }
  });
}

}
