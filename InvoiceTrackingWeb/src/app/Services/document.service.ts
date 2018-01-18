import { Injectable, Inject } from '@angular/core';
import { Headers, Http, RequestOptions, Response, ResponseContentType} from '@angular/http';
import {Observable} from "rxjs/Observable";
import "rxjs/Rx";

import { Baseservice } from './baseservice'

@Injectable()
export class DocumentService extends Baseservice {

    constructor(http: Http) {
        super("document",http);
    }

    get() {
        return this.getDocument(this.url + "/document");
    }

    getById(id: any) {
        return this.getDocument(this.url + "/find/" + id);
    }

    getDocument(url: string) {
        return this.getRequest(url).map(response => response); 
    }

    //Create new document
    postDocument(object : any, typeid: any, invoiceid: any) {

        let body = object;
        return this.http.post(this.url, body).map(response => response.json());
    }

    //Update a document
    putDocument(object) {

        let body = object;
        return this.update(body).map(response => response.json());
    }

    //Delete a document
    deleteDocument(id: number) {

        return this.delete(id).map(response => response);
    }

    getAttachements(id: number){

        let url = this.url + "/list/" + id
        return this.getDocument(url);
    }

    downloadFiles(id: number){

        return this.http.get(this.url + "/find/" + id).map(response => response.json());    
    } 

    DeleteFromTemp(object){

        let body = object

        console.log(object)
        return this.http.post(this.url + "/destroy", body).map(response => response.json());
    }
}

