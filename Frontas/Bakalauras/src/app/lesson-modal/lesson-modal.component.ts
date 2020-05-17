import { LessonsService } from './../Services/lessons.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SubjectModalComponent } from '../subject-modal/subject-modal.component';
import { SubjectsService } from '../Services/subjects.service';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-lesson-modal',
  templateUrl: './lesson-modal.component.html',
  styleUrls: ['./lesson-modal.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ])]

})
export class LessonModalComponent implements OnInit {

  lessonForm = new FormGroup({
    Name: new FormGroup({
      name: new FormControl('', [Validators.required])
    }),
    Abbreviation: new FormGroup({
      abbreviation: new FormControl('', [Validators.required])
    })
  });

  private inputs: any;
  public incorrect = false;
  public tooLong = false;

  constructor(
    public dialogRef: MatDialogRef<LessonModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private lessonService: LessonsService) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.inputs = document.querySelectorAll('.input');
  }

  addcl() {
    // tslint:disable-next-line: deprecation
    const parent = (event.target as HTMLElement).parentNode.parentElement;
    parent.classList.add('focus');
  }

  remcl() {
    // tslint:disable-next-line: deprecation
    const parent = (event.target as HTMLElement).parentNode.parentElement;
    // tslint:disable-next-line: deprecation
    const input = (event.target as HTMLInputElement);
    if (input.value === '') {
      parent.classList.remove('focus');
    }
  }

  inputAnimation() {
    this.inputs.forEach(input => {
      input.addEventListener('focus', this.addcl());
      // input.addEventListener('blur', this.remcl());
    });
  }

  createLesson() {
    if (this.lessonForm.value.Name.name.length === 0 || this.lessonForm.value.Abbreviation.abbreviation.length === 0) {
      this.tooLong = false;
      this.incorrect = true;
      return;
    }
    if (this.lessonForm.value.Name.name.length > 15 || this.lessonForm.value.Abbreviation.abbreviation.length > 5) {
      this.incorrect = false;
      this.tooLong = true;
      return;
    }
    let lesson = { name: this.lessonForm.value.Name.name, abbreviation: this.lessonForm.value.Abbreviation.abbreviation };
    this.lessonService.postLessonsByTeacherBySubject(
      localStorage.getItem('userId'), this.data.subject.id, lesson
    ).subscribe(x => this.onNoClick());
  }

}
