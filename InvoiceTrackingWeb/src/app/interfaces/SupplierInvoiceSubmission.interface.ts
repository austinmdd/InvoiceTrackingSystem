
export interface SupplierInvoiceSubmissionDTO {
    InvoiceNumber : string;
    InvoiceAmount : number;
    InvoiceDate : Date;
    Description :string;
 
    //PurchaseOrder
    POID :number;
    POStatus :string;
    PONumber : number;
    POAmount : number;
    PODate : string;
    DueAmount : number;
 
    //Supplier
    Name : string;
    CSDNumber : number;
    VendorCode : number;

   
 }