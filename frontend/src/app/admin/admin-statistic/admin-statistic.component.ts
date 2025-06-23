import {Component, OnInit} from '@angular/core';
import {DatePipe, NgClass, NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {RouterLink} from '@angular/router';

interface User {
  id: string;
  name: string;
  email: string;
  role: string;
  lastActive: Date;
  status: 'active' | 'inactive' | 'suspended';
}

interface UserStatistics {
  totalCommands: number;
  allowedCommands: number;
  blockedCommands: number;
  successRate: number;
  lastCommand: string;
  lastCommandTime: Date;
  commandHistory: CommandHistory[];
  dailyActivity: DailyActivity[];
}

interface CommandHistory {
  command: string;
  timestamp: Date;
  status: 'success' | 'blocked' | 'failed';
  type: string;
}

interface DailyActivity {
  date: string;
  commands: number;
  blocked: number;
}

@Component({
  selector: 'app-admin-statistic',
  imports: [
    DatePipe,
    NgClass,
    FormsModule,
    NgForOf,
    NgIf,
    RouterLink
  ],
  templateUrl: './admin-statistic.component.html',
  styleUrl: './admin-statistic.component.scss'
})
export class AdminStatisticComponent implements OnInit {
  users: User[] = [
    {
      id: '1',
      name: 'John Doe',
      email: 'john.doe@example.com',
      role: 'Developer',
      lastActive: new Date('2024-01-15T10:30:00'),
      status: 'active'
    },
    {
      id: '2',
      name: 'Jane Smith',
      email: 'jane.smith@example.com',
      role: 'Admin',
      lastActive: new Date('2024-01-15T14:20:00'),
      status: 'active'
    },
    {
      id: '3',
      name: 'Bob Johnson',
      email: 'bob.johnson@example.com',
      role: 'User',
      lastActive: new Date('2024-01-14T09:00:00'),
      status: 'inactive'
    }
  ];

  selectedUser: User | null = null;
  userStatistics: UserStatistics | null = null;
  searchQuery: string = '';
  selectedTimeRange: string = '7days';
  selectedStatus: string = 'all';

  timeRanges = [
    { value: '24hours', label: 'Last 24 Hours' },
    { value: '7days', label: 'Last 7 Days' },
    { value: '30days', label: 'Last 30 Days' },
    { value: '90days', label: 'Last 90 Days' }
  ];

  statuses = [
    { value: 'all', label: 'All Statuses' },
    { value: 'active', label: 'Active' },
    { value: 'inactive', label: 'Inactive' },
    { value: 'suspended', label: 'Suspended' }
  ];

  ngOnInit(): void {
    // Load users from service
  }

  get filteredUsers(): User[] {
    return this.users.filter(user => {
      const matchesSearch = user.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        user.email.toLowerCase().includes(this.searchQuery.toLowerCase());
      const matchesStatus = this.selectedStatus === 'all' || user.status === this.selectedStatus;
      return matchesSearch && matchesStatus;
    });
  }

  selectUser(user: User): void {
    this.selectedUser = user;
    this.loadUserStatistics(user.id);
  }

  loadUserStatistics(userId: string): void {
    // Simulate loading statistics
    this.userStatistics = {
      totalCommands: 1234,
      allowedCommands: 1156,
      blockedCommands: 78,
      successRate: 93.7,
      lastCommand: 'git commit -m "Update feature"',
      lastCommandTime: new Date(),
      commandHistory: [
        {
          command: 'git commit -m "Update feature"',
          timestamp: new Date('2024-01-15T15:30:00'),
          status: 'success',
          type: 'git'
        },
        {
          command: 'rm -rf /',
          timestamp: new Date('2024-01-15T15:25:00'),
          status: 'blocked',
          type: 'destructive'
        },
        {
          command: 'ls -la',
          timestamp: new Date('2024-01-15T15:20:00'),
          status: 'success',
          type: 'file'
        },
        {
          command: 'docker ps',
          timestamp: new Date('2024-01-15T15:15:00'),
          status: 'success',
          type: 'docker'
        },
        {
          command: 'npm install',
          timestamp: new Date('2024-01-15T15:10:00'),
          status: 'success',
          type: 'npm'
        }
      ],
      dailyActivity: [
        { date: '2024-01-09', commands: 45, blocked: 3 },
        { date: '2024-01-10', commands: 52, blocked: 5 },
        { date: '2024-01-11', commands: 38, blocked: 2 },
        { date: '2024-01-12', commands: 61, blocked: 7 },
        { date: '2024-01-13', commands: 43, blocked: 4 },
        { date: '2024-01-14', commands: 55, blocked: 6 },
        { date: '2024-01-15', commands: 48, blocked: 5 }
      ]
    };
  }

  exportUserData(): void {
    if (this.selectedUser && this.userStatistics) {
      console.log('Exporting data for user:', this.selectedUser.name);
      alert('User statistics exported successfully!');
    }
  }

  suspendUser(user: User): void {
    if (confirm(`Are you sure you want to suspend ${user.name}?`)) {
      user.status = 'suspended';
      // Update in backend
    }
  }

  activateUser(user: User): void {
    user.status = 'active';
    // Update in backend
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'success': return 'status-success';
      case 'blocked': return 'status-blocked';
      case 'failed': return 'status-failed';
      case 'active': return 'user-active';
      case 'inactive': return 'user-inactive';
      case 'suspended': return 'user-suspended';
      default: return '';
    }
  }

  getMaxCommands(): number {
    if (!this.userStatistics) return 0;
    return Math.max(...this.userStatistics.dailyActivity.map(d => d.commands));
  }
}
