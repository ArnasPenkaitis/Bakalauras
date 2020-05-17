import { style } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Teacher } from '../Models/Teacher';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TeacherService } from '../Services/teacher.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    Username: new FormGroup({
      username: new FormControl('', [Validators.required])
    }),
    Password: new FormGroup({
      password: new FormControl('', [Validators.required])
    })
  });

  private temp: Teacher;
  private inputs: any;
  token: any;
  userioId: any;
  incorrect = false;
  constructor(private router: Router, private teacherService: TeacherService) {

  }

  ngOnInit(): void {
    this.inputs = document.querySelectorAll('.input');
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
  }

  login() {
    this.temp = new Teacher(
      0,
      '', ''
      , this.loginForm.value.Username.username
      , this.loginForm.value.Password.password
      , '');

    this.teacherService.postTeacher('http://localhost:8089/api/auth/token/teacher', this.temp).subscribe(response => {
      if (response.length === 0) {
        this.incorrect = true;
      } else {
        if (this.loginForm.value.Username.username === 'admin' && this.loginForm.value.Password.password === 'admin') {
          localStorage.setItem('isAdmin', 'true');
        }
        localStorage.setItem('userId', response.id);
        localStorage.setItem('token', response.tokenas);
        const navbar = document.querySelector('.app-main-navbar');
        // ToDo Sutvarkyti animation tarp login ir home navbarui
        this.router.navigate(['/home']);
      }
    });
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
