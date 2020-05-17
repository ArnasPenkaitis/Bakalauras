import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.scss']
})
export class MainNavbarComponent implements OnInit {

  private animated = false;
  constructor() { }

  ngOnInit(): void {
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

}
