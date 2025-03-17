import { Component } from '@angular/core';
import { CommandService } from '../../../services/command.service';

import { ConnectionCommandModel } from '../../../models/connectionCommand.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-command',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './command.component.html',
  styleUrl: './command.component.scss'
})
export class CommandComponent {
  constructor(private commandService: CommandService) {

   }

  }
