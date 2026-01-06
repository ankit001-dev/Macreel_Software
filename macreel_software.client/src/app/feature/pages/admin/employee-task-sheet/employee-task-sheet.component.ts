import { Component } from '@angular/core';

@Component({
  selector: 'app-employee-task-sheet',
  standalone: false,
  templateUrl: './employee-task-sheet.component.html',
  styleUrl: './employee-task-sheet.component.css'
})
export class EmployeeTaskSheetComponent {
 sidebarOpen: boolean = true; 

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }
}
