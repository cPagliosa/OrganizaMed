import { ActivatedRouteSnapshot, ResolveFn, Route, Routes } from "@angular/router";
import { ListagemMedicosComponent } from "./listar/listagem-medicos.component";
import { VisualizarMedicoViewModel } from "./models/medico.models";
import { MedicoService } from "./services/medico.service";
import { CadastroMedicoComponent } from "./cadastrar/cadastro-medico.component";
import { DetalhesMedicoComponent } from "./visualizar/detalhes-medico.component";
import { inject } from "@angular/core";

const visualizarMedicosResolver: ResolveFn<VisualizarMedicoViewModel> = (
  route: ActivatedRouteSnapshot
) => {
  const id = route.params['id'];
  return inject(MedicoService).selecionarPorId(id);
};

export const MedicosRoutes: Routes = [
  { path: '', redirectTo: 'listar', pathMatch: 'full' },
  { path: 'listar', component: ListagemMedicosComponent },
  { path: 'cadastrar', component: CadastroMedicoComponent },
  {
    path: 'visualizar/:id',
    component: DetalhesMedicoComponent,
    resolve: {
      medico: visualizarMedicosResolver,
    },
  },
];
