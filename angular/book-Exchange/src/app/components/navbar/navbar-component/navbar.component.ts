import { Component, inject } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import{ MatIconModule } from'@angular/material/icon'
import { RouterLink } from '@angular/router';

import { NavbarPersonalComponent } from '../navbar-personal/navbar-personal.component';
import { NavbarPrimaryComponent } from '../navbar-primary/navbar-primary.component';
import { NavbarStoreComponent } from '../navbar-store/navbar-store.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [MatIconModule,RouterLink,NavbarPersonalComponent,NavbarPrimaryComponent,NavbarStoreComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
userService = inject(UserService)
user = this.userService.user

logout(){
  this.userService.logoutUser();
}

}
