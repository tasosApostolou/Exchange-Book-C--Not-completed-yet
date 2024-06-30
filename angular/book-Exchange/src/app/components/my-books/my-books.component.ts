import { Component, inject } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Book } from 'src/app/shared/interfaces/book';
import { BookService } from 'src/app/shared/services/book.service';
import { PersonService } from 'src/app/shared/services/person.service';
import { UserService } from 'src/app/shared/services/user.service';
import { MyBooksTableComponent } from '../my-books-table/my-books-table.component';

@Component({
  selector: 'app-my-books',
  standalone: true,
  imports: [MatIcon,MyBooksTableComponent],
  templateUrl: './my-books.component.html',
  styleUrl: './my-books.component.css'
})
export class MyBooksComponent {
    userService = inject(UserService)
    personService = inject(PersonService)
    user = this.userService.user
    bookService = inject(BookService)
    books:Book[]
  ngOnInit(): void {
  this.loadMyBooks()
  }

  loadMyBooks(){
    this.personService.getPersonBooks().subscribe((data:Book[]) => {
      this.books = data
    })
  }
  
  deleteBook(bookId:number){
    this.userService.deleteBookFromLoggedInPersonUser(bookId).subscribe({
      next:(response)=> {
        console.log(response)
        this.loadMyBooks()
      },
      error:(response) => {
        console.error('Error in delete book',response)
      }
    })
  }
  }
  