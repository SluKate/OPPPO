import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  apiUrl: string;

  constructor(
    private http: HttpClient
  ) { 
    this.apiUrl = "https://localhost:7275"
  }

  getProducts(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/api/Product/all')
  }

  postProduct(body: any): Observable<any> {
      return this.http.post<any>(this.apiUrl + '/api/Product', body)
  }

  deleteProduct(id: number| undefined): Observable<any> {
    return this.http.delete<any>(this.apiUrl + `/api/Product/delete/${id}`)
}
}
