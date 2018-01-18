import { AfterContentChecked,AfterViewChecked,ChangeDetectorRef,Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusService } from "./../../services/status.service";
import { DialogService } from "ng2-bootstrap-modal";
import { AlertComponent } from './../../popup/alert.component';
import { ErrorComponent } from '../../popup/error.component';
import { WorkflowProcessService } from './../../services/workflow-process.service';
import { error } from 'util';
import { forEach } from '@angular/router/src/utils/collection';
import { SupplierInvoiceService } from './../../services/supplier-invoice.service';
import {Invoice } from './../../interfaces/invoice';
import { Departmnt } from './../../interfaces/department';
import { WorkflowRouteService } from './../../services/workflow-route-service';
import { InvoiceApprovalComponent } from './../../popup/invoice-approval.component';


@Component({
  selector: 'app-workflowhome',
  templateUrl: './workflowhome.component.html',
  styleUrls: ['./workflowhome.component.css']
})

export class WorkflowhomeComponent implements OnInit {
  //isAction = false;
  pageSize: number = parseInt(localStorage.getItem("pageSize"));
  currentPage = 1;
  flowprocess:any = [];
  wkflowHasData:boolean = false;
  isLoading: boolean = false;
  type: string = "ALL";
  flag:number = 3;
  order: boolean=true
  DeptHasData :boolean = false;
  DeptData:any = [];
  DeptInvoices:any = [];
  message: string = "Loading workflow, please wait...";
  statuses: any = [];
  invoice: any = {};
  dpt: any = {"wf":{}};
  homeView: boolean = true;
  
  constructor(private flowservice:WorkflowProcessService,
              private dialogService: DialogService,
              private invoiceservice:SupplierInvoiceService,
              private routeservice:WorkflowRouteService) { }

  ngOnInit() {
    this.loadDeptWorkflowData();
  }
  
  loadDeptWorkflowData()
  {
    this.isLoading = true;
    this.flowservice.getDepartmentWorkflow(this.currentPage,this.pageSize, 1).subscribe(result => {
      console.log(result);
      if(result != null)
      {
        this.flowprocess = result;
        this.DisplayDepts(this.flowprocess);
      }
      this.isLoading = false;
    },error => {
      error = error.jason();
      this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load work flow process`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
      this.isLoading = false;
    });
  }

  DisplayDepts(resultList:any)
  {
    resultList.forEach(element => {
        delete element.Allprocesses;
        if (element.Status == true && element.RoleID == 1/* && element.RoleID == userRole allowed to access this ID. NB this can be verified through DB*/){
          this.invoiceservice.getSpecificSubID(element.SubmissionID, element.SupplierInvoiceID, true).subscribe(result => {
            this.createInvoiceList(result, element);
            this.DeptData = result;
            this.DeptHasData = true;
          });
        }
    });
  }

  createInvoiceList(resultList:any,element:any){
    resultList.forEach(source =>{
      this.dpt = source;
      this.dpt.wf = element;
      this.DeptInvoices.push(this.dpt);
    });

    console.log("Aowa");
    console.log(this.DeptInvoices);
  }

  taskForward(item: any){
    this.invoice = item;
    this.invoice.showAdminOpt = true;
    this.routeservice.getRoutesbyRoleID(this.invoice.wf.RoleID).subscribe(result => {
      result.forEach(element => {
        delete element.Routes;
      });
      this.invoice.routeData = result;
      this.homeView = false;
      console.log("here");
      console.log(this.invoice);
    });       
  }

  rejectInvoice(id:number){
    localStorage.setItem("invid",id.toString());
    this.dialogService.addDialog(InvoiceApprovalComponent, { title: 'Reject ', message: `Comments`});
  }

}