import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Book, BookWithPersons, BookWithUsers, InsertBook, InsertStoreBook } from '../interfaces/book';
import { environment } from 'src/environments/environment.development';
import { UserService } from './user.service';
const API_URL = `${environment.apiURL}/book`

@Injectable({
  providedIn: 'root'
})
export class BookService {
http:HttpClient = inject(HttpClient)
userService = inject(UserService)
id = this.userService.id
title:string=''
// addBook(book:InsertBook | InsertStoreBook)
addBook(book:InsertBook | InsertStoreBook){
  return this.http.post<{data:JSON}>(`https://localhost:7279/api/Person/AddBookToPerson/${this.userService.user().roleEntityId}/books`,book)
}
// https://localhost:7279/api/Person/AddBookToPerson/personId/books

getBooksByTitle(title:string){
  this.title=title
  console.log(`title:${this.title}`)
  return this.http.get<BookWithPersons[]>(`${API_URL}/books/${this.title}`,{
    headers:{
      Accept:'application/json'
    }
  })
    }

  getUserBooks(personId:number){
    return this.http.get <Book[]>(`${API_URL}/person/${personId}`,{
      headers: {
        Accept:'application/json'
      },
    })
  }

  getBookById(bookId:number){
    return this.http.get<Book>(`${API_URL}/${bookId}`,{
      headers: {
        Accept:'application/json'
      },
    })
  }
}
