import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ListarMedicoViewModel } from '../models/medico.models';

@Component({
  selector: 'app-detalhes-medico',
  standalone: true,
  imports: [
    RouterLink,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
  ],
  templateUrl: './detalhes-medico.component.html',
})
export class DetalhesMedicoComponent implements OnInit {
  Medico?: ListarMedicoViewModel;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.Medico = this.route.snapshot.data['medico'];
  }
}
