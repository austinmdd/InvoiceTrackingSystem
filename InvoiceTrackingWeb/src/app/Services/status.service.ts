import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';

import { Baseservice } from './baseservice'

@Injectable()
export class StatusService extends Baseservice {

    constructor(http: Http) {
        super("statuses", http);
    }

    getByStatus(status: string) {
        return this.getRequest(this.url + "/bystatus/" + status);
    }

}
