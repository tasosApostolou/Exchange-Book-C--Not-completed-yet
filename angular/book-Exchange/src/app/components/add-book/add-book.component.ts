import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Title } from '@angular/platform-browser';
import { Book, InsertBook } from 'src/app/shared/interfaces/book';
import { BookService } from 'src/app/shared/services/book.service';
@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule,MatInputModule,MatButtonModule],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent {
  bookService = inject(BookService)
  regStatus:{succes:boolean;message:string} = {succes:false,message:'Not Attempted yet'}

form = new FormGroup({
  title:new FormControl('',Validators.required),
  name: new FormControl('',Validators.required)
})

onSubmit(value:any){
  const book:InsertBook = {
    title: this.form.get('title').value,
    author: {
      name: this.form.get('name').value
    }
  };
  this.bookService.addBook(book).subscribe({
    next: (response) => {
      console.log('book register', response);
      this.form.reset()
    },
    error: (response) => {
      const message = response
      console.log('Error in adding book',response);
      console.error();
      console.log('Error in adding book',response.error);
      
      this.regStatus.succes=false;
    }
  })
}
}
