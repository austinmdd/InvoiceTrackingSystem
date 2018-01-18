import { Injectable } from "@angular/core";
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from "app/services/baseservice";

@Injectable()
export class WorkflowRouteactionsService extends Baseservice {

  constructor(http:Http) {
super("WorkflowRouteActions",http)
   }
   getAllActions(page:number, pageSize:number){
    return this.getRequest(this.url + "/" + page + "/" + pageSize )
    }
}
