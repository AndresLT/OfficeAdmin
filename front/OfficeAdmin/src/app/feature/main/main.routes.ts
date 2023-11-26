import { Routes } from "@angular/router";

export const routes: Routes = [
  {
    path:'', redirectTo:'office', pathMatch:'full'
  },
  {
    path:'', loadComponent: () => import('./home/home.component').then(m => m.HomeComponent),
    children: [
      {
        path: 'office', loadComponent: () => import('./office/office.component').then(m => m.OfficeComponent)
      },
      {
        path: 'currency', loadComponent: () => import('./currency/currency.component').then(m => m.CurrencyComponent)
      },
      {
        path: 'user', loadComponent: () => import('./user/user.component').then(m => m.UserComponent)
      }
    ]
  }
]
