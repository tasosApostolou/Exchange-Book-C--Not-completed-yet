import { Component, inject } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import{ MatIcon, MatIconModule } from'@angular/material/icon'
import { Router, RouterLink } from '@angular/router';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatBadge, MatBadgeModule } from '@angular/material/badge';
import { MatButtonToggle, MatButtonToggleGroup, MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButton, MatIconButton } from '@angular/material/button';
import { Notifica } from 'src/app/shared/interfaces/notification';
import { AddBookComponent } from '../../add-book/add-book.component';
import { BooksTableComponent } from '../../books-table/books-table.component';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UpdateComponent } from '../../update/update.component';
import { ManageAccountComponent } from '../../manage-account/manage-account.component';
import { SearchBookManagerComponent } from '../../search-book-manager/search-book-manager.component';
import { PersonWelcomeComponent } from '../../person-welcome/person-welcome.component';

@Component({
  selector: 'app-navbar-personal',
  standalone: true,
  imports: [UpdateComponent,SearchBookManagerComponent, BooksTableComponent,ManageAccountComponent,
    MatIcon,RouterLink,AddBookComponent,MatIconModule,MatMenu,MatMenuTrigger,MatBadgeModule,MatBadge,MatButtonToggle,MatButtonToggleGroup,MatButtonToggleModule,MatIcon,MatIconButton,MatButton,BooksTableComponent,BooksTableComponent,PersonWelcomeComponent
    ],
  templateUrl: './navbar-personal.component.html',
  styleUrl: './navbar-personal.component.css'
})
export class NavbarPersonalComponent {

  router = inject(Router)
  userService = inject(UserService)
  notificationService = inject(NotificationService)
  user = this.userService.user
  notifications:Notifica[] = []

// notificationsNumber:any = ''
notificationsUnseenNumber:number
matBadgeValue(){
  if (this.notificationsUnseenNumber == 0){
    return ''
  }else{
    return this.notificationsUnseenNumber
  }
}
  logout(){
    this.userService.logoutUser();
  }

  ngOnInit(){
    this.userService.getUserNotifications().subscribe(
      (data:[]) =>{
        this.notifications = data
        
        this.notificationsUnseenNumber = this.notifications.filter(notification => !notification.isSeen).length
      
      })
  }
  
 notificationClicked(notification:Notifica){
    this.ngOnInit() 
    let notificationToUpdate = this.notifications.find(noti => noti.id === notification.id) //ngOnInit refreshing notification
    this.UpdateNotificationAsSeen(notificationToUpdate)
    
    localStorage.setItem('interestedUser_id',notification.interested.id.toString())// to find user's books after navigating router in user's bookstable 
  }

  UpdateNotificationAsSeen(notificationToUpdate:Notifica){
    notificationToUpdate.isSeen = true
    this.notificationService.updateNotificationStatus(notificationToUpdate as Notifica).subscribe({
      next:(response) => {
        console.log(`${response} data`)
      },
      error:(response) => {
        console.log('Error in notification update', response)
      }
    })
    }

    bgNotif(notif:Notifica):string{
      if (!notif.isSeen){
        return 'bg-light'
      }
      return ''
    }
  
  }

