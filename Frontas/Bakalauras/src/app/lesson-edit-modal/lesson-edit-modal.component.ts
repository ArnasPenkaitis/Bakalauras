import { Component, OnInit, Inject } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LessonsService } from '../Services/lessons.service';

@Component({
  selector: 'app-lesson-edit-modal',
  templateUrl: './lesson-edit-modal.component.html',
  styleUrls: ['./lesson-edit-modal.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ])]
})
export class LessonEditModalComponent implements OnInit {

  lessonForm = new FormGroup({
    Name: new FormGroup({
      name: new FormControl(this.data.name, [Validators.required])
    }),
    Abbreviation: new FormGroup({
      abbreviation: new FormControl(this.data.abbreviation, [Validators.required])
    })
  });

  private inputs: any;
  public incorrect = false;
  public tooLong = false;

  constructor(public dialogRef: MatDialogRef<LessonEditModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private lessonService: LessonsService) { }

  ngOnInit(): void {
    this.inputs = document.querySelectorAll('.input');
  }

  onNoClick(): void {
    this.dialogRef.close();
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

  updateLesson() {
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
    // tslint:disable-next-line: max-line-length
    const lesson = { id: this.data.id, name: this.lessonForm.value.Name.name, abbreviation: this.lessonForm.value.Abbreviation.abbreviation };
    this.lessonService.UpdateLessonsByTeacherBySubject(
      localStorage.getItem('userId'), this.data.subject.id, lesson
    ).subscribe(x => this.onNoClick());
  }

}
