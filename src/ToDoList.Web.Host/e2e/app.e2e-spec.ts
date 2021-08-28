import { ToDoListTemplatePage } from './app.po';

describe('ToDoList App', function() {
  let page: ToDoListTemplatePage;

  beforeEach(() => {
    page = new ToDoListTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
