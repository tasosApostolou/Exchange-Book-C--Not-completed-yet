import { Component, Input, inject } from '@angular/core';
import { SearchBookFormComponent } from '../search-book-form/search-book-form.component'; 
import { BooksWithPersonsComponent } from '../books-with-persons/books-with-persons.component'; 
import { BookWithPersons, StoreBook } from 'src/app/shared/interfaces/book';
import { RouterLink, RouterOutlet } from '@angular/router';
import { BooksInStoresComponent } from '../books-in-stores/books-in-stores.component';
import { SearchBookForm } from 'src/app/shared/interfaces/Search-book-form';
import { BookService } from 'src/app/shared/services/book.service';
import { StoreService } from 'src/app/shared/services/store.service';

@Component({
  selector: 'app-search-book-manager',
  standalone: true,
  imports: [RouterLink,RouterOutlet,SearchBookFormComponent,BooksWithPersonsComponent,BooksInStoresComponent],
  templateUrl: './search-book-manager.component.html',
  styleUrl: './search-book-manager.component.css'
})
export class SearchBookManagerComponent {
  bookService = inject(BookService)
  storeService = inject(StoreService)
  choice = ''
  title = ''
  booksWithPersons:BookWithPersons[] = []
  storeBooks:StoreBook[] = []

  ngOnInit(): void {
    this.loadBooksOfPersons()
    this.loadBooksOfStores
  }

  formResults(formValue:SearchBookForm){
    this.choice = formValue.searching
    this.title = formValue.title
    if (this.choice==="others"){
      this.loadBooksOfPersons()
}
    if (this.choice==="stores"){
      this.loadBooksOfStores()
    }
  }

  loadBooksOfPersons(){
    this.bookService.getBooksByTitle(this.title).subscribe((data) => {
      this.booksWithPersons = data
    })
  }

  loadBooksOfStores(){
    this.storeService.getStoreBooksByTitle(this.title).subscribe((data) => {
      this.storeBooks = data
    })
  }
}
