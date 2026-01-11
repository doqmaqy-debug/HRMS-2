import { Component, ElementRef, ViewChild, } from '@angular/core';
import { Employees } from '../../interfaces/employee';
import { CurrencyPipe, NgFor } from '@angular/common';
import { CommonModule, DatePipe } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { ConfirmationDialogComponent } from '../../shared-coponent/confirmation-dialog/confirmation-dialog.component';
@Component({
  selector: 'app-employees',
  imports: [NgFor, CommonModule, DatePipe, ReactiveFormsModule, NgxPaginationModule,ConfirmationDialogComponent],
  providers: [DatePipe],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent {
  constructor(private _datePipe: DatePipe) {
  }

  @ViewChild('closeModalBtn') closeModalBtn: ElementRef | undefined;

  showConfirmDialog: boolean = false;
  employeeToBeDeleted: number | undefined;

  deleteDialogtitle: string = "Delete Employee";
  deleteDialogcontent: string = "Are You Sure You Want To Delete This Employee?";

  paginationConfig = {
    itemsPerPage: 5,
    currentPage: 2
  }

  employees: Employees[] = [
    {
      id: 1, name: "Emp1", birthDate: new Date(2000, 1, 1), email: "Emp1@example.com",
      salary: 1000, status: true, positionId: 1, positionName: "IT",
      departmentId: 1, departmentName: "Development", userId: 1, managerName: null, managerId: null,
    },

    {
      id: 2, name: "Emp2", birthDate: new Date(2002, 5, 1), email: "Emp2@example.com",
      salary: 2000, status: true, positionId: 2, positionName: "HR",
      departmentId: 1, departmentName: "HR", userId: 2, managerName: null, managerId: null
    },

    {
      id: 3, name: "Emp3", birthDate: new Date(2003, 4, 1), email: "Emp3@example.com",
      salary: 3000, status: false, positionId: 3, positionName: "Finance",
      departmentId: 1, departmentName: "Finance", userId: 3, managerName: null, managerId: null
    },

    {
      id: 4, name: "Emp4", birthDate: new Date(2004, 6, 1), email: "Emp4@example.com",
      salary: 4000, status: false, positionId: 4, positionName: "Marketing",
      departmentId: 1, departmentName: "Marketing", userId: 4, managerName: "Emp2", managerId: 2
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

    {
      id: 5, name: "Emp5", birthDate: new Date(2005, 8, 1), email: "Emp5@example.com",
      salary: 5000, status: true, positionId: 5, positionName: "Sales",
      departmentId: 1, departmentName: "Sales", userId: 5, managerName: "Emp3", managerId: 3
    },

  ];

  employeeForm: FormGroup = new FormGroup({
    id: new FormControl(null),
    name: new FormControl(null, [Validators.required]),
    birthDate: new FormControl(null),
    email: new FormControl(null,),
    salary: new FormControl(null, [Validators.required]),
    status: new FormControl(false, [Validators.required]),
    positionId: new FormControl(null, [Validators.required]),
    departmentId: new FormControl(null, [Validators.required]),
    managerId: new FormControl(null),
  });

  employeestableColumns: string[] = [
    '#',
    'name',
    'position',
    'BirthDate',
    'status',
    'Email',
    'Salary',
    'Department',
    'Manager',

  ];

  departments: any[] = [
    { id: null, name: "Select Department" },
    { id: 1, name: "HR" },
    { id: 2, name: "IT" },
  ]

  positions: any[] = [
    { id: null, name: "Select Position" },
    { id: 1, name: "IT" },
    { id: 2, name: "Developer" },
    { id: 3, name: "Manager" },

  ]

  managers: any[] = [
    { id: null, name: "Select Manager" },
    { id: 1, name: "Emp1" },
    { id: 2, name: "Emp2" },
    { id: 3, name: "Emp3" },
  ]

  saveEmployee() {

    if (!this.employeeForm.value.id) {

      let newEmployee: Employees = {
        id: (this.employees[this.employees.length - 1]?.id ?? 0) + 1,
        name: this.employeeForm.value.name,
        birthDate: this.employeeForm.value.birthDate,
        email: this.employeeForm.value.email,
        salary: this.employeeForm.value.salary,
        status: this.employeeForm.value.status,
        userId: (this.employees[this.employees.length - 1]?.id ?? 0) + 1,

        positionId: this.employeeForm.value.positionId,
        positionName: this.positions.find(x => x.id == this.employeeForm.value.positionId)?.name,

        departmentId: this.employeeForm.value.departmentId,
        departmentName: this.departments.find(x => x.id == this.employeeForm.value.departmentId)?.name,

        managerId: this.employeeForm.value.managerId,
        managerName: this.managers.find(x => x.id == this.employeeForm.value.managerId)?.name,


      };

      this.employees.push(newEmployee);
    } else {
      let Index = this.employees.findIndex(x => x.id == this.employeeForm.value.id);
      this.employees[Index].name = this.employeeForm.value.name;
      this.employees[Index].birthDate = this.employeeForm.value.birthDate;
      this.employees[Index].email = this.employeeForm.value.email;
      this.employees[Index].salary = this.employeeForm.value.salary;
      this.employees[Index].status = this.employeeForm.value.status;

      this.employees[Index].positionId = this.employeeForm.value.positionId;
      this.employees[Index].positionName = this.positions.find(x => x.id == this.employeeForm.value.positionId)?.name;

      this.employees[Index].departmentId = this.employeeForm.value.departmentId;
      this.employees[Index].departmentName = this.departments.find(x => x.id == this.employeeForm.value.departmentId)?.name;

      this.employees[Index].managerId = this.employeeForm.value.managerId;
      this.employees[Index].managerName = this.managers.find(x => x.id == this.employeeForm.value.managerId)?.name;
     };

    this.closeModalBtn?.nativeElement.click();
  }
  resetForm() {
    this.employeeForm.reset({
      status: false
    })
  };

  changePage(pageNumber: number) {
    this.paginationConfig.currentPage = pageNumber;
  }

  LoadEmployeeForm(id: number | undefined) {
    {
    
      let employee = this.employees.find(x => x.id == id);

      if (employee != null) {
        this.employeeForm.patchValue({
          id: employee.id,
          name: employee.name,
          birthDate: this._datePipe.transform(employee.birthDate, 'yyyy-MM-dd'),
          email: employee.email,
          salary: employee.salary,
          status: employee.status,
          positionId: employee.positionId,
          departmentId: employee.departmentId,
          managerId: employee.managerId,
        })
      }


    }
  }

  DeleteEmployee() {
    let index = this.employees.findIndex(x => x.id == this.employeeToBeDeleted);
    this.employees.splice(index, 1);
  }

  showConfirmationDialog(id: number|undefined)
  {
    this.showConfirmDialog = true;
    this.employeeToBeDeleted = id;
  }

  confirmEmployeeDelete(confirm: boolean) 
  {
      if(confirm){
        this.DeleteEmployee();

      }
      this.employeeToBeDeleted = undefined;
      this.showConfirmDialog = false;
  }
}

