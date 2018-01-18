import { Injectable, Inject } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';


import { Baseservice } from './baseservice';

@Injectable()
export class SupplierInvoiceService extends Baseservice {

    constructor(http: Http) {
        super("supplierinvoice", http);
    }


    getAllInvoicesPaging(page: number, pageSize: number,type:string) {
        return this.getRequest(this.url.concat("/all/").concat(page + "/" + pageSize+"/"+type));
    }

    getAllInvoicesSortPaging(page: number, pageSize: number, type: string, flag: number, order: boolean) {
        return this.getRequest(this.url.concat(`/sort/${page}/${pageSize}/${type}/${flag}/${order}`));
    }
    getAllInvoicesforSubmissionID(page: number, pageSize: number,subId:number, type: string, flag: number, order: boolean) {
        return this.getRequest(this.url.concat(`/wrkflow/${page}/${pageSize}/${subId}/${type}/${flag}/${order}`))
    }

    getSpecificSubID(id: number, supInvID: number, oneRec: boolean) {
        return this.getRequest(this.url.concat(`/wrkflow/${id}/${supInvID}/${true}`));
    }


}
