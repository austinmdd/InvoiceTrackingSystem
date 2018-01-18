import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class AddNotesService  extends Baseservice {
  
    constructor(http:Http) { 
      super("notes", http);
    }

}
