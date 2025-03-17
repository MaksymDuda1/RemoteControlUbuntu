import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { LocalService } from '../../services/local.service';
import { SidebarComponent } from "./sidebar/sidebar.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-top-menu',
  standalone: true,
  imports: [CommonModule, RouterModule, SidebarComponent],
  templateUrl: './top-menu.component.html',
  styleUrl: './top-menu.component.scss'
})
export class TopMenuComponent {
  public isAuthorized = true;

  constructor(private localService: LocalService){}

  onLogout(){
    this.localService.remove(LocalService.AuthTokenName);
    window.location.href = "login"
  }
}