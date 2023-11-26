import { Routes } from '@angular/router';
import { LoginComponent } from './feature/login/login.component';
import { HomeComponent } from './feature/main/home/home.component';
import { authGuard } from './core/guards/auth.guard';
import { loggedInGuard } from './core/guards/logged-in.guard';

export const routes: Routes = [
  {path: '', component: LoginComponent, canActivate: [loggedInGuard]},
  {path: 'home', component: HomeComponent, canActivate: [authGuard]},
  {path: '**', component: HomeComponent}
];
