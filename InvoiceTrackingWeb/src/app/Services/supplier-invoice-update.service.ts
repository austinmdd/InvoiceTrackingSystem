import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class SupplierInvoiceUpdateService extends Baseservice {

  constructor(http:Http) { 
    super("SupplierInvoice", http);
  }


  /*(id: number) {
    return this.getRequest(this.url + "/" +  id);
  }*/

}
