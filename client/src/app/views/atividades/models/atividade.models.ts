import {
  ListarMedicoViewModel,
  VisualizarMedicoViewModel,
} from '../../medicos/models/medico.models';
import { tipoAtividadeEnum } from './tipoAtividadeEnum';

export interface InserirAtividadeViewModel {
  titulo : string;
  inicio: Date;
  termino: Date;
  tipo: tipoAtividadeEnum;
  medicosId: number[];
}


export interface AtividadeInseridaViewModel {
  id: string;
  titulo : string;
  inicio: Date;
  termino: Date;
  tipoAtividade: tipoAtividadeEnum;
  medicosId: number[];
}


export interface VisualizarAtividadeViewModel {
  id: string;
  titulo : string;
  inicio: Date;
  termino: Date;
  tipoAtividade: tipoAtividadeEnum;
  medicos: ListarMedicoViewModel[];
}

export interface ListarAtividadeViewModel {
  id: string;
  titulo : string;
  inicio: Date;
  termino: Date;
  tipoAtividade: tipoAtividadeEnum;
  medicos: ListarMedicoViewModel[];
}

