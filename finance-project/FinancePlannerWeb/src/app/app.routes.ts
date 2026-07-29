import { Routes } from '@angular/router';
import { Home } from './features/home/home';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard';
import { authGuard } from './core/guards/auth-guard';
import { Accounts } from './features/accounts/accounts';
import { DashboardHome } from './features/dashboard/pages/dashboard-home/dashboard-home';

export const routes: Routes = [
  {
    path: '',
    component: Home
  },
  {
    path: 'register',
    component: Register
  },
 {
  path: '',
  component: Dashboard,
  canActivate: [authGuard],
  children: [
    {
      path: 'dashboard',
      component: DashboardHome
    },
    {
      path: 'accounts',
      component: Accounts
    }
  ]
}
];