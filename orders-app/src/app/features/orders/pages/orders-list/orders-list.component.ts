import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-orders-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    MatButtonModule
  ],
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.scss']
})
export class OrdersListComponent {
  displayedColumns = ['id', 'customer', 'amount', 'status', 'actions'];

  dataSource = [
    { id: 1, customer: 'Alice', amount: 120, status: 'Paid' },
    { id: 2, customer: 'Bob', amount: 75, status: 'Pending' },
    { id: 3, customer: 'Charlie', amount: 200, status: 'Shipped' }
  ];
}
