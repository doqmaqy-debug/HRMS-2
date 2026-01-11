import { Component } from '@angular/core';
import { EmployeesComponent } from './components/employees/employees.component';
@Component({
  selector: 'app-root',
  imports: [EmployeesComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  
}
  