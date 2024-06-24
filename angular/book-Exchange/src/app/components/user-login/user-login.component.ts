import { Component, inject } from '@angular/core';
import {FormControl,FormGroup,ReactiveFormsModule,Validators} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Credentials, LoggedInUser, User } from 'src/app/shared/interfaces/user';
import { UserService } from 'src/app/shared/services/user.service';


@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.css',
})
export class UserLoginComponent {
  userService = inject(UserService);
  router = inject(Router);

  invalidLogin = false;

  form = new FormGroup({
    Username: new FormControl('',Validators.required),
    Password: new FormControl('', Validators.required),
  });

  onSubmit() {
    const credentials = this.form.value as Credentials;
    console.log(credentials)
    const username = null
    let resp:User
    let role = ''
    this.userService.loginUser(credentials).subscribe({
      next: (response) => {
        console.log(response)
        console.log(jwtDecode(response.token))
        const access_token = response.token
        localStorage.setItem('access_token', access_token);
        const decodedTokenSubject = jwtDecode(access_token) as unknown as LoggedInUser;
        this.userService.user.set({
          userId: decodedTokenSubject.userId,
          sub: decodedTokenSubject.sub,
          role: decodedTokenSubject.role,
          roleEntityId: Number(decodedTokenSubject.roleEntityId)
        });
        const rout = {
          'PERSONAL':'person-welcome',
          'STORE':'store-welcome'
        }
        this.router.navigate([rout[decodedTokenSubject.role]]);
        // this.router.navigate(['person-welcome'])
      },
    // if role is PERSONAL, router navigates to page with its book, else if role is store, router navigates at page with  store books 
      error: (response) => {
        console.error('Login Error', response);
        this.invalidLogin = true;
      },
    });
  }
}