import { Component, OnInit, Input } from '@angular/core';
import { SupplierService } from "./../services/supplier.service";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SystemSettingsService } from './../services/system-settings.service';

@Component({
  selector: 'app-search-invoice',
  templateUrl: './search-invoice.component.html',
  styleUrls: ['./search-invoice.component.css'],
 
})

export class SearchInvoiceComponent implements OnInit {
   
    invoices: any = [];
    isInvNum: boolean = true;   
    invoice: any;
    form;   
    invHasData: boolean = false;    
    errorMsg: string;
    errorC: string;
    isError: boolean = false;
    isSelected: number = 0;
    p: number = 1;   
    count: number = 0;
    pageSize: number = parseInt(localStorage.getItem("pageSize"));
    searchText: string = '';
    isSearching: boolean = false;
    isView = false;
    InvoiceNumber: number = 0;
    message: string = "Searching, please wait...";
    sub: any;
    constructor(private service: SupplierService,
                private router: Router,
                private route: ActivatedRoute,
                private sysSettings: SystemSettingsService 
    ) {

    }

    ngOnInit() {
        this.form = new FormGroup({
            searchText: new FormControl('', Validators.compose([
                Validators.required,
                Validators.pattern('[\\w\-\\s\\/]+')
            ]))
        });

        
        this.sub = this.route.params.subscribe(params => {
            if(params['inv'] != null){
                if (params['inv'].length > 0 ){
                    let sText = params['inv'];
                    this.onSearch(sText.toString());
                }
            }
        });
    }

    onCheck(check: boolean) {
        this.isInvNum = check;  
    }

    onHideView() {
        this.isView = false;
    }

    onSearch(searchText, page: number = 1) {
        this.isSelected = -1;
        if (this.isInvNum) {            
            this.invoices = [];
            this.isSearching = true;
            this.service.getByInvoiceNumber(searchText).subscribe(result => {
                this.invoices.push(result);
                this.invHasData = (this.invoices) ? true : false;
                this.isSearching = false;
            }, error => {
                this.onError(error.json());
                this.isSearching = false;
            });
        } else {
            this.isSearching = true;
            this.service.getByInvoiceBySupplierName(searchText, page, this.pageSize).subscribe(result => {
                this.count = result.Count;                
                this.invoices = result.Items;
                this.invHasData = (this.invoices) ? true : false;
                this.isSearching = false;
            }, error => {
                this.onError(error.json());
                this.isSearching = false;
            });
        }
        return page;
    }

    onError(err) {
        this.errorC = (err.Status == "404") ? "text-warning" : "text-danger";
        this.errorMsg = err.Message;
        this.isError = (this.errorMsg) ? true : false;
    }

    onShowView(id: number) {
        this.getInvoice(id);
        this.isSelected = id;       
        this.isView = true;
    }
    onSendID(id: number) {
        this.router.navigateByUrl("search-invoice/update/" + id);
    }

    getInvoice(id: number) {
        let self = this;
        this.invoices.forEach(inv => {
            
            if (inv.ID == id) {               
                self.invoice = inv;
                this.InvoiceNumber = inv.InvoiceNumber;
                return;
            }
        });
    }
    
    onSubmit(formValue) {
        this.onReset();
        this.searchText = formValue.searchText.trim();
        this.onSearch(this.searchText);
    }

    onReset() {
        this.invHasData = this.isError = false;
        this.p = 1;
    }
  

    resetForm() {        
        this.invHasData = false;
        this.isSelected = -1;
        this.form.reset();
    }

    onChanges(): void {
        this.invHasData = false;
        this.isSelected = -1;
    }
}
