import { AfterContentChecked,AfterViewChecked,ChangeDetectorRef,Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertComponent } from './../../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { SystemSettingsService } from './../../../services/system-settings.service';
import { GlobalsService } from './../../../services/globals.service';
import { ErrorComponent } from '../../../popup/error.component';
import { error } from 'util';
import { WorkflowRouteService } from './../../../services/workflow-route-service';
import { RolesServiceService } from './../../../services/role-service.service';
import { ViewRoutesService } from './../../../services/view-routes.service';

@Component({
  selector: 'app-routes',
  templateUrl: './routes.component.html',
  styleUrls: ['./routes.component.css']
})
export class RoutesComponent implements OnInit {
  page: number = 1;
  pageSize = parseInt(localStorage.getItem("pageSize"));
  order:boolean;
  isLoading: boolean = false;
  allRoutes: any = [];
  IsSearchDataFound: boolean = false;
  removeRoute:any ;
  lblHeading: string = "";
  isRemove:boolean = false;
  deletedRoute:any = {};
  message = "Loading routes...";
  direction: boolean = false;
  searchText: string ='';
  searchResults: any = [];
 
  constructor(private routeservice:WorkflowRouteService,private roleservice:RolesServiceService,
  private viewroutesservice:ViewRoutesService,private dialogService:DialogService) { }

  ngOnInit() {
    this.onChangeHeandig();
    this.getRoutes();
  }
  getRoutes(){
    this.isLoading = true;
    this.viewroutesservice.GetRoutes(this.page,this.pageSize).subscribe(result =>{
     this.allRoutes = result;
     //let newaction = this.allRoutes.Action;
     this.isLoading = false;
    })
  }
  onConfirm(rule:any){
    this.removeRoute = rule;
    this.isRemove = true;
    this.onChangeHeandig();
  }
  onDelete(obj:any){
  this.isLoading = true;
  obj.Enabled = false;
  this.deletedRoute.ID = obj.ID;
  this.deletedRoute.RoleFrom = obj.RoleFrom;
  this.deletedRoute.RoleTo = obj.RoleTo;
  this.deletedRoute.Action = obj.Action;
  this.deletedRoute.WFStatusToID = obj.WFStatusToID;
  this.deletedRoute.Enabled = obj.Enabled;
  this.deletedRoute.UserCreated = obj.UserCreated;
  this.deletedRoute.UserUpdated ="Praxis";
  this.deletedRoute.DateCreated = obj.DateCreated
  this.deletedRoute.DateUpdated = new Date();
  this.routeservice.update(this.deletedRoute).subscribe(result =>{
   this.deletedRoute = result;
   this.isLoading = true;
   this.dialogService.addDialog(AlertComponent, { title: 'Delete route', message: `Route ${obj.RoleFromName} deleted.`, bold: `${obj.RoleFromName}` });                        
   this.onNo();
   this.getRoutes();
   this.isLoading = false;
  },error =>{
    error = error.json();
      this.dialogService.addDialog(ErrorComponent, { title: 'Delete route', message: `Route ${obj.RoleFromName } was not deleted.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
      this.isLoading = false;
  });
  }
  onChangeHeandig(change: boolean = true) {
    this.lblHeading = (change) ? "Routes" : "Delete Route";
  }
  onNo() {
    this.isRemove = false;
    this.onChangeHeandig();
  }
  onSort(){
    this.direction = !this.direction;
  }
  resetForm() {
    this.searchText = null;
    this.IsSearchDataFound = false;
  
  }
  SearchRoute(){
    this.viewroutesservice.Searchroute(this.page,this.pageSize,this.searchText.trim()).subscribe(result =>{
    this.searchResults = result;
    this.IsSearchDataFound = true;
    },error => {
      error = error.json();
      this.dialogService.addDialog(ErrorComponent, { title: 'Search error', message: `Could not find route`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
      this.isLoading = false;
    })

  }
}
