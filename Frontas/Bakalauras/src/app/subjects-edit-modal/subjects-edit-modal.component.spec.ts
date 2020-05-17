import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectsEditModalComponent } from './subjects-edit-modal.component';

describe('SubjectsEditModalComponent', () => {
  let component: SubjectsEditModalComponent;
  let fixture: ComponentFixture<SubjectsEditModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubjectsEditModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubjectsEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
