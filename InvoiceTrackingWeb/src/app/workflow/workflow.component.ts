import { Component, OnInit } from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { WorkflowpopupComponent } from './workflowpopup.component';
import { ViewComponent } from './../search-invoice/view/view.component';

@Component({
  selector: 'app-workflow',
  templateUrl: './workflow.component.html',
  styleUrls: ['./workflow.component.css']
})
export class WorkflowComponent implements OnInit {

  isVerify:boolean = false;

  constructor(private dialogService: DialogService) { }

  ngOnInit() {
  }

  invoiceAdmin(id:number){
    this.dialogService.addDialog(WorkflowpopupComponent, { title: 'Test ', message: `Comments`});
  }

  onChange(newValue, id:number) {
    console.log(newValue);
    this.dialogService.addDialog(WorkflowpopupComponent, { title: `${newValue} `, message: `Comments`});
  }

  viewDetails(act:number){
    if (this.isVerify && act == 1){
      this.dialogService.addDialog(WorkflowpopupComponent, { title: `Verify `, message: `Comments`});
    }
    this.isVerify = !this.isVerify;
  }

}
