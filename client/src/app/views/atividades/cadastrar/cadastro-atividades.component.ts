import { NgForOf, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
  FormArray,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { ValidadorCustomizadoCRM } from '../../medicos/Validator/ValidadorCustomizado';
import {
  AtividadeInseridaViewModel,
  InserirAtividadeViewModel,
} from '../models/atividade.models';
import { AtividadeService } from '../services/atividades.service';
import { tipoAtividadeEnum } from '../models/tipoAtividadeEnum';
import { ListarMedicoViewModel } from '../../medicos/models/medico.models';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-cadastro-atividades',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatSelectModule,
  ],
  templateUrl: './cadastro-atividades.component.html',
})
export class CadastroAtividadesComponent implements OnInit {
  AtividadeForm: FormGroup;
  public medicos: ListarMedicoViewModel[] = [];

  public opcaoAtividade = Object.values(tipoAtividadeEnum).filter(
    (v) => !Number.isFinite(v)
  );

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private AtividadeService: AtividadeService,
    private notificacao: NotificacaoService
  ) {
    this.AtividadeForm = this.fb.group({
      titulo: ['',Validators.required],
      data: [new Date().toISOString().substring(0, 10), [Validators.required]],
      horaInicio: ['08:00', Validators.required],
      horaTermino: ['13:00', Validators.required],
      tipoAtividade: [0, [Validators.required]],
      medicosId: [[], Validators.required],
    });
  }

  get titulo(){
    return this.AtividadeForm.get('titulo');
  }
  get data() {
    return this.AtividadeForm.get('data');
  }
  get horaInicio() {
    return this.AtividadeForm.get('horaInicio');
  }
  get horaTermino() {
    return this.AtividadeForm.get('horaTermino');
  }
  get tipoAtividade() {
    return this.AtividadeForm.get('tipoAtividade');
  }
  get medicosSelecionados() {
    return this.AtividadeForm.get('medicosSelecionados') as FormArray;
  }

  ngOnInit(): void {
    this.medicos = this.route.snapshot.data['medicos'];
  }

  cadastrar() {
    if (this.AtividadeForm.invalid) {
      this.notificacao.erro('Preencha os campos corretamente.');
      return;
    }

    // Construir o objeto no formato esperado
    const novaAtividadeAux: InserirAtividadeViewModel = this.AtividadeForm.value;

    const novaAtividade: InserirAtividadeViewModel = {
      titulo: this.titulo?.value,
      inicio: this.obterDataHora('horaInicio'),
      termino: this.obterDataHora('horaTermino'),
      tipo: this.tipoAtividade?.value,
      medicosId: novaAtividadeAux.medicosId,
    };

    console.log('Atividade a ser cadastrada:', novaAtividade);

    // Enviar ao serviço
    this.AtividadeService.cadastrar(novaAtividade).subscribe({
      next: (atividadeInserida) => this.processarSucesso(atividadeInserida),
      error: (erro) => this.processarFalha(erro),
    });
  }

  private obterDataHora(horaAtividade: string): Date {
    const data = this.AtividadeForm.get('data')?.value;
    const hora = this.AtividadeForm.get(horaAtividade)?.value;

    const [horaParte, minutoParte] = hora.split(':').map(Number);
    const dataObj = new Date(data);
    dataObj.setHours(horaParte, minutoParte, 0, 0); // Define a hora corretamente
    return dataObj;
  }

  private processarSucesso(registro: AtividadeInseridaViewModel): void {
    this.notificacao.sucesso(
      `Atividade "${registro.id}" cadastrada com sucesso!`
    );

    this.router.navigate(['/atividades', 'listar']);
  }

  private processarFalha(erro: Error) {
    this.notificacao.erro(erro.message);
  }


}
