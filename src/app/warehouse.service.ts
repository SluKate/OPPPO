import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  apiUrl: string;

  constructor(
    private http: HttpClient
  ) { 
    this.apiUrl = "https://localhost:7275/api/Warehouse"
  }

  getWarehouse(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/all')
  }

  addWarehouse(body: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + '/create', body)
  }

  getWarehouseTotal(body: any): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/warehouse-stats', body)
  }

  postWarehouse(body: any): Observable<any> {
      return this.http.post<any>(this.apiUrl, body)
  }
}
