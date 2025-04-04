import {Component, inject, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import {MatButton} from '@angular/material/button';
import {ConnectionService} from '../../../services/connection.service';
import {ConnectionModel} from '../../../models/connection.mode';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {TokenService} from '../../../services/token.service';
import {ConnectionsComponent} from '../connections.component';

@Component({
  selector: 'app-connection',
  imports: [MatDialogModule, MatButton, ReactiveFormsModule],
  templateUrl: './connection.component.html',
  styleUrl: './connection.component.scss'
})
export class ConnectionComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: {id: string},
    private dialogRef: MatDialogRef<ConnectionsComponent>
  ) { }

  private formBuilder = inject(FormBuilder);
  private tokenService = inject(TokenService);
  private connectionService = inject(ConnectionService);

  public connectionForm: FormGroup = new FormGroup({});
  public connection: ConnectionModel = new ConnectionModel();

  public isUpdate: boolean = false;

  ngOnInit(): void {
    this.initializeForm();

    if(this.data.id){
      this.connectionService.getById(this.data.id).subscribe(connection => {
        this.connection = connection;
        this.isUpdate = true;
        this.connectionForm.patchValue(connection);
      });
    }
  }

  private initializeForm(): void {
    this.connectionForm = this.formBuilder.group({
      id: [{ value: null, disabled: true }, [Validators.required]],
      name: [null, [Validators.required]],
      host: [null, [Validators.required]],
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
      userId: [this.tokenService.getUserId()]
    });
  }

  onAdd(){
   this.connectionService.create(this.connectionForm.getRawValue()).subscribe(() => {
     this.connection = this.connectionForm.getRawValue();

     this.dialogRef.close(this.connection);
   });
  }

  onUpdate(){
    this.connectionService.update(this.connection.id, this.connectionForm.getRawValue()).subscribe(() => {
      this.connection = this.connectionForm.getRawValue();
      this.dialogRef.close(this.connection);
    });
  }


}
