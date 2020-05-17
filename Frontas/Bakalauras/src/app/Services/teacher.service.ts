import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Teacher } from '../Models/Teacher';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeacherService extends BaseService {
  webApiUrlBase = 'http://localhost:8089/api';

  constructor(http: HttpClient) {
    super(http);
  }

  public getTeacher(url: string): Observable<Teacher[]> {
    return this.get<any>(url);
  }

  public getTeachers(url: string): Observable<Teacher[]> {
    return this.get<any>(this.webApiUrlBase + '/teacher');
  }

  public postTeacher(url: string, user: any): Observable<any> {
    return this.post<any>(url, user);
  }

  public deleteTeacher(url: string, teacher: any): Observable<any> {
    return this.delete<any>(this.webApiUrlBase + '/teacher/' + teacher);
  }

  public UpdateTeacher(url: string, user: any): Observable<any> {
    return this.update(this.webApiUrlBase + '/teacher/' + user.id, user);
  }
}
