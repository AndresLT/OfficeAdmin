import { Routes } from '@angular/router';
import { LoginComponent } from './feature/login/login.component';
import { HomeComponent } from './feature/main/home/home.component';

export const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'home', component: HomeComponent},
  {path: '**', component: HomeComponent}
];
