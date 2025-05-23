import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandTypeDialogComponent } from './command-type-dialog.component';

describe('CommandTypeDialogComponent', () => {
  let component: CommandTypeDialogComponent;
  let fixture: ComponentFixture<CommandTypeDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandTypeDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandTypeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
