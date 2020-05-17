import { Injectable, Output, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubServiceService {

  private hubConnection: signalR.HubConnection;

  @Output() visualizationCreationSignal = new EventEmitter();
  @Output() visualizationUpdateSignal = new EventEmitter();
  @Output() accountCreateSignal = new EventEmitter();
  @Output() accountUpdateSignal = new EventEmitter();
  @Output() subjectCreateSignal = new EventEmitter();
  @Output() subjectUpdateSignal = new EventEmitter();
  @Output() lessonCreateSignal = new EventEmitter();
  @Output() lessonUpdateSignal = new EventEmitter();



  constructor() {
    this.buildConnection();
    this.startConnection();
  }

  public buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:8089/pushingHub')
      .build();
  }

  public startConnection = () => {
    this.hubConnection.start()
      .then(() => {
        this.registerSignalEvents();
      })
      .catch(err => {
        console.log('Error while starting connection with hub: ' + err);

        setTimeout(function() { this.startConnection(); }, 3000);
      });
  }

  private registerSignalEvents() {
    this.hubConnection.on('visualizationCreated', (data: any) => {
      this.visualizationCreationSignal.emit(data);
    });
    this.hubConnection.on('visualizationUpdated', (data: any) => {
      this.visualizationUpdateSignal.emit(data);
    });
    this.hubConnection.on('accountCreated', (data: any) => {
      this.accountCreateSignal.emit(data);
    });
    this.hubConnection.on('accountUpdated', (data: any) => {
      this.accountUpdateSignal.emit(data);
    });
    this.hubConnection.on('subjectCreated', (data: any) => {
      this.subjectCreateSignal.emit(data);
    });
    this.hubConnection.on('subjectUpdated', (data: any) => {
      this.subjectUpdateSignal.emit(data);
    });
    this.hubConnection.on('lessonCreated', (data: any) => {
      this.lessonCreateSignal.emit(data);
    });
    this.hubConnection.on('lessonUpdated', (data: any) => {
      this.lessonUpdateSignal.emit(data);
    });
  }
}
