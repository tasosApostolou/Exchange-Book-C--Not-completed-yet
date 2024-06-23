import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Person } from 'src/app/shared/interfaces/person';
import { Store } from 'src/app/shared/interfaces/store';
import { StoreService } from 'src/app/shared/services/store.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-update-store',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './update-store.component.html',
  styleUrl: './update-store.component.css'
})
export class UpdateStoreComponent {
  storeService = inject(StoreService)
  userService = inject(UserService)
  form = new FormGroup({
    name: new FormControl('',Validators.required),
    address: new FormControl('',Validators.required),
  });
  clicked = false
  updatedName = ''
  updatedAddress = ''
  invalidUpdate= false;
  onSubmit(){
    this.clicked=true
    const store = this.form.value as Store
    store.id = this.userService.user().roleEntityId
    store.userId =  this.userService.user().userId
  console.log(store)
    this.storeService.updateStore(store).subscribe({
      next: (response) => {
        this.updatedName = this.form.get('name').value
        this.updatedAddress=this.form.get('address').value
        console.log(response)
        this.form.reset()
        this.invalidUpdate = false
      },
      error: (response) => {
        console.log("Error in update", response);
        this.invalidUpdate = true
      }
    })
  }
}


