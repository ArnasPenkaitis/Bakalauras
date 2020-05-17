import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainComponent } from './main/main.component';
import { MainNavbarComponent } from './main-navbar/main-navbar.component';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatDialogModule} from '@angular/material/dialog';
import { OverlayModule } from '@angular/cdk/overlay';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from '../app/Services/AuthInterceptor';
import { Router } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HomeNavbarComponent } from './home-navbar/home-navbar.component';
import { SubjectsComponent } from './subjects/subjects.component';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { SceneComponent } from './scene/scene.component';
import { SubjectModalComponent } from './subject-modal/subject-modal.component';
import { LessonModalComponent } from './lesson-modal/lesson-modal.component';
import { LessonEditModalComponent } from './lesson-edit-modal/lesson-edit-modal.component';
import { SubjectsEditModalComponent } from './subjects-edit-modal/subjects-edit-modal.component';
import { AccountsComponent } from './accounts/accounts.component';
import { VisualizationsComponent } from './visualizations/visualizations.component';
import { AccountEditModalComponent } from './account-edit-modal/account-edit-modal.component';
import { VisualizationsEditModalComponent } from './visualizations-edit-modal/visualizations-edit-modal.component';
import { VisualizationsCreateModalComponent } from './visualizations-create-modal/visualizations-create-modal.component';



@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    MainNavbarComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    HomeNavbarComponent,
    SubjectsComponent,
    SceneComponent,
    SubjectModalComponent,
    LessonModalComponent,
    LessonEditModalComponent,
    SubjectsEditModalComponent,
    AccountsComponent,
    VisualizationsComponent,
    AccountEditModalComponent,
    VisualizationsEditModalComponent,
    VisualizationsCreateModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    OverlayModule,
    HttpClientModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    ScrollingModule,
    MatDialogModule,
    MatProgressSpinnerModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useFactory(router: Router) {
    return new AuthInterceptor(router);
  }, multi: true, deps: [Router]}],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
  ]
})
export class AppModule { }
