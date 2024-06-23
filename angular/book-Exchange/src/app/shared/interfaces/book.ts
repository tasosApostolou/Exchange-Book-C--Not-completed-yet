
import { Author, AuthorInsertDTO } from "./author-insert-dto";
import { Person } from "./person";
import { Store } from "./store";
import { User } from "./user";

export interface Book {
    id: number | null;
    title: string;
    author: Author;
}
export interface StoreBook{
    book:Book;
    store:Store;
    price:number;
}
export interface BookWithPrice{
    book:Book,
    price:number
}

export interface InsertBook{
    title:string;
    author:AuthorInsertDTO
}
export interface InsertStoreBook{
    book:InsertBook,
    price:number
}

export interface BookWithUsers {
    id:number;
    title:string
    author:Author
    users: User[];
  }

export interface BookWithPersons{
    id:number;
    title:string;
    author:Author;
    persons: Person[]
}
