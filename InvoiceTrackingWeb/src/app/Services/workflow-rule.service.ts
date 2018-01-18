import { Injectable } from "@angular/core";
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from "app/services/baseservice";

@Injectable()
export class WorkflowRuleService extends Baseservice{
    constructor(http:Http){
    super("WorkflowRule",http)
    }
    getAllRules(page:number, pageSize:number){
    return this.getRequest(this.url + "/" + page + "/" + pageSize )
    }
    getByType(type: string) {
        return this.getRequest(this.url + "/byname/" + type);
      }
      Searchrule(page: number, pageSize: number,searchtype:string){
        return this.getRequest(this.url.concat(`/search/${page}/${pageSize}/${searchtype}`));
        }

}
