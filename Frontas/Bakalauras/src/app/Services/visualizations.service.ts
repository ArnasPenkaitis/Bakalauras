import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VisualizationsService extends BaseService {
  webApiUrlBase = 'http://localhost:8089/api';
  constructor(http: HttpClient) {
    super(http);
  }

  public getVisualizationByClass(classId: any): Observable<any[]> {
    return this.get<any>(this.webApiUrlBase + '/visualization/' + 'class/' + classId);
  }
  public getAllVisualizations(): Observable<any[]> {
    return this.get<any>(this.webApiUrlBase + '/visualization/');
  }

  public postVisualization(visualization: any, lesson: any): Observable<any> {
    return this.post<any>(this.webApiUrlBase + '/visualization/class/' + lesson + '/' + visualization
    , { VisualizationId: visualization, ClassId: lesson });
  }

  public postVisualizationNotChain(visualization: any, filename: any): Observable<any> {
    return this.post<any>(this.webApiUrlBase + '/visualization/' + filename, visualization);
  }

  public deleteVisualization(visualization: any, lesson: any): Observable<any> {
    return this.delete<any>(this.webApiUrlBase + '/visualization/class/' + lesson + '/' + visualization);
  }
  public deleteVisualizationNotChain(visualization: any): Observable<any> {
    return this.delete<any>(this.webApiUrlBase + '/visualization/' + visualization.id);
  }


  public UpdateVisualization(id: number, visualization: any): Observable<any> {
    return this.update(this.webApiUrlBase + '/visualization/' + id, visualization);
  }
}
