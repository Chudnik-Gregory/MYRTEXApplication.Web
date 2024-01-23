import { Route } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';

export const appRoutes: Route[] = [
  { path: 'about', component: AboutComponent },
  {
    path: 'employees',
    loadChildren: () =>
      import('./pages/employees/employees.routes').then(
        (mod) => mod.employeesRoutes
      ),
  },
  { path: '', redirectTo: '/about', pathMatch: 'full' },
  { path: '**', redirectTo: '/about' },
];
