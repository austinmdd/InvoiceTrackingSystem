import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class EmailService extends Baseservice{

  constructor(http:Http) { 
    super("Email", http);
  }

  SendMail(body: any){
    return this.http.post(this.url, body, this.options).map(response => response.json());
  }

}
