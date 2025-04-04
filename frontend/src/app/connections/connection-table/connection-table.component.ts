// connection-table.component.ts
import {Component, Input, OnInit, ViewChild, OnChanges, SimpleChanges, inject} from '@angular/core';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ConnectionModel } from '../../../models/connection.mode';
import { MatIconModule } from '@angular/material/icon'
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { ConnectionComponent } from '../connection/connection.component';
@Component({
  selector: 'app-connection-table',
  templateUrl: './connection-table.component.html',
  styleUrls: ['./connection-table.component.scss'],
  animations: [
    trigger('animateStagger', [
      state('default', style({ opacity: 1 })),
      transition('void => *', [
        style({ opacity: 0 }),
        animate('300ms ease-in')
      ])
    ])
  ],
  standalone: true,
  imports: [
    MatTableModule,
    RouterModule,
    CommonModule,
    MatSortModule,
    MatIconModule,
    MatDialogModule
  ]
})
export class ConnectionTableComponent implements OnInit, OnChanges {
  @ViewChild(MatSort) sort!: MatSort;

  @Input() connections: ConnectionModel[] = [];
  @Input() errorMessage: string = 'No connections available';
  @Input() disabled: boolean = false;

  public displayedColumns: string[] = ['id', 'name', 'host', 'username'];
  public dataSource = new MatTableDataSource<ConnectionModel>([]);
  public hasConnections: boolean = false;

  readonly dialog = inject(MatDialog);

  constructor() {}

  ngOnInit(): void {
    this.initializeTable();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['connections']) {
      this.initializeTable();
    }
  }

  private initializeTable(): void {
    if (this.connections && this.connections.length > 0) {
      this.dataSource.data = this.connections;
      this.hasConnections = true;

      // Apply sort after view is initialized
      setTimeout(() => {
        if (this.sort) {
          this.dataSource.sort = this.sort;
        }
      });
    } else {
      this.dataSource.data = [];
      this.hasConnections = false;
    }
  }

  openDialog(id: string): void {
    const dialogRef = this.dialog.open(ConnectionComponent, {
      panelClass: 'custom-dialog',
      data: { id: id },
    });

    dialogRef.afterClosed().subscribe((connection: ConnectionModel | undefined) => {
      if (!connection) return;

      const index = this.connections.findIndex(conn => conn.id === connection.id);

      console.log(index);

      if (index !== -1) {
        this.connections[index] = connection;
      } else {
        this.connections = [...this.connections, connection];
      }

      this.initializeTable();
    });
  }


}
