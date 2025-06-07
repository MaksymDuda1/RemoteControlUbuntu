import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandsSetBuilderComponent } from './commands-set-builder.component';

describe('CommandsSetBuilderComponent', () => {
  let component: CommandsSetBuilderComponent;
  let fixture: ComponentFixture<CommandsSetBuilderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandsSetBuilderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandsSetBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
