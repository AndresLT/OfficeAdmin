import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { loggedInGuard } from './core/guards/logged-in.guard';

export const routes: Routes = [
  {path: '', loadComponent: () => import('./feature/login/login.component').then(m => m.LoginComponent),canActivate:[loggedInGuard]},
  {path: 'home', loadChildren: () => import('./feature/main/main.routes').then(m => m.routes), canActivate: [authGuard]},
  {path: '**', loadChildren: () => import('./feature/main/main.routes').then(m => m.routes), canActivate: [authGuard]}
];
