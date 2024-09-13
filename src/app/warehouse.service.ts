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
    this.apiUrl = "https://localhost:7275"
  }

  getWarehouse(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/api/Warehouse/all')
  }

  postProduct(body: any): Observable<any> {
      return this.http.post<any>(this.apiUrl + '/api/Product', body)
  }
}
