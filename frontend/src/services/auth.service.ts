import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { LoginModel } from "../models/login.model";
import { RegistrationModel } from "../models/registration.model";

@Injectable({providedIn: "root"})
export class AuthService{
    constructor(private client: HttpClient){

    }

    private path: string = "api/auth";

    login(loginModel: LoginModel){
        return this.client.post(this.path + "/login", loginModel);
    }

    registration(registrationModel: RegistrationModel){
        return this.client.post(this.path + "/registration", registrationModel);    
    }
}