import { Component, Inject, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { User } from 'src/app/shared/interfaces/user';
import { UserService } from 'src/app/shared/services/user.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatMenu } from '@angular/material/menu';
import {
  Dialog,
  DialogRef,
  DIALOG_DATA,
  DialogModule,
} from '@angular/cdk/dialog';
import { PersonService } from 'src/app/shared/services/person.service';
// import { ChangePasswordComponent } from '../change-password/change-password.component';
import { Person } from 'src/app/shared/interfaces/person';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { ChangePasswordComponent } from '../change-password/change-password.component';
@Component({
  selector: 'app-manage-account',
  standalone: true,
  imports: [ ReactiveFormsModule, MatFormFieldModule,MatInputModule,MatSelectModule,MatButtonModule,RouterLink,RouterOutlet,MatIconModule,MatMenu,MatButtonModule],
  templateUrl: './manage-account.component.html',
  styleUrl: './manage-account.component.css'
})
export class ManageAccountComponent {
  constructor(public dialog: Dialog, dia:Dialog) {}
  userService = inject(UserService);
  userSignal = this.userService.user
  personService = inject(PersonService);
  router:Router = 
  inject(Router)
  registrationStatus:{succes:boolean;message:string} = {succes:false,message:'Not Attempted yet'}
  
  passwordChange(){
    this.dialog.open(ChangePasswordComponent)
  }
  
  deleteUserDialog(){
    this.dialog.open(DeleteDialogComponent);
  }
  }
