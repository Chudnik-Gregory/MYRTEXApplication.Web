import { Employee } from './../pages/employees/domain/employees.interfaces';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) {}

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(environment.apiUrl + '/employees');
  }

  addEmployee(employee: Partial<Employee>) {
    console.log('ZASHEL')
    return this.http.post<Employee>(
      environment.apiUrl + '/employees/add',
      employee
    );
  }

  updateEmployee(employee: Partial<Employee>) {
    return this.http.put<Employee>(
      environment.apiUrl + '/employees/update',
      employee
    );
  }

  deleteEmployee(id: string): Observable<Employee> {
    return this.http.delete<Employee>(environment.apiUrl + '/employees/delete/' + id);
  }
}
