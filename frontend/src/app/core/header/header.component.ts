import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { TuiButtonModule, TuiSvgModule } from '@taiga-ui/core';
import { TuiTabsModule } from '@taiga-ui/kit';
import { ReactiveFormsModule } from '@angular/forms';
import { NavItem } from './domain/header.interfaces';
import { ThemeOption, ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'chudnik-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    TuiTabsModule,
    TuiSvgModule,
    TuiButtonModule,
    RouterModule,
    ReactiveFormsModule,
  ],
})
export class HeaderComponent {
  public readonly theme = inject(ThemeService);
  ThemeOption = ThemeOption;
  activeItemIndex: number = 0;
  readonly NAV_ITEMS: NavItem[] = [
    { text: 'О компании', icon: 'tuiIconInfo', routerLink: '/about' },
    { text: 'Сотрудники', icon: 'tuiIconUsers', routerLink: '/employees' },
  ];

  trackByNavItems(idx: number, _: NavItem): number {
    return idx;
  }

  changeTheme(): void {
    if (this.theme.currentTheme === ThemeOption.Dark) {
      this.theme.currentTheme = ThemeOption.Light;
    } else {
      this.theme.currentTheme = ThemeOption.Dark;
    }
  }
}
