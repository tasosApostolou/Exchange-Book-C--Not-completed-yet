export interface Store{
  id:number
  name: string;
  address: string;
  // user:User;
  userId:number;
}

export interface storeRegister{
  name:string;
  address:string;
  username:string;
  Email:string
  password:string;
  userRole:string;
}