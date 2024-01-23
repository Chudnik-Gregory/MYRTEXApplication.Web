import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  TuiInputDateModule,
  TuiInputModule,
  TuiInputNumberModule,
} from '@taiga-ui/kit';
import {
  TuiDialogService,
  TuiDialogContext,
  TuiButtonModule,
} from '@taiga-ui/core';
import { POLYMORPHEUS_CONTEXT } from '@tinkoff/ng-polymorpheus';
import { Employee } from '../../domain/employees.interfaces';
import { TuiDay } from '@taiga-ui/cdk';

@Component({
  selector: 'chudnik-employee-details-modal',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TuiInputModule,
    TuiButtonModule,
    TuiInputDateModule,
    TuiInputNumberModule,
  ],
  templateUrl: './employee-details-modal.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmployeeDetailsModalComponent {
  form = new FormGroup({
    fio: new FormControl<string>(
      this.context.data?.fio ?? '',
      Validators.required
    ),
    department: new FormControl<string>(
      this.context.data?.department ?? '',
      Validators.required
    ),
    birthDate: new FormControl<TuiDay>(
      this.context.data?.birthDate
        ? new TuiDay(
            this.context.data?.birthDate?.getFullYear(),
            this.context.data?.birthDate?.getMonth(),
            this.context.data?.birthDate?.getDay()
          )
        : TuiDay.currentLocal(),
      Validators.required
    ),
    employmentDate: new FormControl<TuiDay>(
      this.context.data?.employmentDate
        ? new TuiDay(
            this.context.data?.employmentDate?.getFullYear(),
            this.context.data?.employmentDate?.getMonth(),
            this.context.data?.employmentDate?.getDay()
          )
        : TuiDay.currentLocal(),
      Validators.required
    ),
    salary: new FormControl<number>(
      this.context.data?.salary ?? 0,
      Validators.required
    ),
  });

  constructor(
    @Inject(TuiDialogService) private readonly dialogs: TuiDialogService,
    @Inject(POLYMORPHEUS_CONTEXT)
    private readonly context: TuiDialogContext<Partial<Employee>, Employee>
  ) {}

  saveEmployee(): void {
    this.context.completeWith(this.form.value as Partial<Employee>);
  }
}
