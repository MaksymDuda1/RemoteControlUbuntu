import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LocalService } from '../../services/local.service';

@Component({
  selector: 'app-top-menu',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './top-menu.component.html',
  styleUrl: './top-menu.component.scss'
})
export class TopMenuComponent {
  constructor(private localService: LocalService){}

  onLogout(){
    this.localService.remove(LocalService.AuthTokenName);
    window.location.href = "login"
  }
}