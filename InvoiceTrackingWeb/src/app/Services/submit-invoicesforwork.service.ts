import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class NewInvoiceSubmittedsservice extends Baseservice {

    constructor(http:Http) { 
        super("InvoiceCategories", http);
      }
      getAllFixed(page, pageSize){
        return this.getRequest(this.url + "/" + page + "/" + pageSize);
      }
      getByType(type: string) {
        return this.getRequest(this.url + "/bycategory/" + type);
      }
    
}