import { HubServiceService } from './../Services/hub-service.service';
import { SubjectsEditModalComponent } from './../subjects-edit-modal/subjects-edit-modal.component';
import { LessonEditModalComponent } from './../lesson-edit-modal/lesson-edit-modal.component';
import { LessonModalComponent } from './../lesson-modal/lesson-modal.component';
import { VisualizationsService } from './../Services/visualizations.service';
import { LessonsService } from './../Services/lessons.service';
import { SubjectsService } from './../Services/subjects.service';
import { Component, OnInit } from '@angular/core';
import { trigger, transition, style, animate, state } from '@angular/animations';
import '@google/model-viewer';
import { MatDialog } from '@angular/material/dialog';
import { SubjectModalComponent } from '../subject-modal/subject-modal.component';
@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss'],
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
export class SubjectsComponent implements OnInit {


  SubjectList: any;
  LessonList: any;
  SelectedVisualizationsList: any;
  AllVisualizationsList: any;

  selectedRowI: number;
  selectedRowY: number;
  selectedRowZ: number;
  selectedRowX: number;
  fromAll = false;
  fromSelected = false;

  SubjectSelected = false;
  LessonSelected = false;
  subject: any;
  lesson: any;

  path: any;

  constructor(
    private subjectsService: SubjectsService,
    private lessonsService: LessonsService,
    private visualizationsService: VisualizationsService,
    public dialog: MatDialog,
    public dialogLesson: MatDialog,
    private signalRService: HubServiceService
    ) {
    this.subjectsService.getSubjectsByTeacher(localStorage.getItem('userId')).subscribe(x => this.SubjectList = x);
  }

  ngOnInit(): void {
    this.signalRService.visualizationCreationSignal.subscribe((signal: any) => {
      this.onVisualizationCreated(signal);
    });
    this.signalRService.visualizationUpdateSignal.subscribe((signal: any) => {
      this.onVisualizationUpdated(signal);
    });
    this.signalRService.subjectCreateSignal.subscribe((signal: any) => {
      this.onSubjectCreated(signal);
    });
    this.signalRService.subjectUpdateSignal.subscribe((signal: any) => {
      this.onSubjectUpdated(signal);
    });
    this.signalRService.lessonCreateSignal.subscribe((signal: any) => {
      this.onLessonCreated(signal);
    });
    this.signalRService.lessonUpdateSignal.subscribe((signal: any) => {
      this.onLessonUpdated(signal);
    });
  }
  onVisualizationCreated(signal){
    this.AllVisualizationsList.push(signal);
  }
  onVisualizationUpdated(signal){
    this.AllVisualizationsList.forEach(element => {
      if (element.id === signal.id) {
        element.name = signal.name;
        element.description = signal.description;
        element.fileUrl = signal.fileUrl;
      }
    });
  }
  onSubjectCreated(signal){
    this.SubjectList.push(signal);
  }
  onSubjectUpdated(signal){
    this.SubjectList.forEach(element => {
      if (element.id === signal.id) {
        element.name = signal.name;
      }
    });
  }
  onLessonCreated(signal){
    this.LessonList.push(signal);
  }
  onLessonUpdated(signal){
    this.LessonList.forEach(element => {
      if (element.id === signal.id) {
        element.name = signal.name;
        element.abbreviation = signal.abbreviation;
      }
    });
  }


  SelectedSubject(subject) {
    if (this.subject === subject) {
      return;
    }
    this.fromSelected = false;
    this.fromAll = false;
    this.LessonSelected = false;
    this.subject = subject;
    this.LessonList = [];
    this.lessonsService.getLessonsByTeacherBySubject(localStorage.getItem('userId'), subject.id).subscribe(x => {
      this.LessonList = x;
      this.SubjectSelected = true;
    });

  }

  SelectedLesson(lesson) {
    if (this.lesson === lesson) {
      return;
    }
    this.fromSelected = false;
    this.fromAll = false;
    this.lesson = lesson;
    this.SelectedVisualizationsList = [];

    this.visualizationsService.getAllVisualizations().subscribe(x => {
      this.AllVisualizationsList = x;
      this.visualizationsService.getVisualizationByClass(lesson.id).subscribe(y => {
        this.SelectedVisualizationsList = y;

        for (let allindex = 0; allindex < this.AllVisualizationsList.length; allindex++) {
          // tslint:disable-next-line: prefer-for-of
          for (let selectedIndex = 0; selectedIndex < this.SelectedVisualizationsList.length; selectedIndex++) {
            if (this.AllVisualizationsList[allindex].id === this.SelectedVisualizationsList[selectedIndex].id) {
              this.AllVisualizationsList.splice(allindex, 1);
              allindex--;
              break;
            }
          }
        }
        this.LessonSelected = true;
      });
    });
  }


  SelectedVisualization(visualization, event) {
    event.stopPropagation();
    this.selectedRowX = undefined;
    this.selectedRowZ = undefined;
    this.visualizationsService.postVisualization(visualization.id, this.lesson.id).subscribe(x => {
      let count = 0;
      this.AllVisualizationsList.forEach(element => {
        if (element.name === visualization.name) {
          this.AllVisualizationsList.splice(count, 1);
          this.SelectedVisualizationsList.push(element);
        }
        count++;
      });
    });
  }
  SelectedVisualizationRemove(visualization, event) {
    event.stopPropagation();
    this.selectedRowX = undefined;
    this.selectedRowZ = undefined;
    this.visualizationsService.deleteVisualization(visualization.id, this.lesson.id).subscribe(x => {
      let count = 0;
      this.SelectedVisualizationsList.forEach(element => {
        if (element.name === visualization.name) {
          this.SelectedVisualizationsList.splice(count, 1);
          this.AllVisualizationsList.push(element);
        }
        count++;
      });
    });
  }

  LoadVisualization(visualization) {
    this.path = visualization.fileUrl;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(SubjectModalComponent, {
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDialogLesson(): void {
    const dialogRef = this.dialogLesson.open(LessonModalComponent, {
      data: { subject: this.subject },
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDialogLessonUpdate(lesson, event): void {
    event.stopPropagation();
    const dialogRef = this.dialogLesson.open(LessonEditModalComponent, {
      data: { subject: this.subject, name: lesson.name, abbreviation: lesson.abbreviation, id: lesson.id },
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDialogSubjectUpdate(subject, event): void {
    event.stopPropagation();
    const dialogRef = this.dialogLesson.open(SubjectsEditModalComponent, {
      data: { subject: this.subject, name: subject.name, id: subject.id },
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  deleteSubject(subject, event) {
    event.stopPropagation();
    this.subjectsService.deleteSubjectByTeacher(localStorage.getItem('userId'), subject.id).subscribe(x => {
      let count = 0;
      this.SubjectList.forEach(element => {
        if (element.id === subject.id) {
          this.SubjectList.splice(count, 1);
        }
        count++;
      });
      this.selectedRowI = undefined;
    });
  }

  deleteLesson(lesson, event) {
    event.stopPropagation();
    this.lessonsService.deleteLessonsByTeacherBySubject(localStorage.getItem('userId'), 0, lesson.id).subscribe(x => {
      let count = 0;
      this.LessonList.forEach(element => {
        if (element.id === lesson.id) {
          this.LessonList.splice(count, 1);
        }
        count++;
      });
      this.selectedRowY = undefined;
    });
  }

  setClickedRowSubject(index) {
    this.selectedRowI = index;
    this.selectedRowY = undefined;
  }
  setClickedRowLesson(index) {
    this.selectedRowY = index;
  }
  setClickedRowSelected(index) {
    this.fromAll = false;
    this.selectedRowZ = index;
    this.fromSelected = true;
  }
  setClickedRowAll(index) {
    this.fromSelected = false;
    this.selectedRowX = index;
    this.fromAll = true;
  }
}
