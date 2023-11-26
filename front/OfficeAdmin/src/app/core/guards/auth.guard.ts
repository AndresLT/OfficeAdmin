import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { LocalService } from '../services/local.service';

export const authGuard: CanActivateFn = (route, state) => {
  const user = inject(LocalService).get('user')
  const router = inject(Router)
  if(user){
    return true;
  }else{
    router.navigate([''])
    return false;
  }
};
