import { ShippingInstructionsTemplatePage } from './app.po';

describe('ShippingInstructions App', function() {
  let page: ShippingInstructionsTemplatePage;

  beforeEach(() => {
    page = new ShippingInstructionsTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
