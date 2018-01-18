import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class RolesServiceService extends Baseservice{

  constructor(http:Http) { 
    super("role", http);
  }
  GetAllRoles(page:number,pageSize:number){
    return this.getRequest(this.url.concat(`/${page}/${pageSize}`));
    }

}