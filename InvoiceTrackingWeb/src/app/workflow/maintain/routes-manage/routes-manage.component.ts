import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertComponent } from '../../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { ErrorComponent } from '../../../popup/error.component';
import { RolesServiceService} from './../../../services/role-service.service';
import { WorkflowStatusservice } from './../../../services/workflow-status-service';
import { WorkflowRouteactionsService} from './../../../services/workflow-routeactions.service';
import { ViewRoutesService } from './../../../services/view-routes.service';
import { WorkflowRouteService } from './../../../services/workflow-route-service';
@Component({
  selector: 'app-routes-manage',
  templateUrl: './routes-manage.component.html',
  styleUrls: ['./routes-manage.component.css']
})
export class RoutesManageComponent implements OnInit {
  form;
  routes:any={};
  roles:any = [];
  statuses:any =[];
  routeactions:any =[];
  page: number = 1;
  pageSize = parseInt(localStorage.getItem("pageSize"));
  sub: any;
  lblHeading: string = "";
  btnActionLabel: string = "";
  isEdit: boolean;
  isLoading : boolean = false;
  viewruts:any={};
  addruts:any={};
  userId : number = 1;
  isduplicate:boolean = false;
  constructor(private roleservice: RolesServiceService,private statusservice:WorkflowStatusservice,
  private actionservice:WorkflowRouteactionsService, private router: Router, 
  private  route: ActivatedRoute,private viewrutservice:ViewRoutesService,private dialogService: DialogService,
   private wrkflowrutservice:WorkflowRouteService ) { }

  ngOnInit() {
    this.initForm();
    this.getRoles();
    this.getStatus();
    this.getRouteActions();

    this.sub = this.route.params.subscribe(params => {
      if (this.router.url.indexOf('edit') != -1) {
          this.isEdit = true;
          this.lblHeading = "Update Workflow Route";
          this.btnActionLabel = "Update";
          let id = +params['id'];
          this.isLoading = true; 
          this.viewrutservice.getById(id).subscribe(result => {
              this.viewruts = result;
             // console.log(this.viewruts);
            
              (<FormGroup>this.form)
                  .setValue({
                      ID: this.viewruts.ID, wrkRolefrom: this.viewruts.RoleFrom,
                      wrkRoleto: this.viewruts.RoleTo,
                      wrkStatus:this.viewruts.WFStatusToID,
                      wrkAction:this.viewruts.Action 
                  }, { onlySelf: true }); 
                  this.isLoading = false;
          },error => {
              error = error.json();
              this.isLoading = false;
              this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load data for selected route`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
              this.navigate();
          });
      }
      else {
          this.isEdit = false;
          this.lblHeading = "Add a new Route";
          this.btnActionLabel = "Save";
      }

  });
  }
  getRoles(){
     this.roleservice.GetAllRoles(this.page,this.pageSize).subscribe(result =>{
       this.roles = result;
     })
  }
  getStatus(){
  this.statusservice.getAllStatus(this.page,this.pageSize).subscribe(result =>{
   this.statuses = result;
   
  })
  }
  getRouteActions(){
   this.actionservice.getAllActions(this.page,this.pageSize).subscribe(result =>{
  this.routeactions = result;
  //console.log(this.routeactions);
   })
  }
  navigate() {
    this.router.navigateByUrl("routes");    
 }
  initForm() {      
    this.form = new FormGroup({
      wrkRolefrom: new FormControl(this.routes.wrkRolefrom , Validators.compose([
            Validators.required
            ])),
     wrkRoleto: new FormControl(this.routes.wrkRoleto, Validators.compose([
              Validators.required
              ])),
    wrkStatus: new FormControl(this.routes.wrkStatus, Validators.compose([
                Validators.required
                ])), 
    wrkAction: new FormControl(this.routes.wrkAction, Validators.compose([
                  Validators.required
                  ])), 
                 
        
        ID: new FormControl(this.routes.ID),
  
    });
  }
  onSubmit(ruts:any){
   // if (this.isDup) return;
    this.addruts.RoleFrom = ruts.wrkRolefrom;
    this.addruts.RoleTo =  ruts.wrkRoleto;
    this.addruts.Action = ruts.wrkAction;
    this.addruts.WFStatusToID = ruts.wrkStatus;
    
    this.isLoading = true;
    if (this.isEdit) {
        this.addruts.ID = this.viewruts.ID; 
        this.addruts.DateUpdated = new Date();
        this.addruts.UserUpdated = this.userId;
        this.addruts.DateCreated = this.viewruts.DateCreated;
        this.addruts.UserCreated = this.viewruts.UserCreated;
        this.DuplicateCheck(this.addruts);
       } else {
      this.addruts.Enabled = 1;
      this.addruts.DateUpdated = this.addruts.DateCreated = new Date();
      this.addruts.UserUpdated  = this.userId;
      this.addruts.UserCreated = "Praxis";
      this.DuplicateCheck(this.addruts);
    }
  }
  DuplicateCheck(obj:any){
 this.wrkflowrutservice.CheckDuplicates(obj.RoleFrom,obj.RoleTo,obj.Action,obj.WFStatusToID).subscribe(result => {
  this.isduplicate = result.Duplicate;
 
if(this.isduplicate == true ){
 this.dialogService.addDialog(AlertComponent, { title: 'Add Route Duplicate Error', message: `Route already exist` }); 
 this.isLoading = false;        
  }
if(this.isduplicate == false && !this.isEdit ){
    this.wrkflowrutservice.add(this.addruts).subscribe(result => {
        this.addruts = result;
        this.dialogService.addDialog(AlertComponent, { title: 'Add a Route', message: `Route ${this.addruts.RoleFrom} added succesfully.`, bold: `${this.addruts.RoleFrom}` });              
        this.isLoading = false;            
        this.navigate();
    },error => {
        error = error.json();
        this.dialogService.addDialog(ErrorComponent, { title: 'Add error', message: `Could not add route ${this.addruts.RoleFrom}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
        this.isLoading = false;
        this.navigate();
    });    
  }
  if(this.isduplicate == false && this.isEdit ){
    this.wrkflowrutservice.update(this.addruts).subscribe(result => {
        this.addruts = result;
        this.dialogService.addDialog(AlertComponent, { title: 'Update Route', message: `Route ${this.addruts.RoleFrom} updated succesfully.`, bold: `${this.addruts.RoleFrom}` });
        this.isLoading = false;
        this.navigate();
    },error => {
        error = error.json();
        this.dialogService.addDialog(ErrorComponent, { title: 'Update error', message: `Could not update route ${this.addruts.RoleFrom}`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
        this.isLoading = false;
        this.navigate();
    }); 
  }
 })
  }

  onChange(){
    
  }

}
