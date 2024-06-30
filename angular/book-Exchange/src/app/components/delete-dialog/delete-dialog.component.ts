import { Component, Inject, inject } from "@angular/core"
import { DIALOG_DATA, DialogRef } from "@angular/cdk/dialog"
import { Person } from "src/app/shared/interfaces/person"
import { UserService } from "src/app/shared/services/user.service"
import { PersonService } from "src/app/shared/services/person.service"
import { Router } from "@angular/router"

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
    router: Router = inject(Router);
    constructor(public dialogDel: DialogRef) {}
    personService = inject(PersonService)
    userService = inject(UserService)
    userDelete(){
      this.personService.deletePersonUser().subscribe({
            next:(response) =>{
              // this.userService.user.set(null)
              // this.dialogDel.close()

              // this.router.navigate(['login']);
              console.log(response)
              this.userService.logoutUser()
            },
            error:(response) => {
              console.log(response)
            }
          })
      
      this.dialogDel.close()
    }
  }
  
