import { Component, OnInit,Input, EventEmitter } from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { InvoiceApprovalComponent } from './../../popup/invoice-approval.component';

@Component({
  selector: 'view-invoice',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
    @Input() invoice: any;
    showAdminOpt: boolean = false;       

    constructor(private dialogService: DialogService) { }

    ngOnInit() {
        console.log(this.invoice);
        this.showAdminOpt = this.invoice.showAdminOpt;
    }

    invoiceAdmin(adminOpt){
        localStorage.setItem("invid",this.invoice.wf.ID);
        //localStorage.setItem("adminOpt", adminOpt);
        //localStorage.setItem("supName", this.invoice.Supplier.Name);
        //localStorage.setItem("mailAdd", this.invoice.Supplier.VendorPortalID);
        if (adminOpt == "Reject"){
            this.dialogService.addDialog(InvoiceApprovalComponent, { title: 'Reject ', message: `Comments`});
        }
        if (adminOpt == "Approve"){
            //this.dialogService.addDialog(InvoiceApprovalComponent, { title: 'Approve', message: `Comments`});
        }
                
    }

    tempStore(){
        
    }


}