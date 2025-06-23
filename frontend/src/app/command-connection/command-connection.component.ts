import {Component, inject, Input, OnInit} from '@angular/core';
import {MatFormField, MatInput} from '@angular/material/input';
import {MatIcon} from '@angular/material/icon';
import {MatDivider} from '@angular/material/divider';
import {MatList, MatListItem} from '@angular/material/list';
import {TranslatePipe} from '@ngx-translate/core';
import {ReactiveFormsModule} from '@angular/forms';
import {ConnectionComponent} from '../connections/connection/connection.component';
import {MatDialog} from '@angular/material/dialog';
import {CommandModel} from '../../models/command.model';
import {CommandService} from '../../services/command.service';
import {TokenService} from '../../services/token.service';

@Component({
  selector: 'app-command-connection',
  imports: [
    MatFormField,
    MatIcon,
    MatDivider,
    MatList,
    MatListItem,
    TranslatePipe,
    MatInput,
    ReactiveFormsModule
  ],
  templateUrl: './command-connection.component.html',
  styleUrl: './command-connection.component.scss'
})
export class CommandConnectionComponent implements OnInit {
  ngOnInit(): void {
      if (!(this.commands.length > 0)) {
        this.commandService.getAllByUserId(this.tokenService.getUserId()).subscribe((data) => {
          this.commands = data.items
        },
          (err) => {
            console.log(err);
          });
      }
  }

  constructor(private commandService: CommandService, private tokenService: TokenService) {
  }

  @Input() commands: CommandModel[] = [];

  readonly dialog = inject(MatDialog);

  availableConnections = [
    { id: 'conn1', name: 'Production-DB-01' },
    { id: 'conn2', name: 'Staging-Server-AZ' },
    { id: 'conn3', name: 'QA-Environment-East' },
    { id: 'conn4', name: 'Dev-Machine-John' },
    { id: 'conn5', name: 'Analytics-Node-03' },
    { id: 'conn6', name: 'Backup-Server-EU' },
    { id: 'conn7', name: 'Remote-Linux-01' },
    { id: 'conn8', name: 'Finance-Server-WIN' },
    { id: 'conn9', name: 'Support-PC-Chicago' },
    { id: 'conn10', name: 'Testing-VPN-Node' },
  ];

  openDialog(id: string | null): void {
    const dialogRef = this.dialog.open(ConnectionComponent, {
      panelClass: 'custom-dialog',
      data: {id: id},
    });
  }

  confirmConnections() {
    // Логіка підтвердження вибраних коннекшинів
    console.log(this.commands);
  }
}
