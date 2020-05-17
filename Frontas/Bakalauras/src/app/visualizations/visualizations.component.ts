import { HubServiceService } from './../Services/hub-service.service';
import { VisualizationsEditModalComponent } from './../visualizations-edit-modal/visualizations-edit-modal.component';
import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import '@google/model-viewer';
import { VisualizationsService } from '../Services/visualizations.service';
import { MatDialog } from '@angular/material/dialog';
import { VisualizationsCreateModalComponent } from '../visualizations-create-modal/visualizations-create-modal.component';

@Component({
  selector: 'app-visualizations',
  templateUrl: './visualizations.component.html',
  styleUrls: ['./visualizations.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ]),
    trigger('FadingHeaders', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 1 })),
      transition(':enter', animate('800ms ease-out')),
      transition(':leave', animate('800ms ease-in')),
    ])
  ],
  template: `<script type="module" src="https://unpkg.com/@google/model-viewer/dist/model-viewer.js"></script>`
})
export class VisualizationsComponent implements OnInit {


  AllVisualizationsList: any;
  filteredItems: any;
  creationModal = false;
  selectedRowI: number;
  path: any;

  constructor(
    private visualizationsService: VisualizationsService,
    public dialog: MatDialog,
    public dialogLesson: MatDialog,
    private signalRService: HubServiceService) {

  }

  ngOnInit(): void {
    this.signalRService.visualizationCreationSignal.subscribe((signal: any) => {
      this.onVisualizationCreatedEvent(signal);
    });
    this.signalRService.visualizationUpdateSignal.subscribe((signal: any) => {
      this.onVisualizationUpdatedEvent(signal);
    });
    this.visualizationsService.getAllVisualizations().subscribe(x => {
      this.AllVisualizationsList = x;
      this.assignCopy();
    });
  }

  onVisualizationCreatedEvent(signal: any) {
    this.AllVisualizationsList.push(signal);
    this.assignCopy();
  }
  onVisualizationUpdatedEvent(signal) {
    this.AllVisualizationsList.forEach(element => {
      if (element.id === signal.id) {
        element.name = signal.name;
        element.description = signal.description;
        element.fileUrl = signal.fileUrl;
      }
    });
    this.assignCopy();
  }
  LoadVisualization(visualization) {
    this.path = visualization.fileUrl;
  }

  setClickedRowAll(index) {
    this.selectedRowI = index;
  }

  assignCopy() {
    this.filteredItems = Object.assign([], this.AllVisualizationsList);
  }

  filterItem(value) {
    if (!value) {
      this.assignCopy();
    } // when nothing has typed
    this.filteredItems = Object.assign([], this.AllVisualizationsList).filter(
      item => item.name.toLowerCase().indexOf(value.toLowerCase()) > -1
    );
  }

  deleteVisualization(visualization, event) {
    event.stopPropagation();
    this.visualizationsService.deleteVisualizationNotChain(visualization).subscribe(x => {
      let count = 0;
      this.AllVisualizationsList.forEach(element => {
        if (element.id === visualization.id) {
          this.AllVisualizationsList.splice(count, 1);
          this.assignCopy();
        }
        count++;
      });
      this.selectedRowI = undefined;
    });
  }

  openDialog(visualization, event): void {
    event.stopPropagation();
    const dialogRef = this.dialog.open(VisualizationsEditModalComponent, {
      data: { name: visualization.name, description: visualization.description, id: visualization.id, fileUrl: visualization.fileUrl },
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDialogCreate(event): void {
    event.stopPropagation();
    const dialogRef = this.dialog.open(VisualizationsCreateModalComponent, {
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
