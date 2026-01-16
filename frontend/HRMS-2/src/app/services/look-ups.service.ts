import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class LookUpsService {
 UrlApi: string = "https://localhost:44389/api/LookUps";
  constructor(private _http: HttpClient) { }

  GetByMajorCode(majorCode: number) {
    let params = new HttpParams();
    params = params.set("MajorCode", majorCode);

    return this._http.get(this.UrlApi + "/GetByMajorCode", { params });
  }
}
