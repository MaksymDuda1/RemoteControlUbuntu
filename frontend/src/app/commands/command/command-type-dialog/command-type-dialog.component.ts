import { Component, inject } from '@angular/core';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {MatButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-command-type-dialog',
  imports: [
    MatDialogContent,
    MatDialogActions,
    MatButton,
    MatDialogClose,
    MatDialogTitle,
    MatIcon
  ],
  templateUrl: './command-type-dialog.component.html',
  styleUrl: './command-type-dialog.component.scss'
})
export class CommandTypeDialogComponent {
  readonly dialogRef = inject(MatDialogRef<CommandTypeDialogComponent>);
}
