// Angular Imports
import { NgModule } from '@angular/core';

// This Module's Components
import { PurchaseOrderComponent } from './purchase-order.component';
//Child Component
import { UploadDocumentComponent } from './upload/upload-document.component';

@NgModule({
    declarations: [
        UploadDocumentComponent]
})
export class PurchaseOrderModule {

}
