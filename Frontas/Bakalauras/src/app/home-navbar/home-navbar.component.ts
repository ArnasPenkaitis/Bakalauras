import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
  selector: 'app-home-navbar',
  templateUrl: './home-navbar.component.html',
  styleUrls: ['./home-navbar.component.scss'],
  animations: [
    trigger('panelInOut', [
      transition('void => *', [
        style({ opacity: 0 }),
        animate(1600)
      ]),
      transition('* => void', [
        animate(1600, style({ opacity: 1 }))
      ])
    ])
  ]
})
export class HomeNavbarComponent implements OnInit {

  private animated = false;
  admin = true;
  constructor(private router: Router) { }

  ngOnInit(): void {
    const temp = localStorage.getItem('isAdmin') as unknown;
    this.admin = temp as boolean;
  }

  navSlide() {
    const burger = document.querySelector('.burger');
    const nav = document.querySelector('.navLinks');
    const navLinks = document.querySelectorAll('.navLinks li');
    nav.classList.toggle('nav-active');
    burger.classList.toggle('toggle');
    if (!this.animated) {
      navLinks.forEach((link, index) => {

        setTimeout(() => {
          link.animate([
            { transform: 'translateX(50px)', opacity: 0 },
            { transform: 'translateX(0px)', opacity: 1 }
          ], {
            duration: 500,
            easing: 'ease',
            fill: 'forwards'

          });
        }, 500 * (index + 1));

      });
      this.animated = true;
    }
    else {
      navLinks.forEach((link, index) => {

        setTimeout(() => {
          link.animate([
            { transform: 'translateX(0px)' },
            { transform: 'translateX(50px)' }
          ], {
            duration: 500,
            easing: 'ease',
            fill: 'forwards'
          });
        }, 500 * (index + 1));

      });
      this.animated = false;
    }

  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('isAdmin');
    this.router.navigate(['/#']);
  }


}
