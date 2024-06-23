import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { UserService } from './user.service';
import { Notifica, Notification } from '../interfaces/notification';

const API_URL = `${environment.apiURL}/notification`
@Injectable({
  providedIn: 'root'
})
export class NotificationService {
http:HttpClient = inject(HttpClient)

userService = inject(UserService)

createNotification(notification:JSON){
  console.log(`${this.userService.user().userId} poios`)
return this.http.post<{notification: Notifica}>(`${API_URL}/add/${this.userService.user().userId}`,notification)
}

updateNotificationStatus(notification:Notifica){
  return this.http.put<{notification:Notification}>(`${API_URL}/notifications/${notification.id}`,notification)
}

getNotificationById(id:Number){
  return this.http.get<{notification:Notifica}>(`${API_URL}/${id}`,{
    headers: {
      Accept:'application/json'
    },
  })
}
}
