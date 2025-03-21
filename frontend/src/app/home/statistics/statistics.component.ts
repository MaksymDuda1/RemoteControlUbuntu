import { Component, OnInit } from '@angular/core';
import { TranslatePipe } from '@ngx-translate/core';
import { ChartModule } from 'primeng/chart';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [ChartModule, TranslatePipe],
  templateUrl: './statistics.component.html',
  styleUrl: './statistics.component.scss'
})
export class StatisticsComponent implements OnInit {
  chartData: any;
  chartOptions: any;

  ngOnInit() {
    this.chartData = {
      labels: ['Executed', 'Pending', 'Failed'],
      datasets: [
        {
          label: 'Commands',
          data: [13, 7, 5], // Дані для графіка
          backgroundColor: ['#42A5F5', '#66BB6A', '#FFCA28']
        }
      ]
    };

    this.chartOptions = {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        x: { beginAtZero: true },
        y: { beginAtZero: true }
      }
    };
  }
}