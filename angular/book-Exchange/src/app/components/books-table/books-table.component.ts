import { Component, inject,QueryList, ViewChildren, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm, NgModel, NgModelGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormField, MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Title } from '@angular/platform-browser';
import { Book, BookWithUsers } from 'src/app/shared/interfaces/book';
import { User } from 'src/app/shared/interfaces/user';
import { BookService } from 'src/app/shared/services/book.service';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';

import { UserService } from 'src/app/shared/services/user.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { UserLoginComponent } from '../user-login/user-login.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenu } from '@angular/material/menu';
import { MatToolbar, MatToolbarModule, MatToolbarRow } from '@angular/material/toolbar';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { PersonService } from 'src/app/shared/services/person.service';
import { Person } from 'src/app/shared/interfaces/person';
// import { Person } from 'src/app/shared/interfaces/person';


@Component({
  selector: 'app-books-table',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule,MatInputModule,MatSelectModule,MatButtonModule,RouterLink,RouterOutlet,UserLoginComponent,MatIconModule,MatMenu, MatToolbar, MatToolbarRow,MatToolbarModule,MatButtonModule, MatIconModule, MatButtonModule,MatButtonToggleModule,MatIconModule,MatIconButton],
  templateUrl: './books-table.component.html',
  styleUrl: './books-table.component.css'
})
export class BooksTableComponent {

bookService = inject(BookService)
notificationService = inject(NotificationService)
personService = inject(PersonService)
books:Book[] = []
person:Person
personID:number
getUserID(){
  return Number(localStorage.getItem('interestedUser_id'))

}
 ngOnInit(){
this.getPersonBooks(this.getUserID())
}
getPersonBooks(userId:number){
  this.personService.getPersonByUserId(this.getUserID()).subscribe({
    next:(response) => {
      this.loadBooks(response.id)
    }
  })

}
loadBooks(personId:number){
  this.bookService.getUserBooks(personId).subscribe((data:Book[]) => {
    this.books = data
  }
)
}

interest(event:Event,book:Book){

  this.iconType(event)
  const notification = {
    isSeen:false,
    user:{
      id:this.getUserID()
    },
    book:book
  }
  
  this.notificationService.createNotification(notification as any as JSON ).subscribe({
    next:(response) => {
      console.log(`${response} data`)
    },
    error:(response) => {
      console.log('Error in notification creation', response)
    }
  })
  }

  iconType(event:Event){
    let matIcon: HTMLElement = event.target as HTMLElement;
    if(matIcon.textContent ==="thumb_down"){
      matIcon.innerText = "thumb_up"
    }
    else{
      matIcon.innerText = "thumb_down"
    }
  }


}

