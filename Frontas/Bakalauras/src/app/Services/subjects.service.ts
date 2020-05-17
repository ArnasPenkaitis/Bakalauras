import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { Subject } from './../Models/Subject';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService extends BaseService {
  webApiUrlBase = 'http://localhost:8089/api';
  constructor(http: HttpClient) {
    super(http);
  }

  public getSubjectsByTeacher(teacher: any): Observable<any[]> {
    return this.get<any>(this.webApiUrlBase + '/teacher/' + teacher + '/subject');
  }

  public postSubjectByTeacher(teacher: any, subject: any): Observable<any> {
    return this.post<any>(this.webApiUrlBase + '/teacher/' + teacher + '/subject', subject);
  }

  public deleteSubjectByTeacher(teacher: any, subject: any): Observable<any> {
    return this.delete<any>(this.webApiUrlBase + '/teacher/' + teacher + '/subject/' + subject);
  }

  public UpdateSubjectByTeacher(teacher: any, subject: any): Observable<any> {
    return this.update(this.webApiUrlBase + '/teacher/' + teacher + '/subject/' + subject.id, subject);
  }
}
