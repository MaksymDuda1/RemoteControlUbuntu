import { Component, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';
import { TranslatePipe } from '@ngx-translate/core';
import { ChartModule } from 'primeng/chart';
import { MultiSelect } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';

interface CommandCategory {
  label: string;
  value: string;
  color: string;
}

interface AnalyticsData {
  frequency: any;
  errors: any;
  successRate: any;
  averageTime: any;
}

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [ChartModule, TranslatePipe, MultiSelect, FormsModule, DropdownModule, CommonModule],
  templateUrl: './statistics.component.html',
  styleUrl: './statistics.component.scss'
})
export class StatisticsComponent implements OnInit {
  // Кольорова палітра на базі #e3e6f9 (ніжно-синій)
  private colorPalette = {
    primary: ['#4f5b93', '#6b7bb7', '#879bdb', '#a3b5ef', '#bfcff3', '#dbe9f7', '#f7f9fb', '#333d66', '#2a3454'],
    secondary: ['#3d4873', '#586699', '#7384bf', '#8ea2d5', '#a9c0eb', '#c4def1', '#dff6f7', '#424d7a', '#1f2640'],
    tertiary: ['#2a3454', '#4f5b93', '#6b7bb7', '#879bdb', '#a3b5ef', '#bfcff3', '#dbe9f7', '#f7f9fb', '#333d66']
  };

  analyticsTypes = [
    { label: 'Частота виконання команд', value: 'frequency' },
    { label: 'Найпоширеніші помилки', value: 'errors' },
    { label: 'Відсоток успішних виконань', value: 'successRate' },
    { label: 'Середній час виконання', value: 'averageTime' }
  ];

  selectedAnalyticsType = 'frequency';

  availableChartTypes = [
    { label: 'Стовпчики', value: 'bar' },
    { label: 'Лінійний', value: 'line' },
    { label: 'Кругова діаграма', value: 'pie' },
    { label: 'Пончик', value: 'doughnut' }
  ];

  selectedChartType: ChartType = 'bar';

  availableCategories: CommandCategory[] = [
    { label: 'Компіляція проєкту', value: 'compile', color: this.colorPalette.primary[0] },
    { label: 'Перевстановлення залежностей', value: 'reinstall', color: this.colorPalette.primary[1] },
    { label: 'Локальний сервер', value: 'localServer', color: this.colorPalette.primary[2] },
    { label: 'Git синхронізація', value: 'syncGit', color: this.colorPalette.primary[3] },
    { label: 'Перевірка оновлень', value: 'checkUpdates', color: this.colorPalette.primary[4] },
    { label: 'Запуск тестів', value: 'runTests', color: this.colorPalette.primary[5] },
    { label: 'Оновлення конфігурацій', value: 'updateConfigs', color: this.colorPalette.primary[6] },
    { label: 'Production білд', value: 'buildProd', color: this.colorPalette.primary[7] },
    { label: 'Очищення кешу', value: 'clearCache', color: this.colorPalette.primary[8] }
  ];

  selectedCategories = ['compile', 'reinstall', 'localServer', 'syncGit', 'runTests'];

  // Статистичні дані
  totalExecutions = 1247;
  averageSuccessRate = 87.3;
  mostFrequentCommand = 'Локальний сервер';

  // Дані для різних типів аналітики
  analyticsData: AnalyticsData = {
    // Частота виконання команд за місяць
    frequency: {
      labels: ['Тиждень 1', 'Тиждень 2', 'Тиждень 3', 'Тиждень 4'],
      datasets: [
        {
          label: 'Компіляція проєкту',
          backgroundColor: this.colorPalette.primary[0],
          data: [45, 52, 48, 61],
          borderColor: this.colorPalette.secondary[0],
          borderWidth: 2
        },
        {
          label: 'Перевстановлення залежностей',
          backgroundColor: this.colorPalette.primary[1],
          data: [12, 8, 15, 10],
          borderColor: this.colorPalette.secondary[1],
          borderWidth: 2
        },
        {
          label: 'Локальний сервер',
          backgroundColor: this.colorPalette.primary[2],
          data: [78, 85, 71, 92],
          borderColor: this.colorPalette.secondary[2],
          borderWidth: 2
        },
        {
          label: 'Git синхронізація',
          backgroundColor: this.colorPalette.primary[3],
          data: [34, 41, 38, 45],
          borderColor: this.colorPalette.secondary[3],
          borderWidth: 2
        },
        {
          label: 'Запуск тестів',
          backgroundColor: this.colorPalette.primary[4],
          data: [28, 32, 25, 35],
          borderColor: this.colorPalette.secondary[4],
          borderWidth: 2
        }
      ]
    },

    // Найпошириніші помилки
    errors: {
      labels: ['Помилка компіляції', 'Мережева помилка', 'Відсутність залежностей', 'Помилка конфігурації', 'Timeout'],
      datasets: [{
        label: 'Кількість помилок',
        backgroundColor: [
          this.colorPalette.primary[0],
          this.colorPalette.primary[1],
          this.colorPalette.primary[2],
          this.colorPalette.primary[3],
          this.colorPalette.primary[4]
        ],
        data: [23, 18, 15, 12, 8],
        borderColor: '#FFFFFF',
        borderWidth: 3,
        hoverBackgroundColor: [
          this.colorPalette.secondary[0],
          this.colorPalette.secondary[1],
          this.colorPalette.secondary[2],
          this.colorPalette.secondary[3],
          this.colorPalette.secondary[4]
        ],
        hoverBorderWidth: 4
      }]
    },

    // Відсоток успішних виконань
    successRate: {
      labels: this.availableCategories.map(cat => cat.label),
      datasets: [{
        label: 'Відсоток успіху (%)',
        backgroundColor: this.colorPalette.primary,
        data: [92, 78, 95, 88, 85, 91, 82, 89, 94],
        borderColor: '#FFFFFF',
        borderWidth: 3,
        hoverBackgroundColor: this.colorPalette.secondary,
        hoverBorderWidth: 4
      }]
    },

    // Середній час виконання (в секундах)
    averageTime: {
      labels: this.availableCategories.map(cat => cat.label),
      datasets: [{
        label: 'Час виконання (сек)',
        backgroundColor: this.colorPalette.primary[0],
        borderColor: this.colorPalette.secondary[0],
        data: [45.2, 125.8, 2.1, 8.7, 15.3, 89.4, 5.2, 156.7, 12.1],
        borderWidth: 3,
        fill: false,
        pointBackgroundColor: this.colorPalette.primary[1],
        pointBorderColor: this.colorPalette.secondary[1],
        pointBorderWidth: 2,
        pointRadius: 6,
        pointHoverRadius: 8,
        pointHoverBackgroundColor: this.colorPalette.primary[2],
        pointHoverBorderColor: this.colorPalette.secondary[2]
      }]
    }
  };

  currentChartData: any;
  currentChartOptions: any;

  ngOnInit() {
    this.updateChartData();
  }

  onAnalyticsTypeChange() {
    // Встановлюємо оптимальний тип графіка для кожного типу аналітики
    switch (this.selectedAnalyticsType) {
      case 'frequency':
        this.selectedChartType = 'bar';
        break;
      case 'errors':
        this.selectedChartType = 'doughnut';
        break;
      case 'successRate':
        this.selectedChartType = 'pie';
        break;
      case 'averageTime':
        this.selectedChartType = 'bar';
        break;
    }
    this.updateChartData();
  }

  onChartTypeChange() {
    this.updateChartData();
  }

  updateChartData() {
    const baseData = this.analyticsData[this.selectedAnalyticsType as keyof AnalyticsData];

    if (this.selectedAnalyticsType === 'successRate' || this.selectedAnalyticsType === 'averageTime') {
      // Для цих типів не застосовуємо фільтрацію
      this.currentChartData = { ...baseData };
    } else if (this.selectedAnalyticsType === 'frequency') {
      // Фільтруємо дані за вибраними категоріями
      this.currentChartData = {
        ...baseData,
        datasets: baseData.datasets.filter((dataset: any) => {
          const category = this.availableCategories.find(cat => cat.label === dataset.label);
          return category ? this.selectedCategories.includes(category.value) : false;
        })
      };
    } else {
      // Для помилок залишаємо як є
      this.currentChartData = { ...baseData };
    }

    this.updateChartOptions();
  }

  updateChartOptions() {
    const baseOptions = {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          position: 'top' as const,
          labels: {
            padding: 20,
            font: {
              size: 12,
              family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
            },
            color: this.colorPalette.secondary[1]
          }
        }
      }
    };

    switch (this.selectedAnalyticsType) {
      case 'frequency':
        this.currentChartOptions = {
          ...baseOptions,
          plugins: {
            ...baseOptions.plugins,
            title: {
              display: true,
              text: 'Частота виконання команд по тижнях',
              font: {
                size: 16,
                weight: 'bold',
                family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
              },
              color: this.colorPalette.secondary[1]
            }
          },
          scales: {
            y: {
              beginAtZero: true,
              title: {
                display: true,
                text: 'Кількість виконань',
                color: this.colorPalette.secondary[2],
                font: {
                  size: 12,
                  weight: 'bold'
                }
              },
              ticks: {
                color: this.colorPalette.tertiary[1]
              },
              grid: {
                color: 'rgba(79, 91, 147, 0.1)'
              }
            },
            x: {
              title: {
                display: true,
                text: 'Періоди',
                color: this.colorPalette.secondary[2],
                font: {
                  size: 12,
                  weight: 'bold'
                }
              },
              ticks: {
                color: this.colorPalette.tertiary[1]
              },
              grid: {
                color: 'rgba(79, 91, 147, 0.1)'
              }
            }
          }
        };
        break;

      case 'errors':
        this.currentChartOptions = {
          ...baseOptions,
          plugins: {
            ...baseOptions.plugins,
            title: {
              display: true,
              text: 'Розподіл найпоширеніших помилок',
              font: {
                size: 16,
                weight: 'bold',
                family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
              },
              color: this.colorPalette.secondary[1]
            },
            tooltip: {
              backgroundColor: 'rgba(79, 91, 147, 0.95)',
              titleColor: '#FFFFFF',
              bodyColor: '#FFFFFF',
              borderColor: this.colorPalette.primary[1],
              borderWidth: 2,
              callbacks: {
                label: (context: any) => {
                  const total = context.dataset.data.reduce((sum: number, value: number) => sum + value, 0);
                  const percentage = ((context.parsed / total) * 100).toFixed(1);
                  return `${context.label}: ${context.parsed} (${percentage}%)`;
                }
              }
            },
            datalabels: {
              display: true,
              color: '#FFFFFF',
              font: {
                weight: 'bold',
                size: 12,
                family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
              },
              formatter: (value: number, context: any) => {
                const total = context.dataset.data.reduce((sum: number, val: number) => sum + val, 0);
                const percentage = ((value / total) * 100).toFixed(1);
                return `${percentage}%`;
              }
            }
          }
        };
        break;

      case 'successRate':
        this.currentChartOptions = {
          ...baseOptions,
          plugins: {
            ...baseOptions.plugins,
            title: {
              display: true,
              text: 'Відсоток успішних виконань по командах',
              font: {
                size: 16,
                weight: 'bold',
                family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
              },
              color: this.colorPalette.secondary[1]
            },
            tooltip: {
              backgroundColor: 'rgba(79, 91, 147, 0.95)',
              titleColor: '#FFFFFF',
              bodyColor: '#FFFFFF',
              borderColor: this.colorPalette.primary[1],
              borderWidth: 2
            }
          }
        };
        break;

      case 'averageTime':
        this.currentChartOptions = {
          ...baseOptions,
          indexAxis: 'y', // Це робить діаграму горизонтальною
          plugins: {
            ...baseOptions.plugins,
            title: {
              display: true,
              text: 'Середній час виконання команд',
              font: {
                size: 16,
                weight: 'bold',
                family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
              },
              color: this.colorPalette.secondary[1]
            },
            legend: {
              display: false // Приховуємо легенду для горизонтальної діаграми
            },
            tooltip: {
              backgroundColor: 'rgba(79, 91, 147, 0.95)',
              titleColor: '#FFFFFF',
              bodyColor: '#FFFFFF',
              borderColor: this.colorPalette.primary[1],
              borderWidth: 2
            }
          },
          scales: {
            x: {
              beginAtZero: true,
              title: {
                display: true,
                text: 'Час (секунди)',
                color: this.colorPalette.secondary[2],
                font: {
                  size: 12,
                  weight: 'bold'
                }
              },
              grid: {
                display: true,
                color: 'rgba(79, 91, 147, 0.1)'
              },
              ticks: {
                color: this.colorPalette.tertiary[1]
              }
            },
            y: {
              title: {
                display: true,
                text: 'Команди',
                color: this.colorPalette.secondary[2],
                font: {
                  size: 12,
                  weight: 'bold'
                }
              },
              ticks: {
                maxRotation: 0,
                font: {
                  size: 10
                },
                color: this.colorPalette.tertiary[1]
              },
              grid: {
                color: 'rgba(79, 91, 147, 0.1)'
              }
            }
          }
        };
        break;
    }
  }
}
