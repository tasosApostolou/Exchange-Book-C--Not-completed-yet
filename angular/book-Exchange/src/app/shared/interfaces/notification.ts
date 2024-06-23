import { Book } from "./book";
import { User } from "./user";

export interface Notification{
id:number;
user:User;
book:Book;
createdAt:string;
isSeen:boolean;
}
export interface Notifica{
    id:number;
    interested:User
    user:User;
    book:Book;
    createdAt:string;
    notificationType:string,
    isSeen:boolean;
    }