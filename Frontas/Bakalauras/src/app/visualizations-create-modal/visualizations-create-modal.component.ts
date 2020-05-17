import { async } from '@angular/core/testing';
import { Component, OnInit, Inject } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { VisualizationsService } from '../Services/visualizations.service';

@Component({
  selector: 'app-visualizations-create-modal',
  templateUrl: './visualizations-create-modal.component.html',
  styleUrls: ['./visualizations-create-modal.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ])]
})
export class VisualizationsCreateModalComponent implements OnInit {

  visualizationForm = new FormGroup({
    Name: new FormGroup({
      name: new FormControl('', [Validators.required])
    }),
    Description: new FormGroup({
      description: new FormControl('', [Validators.required])
    })
  });

  private inputs: any;
  public incorrect = false;
  public tooLong = false;
  public content: string;
  public loader = false;
  constructor(public dialogRef: MatDialogRef<VisualizationsCreateModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private visualizationService: VisualizationsService) { }

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

  createVisualization() {
    if (this.visualizationForm.value.Name.name.length === 0 ||
      this.visualizationForm.value.Description.description.length === 0 ||
      this.content.length === 0) {
      this.tooLong = false;
      this.incorrect = true;
      return;
    }
    this.loader = true;
    const ref: HTMLElement = document.getElementById('fileInputLabel') as HTMLElement;

    this.visualizationService.postVisualizationNotChain({
      name: this.visualizationForm.value.Name.name,
      description: this.visualizationForm.value.Description.description,
      content: this.content
    }, ref.textContent).subscribe(x => {
      this.loader = false;
      this.onNoClick();
    });
  }

  toggleFileBrowser() {
    const ref: HTMLElement = document.getElementById('fileInput') as HTMLElement;
    ref.click();
  }

  async selectedFile(event) {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      this.content = '';
      reader.readAsDataURL(event.target.files[0]);
      this.loader = true;
      const result: any = await new Promise((resolve, reject) => {
        // tslint:disable-next-line: only-arrow-functions
        reader.onload = async function(e: any) {
          const ref: HTMLElement = document.getElementById('fileInputLabel') as HTMLElement;
          const labelText = event.target.value.replace(/^.*[\\\/]/, '').split('/');
          ref.textContent = labelText[0];
          resolve(e.target.result);
        };
      });
      this.loader = false;
      const contentList = result.split(',');
      this.content = contentList[1];
    }

  }

}
