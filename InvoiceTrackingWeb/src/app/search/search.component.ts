import { Component, OnInit } from '@angular/core';
import { SupplierService } from "./../services/supplier.service";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from "ng2-bootstrap-modal";
import { AlertComponent } from './../popup/alert.component';
import { ErrorComponent } from '../popup/error.component';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})

export class SearchComponent implements OnInit {

    message: string = "Searching, please wait...";
    suppliers: any =[];   
    isPO: boolean = true;    
    index: number = 0;
    suppos: any;
    form;
    poHasData: boolean = false;
    supHasData: boolean = false;
    poHasPos: boolean = false;
    errorMsg: string;
    errorC: string;
    isError: boolean = false;
    isSelected: number = 0;
    p: number = 1;
    po: number = 1;
    count: number = 0;
    pageSize: number = 5;
    searchText: string = '';
    isSearching: boolean = false;

    constructor(private service: SupplierService, private router: Router, private route: ActivatedRoute, private dialogService: DialogService) { }

    ngOnInit() {        
        this.form = new FormGroup({
            searchText: new FormControl('', Validators.compose([
                Validators.required,
                Validators.pattern('[\\w\-\\s\\/]+')
            ]))
        });
  }

  onCheck(check: boolean) {
      this.isPO = check;
      this.po = 0;
  }

  onSearch(searchText,page:number = 1) {      
      if (this.isPO) {
          if (isNaN(searchText)) {
              this.onError({ Status: 500, Message: "PO Number must be a number." });
              return;
          }
          this.suppliers = [];
          this.isSearching = true;
          this.service.getByPO(searchText).subscribe(result => {              
            console.log(result) ;
            this.suppliers.push(result);              
              this.supHasData = (this.suppliers) ? true : false;             
              if(this.suppliers.length > 0) {
                  this.getPOObject(this.suppliers[0].ID, searchText);
              }
              this.isSearching = false;
          }, error => {              
              this.onException(error.json(), searchText);             
              this.isSearching = false;
              });          
      } else {
          this.isSearching = true;
          this.service.getByName(searchText, page, this.pageSize).subscribe(result => {              
              this.count = result.Count;              
              this.suppliers = result.Items;
              this.supHasData = (this.suppliers) ? true : false;
              this.isSearching = false;
          }, error => {                           
              this.onException(error.json(), searchText, 'Search for supplier name ','Search by supplier name failed.');
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

  getPO(obj: any) {
      if (obj.PurchaseOrders) {
          return obj.PurchaseOrders[this.index].PONumber;
      }
  }

  getAmount(obj: any) {
      if (obj.ITS_PurchaseOrder) {
          return obj.PurchaseOrders[this.index].POAmount;
      }
  }


  getPOs(id: number) {           
      let pos = this.searchPOs(id);
      this.isError = false;
      this.isSelected = id;
      this.po = 1;
      let self = this;
      self.suppos = []; 
      if (pos.PurchaseOrders) {
          pos.PurchaseOrders.forEach(function(item) {
              
              self.suppos.push(
                  {
                      'ID': item.ID, 'PONumber': item.PONumber, 'POAmount': item.POAmount, 'PODate': item.PODate, 'InvoicedAmount': item.InvoicedAmount, 'AmountOutstanding': item.AmountOutstanding
                  }
              );
          })
      }
      this.poHasPos = (self.suppos.length > 0) ? true : false;      
      if (!this.poHasPos) {
          this.onError({ Status: "404", Message: pos.Name + " has no purchase orders." });
      }            
  }

  getPOObject(id: number,search:string) {
      let pos = this.searchPOs(id);
      this.isError = false;
      this.isSelected = id;
      let self = this;
      self.suppos = [];
      if (pos.PurchaseOrders) {
          pos.PurchaseOrders.forEach(function (item) {
              if (item.PONumber.toLowerCase() === search.toLowerCase()) {
                  self.suppos.push(
                      {
                          'ID': item.ID, 'PONumber': item.PONumber, 'POAmount': item.POAmount, 'PODate': item.PODate, 'InvoicedAmount': item.InvoicedAmount, 'AmountOutstanding': item.AmountOutstanding
                      }
                  );
              }
          })
      }
      this.poHasPos = (self.suppos.length > 0) ? true : false;
      if (!this.poHasPos) {
          this.onError({ Status: "404", Message: pos.Name + " has no purchase orders." });
      }
  }

searchPOs(id: number) {           
      let pos: any;
      this.suppliers.forEach(function (item) {
          if (item.ID == id) {
              pos = item;              
          }
      });
      
      return pos;
  }

  onSubmit(formValue) {
      this.onReset();
      this.searchText = formValue.searchText.trim();
      this.onSearch(this.searchText);
  }

  onReset() {
      this.poHasData = this.supHasData = this.isError = this.poHasPos = false;
      this.p = this.po = 1;
  }

  navigate(id) {        
      this.router.navigateByUrl("purchaseorder/"+id);      

  }

  resetForm() {
      this.poHasPos = false;
      this.supHasData = this.isError = false;
      this.form.reset();
      this.p = 1;
  }

  onChanges(): void {
      this.poHasPos = false;     
      this.isSelected = -1;
  }

  onException(error, searchText, searchType ='Search for PO number', title = 'Search by PO number failed.') {                  
          this.dialogService.addDialog(ErrorComponent, { title: title, message: `${searchType} {${searchText}} was unsuccessful.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message });
  } 

}
