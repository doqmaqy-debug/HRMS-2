import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {
  UrlApi: string = "https://localhost:44389/api/Department";
  constructor(private _http: HttpClient) { }

  GetDepartmentsList() {
    return this._http.get(this.UrlApi + "/GetDepartmentList");
  }
}
