import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { NgForOf } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ItemDashboard } from './models/item-dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  itens: ItemDashboard[] = [
    {
      titulo: 'Médicos',
      descricao:
        'Organize o cadastro e as especialidades dos médicos de forma simples e prática.',
      rota: '/medicos',
      icone: 'groups',
      cypressTag: 'medicos',
    },
    {
      titulo: 'Atividades',
      descricao:
        'Gerencie atividades de maneira integrada, garantindo organização e eficiência.',
      rota: '/atividades',
      icone: 'monitor_heart',
      cypressTag: 'atividades',
    },
  ];
}
