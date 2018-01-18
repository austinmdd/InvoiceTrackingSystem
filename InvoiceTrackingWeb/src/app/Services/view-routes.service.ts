import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class ViewRoutesService extends Baseservice {

  constructor(http:Http) {
    super("ViewRoutes", http)
   }
   GetRoutes(page:number,pageSize:number){
    return this.getRequest(this.url.concat(`/${page}/${pageSize}`))
   }
   Searchroute(page: number, pageSize: number,searchtype:string){
    return this.getRequest(this.url.concat(`/search/${page}/${pageSize}/${searchtype}`));
    }

}
