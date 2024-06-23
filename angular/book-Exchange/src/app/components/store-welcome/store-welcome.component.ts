import { Component, inject } from '@angular/core';
import { Store } from 'src/app/shared/interfaces/store';
import { StoreService } from 'src/app/shared/services/store.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-store-welcome',
  standalone: true,
  imports: [],
  templateUrl: './store-welcome.component.html',
  styleUrl: './store-welcome.component.css'
})
export class StoreWelcomeComponent {
userService = inject(UserService)
storeService = inject(StoreService)
store:Store
ngOnInit(){
  // this.getStore()
}
// getStore(){
//   this.storeService.getStoreById(this.userService.user().roleEntityId).subscribe((data:Store) => {
//     this.store=data
//   })
// }
}
