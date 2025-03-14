import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { LocalService } from '../services/local.service';

export const authGuard: CanActivateFn = (route, state) => {
  const localService = inject(LocalService);

  if (localService.isAuthenticated(LocalService.AuthTokenName)) {
    return true;
  } else {
    window.location.href = '/login'; //fix
    return false;
  }
};
