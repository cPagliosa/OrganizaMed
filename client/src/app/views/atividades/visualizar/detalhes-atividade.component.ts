import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { VisualizarAtividadeViewModel } from '../models/atividade.models';
import { tipoAtividadeEnum } from '../models/tipoAtividadeEnum';
import { AtividadeService } from '../services/atividades.service';
import { NgForOf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-detalhes-atividade',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
  ],
  templateUrl: './detalhes-atividade.component.html',
})
export class DetalhesAtividadeComponent implements OnInit {
  Atividade?: VisualizarAtividadeViewModel;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.Atividade = this.route.snapshot.data['atividade'];

    console.log(this.Atividade);
  }

  public obterTextoTipoAtividade(tipo: tipoAtividadeEnum | undefined): string {
    return tipoAtividadeEnum[Number(tipo)];
  }
  public formatarData(data: Date | undefined): string | null {
    var date = new Date(data!);
    var [ano, mes, dia] = date.toISOString().split('T')[0].split('-');
    return `${dia}/${mes}/${ano}`;
  }
  public formatarHora(data: Date | undefined): string | null {
    var date = new Date(data!);
    var [hora, minuto] = date
      .toISOString()
      .split('T')[1]
      .split('.')[0]
      .split(':');
    return `${hora}:${minuto}`;
  }
}
