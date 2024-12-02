import { NgIf, NgForOf, AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { map, Observable, of, tap } from 'rxjs';
import { ListarMedicoViewModel } from '../models/medico.models';
import { MedicoService } from '../services/medico.service';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-listagem-medicos',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    RouterLink,
    AsyncPipe,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule,
  ],
  templateUrl: './listagem-medicos.component.html',
  styleUrl: './listagem-medicos.component.scss',
})
export class ListagemMedicosComponent implements OnInit {
  medicos$?: Observable<any>;
  ordenado: boolean;

  medicosEmCache: ListarMedicoViewModel[];
  medicosOrdenados: ListarMedicoViewModel[];

  constructor(private medicoService: MedicoService) {
    this.medicosEmCache = [];
    this.medicosOrdenados = [];
    this.ordenado = false;
  }

  ngOnInit(): void {
    this.medicoService.selecionarTodos().subscribe((medicos) => {
      this.medicosEmCache = medicos;
      this.medicos$ = of(medicos);
    });
    this.medicos$ = of(this.medicosEmCache);
  }

  ordenarMedicos() {
    if (!this.ordenado) {
      this.medicos$ = of(this.medicosOrdenados);
      this.ordenado = true;
      return;
    }
    this.ordenado = false;

  }
}
