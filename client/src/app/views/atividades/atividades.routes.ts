import { ActivatedRouteSnapshot, ResolveFn, Routes } from '@angular/router';
import { ListagemAtividadesComponent } from './listar/listagem-atividades.component';
import { MedicoService } from '../medicos/services/medico.service';
import { inject } from '@angular/core';
import { ListarMedicoViewModel } from '../medicos/models/medico.models';
import { VisualizarAtividadeViewModel } from './models/atividade.models';
import { AtividadeService } from './services/atividades.service';
import { DetalhesAtividadeComponent } from './visualizar/detalhes-atividade.component';

const visualizarAtividadesResolver: ResolveFn<VisualizarAtividadeViewModel> = (
  route: ActivatedRouteSnapshot
) => {
  const id = route.params['id'];
  return inject(AtividadeService).selecionarPorId(id);
};

const ListagemMedicosResolver: ResolveFn<ListarMedicoViewModel[]> = () => {
  return inject(MedicoService).selecionarTodos();
};

export const AtividadesRoutes: Routes = [
  { path: '', redirectTo: 'listar', pathMatch: 'full' },
  { path: 'listar', component: ListagemAtividadesComponent },
  {
    path: 'visualizar/:id',
    component: DetalhesAtividadeComponent,
    resolve: { atividade: visualizarAtividadesResolver },
  },
];
