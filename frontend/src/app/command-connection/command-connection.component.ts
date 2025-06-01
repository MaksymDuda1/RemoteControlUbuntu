import {Component, inject} from '@angular/core';
import {MatOption} from '@angular/material/core';
import {MatSelect} from '@angular/material/select';
import {MatFormField, MatInput} from '@angular/material/input';
import {MatButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import {NgForOf, NgIf} from '@angular/common';
import {MatDivider} from '@angular/material/divider';
import {MatList, MatListItem} from '@angular/material/list';
import {TranslatePipe} from '@ngx-translate/core';
import {ReactiveFormsModule} from '@angular/forms';
import {ConnectionComponent} from '../connections/connection/connection.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-command-connection',
  imports: [
    MatOption,
    MatSelect,
    MatFormField,
    MatButton,
    MatIcon,
    NgForOf,
    NgIf,
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
export class CommandConnectionComponent {
  readonly dialog = inject(MatDialog);

  commands = [
    { name: 'Команда 1', connection: null },
    { name: 'Команда 2', connection: null },
    // ...
  ];

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
