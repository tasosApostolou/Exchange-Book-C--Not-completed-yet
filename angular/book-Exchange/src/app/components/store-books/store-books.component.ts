import { Component,inject } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Book, BookWithPrice, StoreBook } from 'src/app/shared/interfaces/book'
import { StoreService } from 'src/app/shared/services/store.service';
import { UserService } from 'src/app/shared/services/user.service';
@Component({
  selector: 'app-store-books',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './store-books.component.html',
  styleUrl: './store-books.component.css'
})
export class StoreBooksComponent {
  userService = inject(UserService)
  user = this.userService.user
  storeService = inject(StoreService)
  books:Book[] = []
  storeBooks:StoreBook[]
  booksWithPrice:BookWithPrice[] = []

  ngOnInit(): void {
    this.loadBooks()
  }
  loadBooks(){
    console.log(`daeefsafsfsf `)

    console.log(`daeefsafsfsf ${this.userService.user().roleEntityId}`)
    this.storeService.getStoreBooks().subscribe((data:StoreBook[]) => {
      this.storeBooks = data
      data.forEach(storeBook => this.books.push(storeBook.book))
    })
  }

  deleteBook(bookId:number){
    this.userService.deleteBookFromLoggedInUser(bookId).subscribe({
      next:(response)=> {
        console.log("book deleted",response)
        if(this.books.length===1){
          this.books = null
        } 
        this.loadBooks()
      },
      error:(response) => {
        console.error('Error in delete book',response)
      }
    })
  }
  }
