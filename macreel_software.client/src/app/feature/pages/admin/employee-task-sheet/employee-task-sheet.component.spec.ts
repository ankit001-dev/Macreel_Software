import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeTaskSheetComponent } from './employee-task-sheet.component';

describe('EmployeeTaskSheetComponent', () => {
  let component: EmployeeTaskSheetComponent;
  let fixture: ComponentFixture<EmployeeTaskSheetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeTaskSheetComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeTaskSheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
