import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class WorkflowProcessService extends Baseservice {

    constructor(http:Http) { 
        super("WorkflowProcess", http);
      }
      getAllFixed(page, pageSize){
        return this.getRequest(this.url + "/" + page + "/" + pageSize);
      }
      getByType(type: string) {
        return this.getRequest(this.url + "/bycategory/" + type);
      }
      getDepartmentWorkflow(page: number, pageSize: number, roleID: number)
      {
        return this.getRequest(this.url + "/" + page + "/" + pageSize + "/" + roleID);
      }
    
}