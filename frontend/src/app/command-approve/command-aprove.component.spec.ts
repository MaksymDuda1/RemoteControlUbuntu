import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandAproveComponent } from './command-aprove.component';

describe('CommandAproveComponent', () => {
  let component: CommandAproveComponent;
  let fixture: ComponentFixture<CommandAproveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandAproveComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandAproveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
