import {Component, Input} from '@angular/core';
import {MatButton} from '@angular/material/button';

@Component({
  selector: 'app-command-approve',
  imports: [
    MatButton
  ],
  templateUrl: './command-aprove.component.html',
  styleUrl: './command-aprove.component.scss'
})
export class CommandAproveComponent {
  @Input() command = 'delete-records';
  @Input() connection = 'CRM-prod';
  @Input() description = 'Delete records from the CRM database for the selected segment.';

  confirmAction() {
    // Тут можна реалізувати логіку підтвердження (виклик сервісу тощо)
    console.log('Команда підтверджена!');
  }

}
