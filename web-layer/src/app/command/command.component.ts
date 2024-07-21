import { Component } from '@angular/core';
import { CommandService } from '../../services/command.service';

import { ConnectionCommandModel } from '../../models/connectionCommand.model';
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

  errorMessage = "";
  connectionCommandModel= new ConnectionCommandModel();

  onCommandExecute()
  {
    this.connectionCommandModel.connectionId = "760486dd-4c3c-48a1-b51f-f63dd447c3a7";
    this.connectionCommandModel.commandId = "c027e865-0c02-4fc2-933e-2c8d1eaac5ba";
    this.commandService.execute(this.connectionCommandModel).subscribe(data =>{},
      errorResponse => this.errorMessage = errorResponse
    )
  }

  }
