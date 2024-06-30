import { HttpClient } from '@angular/common/http';
import { Injectable, effect, inject, signal } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Credentials, JwtTokenDTO, LoggedInUser, User } from '../interfaces/user';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Book, StoreBook } from '../interfaces/book';
import { Observable } from 'rxjs';

const API_USER = `${environment.apiURL}/User`;

@Injectable({
  providedIn: 'root',
})
export class UserService {

http: HttpClient = inject(HttpClient);
router: Router = inject(Router);
user = signal<LoggedInUser | null>(null);
id:number;

  constructor() {
    effect(() => {
      if (this.user()) {
        // console.log('USer loggedin', this.user().sub);
        this.id=this.user().userId
      } else {
        console.log('No user Logged In');
      }
    });
  }

  loginUser(credentials: Credentials) {
    return this.http.post<JwtTokenDTO >(`${API_USER}/LoginUser`,credentials);
   }

  logoutUser(){
    this.user.set(null)
localStorage.removeItem('access_token');
this.router.navigate(['login']);
  }
  // api/{role}/roleEntityId/books
  // getUserBooks(){
  //   return this.http.get<Book[] | StoreBook[]>(`${environment.apiURL}/${this.user().role.toLowerCase()}/${this.user().roleEntityId}/books`,{
  //     headers: {
  //       Accept:'application/json'
  //     },
  //   })
  
  // }
  deleteBookFromLoggedInPersonUser(bookId:number): Observable<any>{
    return this.http.delete<string>(`https://localhost:7279/api/Person/DeletesPersonBook/personal/${this.user().roleEntityId}/book/${bookId}
`)
  
  }

  deleteBookFromLoggedInStoreUser(bookId:number){
    return this.http.delete<{any:User}>(`https://localhost:7279/api/Store/RemoveBookFromStore/${this.user().roleEntityId}/books/${bookId}`)
  //https://localhost:7279/api/Store/RemoveBookFromStore/{storeId}/books/{bookId}

  }
  getUserNotifications(){//getNotificationsByUserID
  return this.http.get<Notification[]>(`${API_USER}/notification/${this.id}`)
}

  updateUser(user:User){
    return this.http.put<{user:User}>(`${API_USER}/UpdateUserAccount?id=${this.user().userId}`,user)
  }

  deleteUser(){
    return this.http.delete<{user:User}>(`${API_USER}/${this.user().roleEntityId}`)
  }
}