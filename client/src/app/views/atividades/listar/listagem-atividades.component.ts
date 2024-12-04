import { Component, OnInit } from '@angular/core';
import { ListarAtividadeViewModel } from '../models/atividade.models';
import { AtividadeService } from '../services/atividades.service';
import { Observable, of } from 'rxjs';
import { NgIf, NgForOf, AsyncPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { tipoAtividadeEnum } from '../models/tipoAtividadeEnum';

@Component({
  selector: 'app-listagem-atividades',
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
  templateUrl: './listagem-atividades.component.html',
  styleUrl: './listagem-atividades.component.scss',
})
export class ListagemAtividadesComponent implements OnInit {
  atividades$?: Observable<any>;
  atividadesEmCache: ListarAtividadeViewModel[];

  constructor(private atividadeService: AtividadeService) {
    this.atividadesEmCache = [];
  }

  ngOnInit(): void {
    this.atividadeService.selecionarTodos().subscribe((atividades) => {
      this.atividadesEmCache = atividades;
      this.atividades$ = of(atividades);
    });
  }

  ordenarAtividades(tipo: number) {
    if(tipo == 1){
     return  `Cunsulta`;
    }else{
      return `Cirurgia`;
    }
  }

  public obterTextoTipoAtividade(prioridade: tipoAtividadeEnum): string {
    return tipoAtividadeEnum[Number(prioridade)];
  }

  public formatarData(data: Date): string | null {
    var date = new Date(data);
    var [ano, mes, dia] = date.toISOString().split('T')[0].split('-');
    return `${dia}/${mes}/${ano}`;
  }

  public formatarHora(data: Date): string | null {
    var date = new Date(data);
    var [ho, mi] = date.toISOString().split('T')[1].split(':');
    return `${ho}:${mi}`;
  }

  private obterAtividadesFiltradas(
    atividades: ListarAtividadeViewModel[],
    tipo: number
  ) {
    if (tipo < 2) {
      return atividades.filter((n) => n.tipoAtividade == tipo);
    }
    return this.atividadesEmCache;
  }
}
