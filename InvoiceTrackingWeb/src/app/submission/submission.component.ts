import { Component } from '@angular/core';
import {SubmissionService} from "../services/submission.service";

@Component({
    moduleId: module.id,
    selector: 'submission',
    providers: [SubmissionService],
    templateUrl: 'submission.component.html',
    styleUrls: ['submission.component.scss']
})
export class SubmissionComponent {

    constructor(private service : SubmissionService) 
    {
      

    }
        
}
