import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice'

@Injectable()
export class DocumenttypeService extends Baseservice {
    constructor(http: Http) {
        super("documenttypes", http);
    }

    getByType(type: string) {
        return this.getRequest(this.url + "/bytype/" + type);
    }

    getLinkedAndUnlinkedDoctypes(invCatId: number) {
        return this.getRequest(this.url + "/links/" + invCatId);
    }

    addToLinks(body: any) {
        return this.http.post(this.url + "/links/", body, this.options).map(response => response.json());
    }

    deleteLink(id: number) {
        return this.http.delete(this.url + "/links/" +  id).map(response => response.json());
    }
    updateLink(body: any) {
        return this.http.put(this.url + "/links/", body, this.options).map(response => response.json());
    }

    getLinkById(id: number) {
        return this.getRequest(this.url + `/links/find/${id}`);
    }
    
    getAllFixed(page, pageSize){
        return this.getRequest(this.url + "/" + page + "/" + pageSize);
    }


    
    
}
