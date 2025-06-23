import { Component } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatIconButton} from '@angular/material/button';
import {NgIf} from '@angular/common';
import {MatIcon} from '@angular/material/icon';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-instruction',
  imports: [
    MatIconButton,
    NgIf,
    MatIcon,
    FormsModule
  ],
  templateUrl: './instruction.component.html',
  styleUrl: './instruction.component.scss'
})
export class InstructionComponent {
  selectedOS: string = '';

  constructor(private snackBar: MatSnackBar) {}

  onOSChange(): void {
    // Можна додати логіку при зміні ОС
    console.log('Selected OS:', this.selectedOS);
  }

  copyToClipboard(text: string): void {
    navigator.clipboard.writeText(text).then(() => {
      this.snackBar.open('Команду скопійовано!', 'Закрити', {
        duration: 2000,
        horizontalPosition: 'center',
        verticalPosition: 'bottom'
      });
    }).catch(err => {
      console.error('Failed to copy text: ', err);
    });
  }
}
