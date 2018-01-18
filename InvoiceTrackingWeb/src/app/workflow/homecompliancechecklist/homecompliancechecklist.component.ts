import { Component, OnInit,Input, EventEmitter } from '@angular/core';
import { WorkflowProcessService } from './../../services/workflow-process.service';
//import { RoleLinkService } from './../../services/role-link.service';


@Component({
  selector: 'app-homecompliancechecklist',
  templateUrl: './homecompliancechecklist.component.html',
  styleUrls: ['./homecompliancechecklist.component.css']
})
export class HomecompliancechecklistComponent implements OnInit {
  
  @Input() invoice:any;

  oldWFObj: any = {};

  constructor(private flowprocess: WorkflowProcessService//,
              //private roleLinkService: RoleLinkService
            ) {
  }

  ngOnInit() {
    this.oldWFObj = this.invoice.wf;
    console.log(this.oldWFObj);
  }


  getRouteData(){
    this.invoice.routeData.forEach(routeDat => {
      if (routeDat.Action == 0 && routeDat.RoleFrom == this.oldWFObj.RoleID){
        this.oldWFObj.Status = !this.oldWFObj.Status;      
        this.flowprocess.update(this.oldWFObj).subscribe(result => {
          console.log(result);
          this.updateWorkflow(routeDat.RoleTo);
        },error => {
            error = error.json();
            console.log(error);
            //this.dialogService.addDialog(ErrorComponent, { title: 'Load error', message: `Could not load invoice categories`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
        });
      }else{
        //display pop up error. action not allowed, contact administrator...
      }
    }); 
  }

  updateWorkflow(newRoleID:number)
  {
    this.oldWFObj.Status = !this.oldWFObj.Status;
    this.oldWFObj.RoleID = newRoleID;
    this.flowprocess.add(this.oldWFObj).subscribe(res => {
      console.log(res);
    });
  }

  getCompliance(){
    
  }
  

}
