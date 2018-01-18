import { Injectable, Inject } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';


import { Baseservice } from './baseservice';

@Injectable()
export class SupplierService extends Baseservice {

    constructor(http: Http) {
        super("suppliers", http);
    }

    getByPO(po: string) {
        return this.getByNumber(po);
    }

    getByInvoiceNumber(invNum: string) {
        return this.getByNumber(invNum, "/find/invoice/");
    }

    getByName(name: string, page: number, pageSize: number) {
        return this.searchByName(name, page, pageSize);
    }

    getByInvoiceBySupplierName(name: string, page: number, pageSize: number) {
        return this.searchByName(name, page, pageSize, "/invoice/supplier/");
    }

    searchByName(name: string, page: number, pageSize: number, endpoint: string = "/byname/") {
        return this.getRequest(this.url.concat(endpoint).concat(page + "/").concat(pageSize + "/").concat(name));
    }

    getByNumber(num: string, endpoint: string = "/find/") {
        return this.getRequest(this.url.concat(endpoint).concat(num));
    }
}
