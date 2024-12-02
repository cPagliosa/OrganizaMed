export interface InserirMedicoViewModel {
  nome: string;
  crm: string;
  email: string;
  telefone: string;
}

export interface MedicoInseridoViewModel {
  id: string;
  nome: string;
  crm: string;
  email: string;
  telefone: string;
}

export interface EdicaoMedicoViewModel {
  nome: string;
  crm: string;
  email: string;
  telefone: string;
}

export interface MedicoEditadoViewModel {
  id: string;
  nome: string;
  crm: string;
  email: string;
  telefone: string;
}

export interface VisualizarMedicoViewModel {
  id: string;
  nome: string;
  crm: string;
  email: string;
  telefone: string;
  cooldown: string
}

export interface ListarMedicoViewModel {
  id: string;
  nome: string;
  crm: string;
  email: string;
  telefone: string;
  cooldown: string
}

export interface MedicoExcluidoViewModel {}
