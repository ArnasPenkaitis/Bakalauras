import { VisualizationsComponent } from './visualizations/visualizations.component';
import { AccountsComponent } from './accounts/accounts.component';
import { SubjectsComponent } from './subjects/subjects.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'login', component: LoginComponent, data: { animation: 'isLeft' } },
  { path: 'register', component: RegisterComponent, data: { animation: 'isRight' } },
  { path: 'home', component: HomeComponent},
  { path: 'subjects', component: SubjectsComponent, data: {animation: 'isRight'}},
  { path: 'accounts', component: AccountsComponent},
  { path: 'visualizations', component: VisualizationsComponent, data: {animation: 'isRight'}},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
