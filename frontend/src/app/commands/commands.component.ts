import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator'
import { MatTableDataSource, MatTableModule } from '@angular/material/table'
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';
import { Subject } from 'rxjs';
import {ConnectionTableComponent} from "../connections/connection-table/connection-table.component";
import {MatInput} from "@angular/material/input";
import {TranslatePipe} from "@ngx-translate/core";
import {ConnectionService} from '../../services/connection.service';
import {TokenService} from '../../services/token.service';
import {MatDialog} from '@angular/material/dialog';
import {PaginatedList} from '../../models/paginatedList';
import {ConnectionModel} from '../../models/connection.mode';
import {ConnectionComponent} from '../connections/connection/connection.component';
import {CommandModel} from '../../models/command.model';
import {CommandService} from '../../services/command.service';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {CommandTableComponent} from './command-table/command-table.component';

@Component({
  selector: 'app-commands',
  imports: [MatPaginatorModule, MatTableModule, RouterModule, MatFormFieldModule, MatSelectModule, MatInput, ReactiveFormsModule, TranslatePipe, MatDatepickerInput, MatDatepickerToggle, MatDatepicker, CommandTableComponent],
  templateUrl: './commands.component.html',
  styleUrl: './commands.component.scss'
})
export class CommandsComponent {
  constructor(
    private commandService: CommandService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private tokenService: TokenService) {
    this.unsubscribeAll = new Subject<any>();
  }

  readonly dialog = inject(MatDialog);
  public errorMessage = 'No Commands';
  public paginatedList: PaginatedList<CommandModel> = new PaginatedList<CommandModel>();
  public dataSource: MatTableDataSource<CommandModel> = new MatTableDataSource;
  public hasCommand: boolean = false;
  public commands: CommandModel[] = [];

  public filterForm: FormGroup = new FormGroup({});
  // public workspaces: WorkspaceBaseModel[] = [];

  public pageSize = 10;
  public pageIndex = 0;
  public totalItems = 0;

  inProgress: boolean = false;

  private unsubscribeAll: Subject<any>;
  private appliedFilters: any = {};

  userId: number = 0;

  ngOnInit(): void {
    this.userId = this.tokenService.getUserId();
    this.commandService.getAllByUserId(this.userId).subscribe(data => {
      this.commands = data.items;
    });
    // TODO rewrite to paginated list
    this.totalItems = 10;

    this.dataSource = new MatTableDataSource<CommandModel>(this.commands);
    this.hasCommand = this.dataSource.data.length > 0;

    this.initFilterForm();
    this.applyFilters();
  }

  ngOnDestroy(): void {
    this.unsubscribeAll.next(null);
    this.unsubscribeAll.complete();
  }

  initFilterForm(): void {
    this.filterForm = this.formBuilder.group({
      name: [''],
      command: [''],
      type: [''],
      dateFrom: [''],
      dateTo: [''],
    });
  }

  resetFilters(): void {
    this.filterForm.reset({
      host: null,
      username: null,
      name: null
    });
    this.applyFilters();
  }

  applyFilters(): void {
    this.appliedFilters = { ...this.filterForm.value };
    this.fetchLogs();
  }

  onPageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.fetchLogs();
  }

  setDateFrom(date: any): void {
    this.appliedFilters.dateFrom = date;
  }

  setDateTo(date: any): void {
    this.appliedFilters.dateTo = date;
  }

  fetchLogs(): void {
    const requestPayload = {
      type: this.appliedFilters.type ?? null,
      command: this.appliedFilters.command ??  null,
      dateFrom: this.appliedFilters.dateFrom ?? null,
      dateTo: this.appliedFilters.dateTo ?? null,
      pagination: {
        pageIndex: this.pageIndex + 1,
        pageSize: this.pageSize
      }
    };

    this.commandService.getAllByUserId(this.userId, requestPayload).subscribe(
      (data) => {
        this.paginatedList = data;
        this.commands = data.items;
        // this.totalItems = response.totalCount;
        this.dataSource.data = this.commands;
      },
      (error) => {
        this.errorMessage = error;
      }
    );
  }
}
