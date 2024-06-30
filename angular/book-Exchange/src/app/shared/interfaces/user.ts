export interface User{
    id:number
    username:string
    email:string;
    password:string;
    userRole:string
}

export interface Credentials{
    Username:string;
    Password: string;
}
export interface LoggedInUser{
    userId:number;
    name:string; // username taken from jwt as sub
    email:string
    role :string;
    roleEntityId:number //PERSONAL or STORE logged in user

    
}
export interface JwtTokenDTO{
    token:string
}
