import { Component, ElementRef, ViewChild,OnInit } from '@angular/core';
import { Employees } from '../../interfaces/employee';
import { CurrencyPipe, NgFor } from '@angular/common';
import { CommonModule, DatePipe } from '@angular/common';
import { Form, FormControl, FormGroup, isFormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { ConfirmationDialogComponent } from '../../shared-component/confirmation-dialog/confirmation-dialog.component';
import { EmployeesService } from '../../services/employees.service';
import { List } from '../../interfaces/List';
import { DepartmentsService } from '../../services/departments.service';
import { LookUpsService } from '../../services/look-ups.service';
import { RouterLink,RouterLinkActive,RouterOutlet } from '@angular/router';
import { LookUpsMajorCodes } from '../../Enums/Lookups-Major-codes';

@Component({
  selector: 'app-employees',
  imports: [NgFor, CommonModule, DatePipe, ReactiveFormsModule,
    NgxPaginationModule, ConfirmationDialogComponent,RouterLink,
    RouterOutlet,RouterLinkActive,
  ],
  providers: [DatePipe],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent {
  constructor(private _datePipe: DatePipe,
    private _employeeService: EmployeesService,
    private _departmentService: DepartmentsService,
    private _LookUpService: LookUpsService

  ) {}

  ngOnInit() {
    this.LoadEmployees();
    this.LoadPositionsList();

  }

  @ViewChild('closeModalBtn') closeModalBtn: ElementRef | undefined;
  @ViewChild('mageInput') imageInput !:ElementRef;

  showConfirmDialog: boolean = false;
  employeeToBeDeleted: number | undefined;

  deleteDialogtitle: string = "Delete Employee";
  deleteDialogcontent: string = "Are You Sure You Want To Delete This Employee?";

  paginationConfig = {
    itemsPerPage: 5,
    currentPage: 2
  }

  employees: Employees[] = [
  ];

  employeeForm: FormGroup = new FormGroup({
    id: new FormControl(null),
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    birthDate: new FormControl(null),
    email: new FormControl(null,),
    salary: new FormControl(null, [Validators.required]),
    status: new FormControl(false, [Validators.required]),
    positionId: new FormControl(null, [Validators.required]),
    departmentId: new FormControl(null, [Validators.required]),
    managerId: new FormControl(null),
    image: new FormControl(null),
    isImage: new FormControl(false)
  });

  SearchFilterForm : FormGroup = new FormGroup({
    name: new FormControl(null),
    positionId: new FormControl(null),
    status: new FormControl(null),
  });

  employeestableColumns: string[] = [
    '#',
    "Image",
    'name',
    'position',
    'BirthDate',
    'status',
    'Email',
    'Salary',
    'Department',
    'Manager',

  ];

  departments: List[] = []

  positions: List[] = []

  managers: List[] = []
  
  statusList = [
    { value: null, name: "Select Status" },
    { value: 'true', name: "Active" },
    { value: 'false', name: "Inactive" },
  ];
  UploadImage(event:any){
    
    this.employeeForm.patchValue({
      image: event.target.files[0]
    });
  }


  LoadEmployees() {
    this.employees = [];
    let searchOBJ = {
      name: this.SearchFilterForm.value.name,
      positionId: this.SearchFilterForm.value.positionId,
      status: this.SearchFilterForm.value.status,
    }
    this._employeeService.GetByCriteria(searchOBJ).subscribe({
      next: (res: any) => {
        if (res.length > 0) {
          res.forEach((x: any) => {
            let employee: Employees = {
              id: x.id,
              name: x.name,
              positionId: x.positionId,
              positionName: x.positionName,
              status: x.status,
              birthDate: x.birthDate,
              email: x.email,
              salary: x.salary,
              departmentId: x.departmentId,
              departmentName: x.departmentName,
              managerId: x.managerId,
              managerName: x.managerName,
              userId: x.userId,
              imagePath:x.imagePath
            };
            this.employees.push(employee);


          });
        }
      },

      error: err => {
        console.log(err.error?.message ?? err?.error ?? "Http Response Error");
      },
    });
  }
  LoadPositionsList() {
    this.positions = [
      { id: null, name: "Select Position" }
    ];
    this._LookUpService.GetByMajorCode(LookUpsMajorCodes.Position).subscribe({
      next: (res: any) => {
        if (res.length > 0) {
          res.forEach((x: any) => {
            let position: List = {
              id: x.id,
              name: x.name,
            };
            this.positions.push(position);
          });
        }
      },
      error: err => {
        console.log(err.error?.message ?? err?.error ?? "Http Response Error");
      },
    });
  }

  LoadDepartmentsList() {
    this.departments = [
      { id: null, name: "Select Department" }
    ];
    this._departmentService.GetDepartmentsList().subscribe({
      next: (res: any) => {
        if (res.length > 0) {
          res.forEach((x: any) => {
            let department: List = {
              id: x.id,
              name: x.name,
            };
            this.departments.push(department);
          });
        }
      },
      error: err => {
        console.log(err.error?.message ?? err?.error ?? "Http Response Error");
      },
    });
  }

  LoadManagersLists() {
    this.managers = [
      { id: null, name: "Select Manager" }
    ];

    this._employeeService.GetManagers().subscribe({
      next: (res: any) => {
        if (res.length > 0) {
          res.forEach((x: any) => {
            let manager: List = {
              id: x.id,
              name: x.name,
            };
            this.managers.push(manager);
          });
        }

      },

      error: err => {
        console.log(err.error?.message ?? err?.error ?? "Http Response Error");
      },
    });
  }

  saveEmployee() {
       let newEmployee: Employees = {
        id:this.employeeForm.value.id ?? 0,
        firstName: this.employeeForm.value.firstName,
        lastName: this.employeeForm.value.lastName,
        birthDate: this.employeeForm.value.birthDate,
        email: this.employeeForm.value.email,
        salary: this.employeeForm.value.salary,
        status: this.employeeForm.value.status,
        positionId: this.employeeForm.value.positionId,
        departmentId: this.employeeForm.value.departmentId,
        managerId: this.employeeForm.value.managerId,
        image: this.employeeForm.value.image,
        isImage: this.employeeForm.value.isImage
      };

    if (!this.employeeForm.value.id) {

      this._employeeService.Add(newEmployee).subscribe({
        next: (res: any) => {
          this.LoadEmployees();
        },

        error: err => {
          console.log(err.error?.message ?? err?.error ?? "Http Response Error");
        },
      });
    } else {
      this._employeeService.UpDate(newEmployee).subscribe({
        next: (res: any) => {
          this.LoadEmployees();
        },
        error: err => {
          console.log(err.error?.message ?? err?.error ?? "Http Response Error");
        }
      });
    }

    this.closeModalBtn?.nativeElement.click();
  }

  LoadSaveDialog() {
    this.LoadManagersLists();
    this.resetForm();
    this.LoadDepartmentsList();
    this.LoadPositionsList();
    
    
    
  }
  resetForm() {
    this.employeeForm.reset({
      status: false
    })
    this.clearImage();
  };
  clearImage(){
    this.imageInput.nativeElement.value="";
  }
  removeImage(){
    this.employeeForm.patchValue(
    { isImage:false}
    )
  }

  changePage(pageNumber: number) {
    this.paginationConfig.currentPage = pageNumber;
  }

  LoadEmployeeForm(id: number | undefined) {
    {
      this.LoadSaveDialog();
      if (!id) {
        return
      }

      this._employeeService.GetById(id).subscribe({
        next: (employee: any) => {
          if (employee != null) {
            this.employeeForm.patchValue({
              id: employee.id,
              firstName: employee.firstName,
              lastName: employee.lastName,
              birthDate: this._datePipe.transform(employee.birthDay,'yyyy-MM-dd'),
              email: employee.email,
              salary: employee.salary,
              status: employee.status,
              positionId: employee.positionId,
              departmentId: employee.departmentId,
              managerId: employee.managerId,
              isImage: employee.imagePath? true :false 
            })
          }
        },
        error: err => {
          console.log(err.error?.message ?? err?.error ?? "Http Response Error");
        }
      })
    }
  }


  DeleteEmployee() {
    // let index = this.employees.findIndex(x => x.id == this.employeeToBeDeleted);
    //  this.employees.splice(index, 1);
    if(this.employeeToBeDeleted){
      this._employeeService.delete(this.employeeToBeDeleted).subscribe({
        next:(res:any)=>{
          this.LoadEmployees();
        },
        error: err => {
          alert(err.error?.message ?? err?.error ?? "Http Response Error");
        }
      });
    }
  }

  showConfirmationDialog(id: number | undefined) {
    this.employeeToBeDeleted = id;
    this.showConfirmDialog = true;
    
  }

  confirmEmployeeDelete(confirm: boolean) {
    if (confirm) {
      this.DeleteEmployee();

    }
    this.employeeToBeDeleted = undefined;
    this.showConfirmDialog = false;
  }
}

