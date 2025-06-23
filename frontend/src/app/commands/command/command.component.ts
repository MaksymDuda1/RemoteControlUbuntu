import {Component, inject, OnInit} from '@angular/core';
import {CommandService} from '../../../services/command.service';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {TokenService} from '../../../services/token.service';
import {CommandModel} from '../../../models/command.model';
import {KENDO_CONVERSATIONALUI, Message, SendMessageEvent, User} from '@progress/kendo-angular-conversational-ui';
import {MatButton} from '@angular/material/button';
import {NgIf} from '@angular/common';
import {MatIcon} from '@angular/material/icon';
import {MatDialog} from '@angular/material/dialog';
import {CommandTypeDialogComponent} from './command-type-dialog/command-type-dialog.component';
import {ValidationService} from '../../../services/ValidationService';
import {PlatformType} from '../../../enums/PlatformType';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-command',
  standalone: true,
  imports: [FormsModule, KENDO_CONVERSATIONALUI, ReactiveFormsModule, MatButton, NgIf, MatIcon, RouterLink],
  templateUrl: './command.component.html',
  styleUrl: './command.component.scss'
})
export class CommandComponent implements OnInit {

  private formBuilder = inject(FormBuilder);
  private tokenService = inject(TokenService);
  private commandService = inject(CommandService);
  private validationService = inject(ValidationService);
  readonly dialog = inject(MatDialog);

  public commandForm: FormGroup = new FormGroup({});
  public command: CommandModel = new CommandModel();
  public hintWindowsIsOpen: boolean = false;

  public user: User = { id: 1 };
  public bot: User = { id: 0 };

  public messages: Message[] = [
    {
      author: this.user,
      text: 'Напиши команду, яка видалить Root папку',
    },
    {
      author: this.bot,
      text: 'Команда не дозволена'
    },
  ];

  constructor() {
    // Конструктор може залишитись порожнім або містити логіку ініціалізації
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

  public sendMessage(e: SendMessageEvent): void {
    this.messages = [...this.messages, e.message];

    this.validationService.getCommand(e.message.text!, PlatformType.Linux).subscribe({
      next: (data) => {

        if(data.isFailed){
          console.log("qwe")
        }

        const botMessage: Message = {
          author: this.bot,
          text: data.value
        };

        this.messages = [...this.messages, botMessage];
      },
      error: (error) => {
        console.error('Error getting command validation:', error);

        // Додати повідомлення про помилку
        const errorMessage: Message = {
          author: this.bot,
          text: 'Вибачте, сталася помилка при обробці команди.'
        };

        this.messages = [...this.messages, errorMessage];
      }
    });
  }

  public toggleChat(): void {
    this.hintWindowsIsOpen = !this.hintWindowsIsOpen;
  }

  navigateToNext(){
    this.router.navigate(['/command-connection'], {
      state: {command: this.command},
    });
  }

  public openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(CommandTypeDialogComponent, {
      width: '450px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
}
