import { NgModule, LOCALE_ID } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule, JsonpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser'; 
import { Ng2OrderModule } from 'ng2-order-pipe';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators  } from '@angular/forms';  
import { BootstrapModalModule } from 'ng2-bootstrap-modal';
import { CommonModule, DatePipe, CurrencyPipe  } from "@angular/common";
import { NgxPaginationModule } from 'ngx-pagination';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SearchComponent } from './search/search.component';
import { UploadComponent } from './upload/upload.component';
import { DocumenttypeComponent } from './maintain/documenttype/documenttype.component';
import { InvoicestatusComponent } from './maintain/invoicestatus/invoicestatus.component';
import { DocumentmanageComponent } from './maintain/documentmanage/documentmanage.component';
import { StatusmanageComponent } from './maintain/statusmanage/statusmanage.component';
import { SubmissionComponent } from './submission/submission.component';
import { PurchaseOrderComponent } from './purchase-order/purchase-order.component';
import { UploadDocumentComponent} from './purchase-order/upload/upload-document.component';
import { ListDocumentComponent } from './purchase-order/list/list-document.component';
import { ConfirmComponent } from './popup/confirm.component';
import { AlertComponent } from './popup/alert.component';
import { PromptComponent } from './popup/prompt.component';
import { ParentDialogComponent } from './popup/parent-dialog.component';
import { ErrorComponent } from './popup/error.component';
import { ViewerComponent } from './popup/viewer.component';
import { LoadingModule } from 'ngx-loading';

import { SupplierService } from "./services/supplier.service";
import { DocumenttypeService } from "./services/documenttype.service";
import { StatusService } from "./services/status.service";
import { SubmissionService } from "./services/submission.service";
import { PurchaseOrderService } from "./services/purchaseorder.service";
import { InvoiceCategoryService } from "./services/invoice-category.service";
import { SearchInvoiceComponent } from './search-invoice/search-invoice.component';
import { ViewComponent } from './search-invoice/view/view.component';
import { InvoiceCategoriesComponent } from './maintain/invoice-categories/invoice-categories.component';
import { InvoiceManageComponent } from './maintain/invoice-manage/invoice-manage.component';

import { BusyComponent } from './busy/busy.component';
import { SupplierInvoiceUpdateService } from "./services/supplier-invoice-update.service";
import { UpdateInvoiceComponent } from './search-invoice/update-invoice/update-invoice.component';
import { AddNotesService } from './services/add-notes.service';
import { DoctypeLinksComponent } from './doctype-links/doctype-links.component';

import { CurrencyFormat } from './pipes/currencyFormat.pipe';
import { SystemSettingsService } from './services/system-settings.service';
import { GlobalsService } from './services/globals.service';
import { ListInvoicesComponent } from './list-invoices/list-invoices.component';
import { SupplierInvoiceService } from './services/supplier-invoice.service';
import { InvoiceApprovalComponent } from './popup/invoice-approval.component';
import { EmailService } from './services/email.service';
import { WorkflowprocessComponent } from './workflow/workflowprocess/workflowprocess.component';
import { WorkflowStatusservice } from './services/workflow-status-service';
import {NewInvoiceSubmittedsservice } from './services/submit-invoicesforwork.service';
import { WorkflowhomeComponent } from './workflow/workflowhome/workflowhome.component';
import { HomecompliancechecklistComponent } from './workflow/homecompliancechecklist/homecompliancechecklist.component';
import { WorkflowProcessService } from './services/workflow-process.service';
import { WorkflowRouteService } from './services/workflow-route-service';
import { RulesComponent } from './workflow/maintain/rules/rules.component';
import { WorkflowRuleService } from './services/workflow-rule.service';
import { RuleManageComponent } from './workflow/maintain/rule-manage/rule-manage.component';
import { WorkflowRuleRoleLinkService } from './services/workflow-rulerolelink.service';
import { RoutesComponent } from './workflow/maintain/routes/routes.component';
import { RolesServiceService } from './services/role-service.service';
import { ViewRoutesService } from './services/view-routes.service';
import { RoutesManageComponent } from './workflow/maintain/routes-manage/routes-manage.component';
import { WorkflowRouteactionsService } from './services/workflow-routeactions.service';





@NgModule({  

    declarations: [
        AppComponent,
        HomeComponent,
        SearchComponent,
        UploadComponent,
        DocumenttypeComponent,
        InvoicestatusComponent,
        DocumentmanageComponent,
        StatusmanageComponent,
        SubmissionComponent,
        PurchaseOrderComponent,
        UploadDocumentComponent,
        ListDocumentComponent,
        //Popup Components
        ConfirmComponent,
        AlertComponent,
        PromptComponent,
        ParentDialogComponent,
        SearchInvoiceComponent,
        ViewComponent,
        ErrorComponent,
        InvoiceCategoriesComponent,
        InvoiceManageComponent,
        BusyComponent,
        UpdateInvoiceComponent,
        ViewerComponent,
        //Currency
        CurrencyFormat,
        DoctypeLinksComponent,
        ListInvoicesComponent,
        InvoiceApprovalComponent,
        WorkflowprocessComponent,
        WorkflowhomeComponent,
        HomecompliancechecklistComponent,
        RulesComponent,
        RuleManageComponent,
        RoutesComponent,
        RoutesManageComponent
    ],
  bootstrap:    [ AppComponent ],
  imports: [BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserModule,
      HttpModule,
      JsonpModule,
      Ng2OrderModule,
      BootstrapModalModule,
      NgxPaginationModule,
      LoadingModule,

    RouterModule.forRoot([
        { path: '', redirectTo: 'home', pathMatch: 'full' },        
        { path: 'search', component: SearchComponent },
        { path: 'home', component: HomeComponent },
        { path: 'purchaseorder/:id', component: PurchaseOrderComponent },   
        { path: 'submissions', component: SubmissionComponent },         
        { path: 'upload/:id', component: UploadComponent },
        { path: 'doctypes', component: DocumenttypeComponent },
        { path: 'doctypes/add', component: DocumentmanageComponent },
        { path: 'doctypes/edit/:id', component: DocumentmanageComponent },
        { path: 'statuses', component: InvoicestatusComponent },
        { path: 'statuses/add', component: StatusmanageComponent},
        { path: 'statuses/edit/:id', component: StatusmanageComponent },
        { path: 'search-invoice', component: SearchInvoiceComponent},
        { path: 'search-invoice-list/:inv', component: SearchInvoiceComponent},
        { path: 'search-invoice/update/:id', component: UpdateInvoiceComponent},
        { path: 'search-invoice/approval/:ids/:invnum', component: SearchInvoiceComponent},
        { path: 'home', component: HomeComponent },
        { path: 'invoicecategory', component: InvoiceCategoriesComponent},
        { path: 'invoicecategory/add', component: InvoiceManageComponent},
        { path: 'invoicecategory/edit/:id', component: InvoiceManageComponent },
        { path: 'busy', component: BusyComponent },
        { path: 'link-doctypes', component: DoctypeLinksComponent },
        { path: 'invoices', component: ListInvoicesComponent },
        { path: 'Workflow', component: WorkflowprocessComponent },
        { path: 'workflow/home',component : WorkflowhomeComponent},
        { path: 'Workflow/home/compliance',component:HomecompliancechecklistComponent},
        { path: 'checklist',component:RulesComponent},
        { path: 'checklist/edit/:id',component:RuleManageComponent},
        { path: 'checklist/add',component:RuleManageComponent},
        { path: 'routes',component:RoutesComponent},
        { path: 'routes/edit/:id',component:RoutesManageComponent},
        { path: 'routes/add',component:RoutesManageComponent}
      ])
    
  ],
  
  entryComponents: [
      ConfirmComponent,
      AlertComponent,
      PromptComponent,
      ParentDialogComponent,
      ErrorComponent,
      ViewerComponent,
      InvoiceApprovalComponent
  ],

  providers: [
    DatePipe,
    CurrencyPipe,
    SupplierService, 
    DocumenttypeService, 
    StatusService,
    SubmissionService,
    PurchaseOrderService,
    InvoiceCategoryService,
    SupplierInvoiceUpdateService,
    AddNotesService,
    SystemSettingsService,
    GlobalsService,
    SupplierInvoiceService,
    EmailService,
    WorkflowStatusservice,
    NewInvoiceSubmittedsservice,
    WorkflowProcessService,
    WorkflowRouteService,
    WorkflowRuleService,
    WorkflowRuleRoleLinkService,
    RolesServiceService,
    ViewRoutesService,
    WorkflowRouteactionsService,
    
  
     {provide: LOCALE_ID,
          useValue: 'en-ZA' 
    }
   
  ]
})
export class AppModule {}
