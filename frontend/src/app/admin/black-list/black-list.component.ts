import { Component } from '@angular/core';
import {Command} from '../../commands-set-builder/commands-set-builder.component';
import {CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-black-list',
  imports: [
    FormsModule,
    NgIf,
    CdkDropList,
    NgForOf,
    CdkDrag,
    RouterLink
  ],
  templateUrl: './black-list.component.html',
  styleUrl: './black-list.component.scss'
})
export class BlackListComponent {
  searchQuery: string = '';
  selectedCategory: string = 'all';
  showAddModal: boolean = false;
  newCommand: Partial<Command> = {};

  availableCommands: Command[] = [
    {
      id: '1',
      name: 'Delete All',
      description: 'Permanently deletes all data',
      type: 'destructive',
      terminalCommand: 'rm -rf /',
      icon: '⚠️',
      isBlocked: false
    },
    {
      id: '2',
      name: 'Force Shutdown',
      description: 'Forcefully shuts down the system',
      type: 'system',
      terminalCommand: 'shutdown -h now',
      icon: '🔌',
      isBlocked: false
    },
    {
      id: '3',
      name: 'Drop Database',
      description: 'Drops entire database',
      type: 'database',
      terminalCommand: 'DROP DATABASE production;',
      icon: '🗄️',
      isBlocked: false
    },
    {
      id: '4',
      name: 'Kill Process',
      description: 'Terminates all processes',
      type: 'system',
      terminalCommand: 'killall -9',
      icon: '💀',
      isBlocked: false
    }
  ];

  blacklistedCommands: Command[] = [
    {
      id: '5',
      name: 'Format Disk',
      description: 'Formats the main disk',
      type: 'destructive',
      terminalCommand: 'format C:',
      icon: '💾',
      isBlocked: true
    }
  ];

  categories = [
    { value: 'all', label: 'All Categories' },
    { value: 'destructive', label: 'Destructive' },
    { value: 'system', label: 'System' },
    { value: 'database', label: 'Database' }
  ];

  ngOnInit(): void {
    // Load blacklisted commands from service
  }

  get filteredAvailableCommands(): Command[] {
    return this.availableCommands.filter(cmd => {
      const matchesSearch = cmd.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        cmd.description.toLowerCase().includes(this.searchQuery.toLowerCase());
      const matchesCategory = this.selectedCategory === 'all' || cmd.type === this.selectedCategory;
      return matchesSearch && matchesCategory;
    });
  }

  drop(event: CdkDragDrop<Command[]>): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const command = { ...event.previousContainer.data[event.previousIndex] };
      command.isBlocked = event.container.id === 'blacklist';

      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  removeFromBlacklist(command: Command, index: number): void {
    this.blacklistedCommands.splice(index, 1);
    command.isBlocked = false;
    this.availableCommands.push(command);
  }

  clearBlacklist(): void {
    if (confirm('Are you sure you want to clear all blacklisted commands?')) {
      this.availableCommands.push(...this.blacklistedCommands.map(cmd => ({ ...cmd, isBlocked: false })));
      this.blacklistedCommands = [];
    }
  }

  saveBlacklist(): void {
    // Save to backend
    console.log('Saving blacklist:', this.blacklistedCommands);
    alert('Blacklist saved successfully!');
  }

  openAddModal(): void {
    this.showAddModal = true;
    this.newCommand = {};
  }

  closeAddModal(): void {
    this.showAddModal = false;
    this.newCommand = {};
  }

  addNewCommand(): void {
    if (this.newCommand.name && this.newCommand.terminalCommand) {
      const command: Command = {
        id: Date.now().toString(),
        name: this.newCommand.name,
        description: this.newCommand.description || '',
        type: this.newCommand.type || 'custom',
        terminalCommand: this.newCommand.terminalCommand,
        icon: '🔧',
        isBlocked: true
      };

      this.blacklistedCommands.push(command);
      this.closeAddModal();
    }
  }
}
