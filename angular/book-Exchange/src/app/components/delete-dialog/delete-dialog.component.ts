import { Component, Inject, inject } from "@angular/core"
import { DIALOG_DATA, DialogRef } from "@angular/cdk/dialog"
import { Person } from "src/app/shared/interfaces/person"
import { UserService } from "src/app/shared/services/user.service"
import { PersonService } from "src/app/shared/services/person.service"

 export @Component({
    imports: [],
    standalone: true,
    template: `
      <p >Are you sure that you want to delete your account?</p>
      <button class="ms-5 btn btn-primary text-bg-danger " (click)="userDelete()">delete  Account</button>
    `,
    styles: [
      `
        :host {
          display: block;
          background: #fff;
          border-radius: 8px;
          padding: 16px;
          max-width: 400px;
        }
      `,
    ],
  })
  class DeleteDialogComponent {
    constructor(public dialogDel: DialogRef) {}
    personService = inject(PersonService)
    userService = inject(UserService)
    userDelete(){
      this.personService.deleteUser().subscribe({
            next:(response) =>{
              console.log("user deleted",response)
              this.userService.logoutUser()
            },
            error:(response) => {
              console.log(response)
            }
          })
      
      this.dialogDel.close()
    }
  }
  
