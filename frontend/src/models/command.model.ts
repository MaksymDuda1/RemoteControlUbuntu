export class CommandModel{
    id: string = '';
    name : string = '';
    type: string = '';
    terminalCommand: string = '';
    userId : string = '';
    lastUsedDate: string = '';
}

// models/command-category.model.ts
export interface CommandCategory {
  label: string;
  value: string;
  color: string;
  description?: string;
  icon?: string;
}

export class CommandCategoryModel implements CommandCategory {
  constructor(
    public label: string,
    public value: string,
    public color: string,
    public description?: string,
    public icon?: string
  ) {}

  static createDefault(): CommandCategoryModel[] {
    return [
      new CommandCategoryModel(
        'Компіляція проєкту',
        'compile',
        '#42A5F5',
        'Збірка та компіляція коду проєкту',
        'pi-cog'
      ),
      new CommandCategoryModel(
        'Перевстановлення залежностей',
        'reinstall',
        '#66BB6A',
        'Видалення та повторне встановлення npm/yarn пакетів',
        'pi-refresh'
      ),
      new CommandCategoryModel(
        'Локальний сервер',
        'localServer',
        '#FFA726',
        'Запуск локального сервера розробки',
        'pi-server'
      ),
      new CommandCategoryModel(
        'Git синхронізація',
        'syncGit',
        '#AB47BC',
        'Операції з системою контролю версій Git',
        'pi-git'
      ),
      new CommandCategoryModel(
        'Перевірка оновлень',
        'checkUpdates',
        '#FF7043',
        'Перевірка доступних оновлень пакетів',
        'pi-search'
      ),
      new CommandCategoryModel(
        'Запуск тестів',
        'runTests',
        '#26A69A',
        'Виконання автоматизованих тестів',
        'pi-check-circle'
      ),
      new CommandCategoryModel(
        'Оновлення конфігурацій',
        'updateConfigs',
        '#EC407A',
        'Оновлення конфігураційних файлів проєкту',
        'pi-file-edit'
      ),
      new CommandCategoryModel(
        'Production білд',
        'buildProd',
        '#78909C',
        'Створення продакшн збірки проєкту',
        'pi-upload'
      ),
      new CommandCategoryModel(
        'Очищення кешу',
        'clearCache',
        '#8D6E63',
        'Очищення кешу компілятора та тимчасових файлів',
        'pi-trash'
      )
    ];
  }

  getDisplayInfo(): { label: string; value: string; color: string } {
    return {
      label: this.label,
      value: this.value,
      color: this.color
    };
  }
}

// models/chart-dataset.model.ts
export interface ChartDataset {
  label: string;
  backgroundColor: string | string[];
  borderColor?: string | string[];
  data: number[];
  borderWidth?: number;
  fill?: boolean;
}

export class ChartDatasetModel implements ChartDataset {
  constructor(
    public label: string,
    public backgroundColor: string | string[],
    public data: number[],
    public borderColor?: string | string[],
    public borderWidth: number = 1,
    public fill: boolean = true
  ) {}

  static createFromCategory(category: CommandCategory, data: number[]): ChartDatasetModel {
    return new ChartDatasetModel(
      category.label,
      category.color,
      data,
      this.darkenColor(category.color),
      1,
      true
    );
  }

  private static darkenColor(color: string): string {
    // Простий алгоритм затемнення кольору
    const hex = color.replace('#', '');
    const r = Math.max(0, parseInt(hex.substr(0, 2), 16) - 30);
    const g = Math.max(0, parseInt(hex.substr(2, 2), 16) - 30);
    const b = Math.max(0, parseInt(hex.substr(4, 2), 16) - 30);

    return `#${r.toString(16).padStart(2, '0')}${g.toString(16).padStart(2, '0')}${b.toString(16).padStart(2, '0')}`;
  }
}

// models/chart-data.model.ts
export interface ChartData {
  labels: string[];
  datasets: ChartDataset[];
}

export class ChartDataModel implements ChartData {
  constructor(
    public labels: string[],
    public datasets: ChartDataset[]
  ) {}

  static createEmpty(): ChartDataModel {
    return new ChartDataModel([], []);
  }

  filterDatasets(allowedValues: string[], categories: CommandCategory[]): ChartDataModel {
    const filteredDatasets = this.datasets.filter(dataset => {
      const category = categories.find(cat => cat.label === dataset.label);
      return category ? allowedValues.includes(category.value) : false;
    });

    return new ChartDataModel(this.labels, filteredDatasets);
  }

  clone(): ChartDataModel {
    return new ChartDataModel(
      [...this.labels],
      this.datasets.map(ds => ({ ...ds, data: [...ds.data] }))
    );
  }
}

// models/error-data.model.ts
export interface ErrorData {
  type: string;
  count: number;
  percentage: number;
  description: string;
  severity: 'low' | 'medium' | 'high' | 'critical';
}

export class ErrorDataModel implements ErrorData {
  constructor(
    public type: string,
    public count: number,
    public percentage: number,
    public description: string,
    public severity: 'low' | 'medium' | 'high' | 'critical'
  ) {}

  static createDefaultErrors(): ErrorDataModel[] {
    return [
      new ErrorDataModel(
        'Помилка компіляції',
        23,
        30.3,
        'Синтаксичні помилки в коді або проблеми з TypeScript',
        'high'
      ),
      new ErrorDataModel(
        'Мережева помилка',
        18,
        23.7,
        'Проблеми з підключенням до зовнішніх ресурсів',
        'medium'
      ),
      new ErrorDataModel(
        'Відсутність залежностей',
        15,
        19.7,
        'Відсутні або несумісні npm пакети',
        'high'
      ),
      new ErrorDataModel(
        'Помилка конфігурації',
        12,
        15.8,
        'Неправильні налаштування в конфігураційних файлах',
        'medium'
      ),
      new ErrorDataModel(
        'Timeout',
        8,
        10.5,
        'Перевищення часу очікування виконання операції',
        'low'
      )
    ];
  }

  getSeverityColor(): string {
    switch (this.severity) {
      case 'critical': return '#DC3545';
      case 'high': return '#FD7E14';
      case 'medium': return '#FFC107';
      case 'low': return '#28A745';
      default: return '#6C757D';
    }
  }
}

// models/performance-metric.model.ts
export interface PerformanceMetric {
  commandValue: string;
  commandLabel: string;
  averageTime: number;
  successRate: number;
  totalExecutions: number;
  lastExecuted: Date;
}

export class PerformanceMetricModel implements PerformanceMetric {
  constructor(
    public commandValue: string,
    public commandLabel: string,
    public averageTime: number,
    public successRate: number,
    public totalExecutions: number,
    public lastExecuted: Date
  ) {}

  static createDefaultMetrics(): PerformanceMetricModel[] {
    const now = new Date();
    return [
      new PerformanceMetricModel('compile', 'Компіляція проєкту', 45.2, 92, 206, new Date(now.getTime() - 2 * 60 * 60 * 1000)),
      new PerformanceMetricModel('reinstall', 'Перевстановлення залежностей', 125.8, 78, 35, new Date(now.getTime() - 24 * 60 * 60 * 1000)),
      new PerformanceMetricModel('localServer', 'Локальний сервер', 2.1, 95, 326, new Date(now.getTime() - 30 * 60 * 1000)),
      new PerformanceMetricModel('syncGit', 'Git синхронізація', 8.7, 88, 158, new Date(now.getTime() - 4 * 60 * 60 * 1000)),
      new PerformanceMetricModel('checkUpdates', 'Перевірка оновлень', 15.3, 85, 67, new Date(now.getTime() - 12 * 60 * 60 * 1000)),
      new PerformanceMetricModel('runTests', 'Запуск тестів', 89.4, 91, 114, new Date(now.getTime() - 1 * 60 * 60 * 1000)),
      new PerformanceMetricModel('updateConfigs', 'Оновлення конфігурацій', 5.2, 82, 28, new Date(now.getTime() - 48 * 60 * 60 * 1000)),
      new PerformanceMetricModel('buildProd', 'Production білд', 156.7, 89, 42, new Date(now.getTime() - 6 * 60 * 60 * 1000)),
      new PerformanceMetricModel('clearCache', 'Очищення кешу', 12.1, 94, 53, new Date(now.getTime() - 8 * 60 * 60 * 1000))
    ];
  }

  getPerformanceRating(): 'excellent' | 'good' | 'average' | 'poor' {
    if (this.successRate >= 95 && this.averageTime <= 10) return 'excellent';
    if (this.successRate >= 90 && this.averageTime <= 30) return 'good';
    if (this.successRate >= 80 && this.averageTime <= 60) return 'average';
    return 'poor';
  }

  getFormattedTime(): string {
    if (this.averageTime < 60) {
      return `${this.averageTime.toFixed(1)}с`;
    } else {
      const minutes = Math.floor(this.averageTime / 60);
      const seconds = (this.averageTime % 60).toFixed(0);
      return `${minutes}хв ${seconds}с`;
    }
  }
}

// models/analytics-data.model.ts
export interface AnalyticsData {
  frequency: ChartData;
  errors: ChartData;
  successRate: ChartData;
  averageTime: ChartData;
}

export class AnalyticsDataModel implements AnalyticsData {
  constructor(
    public frequency: ChartData,
    public errors: ChartData,
    public successRate: ChartData,
    public averageTime: ChartData
  ) {}

  static createDefault(categories: CommandCategory[]): AnalyticsDataModel {
    const errors = ErrorDataModel.createDefaultErrors();
    const metrics = PerformanceMetricModel.createDefaultMetrics();

    return new AnalyticsDataModel(
      // Частота виконання команд за тижні
      new ChartDataModel(
        ['Тиждень 1', 'Тиждень 2', 'Тиждень 3', 'Тиждень 4'],
        [
          ChartDatasetModel.createFromCategory(categories[0], [45, 52, 48, 61]),
          ChartDatasetModel.createFromCategory(categories[1], [12, 8, 15, 10]),
          ChartDatasetModel.createFromCategory(categories[2], [78, 85, 71, 92]),
          ChartDatasetModel.createFromCategory(categories[3], [34, 41, 38, 45]),
          ChartDatasetModel.createFromCategory(categories[5], [28, 32, 25, 35])
        ]
      ),

      // Помилки
      new ChartDataModel(
        errors.map(e => e.type),
        [{
          label: 'Кількість помилок',
          backgroundColor: errors.map(e => e.getSeverityColor()),
          data: errors.map(e => e.count),
          borderColor: '#FFFFFF',
          borderWidth: 2
        }]
      ),

      // Відсоток успіху
      new ChartDataModel(
        categories.map(cat => cat.label),
        [{
          label: 'Відсоток успіху (%)',
          backgroundColor: categories.map(cat => cat.color),
          data: metrics.map(m => m.successRate),
          borderColor: '#FFFFFF',
          borderWidth: 2
        }]
      ),

      // Середній час виконання
      new ChartDataModel(
        categories.map(cat => cat.label),
        [{
          label: 'Час виконання (сек)',
          backgroundColor: '#2196F3',
          borderColor: '#1976D2',
          data: metrics.map(m => m.averageTime),
          borderWidth: 2,
          fill: false
        }]
      )
    );
  }

  getDataByType(type: keyof AnalyticsData): ChartData {
    return this[type];
  }

  updateFrequencyData(newData: number[][]): void {
    this.frequency.datasets.forEach((dataset, index) => {
      if (newData[index]) {
        dataset.data = [...newData[index]];
      }
    });
  }
}

// models/analytics-summary.model.ts
export interface AnalyticsSummary {
  totalExecutions: number;
  averageSuccessRate: number;
  mostFrequentCommand: string;
  totalErrors: number;
  averageExecutionTime: number;
  lastUpdated: Date;
}

export class AnalyticsSummaryModel implements AnalyticsSummary {
  constructor(
    public totalExecutions: number,
    public averageSuccessRate: number,
    public mostFrequentCommand: string,
    public totalErrors: number,
    public averageExecutionTime: number,
    public lastUpdated: Date
  ) {}

  static calculateFromMetrics(metrics: PerformanceMetricModel[]): AnalyticsSummaryModel {
    const totalExecutions = metrics.reduce((sum, m) => sum + m.totalExecutions, 0);
    const averageSuccessRate = metrics.reduce((sum, m) => sum + m.successRate, 0) / metrics.length;
    const mostFrequent = metrics.reduce((prev, current) =>
      prev.totalExecutions > current.totalExecutions ? prev : current
    );
    const errors = ErrorDataModel.createDefaultErrors();
    const totalErrors = errors.reduce((sum, e) => sum + e.count, 0);
    const averageTime = metrics.reduce((sum, m) => sum + m.averageTime, 0) / metrics.length;

    return new AnalyticsSummaryModel(
      totalExecutions,
      Math.round(averageSuccessRate * 10) / 10,
      mostFrequent.commandLabel,
      totalErrors,
      Math.round(averageTime * 10) / 10,
      new Date()
    );
  }

  getFormattedAverageTime(): string {
    if (this.averageExecutionTime < 60) {
      return `${this.averageExecutionTime}с`;
    } else {
      const minutes = Math.floor(this.averageExecutionTime / 60);
      const seconds = Math.round(this.averageExecutionTime % 60);
      return `${minutes}хв ${seconds}с`;
    }
  }
}

// enums/analytics-type.enum.ts
export enum AnalyticsType {
  FREQUENCY = 'frequency',
  ERRORS = 'errors',
  SUCCESS_RATE = 'successRate',
  AVERAGE_TIME = 'averageTime'
}

export interface AnalyticsTypeOption {
  label: string;
  value: AnalyticsType;
  description: string;
  defaultChartType: string;
}

export class AnalyticsTypeOptions {
  static getAll(): AnalyticsTypeOption[] {
    return [
      {
        label: 'Частота виконання команд',
        value: AnalyticsType.FREQUENCY,
        description: 'Показує як часто виконуються команди протягом часу',
        defaultChartType: 'bar'
      },
      {
        label: 'Найпоширеніші помилки',
        value: AnalyticsType.ERRORS,
        description: 'Аналіз типових помилок при виконанні команд',
        defaultChartType: 'doughnut'
      },
      {
        label: 'Відсоток успішних виконань',
        value: AnalyticsType.SUCCESS_RATE,
        description: 'Показує надійність кожної команди',
        defaultChartType: 'pie'
      },
      {
        label: 'Середній час виконання',
        value: AnalyticsType.AVERAGE_TIME,
        description: 'Допомагає виявити повільні команди',
        defaultChartType: 'bar'
      }
    ];
  }

  static getByValue(value: AnalyticsType): AnalyticsTypeOption | undefined {
    return this.getAll().find(option => option.value === value);
  }
}
