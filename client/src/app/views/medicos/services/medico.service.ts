import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { catchError, EMPTY, map, Observable, of, tap, throwError } from 'rxjs';

import {
  InserirMedicoViewModel,
  MedicoInseridoViewModel,
  ListarMedicoViewModel,
  VisualizarMedicoViewModel,
} from '../models/medico.models';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MedicoService {
  private readonly url = `${environment.apiUrl}/Medico`;

  constructor(private http: HttpClient) {}

  cadastrar(
    novoMedico: InserirMedicoViewModel
  ): Observable<MedicoInseridoViewModel> {
    const urlCompleto = `${this.url}`;
    return this.http.post<MedicoInseridoViewModel>(urlCompleto, novoMedico).pipe(
      map((x) => this.processarRes(x) as MedicoInseridoViewModel),
      tap((x) => console.log(x))
    );
  }

  selecionarTodos(): Observable<ListarMedicoViewModel[]> {
    const urlCompleto = `${this.url}`;
    console.log(urlCompleto)
    return this.http
      .get<ListarMedicoViewModel[]>(urlCompleto)
      .pipe(map((x) => this.processarRes(x) as ListarMedicoViewModel[]));
  }


  selecionarPorId(id: string): Observable<VisualizarMedicoViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http
      .get<VisualizarMedicoViewModel>(urlCompleto)
      .pipe(map((x) => this.processarRes(x) as VisualizarMedicoViewModel));
  }

  private processarRes(res: any): any {
    console.log(res);
    console.log(res.dados);
    return res.dados;
  }
}
