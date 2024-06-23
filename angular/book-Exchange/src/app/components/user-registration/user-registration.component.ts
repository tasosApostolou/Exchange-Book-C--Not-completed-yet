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
import { UserSignUpDTO } from 'src/app/shared/interfaces/person';
import { PersonService } from 'src/app/shared/services/person.service';


@Component({
  selector: 'app-user-registration',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule,MatInputModule,MatSelectModule,MatButtonModule,RouterLink,RouterOutlet,UserLoginComponent,MatIconModule,MatMenu, MatToolbar, MatToolbarRow,MatToolbarModule,MatButtonModule, MatIconModule],
  templateUrl: './user-registration.component.html',
  styleUrl: './user-registration.component.css'
})
export class UserRegistrationComponent {
userService = inject(UserService);
personService = inject(PersonService);
router:Router = 
inject(Router)
registrationStatus:{succes:boolean;message:string} = {succes:false,message:'Not Attempted yet'}

form = new FormGroup({
  Username:new FormControl('', Validators.required),
  Email:new FormControl('', Validators.required),
  Password: new FormControl('',[Validators.required, Validators.minLength(4)]),
  Firstname:new FormControl('', Validators.required),
  Lastname: new FormControl('', Validators.required),
  PhoneNumber: new FormControl('', Validators.required),
  UserRole:new FormControl('PERSONAL',Validators.required),
  confirmPassword:new FormControl('',[Validators.required, Validators.minLength(4)]),
},this.passwordConfirmsValidator)

passwordConfirmsValidator(form:FormGroup){
  if (form.get('Password').value !== form.get('confirmPassword').value){
    form.get('confirmPassword').setErrors({passwordMismatch:true});
    return{ passwordMismatch:true}
  }
  return {}
}

onSubmit(value:any){
  console.log(value);
  const personUser = this.form.value as UserSignUpDTO
  delete personUser['confirmPassword']

  this.personService.registerPerson(personUser).subscribe({
    next: (response) => {
      console.log('User register', response);
      this.registrationStatus = {succes:true,message:"User register succesfully"}
    },
    error: (response) => {
      const message = "username already exists or error"
      console.log('Error registration user', message);
      this.registrationStatus = {succes:false,message}
      // this.registrationStatus = {succes:false,message:message}
    }
  })
}
registerUser(){
this.form.reset();
this.form.patchValue({UserRole:"PERSONAL"})
this.registrationStatus ={
  succes:false,message:'Not Attempted yet'
}
}

navigateLogin(){
  this.router.navigate(['login']);
}

}
