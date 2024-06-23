import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Person } from 'src/app/shared/interfaces/person';
import { PersonService } from 'src/app/shared/services/person.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-update',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.css'
})
export class UpdateComponent {
  personalService = inject(PersonService)
  userService = inject(UserService)
  form = new FormGroup({
    firstname: new FormControl('',Validators.required),
    lastname: new FormControl('',Validators.required),
  });
  clicked = false
  updatedFirstname = ''
  updatedLAstname = ''
  invalidUpdate= false;
  onSubmit(){
    this.clicked=true
    const person = this.form.value as Person
    person.id = this.userService.user().roleEntityId
    person.userId =  this.userService.user().userId
  console.log(person)
    this.personalService.updatePersonal(person).subscribe({
      next: (response) => {
        this.updatedFirstname = this.form.get('firstname').value
        this.updatedLAstname=this.form.get('lastname').value
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
