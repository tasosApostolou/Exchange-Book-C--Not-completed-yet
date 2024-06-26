import { User } from "./user";

export interface Person {
  id:number
  firstname: string;
  lastname: string;
  phoneNumber: string;
  userId:number;


}
export interface UserSignUpDTO{
  Username:string;
  Email:string
  Password:string;
  Firstname:string;
  Lastname:string;
  PhoneNumber:string;
  UserRole:string;
}