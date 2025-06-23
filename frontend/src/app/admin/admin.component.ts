import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'app-admin',
  imports: [
    NgForOf
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {
  navigationCards = [
    {
      title: 'Black List Commands',
      description: 'Manage blocked commands and restrictions',
      icon: '🚫',
      route: '/admin-black-list',
      color: '#dc3545'
    },
    {
      title: 'White List Commands',
      description: 'Manage allowed commands and permissions',
      icon: '✅',
      route: '/admin-white-list',
      color: '#28a745'
    },
    {
      title: 'User Statistics',
      description: 'View detailed user statistics and analytics',
      icon: '📊',
      route: '/',
      color: '#6366f1'
    },
    {
      title: 'Admins',
      description: 'Manage Admins',
      icon: '👥',
      route: '/admin/admins',
      color: '#6366f1'
    }
  ];

  constructor(private router: Router) {}

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
}
