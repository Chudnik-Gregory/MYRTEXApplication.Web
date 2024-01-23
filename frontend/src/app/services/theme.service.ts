import { Injectable } from '@angular/core';

export enum ThemeOption {
  Light = 'onLight',
  Dark = 'onDark',
}

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private theme = ThemeOption.Light;

  set currentTheme(option: ThemeOption) {
    this.theme = option;
    localStorage.setItem('theme', this.theme);
  }

  get currentTheme(): ThemeOption {
    const value = localStorage.getItem('theme');
    if (value) {
      return value as ThemeOption;
    } else {
      return this.theme;
    }
  }
}
