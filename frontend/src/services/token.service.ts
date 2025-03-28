import { Injectable } from "@angular/core";
import { LocalService } from "./local.service";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({providedIn: "root"})
export class TokenService{
    constructor(private localService: LocalService, private jwtHelperService: JwtHelperService){

    }

    getDecodedToken(){
      const token = this.localService.get(LocalService.AuthTokenName);

      console.log('token', token);

      if(token){
            return this.jwtHelperService.decodeToken(token);
        }
    }

    getUserId(){
        var decodedToken = this.getDecodedToken();
        console.log(decodedToken);
        return decodedToken.nameid;
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
