import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth';
import { map, take } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.isLoggedIn.pipe(
    take(1),
    map(loggedIn => {
      if (loggedIn) {
        return true; // Grants access to restricted HRMS modules
      } else {
        // Fulfills 'Secure Access' requirement: Redirect to login
        router.navigate(['/login']);
        return false;
      }
    })
  );
};