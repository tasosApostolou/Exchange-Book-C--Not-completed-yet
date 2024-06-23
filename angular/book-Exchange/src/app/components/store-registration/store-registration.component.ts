import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { MatInputModule } from '@angular/material/input';
import { UserService } from 'src/app/shared/services/user.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { UserLoginComponent } from '../user-login/user-login.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenu } from '@angular/material/menu';
import { MatToolbar, MatToolbarModule, MatToolbarRow } from '@angular/material/toolbar';
import { storeRegister } from 'src/app/shared/interfaces/store';
import { StoreService } from 'src/app/shared/services/store.service';
import { Dialog } from '@angular/cdk/dialog';

@Component({
  selector: 'app-store-registration',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule,MatInputModule,MatSelectModule,MatButtonModule,RouterLink,RouterOutlet,UserLoginComponent,MatIconModule,MatMenu, MatToolbar, MatToolbarRow,MatToolbarModule,MatButtonModule, MatIconModule],
  templateUrl: './store-registration.component.html',
  styleUrl: './store-registration.component.css'
})
export class StoreRegistrationComponent {
  userService = inject(UserService);
  storeService = inject(StoreService);
  router:Router = 
  inject(Router)
  registrationStatus:{succes:boolean;message:string} = {succes:false,message:'Not Attempted yet'}
  form = new FormGroup({
    name:new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    username:new FormControl('', Validators.required),
    Email:new FormControl('', Validators.required),
    password: new FormControl('',[Validators.required, Validators.minLength(4)]),
    userRole:new FormControl("STORE",Validators.required),
    confirmPassword:new FormControl('',[Validators.required, Validators.minLength(4)]),
  },this.passwordConfirmsValidator)
  
  passwordConfirmsValidator(form:FormGroup){
    if (form.get('password').value !== form.get('confirmPassword').value){
      form.get('confirmPassword').setErrors({passwordMismatch:true});
      return{ passwordMismatch:true}
    }
    return {}
  }
  
  onSubmit(value:any){
    console.log(value);
    const storeUser = this.form.value as storeRegister
    delete storeUser['confirmPassword']
    console.log(storeUser)
  
    this.storeService.registerStore(storeUser).subscribe({
      next: (response) => {
        console.log('User register', response);
        this.registrationStatus = {succes:true,message:"User register succesfully"}
      },
      error: (response) => {
        const message = "username already exists or error"
        console.log('Error registration user', response);
        this.registrationStatus = {succes:false,message}
        // this.registrationStatus = {succes:false,message:message}
      }
    })
  }
  registerUser(){
  this.form.reset();
  this.form.patchValue({userRole:"STORE"})
  this.registrationStatus ={
    succes:false,message:'Not Attempted yet'
  }
  }
  
  navigateLogin(){
    this.router.navigate(['login']);
  }
  
  }
  