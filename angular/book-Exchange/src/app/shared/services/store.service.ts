import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Book, StoreBook } from '../interfaces/book';
import { Person } from '../interfaces/person';
import { Store, storeRegister } from '../interfaces/store';
import { User } from '../interfaces/user';
import { UserService } from './user.service';
const API_URL = `${environment.apiURL}/store`;

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  http: HttpClient = inject(HttpClient);
  userService = inject(UserService)
  personId = () => {
    return this.userService.user().roleEntityId
  }
  personSignal = signal<Person | null>(null);
  
    constructor() { }
    registerStore(store:storeRegister){
      return this.http.post<{data:JSON}>(`https://localhost:7279/api/User/SignUpStoreUser
`, store);
    }
  
    getStoreBooks(){
      return this.http.get<StoreBook[]>(`${API_URL}/${this.userService.user().roleEntityId}/books`,{
        headers: {
          Accept:'application/json'
        },
      })
    }

    getStoreBooksByTitle(title:string){
      return this.http.get<StoreBook[]>(`${API_URL}/books?title=${title}`,{
        headers: {
          Accept:'application/json'
        },
      })
    }
  
    deleteBookFromLoggedInStore(bookId:number){
     return this.http.delete<{any:Book}>(`${API_URL}/${this.userService.user().roleEntityId}/books/${bookId}`)
    }
    updateStore(store:Store){
      return this.http.put<{any:Store}>(`${API_URL}/update/${
        store.id}`,store)
    }
    getStoreById(storeID:number){
      return this.http.get<any>(`${API_URL}/${storeID}`,{
        headers: {
          Accept:'application/json'
        },
      })
    }
    
    getStoreByUserId(userID:number){
      return this.http.get<Store>(`${API_URL}/user/${userID}`,{
        headers: {
          Accept:'application/json'
        },
      })
    }
  
    deleteUser(){
      return this.http.delete<{user:User}>(`${environment.apiURL}/${this.userService.user().role.toLowerCase()}/${this.userService.user().roleEntityId}`)
    }

  }