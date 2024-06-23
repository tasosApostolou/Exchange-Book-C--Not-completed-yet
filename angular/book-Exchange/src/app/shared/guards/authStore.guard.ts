import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const authStoreGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router)

  if(userService.user().role==="STORE"){
    return true
  }
  return router.navigate(['login']);
};
