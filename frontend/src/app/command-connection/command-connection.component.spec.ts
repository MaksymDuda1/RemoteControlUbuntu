import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandConnectionComponent } from './command-connection.component';

describe('CommandConnectionComponent', () => {
  let component: CommandConnectionComponent;
  let fixture: ComponentFixture<CommandConnectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandConnectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandConnectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
