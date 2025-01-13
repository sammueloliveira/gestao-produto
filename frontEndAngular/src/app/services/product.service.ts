import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Product } from '../interfaces/product.module';

@Injectable({
  providedIn: 'root',
})

export class ProductService {
  baseUrl = 'https://localhost:7205/api/produto';
 

  constructor(private snackBar: MatSnackBar, private http: HttpClient) {}

  showMessage(msg: string): void {
    this.snackBar.open(msg, 'x', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  create(product: Product): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, product).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }

  read(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }

  readById(id: string): Observable<Product> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .get<Product>(url)
      .pipe(catchError((e) => this.errorHandler(e)));
  }

  update(product: Product): Observable<Product> {
    const url = `${this.baseUrl}/${product.id}`;
    return this.http
      .put<Product>(url, product)
      .pipe(catchError((e) => this.errorHandler(e)));
  }

  delete(id: string): Observable<void> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .delete<void>(url)
      .pipe(catchError((e) => this.errorHandler(e)));
  }

  errorHandler(e: any): Observable<any> {
    this.showMessage('Ocorreu um erro!');
    return EMPTY;
  }

 
}