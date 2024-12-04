import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { catchError, map, Observable, tap, throwError } from 'rxjs';

import { environment } from '../../../../environments/environment';
import {
  InserirAtividadeViewModel,
  AtividadeInseridaViewModel,
  ListarAtividadeViewModel,
  VisualizarAtividadeViewModel,
} from '../models/atividade.models';

@Injectable({
  providedIn: 'root',
})
export class AtividadeService {
  private readonly url = `${environment.apiUrl}/Atividade`;

  constructor(private http: HttpClient) {}

  cadastrar(
    novoAtividade: InserirAtividadeViewModel
  ): Observable<AtividadeInseridaViewModel> {
    console.log(novoAtividade)
    const urlCompleto = `${this.url}`;
    return this.http
      .post<AtividadeInseridaViewModel>(urlCompleto, novoAtividade)
      .pipe(
        map((x) => this.processarRes(x) as AtividadeInseridaViewModel),
        catchError(this.processarFalha)
      );
  }


  selecionarTodos(): Observable<ListarAtividadeViewModel[]> {
    const urlCompleto = `${this.url}`;
    return this.http.get<ListarAtividadeViewModel[]>(urlCompleto).pipe(
      map((x) => this.processarRes(x) as ListarAtividadeViewModel[]),
      catchError(this.processarFalha)
    );
  }

  selecionarPorId(id: string): Observable<VisualizarAtividadeViewModel> {
    const urlCompleto = `${this.url}/${id}`;
    return this.http.get<VisualizarAtividadeViewModel>(urlCompleto).pipe(
      map((x) => this.processarRes(x) as VisualizarAtividadeViewModel),
      catchError(this.processarFalha)
    );
  }

  private processarRes(res: any): any {
    return res.dados;
  }
  private processarFalha(resposta: any) {
    return throwError(() => new Error(resposta.error.erros[0]));
  }
}
