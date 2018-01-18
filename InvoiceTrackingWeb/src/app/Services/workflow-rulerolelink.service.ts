import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { Baseservice } from './baseservice';

@Injectable()
export class WorkflowRuleRoleLinkService extends Baseservice {
  
    constructor(http:Http) { 
      super("RuleRoleLink", http);
    }

}
