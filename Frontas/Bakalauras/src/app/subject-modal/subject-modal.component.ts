import { SubjectsService } from './../Services/subjects.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-subject-modal',
  templateUrl: './subject-modal.component.html',
  styleUrls: ['./subject-modal.component.scss']
})
export class SubjectModalComponent implements OnInit {

  subjectForm = new FormGroup({
    Name: new FormGroup({
      name: new FormControl('', [Validators.required])
    })
  });

  private inputs: any;
  public incorrect = false;
  constructor(
    public dialogRef: MatDialogRef<SubjectModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private subjectService: SubjectsService) { }

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

  createSubject() {
    if (this.subjectForm.value.Name.name.length === 0) {
      this.incorrect = true;
      return;
    }
    this.subjectService.postSubjectByTeacher(localStorage.getItem('userId'), this.subjectForm.value.Name).subscribe( x => this.onNoClick());
  }

}
