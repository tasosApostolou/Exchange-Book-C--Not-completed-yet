import { Component, inject } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import{ MatIconModule } from'@angular/material/icon'
import { RouterLink } from '@angular/router';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgModule } from '@angular/core';
import { ɵBrowserAnimationBuilder } from '@angular/animations';
import { Route, Router } from '@angular/router';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import {MatListModule, MatNavList} from '@angular/material/list'; 
import {MatBadgeModule} from '@angular/material/badge';
import { MatButtonToggle, MatButtonToggleChange, MatButtonToggleGroup, MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatOption, MatSelect } from '@angular/material/select';





@Component({
  selector: 'app-navbar-primary',
  standalone: true,
  imports: [MatIconModule,RouterLink,MatIconModule,MatMenu,MatMenuTrigger, MatButtonModule,MatIconModule,MatBadgeModule,MatToolbarModule,MatIconButton,MatSidenavModule,MatNavList,MatFormField,MatLabel,MatSelect,MatOption,MatMenu,MatMenuTrigger,MatButtonToggleModule,
    ],
  templateUrl: './navbar-primary.component.html',
  styleUrl: './navbar-primary.component.css'
})
export class NavbarPrimaryComponent {
  userService = inject(UserService)
  user = this.userService.user
  
  logout(){
    this.userService.logoutUser();
  }
  }
  