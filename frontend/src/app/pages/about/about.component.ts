import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'chudnik-about',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="tui-container">
      <p class="tui-text_body-xl">Тестовый проект Григория Чудника)</p>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AboutComponent {}
