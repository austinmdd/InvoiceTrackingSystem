import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class WorkflowRouteService extends Baseservice {

    constructor(http:Http) { 
        super("WorkflowRoute", http);
    }
    
    getRoutesbyRoleID(role: number) {
        return this.getRequest(this.url + "/" + role);
    }
    GetAllRoutes(page:number,pageSize:number,order:boolean){
    return this.getRequest(this.url.concat(`/allroutes/${page}/${pageSize}/${order}`));
    }
    CheckDuplicates(rolefrom:number,roleto:number,action:number,status:number){
        return this.getRequest(this.url.concat(`/dupefind/${rolefrom}/${roleto}/${action}/${status}`))
    }
 
    
}