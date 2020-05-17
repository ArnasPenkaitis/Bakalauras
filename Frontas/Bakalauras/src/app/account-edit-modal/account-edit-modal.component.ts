import { TeacherService } from './../Services/teacher.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-account-edit-modal',
  templateUrl: './account-edit-modal.component.html',
  styleUrls: ['./account-edit-modal.component.scss']
})
export class AccountEditModalComponent implements OnInit {

  registerForm = new FormGroup({
    Username: new FormGroup({
      username: new FormControl(this.data.account.username, [Validators.required])
    }),
    Password: new FormGroup({
      password: new FormControl(this.data.account.password, [Validators.required])
    }),
    Name: new FormGroup({
      name: new FormControl(this.data.account.name, [Validators.required])
    }),
    Surname: new FormGroup({
      surname: new FormControl(this.data.account.surname, [Validators.required])
    }),
    Email: new FormGroup({
      email: new FormControl(this.data.account.email, [Validators.email, Validators.required])
    }),
  });

  private inputs: any;
  public badCrediantials = false;

  constructor(public dialogRef: MatDialogRef<AccountEditModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private accountService: TeacherService) { }

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

  updateAccount() {
    if (this.registerForm.value.Name.name.length === 0
      || this.registerForm.value.Username.username.length === 0
      || this.registerForm.value.Password.password.length === 0
      || this.registerForm.value.Surname.surname.length === 0
      || this.registerForm.value.Email.email.length === 0) {
      this.badCrediantials = true;
      return;
    }

    const tempAcc = {
      username: this.registerForm.value.Username.username,
      password: this.registerForm.value.Password.password,
      name: this.registerForm.value.Name.name,
      surname: this.registerForm.value.Surname.surname,
      email: this.registerForm.value.Email.email,
      id: this.data.account.id
    };
    this.accountService.UpdateTeacher('', tempAcc).subscribe(x => {
      this.onNoClick();
    });

  }

}
