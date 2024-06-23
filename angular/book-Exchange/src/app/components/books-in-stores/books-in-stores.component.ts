import { Dialog } from '@angular/cdk/dialog';
import { Component, Input, inject } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { StoreBook } from 'src/app/shared/interfaces/book';
import { BuyBookComponent } from '../buy-book/buy-book.component';

@Component({
  selector: 'app-books-in-stores',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './books-in-stores.component.html',
  styleUrl: './books-in-stores.component.css'
})
export class BooksInStoresComponent {
  constructor(public dialog: Dialog, dia:Dialog) {}
  @Input()  storeBooks:StoreBook[]

  buyBook(){
    this.dialog.open(BuyBookComponent)
  }
}
