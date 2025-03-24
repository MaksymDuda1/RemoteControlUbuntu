import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ConnectionService } from '../../services/connection.service';
import { Subject } from 'rxjs';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { ConnectionModel } from '../../models/connection.mode';
import { TokenService } from '../../services/token.service';
import { ConnectionTableComponent } from "./connection-table/connection-table.component";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-connections',
  imports: [
    MatTableModule,
     MatPaginatorModule,
      ConnectionTableComponent,
       MatFormFieldModule,
        ReactiveFormsModule,
         MatInputModule],
  templateUrl: './connections.component.html',
  styleUrl: './connections.component.scss'
})
export class ConnectionsComponent implements OnInit, OnDestroy {
  constructor(
    private route: ActivatedRoute,
     private formBuilder: FormBuilder,
      private connectionService: ConnectionService,
       private tokenService: TokenService) {
    this.unsubscribeAll = new Subject<any>();
  }

  public errorMessage = 'No connections';
  
  public dataSource: MatTableDataSource<ConnectionModel> = new MatTableDataSource;
  public hasConnections: boolean = false;
  public searchControl: FormControl<string> = new FormControl;
  public connections: ConnectionModel[] = [];

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
      this.connectionService.getAllByUserId(this.userId).subscribe(data => {
        this.connections = data;
      });
      // TODO rewrite to paginated list
      this.totalItems = 10;

      this.dataSource = new MatTableDataSource<ConnectionModel>(this.connections);
      this.hasConnections = this.dataSource.data.length > 0;

      this.initFilterForm();
      this.applyFilters();
  }

  ngOnDestroy(): void {
      this.unsubscribeAll.next(null);
      this.unsubscribeAll.complete();
  }

  initFilterForm(): void {
      this.filterForm = this.formBuilder.group({
          connectionName: [null],
          host: [null],
          username: [null],
          name: [null]
      });
  }

  resetFilters(): void {
      this.filterForm.reset({
        connectionName: [null],
        host: [null],
        username: [null],
        name: [null]
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

  fetchLogs(): void {
      const requestPayload = {
          connectionName: this.appliedFilters.connectionName ?? null,
          host: this.appliedFilters.host ?? null,
          username: this.appliedFilters.username ??  null,
          name: this.appliedFilters.name ?? null,
          pagination: {
              pageIndex: this.pageIndex + 1,
              pageSize: this.pageSize
          }
      };

      this.connectionService.getAllByUserId(this.userId).subscribe(
          (data) => {
              this.connections = data;
              // this.totalItems = response.totalCount;
              this.dataSource.data = this.connections;
          },
          (error) => {
              this.errorMessage = error;
          }
      );
  }
}
