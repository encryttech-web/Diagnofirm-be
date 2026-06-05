import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  const router = inject(Router);

  if (sessionStorage.getItem('isLoggedIn')) {
    return true;
  }

  router.navigate(['/login'], { replaceUrl: true });
  return false;
};