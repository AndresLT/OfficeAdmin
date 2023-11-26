import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { tap } from 'rxjs';
import { ApiService } from '../services/api.service';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  inject(ApiService).loading = true

  return next(req)
};
