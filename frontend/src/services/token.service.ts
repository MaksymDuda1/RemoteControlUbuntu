import { Injectable } from "@angular/core";
import { LocalService } from "./local.service";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({providedIn: "root"})
export class TokenService{
    constructor(private localService: LocalService, private jwtHelperService: JwtHelperService){

    }

    getDecodedToken(){
        var token = this.localService.get(LocalService.AuthTokenName);

        if(token){
            return this.jwtHelperService.decodeToken(token);
        }
    }

    getUserId(){
        var decodedToken = this.getDecodedToken();

        return decodedToken.nameId; 
    }

    getRole(){
        var decodedToken = this.getDecodedToken();

        return decodedToken.role;
    }

    getUserEmail(){
        var decodedToken = this.getDecodedToken();

        return decodedToken.email;
    }
}