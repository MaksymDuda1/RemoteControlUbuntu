// command-set-builder.component.ts
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DragDropModule, CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

export interface Command {
  id: string;
  name: string;
  description: string;
  terminalCommand: string;
  type: CommandType;
  parameters?: CommandParameter[];
  icon?: string;
}

export interface CommandParameter {
  name: string;
  type: 'string' | 'number' | 'boolean' | 'select';
  required: boolean;
  defaultValue?: any;
  options?: string[]; // for select type
  placeholder?: string;
}

export enum CommandType {
  System = 'system',
  Network = 'network',
  File = 'file',
  Process = 'process',
  Custom = 'custom'
}

export interface CommandSet {
  id: string;
  name: string;
  description: string;
  commands: Command[];
  createdAt: Date;
}

@Component({
  selector: 'app-command-set-builder',
  standalone: true,
  imports: [CommonModule, FormsModule, DragDropModule],
  templateUrl: './commands-set-builder.component.html',
  styleUrls: ['./commands-set-builder.component.scss']
})
export class CommandSetBuilderComponent implements OnInit {
  @Input() initialCommandSet?: CommandSet;
  @Output() commandSetCreated = new EventEmitter<CommandSet>();
  @Output() commandSetUpdated = new EventEmitter<CommandSet>();

  // Available commands library
  availableCommands: Command[] = [
    {
      id: 'ls',
      name: 'List Files',
      description: 'List directory contents',
      terminalCommand: 'ls -la',
      type: CommandType.File,
      icon: '📁'
    },
    {
      id: 'ps',
      name: 'Process List',
      description: 'Show running processes',
      terminalCommand: 'ps aux',
      type: CommandType.Process,
      icon: '⚙️'
    },
    {
      id: 'top',
      name: 'System Monitor',
      description: 'Display system processes',
      terminalCommand: 'top',
      type: CommandType.System,
      icon: '📊'
    },
    {
      id: 'ping',
      name: 'Ping Host',
      description: 'Ping a network host',
      terminalCommand: 'ping -c 4 {{host}}',
      type: CommandType.Network,
      icon: '🌐',
      parameters: [
        {
          name: 'host',
          type: 'string',
          required: true,
          placeholder: 'Enter hostname or IP'
        }
      ]
    },
    {
      id: 'disk_usage',
      name: 'Disk Usage',
      description: 'Check disk space usage',
      terminalCommand: 'df -h',
      type: CommandType.System,
      icon: '💾'
    },
    {
      id: 'memory_info',
      name: 'Memory Info',
      description: 'Display memory information',
      terminalCommand: 'free -h',
      type: CommandType.System,
      icon: '🧠'
    },
    {
      id: 'network_interfaces',
      name: 'Network Interfaces',
      description: 'Show network interfaces',
      terminalCommand: 'ip addr show',
      type: CommandType.Network,
      icon: '🔌'
    },
    {
      id: 'find_file',
      name: 'Find File',
      description: 'Search for files',
      terminalCommand: 'find {{path}} -name "{{filename}}" -type f',
      type: CommandType.File,
      icon: '🔍',
      parameters: [
        {
          name: 'path',
          type: 'string',
          required: true,
          defaultValue: '/',
          placeholder: 'Search path'
        },
        {
          name: 'filename',
          type: 'string',
          required: true,
          placeholder: 'File name pattern'
        }
      ]
    },
    {
      id: 'service_status',
      name: 'Service Status',
      description: 'Check service status',
      terminalCommand: 'systemctl status {{service}}',
      type: CommandType.System,
      icon: '🔧',
      parameters: [
        {
          name: 'service',
          type: 'string',
          required: true,
          placeholder: 'Service name (e.g., nginx, ssh)'
        }
      ]
    },
    {
      id: 'kill_process',
      name: 'Kill Process',
      description: 'Terminate a process by PID',
      terminalCommand: 'kill {{signal}} {{pid}}',
      type: CommandType.Process,
      icon: '🛑',
      parameters: [
        {
          name: 'signal',
          type: 'select',
          required: false,
          defaultValue: '-TERM',
          options: ['-TERM', '-KILL', '-HUP', '-USR1', '-USR2']
        },
        {
          name: 'pid',
          type: 'number',
          required: true,
          placeholder: 'Process ID'
        }
      ]
    }
  ];

  // Current command set being built
  currentCommandSet: CommandSet = {
    id: '',
    name: '',
    description: '',
    commands: [],
    createdAt: new Date()
  };

  // UI state
  selectedCommands: Command[] = [];
  filteredCommands: Command[] = [];
  searchTerm: string = '';
  selectedCategory: CommandType | 'all' = 'all';
  isEditMode: boolean = false;
  showParameterDialog: boolean = false;
  currentEditingCommand?: Command;
  commandParameters: { [key: string]: any } = {};

  // Command categories for filtering
  commandCategories = [
    { value: 'all', label: 'All Commands', icon: '📋' },
    { value: CommandType.System, label: 'System', icon: '🖥️' },
    { value: CommandType.Network, label: 'Network', icon: '🌐' },
    { value: CommandType.File, label: 'File Operations', icon: '📁' },
    { value: CommandType.Process, label: 'Processes', icon: '⚙️' },
    { value: CommandType.Custom, label: 'Custom', icon: '🔧' }
  ];

  ngOnInit() {
    this.filteredCommands = [...this.availableCommands];

    if (this.initialCommandSet) {
      this.currentCommandSet = { ...this.initialCommandSet };
      this.selectedCommands = [...this.initialCommandSet.commands];
      this.isEditMode = true;
    }
  }

  // Drag and drop handlers
  onCommandDrop(event: CdkDragDrop<Command[]>) {
    if (event.previousContainer === event.container) {
      // Reordering within the same container
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // Moving between containers
      const command = { ...event.previousContainer.data[event.previousIndex] };
      command.id = this.generateUniqueId();

      if (command.parameters && command.parameters.length > 0) {
        // Show parameter dialog for commands with parameters
        this.currentEditingCommand = command;
        this.initializeCommandParameters(command);
        this.showParameterDialog = true;
      } else {
        // Add command directly to selected commands
        this.selectedCommands.push(command);
      }
    }
  }

  // Search and filter commands
  filterCommands() {
    let filtered = this.availableCommands;

    if (this.selectedCategory !== 'all') {
      filtered = filtered.filter(cmd => cmd.type === this.selectedCategory);
    }

    if (this.searchTerm.trim()) {
      const search = this.searchTerm.toLowerCase();
      filtered = filtered.filter(cmd =>
        cmd.name.toLowerCase().includes(search) ||
        cmd.description.toLowerCase().includes(search) ||
        cmd.terminalCommand.toLowerCase().includes(search)
      );
    }

    this.filteredCommands = filtered;
  }

  // Parameter handling
  initializeCommandParameters(command: Command) {
    this.commandParameters = {};
    if (command.parameters) {
      command.parameters.forEach(param => {
        this.commandParameters[param.name] = param.defaultValue || '';
      });
    }
  }

  confirmCommandParameters() {
    if (this.currentEditingCommand) {
      const command = { ...this.currentEditingCommand };

      // Replace parameters in terminal command
      let terminalCommand = command.terminalCommand;
      Object.keys(this.commandParameters).forEach(key => {
        const value = this.commandParameters[key];
        terminalCommand = terminalCommand.replace(new RegExp(`{{${key}}}`, 'g'), value);
      });

      command.terminalCommand = terminalCommand;
      command.name = `${command.name} (${Object.values(this.commandParameters).join(', ')})`;

      this.selectedCommands.push(command);
      this.closeParameterDialog();
    }
  }

  closeParameterDialog() {
    this.showParameterDialog = false;
    this.currentEditingCommand = undefined;
    this.commandParameters = {};
  }

  // Command management
  removeCommand(index: number) {
    this.selectedCommands.splice(index, 1);
  }

  editCommand(command: Command, index: number) {
    // Allow editing of command parameters
    this.currentEditingCommand = { ...command };
    this.initializeCommandParameters(this.currentEditingCommand);
    this.showParameterDialog = true;
  }

  duplicateCommand(command: Command) {
    const duplicated = { ...command };
    duplicated.id = this.generateUniqueId();
    duplicated.name = `${command.name} (Copy)`;
    this.selectedCommands.push(duplicated);
  }

  // Command set management
  saveCommandSet() {
    if (!this.currentCommandSet.name.trim()) {
      alert('Please enter a command set name');
      return;
    }

    this.currentCommandSet.commands = [...this.selectedCommands];
    this.currentCommandSet.id = this.currentCommandSet.id || this.generateUniqueId();

    if (this.isEditMode) {
      this.commandSetUpdated.emit(this.currentCommandSet);
    } else {
      this.commandSetCreated.emit(this.currentCommandSet);
    }
  }

  clearCommandSet() {
    this.selectedCommands = [];
    this.currentCommandSet = {
      id: '',
      name: '',
      description: '',
      commands: [],
      createdAt: new Date()
    };
  }

  // Utility methods
  generateUniqueId(): string {
    return Date.now().toString(36) + Math.random().toString(36).substr(2);
  }

  getCommandIcon(type: CommandType): string {
    const category = this.commandCategories.find(cat => cat.value === type);
    return category?.icon || '🔧';
  }

  // Export command set as JSON
  exportCommandSet() {
    const dataStr = JSON.stringify(this.currentCommandSet, null, 2);
    const dataUri = 'data:application/json;charset=utf-8,'+ encodeURIComponent(dataStr);

    const exportFileDefaultName = `${this.currentCommandSet.name || 'command-set'}.json`;

    const linkElement = document.createElement('a');
    linkElement.setAttribute('href', dataUri);
    linkElement.setAttribute('download', exportFileDefaultName);
    linkElement.click();
  }

  // Preview command execution order
  previewExecution() {
    const preview = this.selectedCommands.map((cmd, index) =>
      `${index + 1}. ${cmd.name}: ${cmd.terminalCommand}`
    ).join('\n');

    alert(`Execution Preview:\n\n${preview}`);
  }
}
