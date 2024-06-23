import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Book } from 'src/app/shared/interfaces/book';

@Component({
  selector: 'app-my-books-table',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './my-books-table.component.html',
  styleUrl: './my-books-table.component.css'
})
export class MyBooksTableComponent {
@Input() books:Book[] | undefined
@Output() emitedBookId = new EventEmitter<number>();
emitBookId(bookId:number){
this.emitedBookId.emit(bookId)
}
}
