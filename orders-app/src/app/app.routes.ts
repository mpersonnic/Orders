import { Routes } from '@angular/router';
import { MainLayoutComponent } from './core/layout/main-layout/main-layout.component';
import { OrdersListComponent } from './features/orders/pages/orders-list/orders-list.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'orders', component: OrdersListComponent },
      { path: '', redirectTo: 'orders', pathMatch: 'full' }
    ]
  }
];
