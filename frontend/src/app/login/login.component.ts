import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { LocalService } from '../../services/local.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginModel } from '../../models/login.model';
import { TokenApiModel } from '../../models/tokenApi.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private authService: AuthService,
    private localService: LocalService,
    private jwtHelperService: JwtHelperService
  ) { }

  loginModel = new LoginModel();
  errorMessage = "";

  onLogin(){
    this.authService.login(this.loginModel).subscribe((data : any) => {
      this.localService.put(LocalService.AuthTokenName, data.accessToken);
        let decodedData = this.jwtHelperService.decodeToken(data.accessToken);

        if(decodedData.role == "Admin")
          window.location.href = '/admin';
        else
          window.location.href = '/home';
    },
    (errorResponse: any) => {
      this.errorMessage = errorResponse;
      })
  }
}
