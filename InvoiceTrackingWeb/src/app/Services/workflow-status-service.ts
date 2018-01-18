import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class WorkflowStatusservice extends Baseservice {

    constructor(http:Http) { 
        super("WorkflowStatus", http);
      }
      getAllStatus(page, pageSize){
        return this.getRequest(this.url + "/" + page + "/" + pageSize);
      }
    
    
}