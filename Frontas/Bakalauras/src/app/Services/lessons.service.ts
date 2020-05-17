import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LessonsService extends BaseService {
  webApiUrlBase = 'http://localhost:8089/api';
  constructor(http: HttpClient) {
    super(http);
  }

  public getLessonsByTeacherBySubject(teacher: any, subject: any): Observable<any[]> {
    return this.get<any>(this.webApiUrlBase + '/teacher/' + teacher + '/subject/' + subject + '/class');
  }

  public postLessonsByTeacherBySubject(teacher: any, subject: any, lessons: any): Observable<any> {
    return this.post<any>(this.webApiUrlBase + '/teacher/' + teacher + '/subject/' + subject + '/class', lessons);
  }

  public deleteLessonsByTeacherBySubject(teacher: any, subject: any, lesson: any): Observable<any> {
    return this.delete<any>(this.webApiUrlBase + '/teacher/' + teacher + '/subject/' + subject + '/class/' + lesson);
  }

  public UpdateLessonsByTeacherBySubject(teacher: any, subject: any, lessons: any): Observable<any> {
    return this.update(this.webApiUrlBase + '/teacher/' + teacher + '/subject/' + subject + '/class/' + lessons.id, lessons);
  }
}

