import { Routes } from '@angular/router';
import { DashboardComponent } from './views/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  {
    path: 'dashboard',
    loadComponent: () =>
      import('./views/dashboard/dashboard.component').then(
        (c) => DashboardComponent
      ),
  },
  {
    path: 'medicos',
    loadChildren: () =>
      import('./views/medicos/medicos.routes').then((m) => m.MedicosRoutes),
  },
  {
    path: 'atividades',
    loadChildren: () =>
      import('./views/atividades/atividades.routes').then(
        (m) => m.AtividadesRoutes
      ),
  },
];


