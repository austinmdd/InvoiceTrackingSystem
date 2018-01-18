import { AfterContentChecked,AfterViewChecked,ChangeDetectorRef,Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { SupplierInvoiceService } from '../services/supplier-invoice.service'
import { StatusService } from "./../services/status.service";
import { DialogService } from "ng2-bootstrap-modal";
import { AlertComponent } from './../popup/alert.component';
import { ErrorComponent } from '../popup/error.component';
import { Invoice } from './../interfaces/invoice';

@Component({
  selector: 'app-list-invoices',
  templateUrl: './list-invoices.component.html',
  styleUrls: ['./list-invoices.component.css']
})
export class ListInvoicesComponent implements OnInit {

    pageSize: number = parseInt(localStorage.getItem("pageSize"));
    currentPage = 1;
    invoices: any = [];
    invHasData: boolean = true;
    count: number = 0;
    isLoading: boolean = false;
    message: string = "Loading invoices, please wait...";
    statuses: any = [];
    selectedStatus: string;
    isError: boolean = false;
    criteria = "InvoiceDate";
    direction: boolean = true;
    rankCtrl: number = 3;
    flags = { flagIN: 1, flagIA: 1, flagID: 1, flagIS: 1, flagPO: 1, flagSN: 1 };
    ranks = { flagIN: 1, flagIA: 2, flagID: 3, flagIS: 4, flagPO: 5, flagSN: 6 };
    sortLabels = { flagIN: 'InvoiceNumber', flagIA: 'InvoiceAmount', flagID: 'InvoiceDate', flagIS: 'Status', flagPO: 'PONumber', flagSN: 'SupplierName' };
    invoice: any;
    isView = false;
    temp: any = [];

    constructor(private ref: ChangeDetectorRef,private invoiceSVC: SupplierInvoiceService, private router: Router, private statusService: StatusService, private dialogService: DialogService) { }

    ngOnInit() {        
        this.loadStatus();
        this.onNext();       
    }

    

    onNext(page: number = 1, type: string = "All", flag: number=3, order: boolean=true) {        
        this.isLoading = true;
        this.invoices = [];
        this.count = 0;        
        this.invoiceSVC.getAllInvoicesSortPaging(this.currentPage, this.pageSize, type, flag, order).subscribe(
            result => {                               
                this.createInvoiceSortableList(result.Items);
                this.temp = result;
                console.log(this.temp);
                this.count = result.Count;
                this.isLoading = false;
                this.onExcuteSort(this.rankCtrl);
            },
            error => {
                this.isLoading = false;
                if (type != "All") {
                    error = error.json();                    
                    this.dialogService.addDialog(ErrorComponent, { title: 'No Invoices Found', message: `No invoices found for invoice category ${type}.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message });                   
                }
            }
        );
        this.ref.detectChanges();
        return page;
    }

    navigate(id) {
        let self = this;
        this.temp.Items.forEach(function(item) {
            if (item.ID == id){
                self.invoice = item;
                self.invoice.showAdminOpt = false;
                self.isView = true;
                console.log(self.invoice);
            }
        });
    }

    reload(){
        this.isView = false;
    }

    loadStatus() {       
        this.statusService.getAll().subscribe(result => {
            this.statuses = result;
        });
    }

    onReload(event) {        
        this.currentPage = 1;       
        let status = event.split(':');
        this.selectedStatus = (status[0] != 0) ? status[1].trim() : "All";        
        this.onNext(this.currentPage, this.selectedStatus);
    }

    onReloadNext(page: number) {
        this.onNext(this.currentPage, this.selectedStatus, this.rankCtrl, this.direction);
        return page;
    }

    onSort(rank: number) {        
        this.direction = !this.direction;       
        this.rankCtrl = rank;
        this.onExcuteSort(rank);        
    }

    createInvoiceSortableList(sourceList: any) {
        sourceList.forEach(source => {
            let destination: Invoice = {
                ID: source.ID,
                InvoiceNumber: source.InvoiceNumber,
                InvoiceAmount: source.InvoiceAmount,
                InvoiceDate: source.InvoiceDate,
                Status: source.Status,
                PONumber: source.PurchaseOrder.PONumber,
                SupplierName: source.Supplier.Name,
                Description: source.Description
            };
            this.invoices.push(destination);
        });              
    }

    onResetFlags() {
        this.flags = { flagIN: 1, flagIA: 1, flagID: 1, flagIS: 1, flagPO: 1, flagSN: 1 };
    }

    onExcuteSort(rank: number) {
        this.onResetFlags();
        let sort = (this.direction) ? 3 : 2;
        this.criteria = null;
        switch (rank) {
            case this.ranks.flagIA: {
                this.criteria = this.sortLabels.flagIA;
                this.flags.flagIA = sort;
                break;
            }
            case this.ranks.flagIN: {
                this.criteria = this.sortLabels.flagIN;
                this.flags.flagIN = sort;
                break;
            }
            
            case this.ranks.flagIS: {
                this.criteria = this.sortLabels.flagIS;
                this.flags.flagIS = sort;
                break;
            }
            case this.ranks.flagPO: {
                this.criteria = this.sortLabels.flagPO;
                this.flags.flagPO = sort;
                break;
            }
            case this.ranks.flagSN: {
                this.criteria = this.sortLabels.flagSN;
                this.flags.flagSN = sort;
                break;
            }

            default: {
                this.criteria = this.sortLabels.flagID;
                this.flags.flagID = sort;
                break;
            }
                           
        }
    }


    
}
