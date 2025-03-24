import { Component, OnDestroy, OnInit } from '@angular/core';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator'
import { MatTableDataSource, MatTableModule } from '@angular/material/table'
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-commands',
  imports: [MatPaginatorModule, MatTableModule, RouterModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './commands.component.html',
  styleUrl: './commands.component.scss'
})
export class CommandsComponent {


}