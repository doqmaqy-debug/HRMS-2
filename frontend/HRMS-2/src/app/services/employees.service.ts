import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Employees } from '../interfaces/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
 
  UrlApi: string = "https://localhost:44389/api/Employees";
  constructor(private _http: HttpClient) { }



  GetByCriteria(searchOBJ: any) {
    let params = new HttpParams();
    params = params.set("Name", searchOBJ.name ?? "")
    params = params.set("PositionId", searchOBJ.positionId ?? "")
    params = params.set("Status", searchOBJ.status ?? "")

    return this._http.get(this.UrlApi + "/GetByCriteria", { params });
  }

  GetManagers() {
    return this._http.get(this.UrlApi + "/GetManagers");
  }
  Add(employee: Employees) {
      let formData=new FormData();
      formData.set("Id",employee.id?.toString()??""),
      formData.set("FirstName",employee.firstName??""),
      formData.set("LastName",employee.lastName??""),
      formData.set("birthDate",employee.birthDate?.toString()??""),
      formData.set("Email",employee.email??""),
      formData.set("Salary",employee.salary?.toString()??""),
      formData.set("Status",employee.status?.toString()??""),
      formData.set("PositionId",employee.positionId?.toString()??""),
      formData.set("DepartmentId",employee.departmentId?.toString()??""),
      formData.set("ManagerId",employee.managerId?.toString()??""),
      formData.set("Image",employee.image);
      formData.set("IsImage",employee.isImage?.toString()??"false")
    return this._http.post(this.UrlApi + "/Add", formData);
  }
  GetById(id: number) {
    return this._http.get(this.UrlApi + `/GetById/${id}`);
  }
  UpDate(employee: Employees) {
    let formData=new FormData();
      formData.set("Id",employee.id?.toString()??""),
      formData.set("FirstName",employee.firstName??""),
      formData.set("LastName",employee.lastName??""),
      formData.set("BirthDate",employee.birthDate?.toString()??""),
      formData.set("Email",employee.email??""),
      formData.set("Salary",employee.salary?.toString()??""),
      formData.set("Status",employee.status?.toString()??""),
      formData.set("PositionId",employee.positionId?.toString()??""),
      formData.set("DepartmentId",employee.departmentId?.toString()??""),
      formData.set("ManagerId",employee.managerId?.toString()??""),
      formData.set("Image",employee.image);
    
    return this._http.put(this.UrlApi + "/Update", formData);
  }
  delete(id:number){
    return this._http.delete(this.UrlApi + `/Delete/${id}`);
  }

    
}
