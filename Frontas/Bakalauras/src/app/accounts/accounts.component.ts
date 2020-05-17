import { AccountEditModalComponent } from './../account-edit-modal/account-edit-modal.component';
import { TeacherService } from './../Services/teacher.service';
import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MatDialog } from '@angular/material/dialog';
import { SubjectsEditModalComponent } from '../subjects-edit-modal/subjects-edit-modal.component';
import { HubServiceService } from '../Services/hub-service.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss'],
  animations: [
    trigger('Fading', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0.7 })),
      transition(':leave', animate('800ms ease-in')),
      transition(':enter', animate('800ms ease-out')),
    ])
  ]
})
export class AccountsComponent implements OnInit {

  accounts: any = [];
  filteredItems: any;

  constructor(
    private usersService: TeacherService,
    public dialog: MatDialog,
    public dialogAccount: MatDialog,
    private signalRService: HubServiceService) { }

  ngOnInit(): void {
    this.signalRService.accountCreateSignal.subscribe((signal: any) => {
      this.onAccountCreated(signal);
    });
    this.signalRService.accountUpdateSignal.subscribe((signal: any) => {
      this.onAccountUpdated(signal);
    });
    this.usersService.getTeachers('').subscribe(x => {
      this.accounts = x;
      this.assignCopy();
    });
  }
  onAccountCreated(signal){
    this.accounts.push(signal);
    this.assignCopy();
  }
  onAccountUpdated(signal){
    this.accounts.forEach(element => {
      if (element.id === signal.id) {
        element.name = signal.name;
        element.surname = signal.surname;
        element.email = signal.email;
        element.password = signal.password;
        element.username = signal.username;
      }
    });
    this.assignCopy();
  }

  deleteAccount(element, event) {
    event.stopPropagation();
    this.usersService.deleteTeacher('', element.id).subscribe(x => {
      for (let index = 0; index < this.accounts.length; index++) {
        if (this.accounts[index] === element) {
          this.accounts.splice(index, 1);
          this.assignCopy();
        }
      }
    });
  }

  openDialogAccountUpdate(element, event): void {
    event.stopPropagation();
    const dialogRef = this.dialogAccount.open(AccountEditModalComponent, {
      data: { account: element },
      panelClass: 'myapp-no-padding-dialog'
    });
    dialogRef.addPanelClass('myapp-no-padding-dialog');

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  assignCopy() {
    this.filteredItems = Object.assign([], this.accounts);
  }

  filterItem(value) {
    if (!value) {
      this.assignCopy();
    } // when nothing has typed
    this.filteredItems = Object.assign([], this.accounts).filter(
      item => item.name.toLowerCase().indexOf(value.toLowerCase()) > -1 ||
        item.surname.toLowerCase().indexOf(value.toLowerCase()) > -1 ||
        item.username.toLowerCase().indexOf(value.toLowerCase()) > -1 ||
        item.password.toLowerCase().indexOf(value.toLowerCase()) > -1 ||
        item.email.toLowerCase().indexOf(value.toLowerCase()) > -1
    );
  }

}
