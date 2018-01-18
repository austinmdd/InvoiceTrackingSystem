import { InvoiceTrackingWebPage } from './app.po';

describe('invoice-tracking-web App', () => {
  let page: InvoiceTrackingWebPage;

  beforeEach(() => {
    page = new InvoiceTrackingWebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
