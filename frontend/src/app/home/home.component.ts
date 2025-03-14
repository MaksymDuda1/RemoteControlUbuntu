import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { StatisticsComponent } from "./statistics/statistics.component";
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { ConnectionModel } from '../../models/connection.mode';
import { CommandModel } from '../../models/command.model';
import { ConnectionService } from '../../services/connection.service';
import { CommandService } from '../../services/command.service';
import { LocalService } from '../../services/local.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, StatisticsComponent, MatDividerModule, MatListModule], 
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  public connections: ConnectionModel[] = [];
  public commands: CommandModel[] = [];

  constructor(private connectionService: ConnectionService, private commandService: CommandService, private localService: LocalService, private jwtHelperService: JwtHelperService){
    const token = localService.get(LocalService.AuthTokenName);
    if (token) {
      var decodedData = this.jwtHelperService.decodeToken(token);
      const userId = decodedData.id;
      this.connectionService.getAllByUserId(userId).subscribe(data => 
        this.connections = data
      );
      this.commandService.getAllByUserId(userId).subscribe(data => 
        this.commands = data
      );
    }
  }
}
