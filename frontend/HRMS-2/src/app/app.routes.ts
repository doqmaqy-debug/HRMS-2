import { Routes } from '@angular/router';
import { EmployeesComponent } from './components/employees/employees.component';
import { DepartmentsComponent } from './components/departments/departments.component';

export const routes: Routes = [
    { path: '', redirectTo: 'employees', pathMatch: 'full'},
    { path: 'employees', component: EmployeesComponent },
    { path : 'departments',component: DepartmentsComponent },   // Placeholder for DepartmentsComponent
    { path: '**', redirectTo: 'employees' }  // Wildcard route for a 404 page can be added here
];

