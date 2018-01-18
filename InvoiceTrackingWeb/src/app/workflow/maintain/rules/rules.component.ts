import { AfterContentChecked,AfterViewChecked,ChangeDetectorRef,Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertComponent } from './../../../popup/alert.component';
import { DialogService } from "ng2-bootstrap-modal";
import { SystemSettingsService } from './../../../services/system-settings.service';
import { GlobalsService } from './../../../services/globals.service';
import { ErrorComponent } from '../../../popup/error.component';
import { error } from 'util';
import { WorkflowRuleService } from './../../../services/workflow-rule.service';
import { WorkflowRuleRoleLinkService } from './../../../services/workflow-rulerolelink.service';

@Component({
  selector: 'app-rules',
  templateUrl: './rules.component.html',
  styleUrls: ['./rules.component.css']
})
export class RulesComponent implements OnInit {
  page: number = 1;
  pageSize = parseInt(localStorage.getItem("pageSize"));
  isLoading: boolean = false;
  allrules: any = [];
  direction: boolean = false;
  lblHeading: string = "";
  removeRule:any;
  isRemove:boolean = false;
  rulerolelink : any ={};
  isDeletable:boolean = false;
  searchText: string = '';
  searchResults: any = [];
  IsSearchDataFound: boolean = false;
  message = "Loading...";
  constructor(private ruleservice:WorkflowRuleService ,private dialogService: DialogService,
  private router: Router,private ref: ChangeDetectorRef,private ruleroleservice:WorkflowRuleRoleLinkService) { }

  ngOnInit() {
    this.onChangeHeandig();
    this.getAllRules();
  }

  getAllRules(){
    this.isLoading = true;
    this.ruleservice.getAllRules(this.page,this.pageSize).subscribe(result =>{
    this.allrules = result;
    //console.log(this.allrules);
    this.isLoading = false;
    },error => {
      this.isLoading = false;
      error = error.json();
      this.dialogService.addDialog(ErrorComponent, { title: 'No rules Found', message: `No rules Found `, statuscode: error.Status, url: error.Uri, servermessage: error.Message });
    })

  }
  onDelete(id: any) {
    this.isLoading = true;
    this.ruleroleservice.getById(id).subscribe(result =>{
    this.rulerolelink = result;
    console.log(this.rulerolelink);
    if (this.rulerolelink.Deletable == false ){
      this.dialogService.addDialog(AlertComponent, { title: 'Delete Rule', message: `Rule ${this.removeRule.ChecklistName} can't be deleted due to referential integrity`, bold: `${this.removeRule.ChecklistName}` });
      this.onNo();
       this.getAllRules();
       this.isLoading = false;
      }
   if(this.rulerolelink.Deletable == true){
    this.ruleservice.delete(id).subscribe(result => {
    this.dialogService.addDialog(AlertComponent, { title: 'Delete rule', message: `Rule ${this.removeRule.ChecklistName} deleted.`, bold: `${this.removeRule.ChecklistName}` });                        
    this.onNo();
    this.getAllRules();
    this.isLoading = false;
  }, error => {
      error = error.json();
      this.dialogService.addDialog(ErrorComponent, { title: 'Delete rule', message: `Rule ${this.removeRule.ChecklistName} was not deleted.`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
      this.isLoading = false;
      });

   }

    
    })
  }
  SearchRule(){
    this.ruleservice.Searchrule(this.page,this.pageSize,this.searchText.trim()).subscribe(result =>{
      this.searchResults = result;
      this.IsSearchDataFound = true;
      //console.log(this.searchResults);
     }, error =>{
      error = error.json();
      this.dialogService.addDialog(ErrorComponent, { title: 'Search error', message: `Could not find rule`, statuscode: error.Status, url: error.Uri, servermessage: error.Message }); 
      this.isLoading = false;
     });

  }
  resetForm() {
    this.searchText ='';
   // this.IsView = false;
    this.IsSearchDataFound = false;
    //this.poHasPos = false;
    //this.supHasData = this.isError = false;
    //this.form.reset();
    //this.p = 1;
  }


  
    //check if the ID exist in rule role link
   
  
  onSort() {
    this.direction = !this.direction;
    }
    onConfirm(r: any) {
      this.removeRule = r;     
      this.isRemove = true;
      this.onChangeHeandig(false);
      }

      onNo() {
        this.isRemove = false;
        this.onChangeHeandig();
      }

    onNext(pg){
      this.page = pg;
      this.getAllRules();
      }
      
      onChangeHeandig(change: boolean = true) {
        this.lblHeading = (change) ? "Compliance CheckLists" : "Delete Compliance CheckList";
      }

}
