import { Component } from '@angular/core';
import { Card } from '../../../../shared/components/card/card';


@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [
    Card
  ],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.css'
})
export class DashboardHome {}