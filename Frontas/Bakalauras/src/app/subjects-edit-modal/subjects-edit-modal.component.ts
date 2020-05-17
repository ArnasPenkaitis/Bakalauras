import { SubjectsService } from './../Services/subjects.service';
import { Component, OnInit, Inject } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-subjects-edit-modal',
  templateUrl: './subjects-edit-modal.component.html',
  styleUrls: ['./subjects-edit-modal.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ])]
})
export class SubjectsEditModalComponent implements OnInit {

  subjectForm = new FormGroup({
    Name: new FormGroup({
      name: new FormControl(this.data.name, [Validators.required])
    })
  });

  private inputs: any;
  public incorrect = false;
  public tooLong = false;

  constructor(public dialogRef: MatDialogRef<SubjectsEditModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private subjectsService: SubjectsService) { }

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

  updateSubject() {
    if (this.subjectForm.value.Name.name.length === 0) {
      this.tooLong = false;
      this.incorrect = true;
      return;
    }
    if (this.subjectForm.value.Name.name.length > 15) {
      this.incorrect = false;
      this.tooLong = true;
      return;
    }
    const subject = { id: this.data.id, name: this.subjectForm.value.Name.name};
    this.subjectsService.UpdateSubjectByTeacher(
      localStorage.getItem('userId'), subject
    ).subscribe(x => this.onNoClick());
  }

}
