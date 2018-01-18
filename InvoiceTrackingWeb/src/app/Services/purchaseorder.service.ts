import { Injectable, Inject } from '@angular/core';
import { Headers, Http, RequestOptions, Response } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/Rx';

import { Baseservice } from './baseservice'

@Injectable()
export class PurchaseOrderService extends Baseservice {

    public SupInvDTO: any = [];

    constructor(http: Http) {
        super("purchaseorder", http);

    }

    getPO(id: any) {
        
        let url = (id)?this.url + "/" + id : this.url;
        return this.getRequest(url);
    
    }
    //Create new PO
    postPO(object: any) {

        let body = object;
        return this.add(body).map(response => response.json());
    }

    //Update a PO
    putPO(object: any) {

        let body = object;
        return this.update(body).map(response => response.json());
    }

    //Delete a PO
    deletePO(id: number) {

        return this.delete(id).map(response => response.json());
    }

}
