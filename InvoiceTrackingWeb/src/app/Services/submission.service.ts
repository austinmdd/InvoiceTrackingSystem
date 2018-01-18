import { Injectable, Inject } from '@angular/core';
import { Headers, Http, RequestOptions, Response} from '@angular/http';
import {Observable} from "rxjs/Observable";
import "rxjs/Rx";

import { Baseservice } from './baseservice'

@Injectable()
export class SubmissionService extends Baseservice {


    constructor(http: Http) {
        super("submission",http);
    }

    get() {
        return this.getSubmission(this.url + "/submission");
    }

    getById(id: any) {
        return this.getSubmission(this.url + "/submission/find/" + id);
    }


    getSubmission(url: string) {

        return this.getRequest(url).map(response => response.json());
    }

    //Create new submission
    postSubmission(object : any) {
        let body = object;
        return this.add(body)
    }

    //Update a submission
    putSubmission(object) {

        let body = object;
        return this.update(body).map(response => response.json());
    }

    //Delete a submission
    deleteSubmission(id: number) {

        return this.delete(id).map(response => response.json());
    }
 
}
