import { ApiService } from './../../services/api.service';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Inject,
  Injector,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';

import { TuiTableModule } from '@taiga-ui/addon-table';
import { Employee } from './domain/employees.interfaces';
import {
  TuiButtonModule,
  TuiDialogService,
  TuiSvgModule,
} from '@taiga-ui/core';
import { TuiGroupModule } from '@taiga-ui/core';
import { TuiPromptModule, TUI_PROMPT } from '@taiga-ui/kit';
import { TuiDestroyService } from '@taiga-ui/cdk';
import {
  catchError,
  concatMap,
  filter,
  iif,
  mergeMap,
  of,
  switchMap,
  takeUntil,
} from 'rxjs';
import { PolymorpheusComponent } from '@tinkoff/ng-polymorpheus';
import { EmployeeDetailsModalComponent } from './components/employee-details-modal/employee-details-modal.component';

@Component({
  selector: 'chudnik-employees',
  standalone: true,
  imports: [
    CommonModule,
    TuiTableModule,
    TuiButtonModule,
    TuiSvgModule,
    TuiGroupModule,
    TuiPromptModule,
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [TuiDestroyService],
})
export class EmployeesComponent implements OnInit {
  constructor(
    @Inject(TuiDialogService) private readonly dialogs: TuiDialogService,
    @Inject(Injector) private readonly injector: Injector,
    private readonly destroy: TuiDestroyService,
    private api: ApiService,
    private cdr: ChangeDetectorRef
  ) {}

  employees: Employee[] = [];

  readonly columns = [
    'Отдел',
    'Ф.И.О.',
    'Дата рождения',
    'Дата устройства на работу',
    'Заработная плата',
    'Удаление/редактирование сотрудника',
  ];

  ngOnInit(): void {
    this.api
      .getEmployees()
      .pipe(takeUntil(this.destroy))
      .subscribe((res) => {
        this.employees = res;
        this.cdr.markForCheck();
      });
  }

  deleteEmployee(id: string): void {
    this.dialogs
      .open<boolean>(TUI_PROMPT, {
        label: 'Подтверждение',
        data: {
          content: 'Вы уверены, что хотите удалить этого сотрудника?',
          yes: 'Да',
          no: 'Нет',
        },
      })
      .pipe(
        filter((response: boolean) => !!response),
        switchMap(() => this.api.deleteEmployee(id)),
        catchError(() => of(null)),
        concatMap(() => this.api.getEmployees()),
        takeUntil(this.destroy)
      )
      .subscribe((res: Employee[]) => (this.employees = res));
  }

  openModal(mode: 'ADD' | 'EDIT', employee?: Employee) {
    this.dialogs
      .open(
        new PolymorpheusComponent(EmployeeDetailsModalComponent, this.injector),
        {
          data: mode === 'EDIT' ? employee : null,
          dismissible: true,
          label: 'Детали сотрудника',
        }
      )
      .pipe(
        mergeMap((result) =>
          iif(
            () => mode === 'ADD',
            this.api.addEmployee(result!),
            this.api.updateEmployee(result!)
          )
        ),
        catchError(() => of(null)),
        concatMap(() => this.api.getEmployees()),
        takeUntil(this.destroy)
      )
      .subscribe((res: Employee[]) => (this.employees = res));
    }
  }
