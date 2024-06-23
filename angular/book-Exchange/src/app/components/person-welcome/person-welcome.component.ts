import { Component, inject } from '@angular/core';
import { Person } from 'src/app/shared/interfaces/person';
import { LoggedInUser } from 'src/app/shared/interfaces/user';
import { PersonService } from 'src/app/shared/services/person.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-person-welcome',
  standalone: true,
  imports: [],
  templateUrl: './person-welcome.component.html',
  styleUrl: './person-welcome.component.css'
})
export class PersonWelcomeComponent {
  userService = inject(UserService)
  personService = inject(PersonService)
  person:Person

ngOnInit(){
  this.getPerson()
}
getPerson(){
  this.personService.getPersonById(this.userService.user().roleEntityId).subscribe((data:Person) => {
    this.person = data
  })
}
}
