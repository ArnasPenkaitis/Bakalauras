import { Teacher } from './../Models/Teacher';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TeacherService } from '../Services/teacher.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm = new FormGroup({
    Username: new FormGroup({
      username: new FormControl('', [Validators.required])
    }),
    Password: new FormGroup({
      password: new FormControl('', [Validators.required])
    }),
    Name: new FormGroup({
      name: new FormControl('', [Validators.required])
    }),
    Surname: new FormGroup({
      surname: new FormControl('', [Validators.required])
    }),
    Email: new FormGroup({
      email: new FormControl('', [Validators.email, Validators.required])
    }),
  });

  private temp: Teacher;
  private inputs: any;
  badCrediantials = false;
  alreadyTaken = false;
  allUsers: any;
  constructor(private teacherService: TeacherService, private router: Router) { }

  ngOnInit(): void {
    this.inputs = document.querySelectorAll('.input');
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    this.teacherService.getTeacher('http://localhost:8089/api/teacher').subscribe(x => { this.allUsers = x; });
  }

  register() {
    this.alreadyTaken = false;
    const username = this.registerForm.value.Username.username;
    const name = this.registerForm.value.Name.name;
    const email = this.registerForm.value.Email.email;
    const surname = this.registerForm.value.Surname.surname;
    const password = this.registerForm.value.Password.password;

    if (username === '' || name === '' || email === '' || surname === '' || password === '') {
      this.badCrediantials = true;
    } else {
      this.allUsers.forEach(element => {
        if (element.username === username) {
          this.alreadyTaken = true;
          this.badCrediantials = false;
        }
      });
      if (this.alreadyTaken) {

      } else {
        const useris = new Teacher(0, name, surname, username, password, email);
        this.teacherService.postTeacher('http://localhost:8089/api/teacher', useris).subscribe(responsas => {
          this.teacherService.postTeacher('http://localhost:8089/api/auth/token/teacher', useris).subscribe(anything => {
            localStorage.setItem('userId', anything.id);
            localStorage.setItem('token', anything.tokenas);
            this.router.navigate(['/home']);
          });
        });
      }
    }
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
}
