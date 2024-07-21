import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { LocalService } from '../../services/local.service';
import { RegistrationModel } from '../../models/registration.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent {
  constructor(private authService: AuthService,
    private localService: LocalService
  ) {

  }

  registrationModel: RegistrationModel = new RegistrationModel();
  errorMessage: string = "";

  onRegistration() {
    this.authService.registration(this.registrationModel).subscribe((data: any) => {
      this.localService.put(LocalService.AuthTokenName, data.accessToken);
      window.location.href = '/';
    },
      errorResponse => this.errorMessage = errorResponse)
  }
}
