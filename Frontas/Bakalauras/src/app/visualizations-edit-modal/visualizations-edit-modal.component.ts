import { VisualizationsService } from './../Services/visualizations.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-visualizations-edit-modal',
  templateUrl: './visualizations-edit-modal.component.html',
  styleUrls: ['./visualizations-edit-modal.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ])]
})
export class VisualizationsEditModalComponent implements OnInit {

  visualizationForm = new FormGroup({
    Name: new FormGroup({
      name: new FormControl(this.data.name, [Validators.required])
    }),
    Description: new FormGroup({
      description: new FormControl(this.data.description, [Validators.required])
    })
  });

  private inputs: any;
  public incorrect = false;
  public tooLong = false;

  constructor(public dialogRef: MatDialogRef<VisualizationsEditModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private visualizationService: VisualizationsService) { }

  ngOnInit(): void {
    console.log(this.data);
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

  updateVisualization() {
    if (this.visualizationForm.value.Name.name.length === 0 || this.visualizationForm.value.Description.description.length === 0) {
      this.tooLong = false;
      this.incorrect = true;
      return;
    }
    console.log(this.data);
    const visualization = {
      fileUrl: this.data.fileUrl,
      id: this.data.id,
      name: this.visualizationForm.value.Name.name,
      description: this.visualizationForm.value.Description.description
    };

    this.visualizationService.UpdateVisualization(this.data.id, visualization).subscribe(x => {
      this.onNoClick();
    });
  }

}
