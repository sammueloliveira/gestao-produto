import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Department } from '../interfaces/department.module';


@Injectable({
  providedIn: 'root'
})

export class DepartmentService {
  apiUrl = 'https://localhost:7205/api/departamento/departamentos';

  constructor(private snackBar: MatSnackBar, private http: HttpClient) {}

  getDepartamentos(): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrl); 
  }


}
