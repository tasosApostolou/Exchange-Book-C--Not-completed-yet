import { Injectable, inject, signal } from '@angular/core';
import {Person, UserSignUpDTO } from '../interfaces/person';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Book } from '../interfaces/book';
import { UserService } from './user.service';
import { User } from '../interfaces/user';
import { storeRegister } from '../interfaces/store';

const API_URL = `${environment.apiURL}/personal`;

@Injectable({
  providedIn: 'root'
})
export class PersonService {
http: HttpClient = inject(HttpClient);
userService = inject(UserService)
personId = () => {
  return this.userService.user().roleEntityId
}
personSignal = signal<Person | null>(null);

  constructor() { }

  registerPerson(userSignupDTO:UserSignUpDTO){
    return this.http.post<{data:JSON}>(`https://localhost:7279/api/User/SignupUser
`, userSignupDTO);
  }

  getPersonBooks(){
    console.log(`func:${this.userService.user().role}`)
    return this.http.get<Book[]>(`https://localhost:7279/api/Person/GetPersonBooks/${this.userService.user().roleEntityId}`,{
      headers: {
        Accept:'application/json'
      },
    })
  }

  // https://localhost:7279/api/Person/GetPersonBooks/8


  deleteBookFromLoggedInPerson(bookId:number){
   return this.http.delete<{any:Book}>(`${API_URL}/${this.userService.user().roleEntityId}/books/${bookId}`)
  }
  updatePersonal(person:Person){
    return this.http.put<{any:Person}>(`https://localhost:7279/api/Person/UpdateUserAccount/${
      person.id}`,person)
  }
  getPersonById(personID:number){
    return this.http.get<Person>(`${API_URL}/${personID}`)
  }

  getPersonByUserId(userID:number){
    return this.http.get<Person>(`${API_URL}/user/${userID}`,{
      headers: {
        Accept:'application/json'
      },
    })
  }

  deletePersonUser(){
    return this.http.delete<{delete:string}>(`https://localhost:7279/api/User/DeleteUser/${this.userService.user().userId}`)
  }
}