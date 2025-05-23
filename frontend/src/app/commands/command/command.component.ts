import {Component, inject, OnInit} from '@angular/core';
import { CommandService } from '../../../services/command.service';

import { ConnectionCommandModel } from '../../../models/connectionCommand.model';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {MatFormField, MatInput, MatSuffix} from '@angular/material/input';
import {TranslatePipe} from '@ngx-translate/core';
import {TokenService} from '../../../services/token.service';
import {ConnectionService} from '../../../services/connection.service';
import {CommandModel} from '../../../models/command.model';
import {MatOption} from '@angular/material/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSelect} from '@angular/material/select';
import {KENDO_CONVERSATIONALUI, Message, SendMessageEvent, User} from '@progress/kendo-angular-conversational-ui';
import {MatButton} from '@angular/material/button';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-command',
  standalone: true,
  imports: [FormsModule, KENDO_CONVERSATIONALUI, MatDatepicker, MatDatepickerInput, MatDatepickerToggle, MatFormField, MatInput, MatOption, MatPaginator, MatSelect, MatSuffix, ReactiveFormsModule, TranslatePipe, MatButton, NgIf],
  templateUrl: './command.component.html',
  styleUrl: './command.component.scss'
})
export class CommandComponent implements OnInit {
  constructor(){

   }

  private formBuilder = inject(FormBuilder);
  private tokenService = inject(TokenService);
  private commandService = inject(CommandService);

  public commandForm: FormGroup = new FormGroup({});
  public command: CommandModel = new CommandModel();
  public hintWindowsIsOpen: boolean = false;

  public user: User = { id: 1 };
  public bot: User = { id: 0 };


  public messages: Message[] = [
    {
      author: this.bot,
      text: 'Do you like Angular?'
    },
    {
      author: this.user,
      text: 'Definitely!',
    }
  ];

  public sendMessage(e: SendMessageEvent): void {
    this.messages = [...this.messages, e.message];
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm(): void {
    this.commandForm = this.formBuilder.group({
      name: [null, [Validators.required]],
      type: [null, [Validators.required]],
      command: [null, [Validators.required]]
    });
  }

  public toggleChat(): void {
    this.hintWindowsIsOpen = !this.hintWindowsIsOpen;
  }
}
