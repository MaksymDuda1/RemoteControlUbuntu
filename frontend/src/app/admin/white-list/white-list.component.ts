import { Component } from '@angular/core';
import {CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import {FormsModule} from '@angular/forms';
import {NgForOf} from '@angular/common';
import {RouterLink} from '@angular/router';

interface Command {
  id: string;
  name: string;
  description: string;
  type: string;
  code: string;
  icon: string;
  isAllowed?: boolean;
}

@Component({
  selector: 'app-white-list',
  imports: [
    FormsModule,
    CdkDropList,
    NgForOf,
    CdkDrag,
    RouterLink
  ],
  templateUrl: './white-list.component.html',
  styleUrl: './white-list.component.scss'
})
export class WhiteListComponent {
  searchQuery: string = '';
  selectedCategory: string = 'all';
  showAddModal: boolean = false;
  newCommand: Partial<Command> = {};

  availableCommands: Command[] = [
    {
      id: '1',
      name: 'List Files',
      description: 'Lists all files in directory',
      type: 'file',
      code: 'ls -la',
      icon: '📁'
    },
    {
      id: '2',
      name: 'Check Status',
      description: 'Checks system status',
      type: 'system',
      code: 'systemctl status',
      icon: '✅'
    },
    {
      id: '3',
      name: 'Read Logs',
      description: 'Reads application logs',
      type: 'monitoring',
      code: 'tail -f /var/log/app.log',
      icon: '📋'
    },
    {
      id: '4',
      name: 'Database Query',
      description: 'Execute read-only queries',
      type: 'database',
      code: 'SELECT * FROM users LIMIT 10;',
      icon: '🔍'
    },
    {
      id: '5',
      name: 'Show Processes',
      description: 'Display running processes',
      type: 'system',
      code: 'ps aux',
      icon: '⚙️'
    }
  ];

  whitelistedCommands: Command[] = [
    {
      id: '6',
      name: 'Help Command',
      description: 'Shows available commands',
      type: 'utility',
      code: 'help',
      icon: '❓',
      isAllowed: true
    },
    {
      id: '7',
      name: 'Echo Text',
      description: 'Outputs text to console',
      type: 'utility',
      code: 'echo "Hello World"',
      icon: '💬',
      isAllowed: true
    }
  ];

  categories = [
    { value: 'all', label: 'All Categories' },
    { value: 'file', label: 'File Operations' },
    { value: 'system', label: 'System' },
    { value: 'database', label: 'Database' },
    { value: 'monitoring', label: 'Monitoring' },
    { value: 'utility', label: 'Utility' }
  ];

  ngOnInit(): void {
    // Load whitelisted commands from service
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
      command.isAllowed = event.container.id === 'whitelist';

      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  removeFromWhitelist(command: Command, index: number): void {
    this.whitelistedCommands.splice(index, 1);
    command.isAllowed = false;
    this.availableCommands.push(command);
  }

  clearWhitelist(): void {
    if (confirm('Are you sure you want to clear all whitelisted commands? This may restrict all user operations!')) {
      this.availableCommands.push(...this.whitelistedCommands.map(cmd => ({ ...cmd, isAllowed: false })));
      this.whitelistedCommands = [];
    }
  }

  saveWhitelist(): void {
    // Save to backend
    console.log('Saving whitelist:', this.whitelistedCommands);
    alert('Whitelist saved successfully!');
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
    if (this.newCommand.name && this.newCommand.code) {
      const command: Command = {
        id: Date.now().toString(),
        name: this.newCommand.name,
        description: this.newCommand.description || '',
        type: this.newCommand.type || 'custom',
        code: this.newCommand.code,
        icon: '🔧',
        isAllowed: true
      };

      this.whitelistedCommands.push(command);
      this.closeAddModal();
    }
  }
}
