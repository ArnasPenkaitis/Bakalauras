import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VisualizationsEditModalComponent } from './visualizations-edit-modal.component';

describe('VisualizationsEditModalComponent', () => {
  let component: VisualizationsEditModalComponent;
  let fixture: ComponentFixture<VisualizationsEditModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VisualizationsEditModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VisualizationsEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
