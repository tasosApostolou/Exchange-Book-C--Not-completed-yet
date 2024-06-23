export interface User{
    id:number
    username:string;
    password:string;
    role:string
}

export interface Credentials{
    Username:string;
    Password: string;
}
export interface LoggedInUser{
    userId:number;
    sub:string; // username taken from jwt as sub
    role :string;
    roleEntityId:number //PERSONAL or STORE logged in user
    phoneNumber:string

    
}
export interface JwtTokenDTO{
    token:string
}
export interface LoggedInJwt{
    userId:number;
    username:string; // username taken from jwt as sub
    role :string;
    Email:string;
}