import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import {
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterLink, Router } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { InserirMedicoViewModel } from '../models/medico.models';
import { MedicoService } from '../services/medico.service';
import { ValidadorCustomizadoCRM } from '../Validator/ValidadorCustomizado';

@Component({
  selector: 'app-cadastro-medico',
  standalone: true,
  imports: [
    NgIf,
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './cadastro-medico.component.html',
})
export class CadastroMedicoComponent {
  MedicoForm: FormGroup;

  constructor(
    private router: Router,
    private MedicoService: MedicoService,
    private notificacao: NotificacaoService
  ) {
    this.MedicoForm = new FormGroup({
      nome: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(3),
      ]),
      crm: new FormControl<string>('', [
        Validators.required,
        ValidadorCustomizadoCRM(/^\d{5}-[A-Z]{2}$/),
      ]),
      email: new FormControl<string>('', [
        Validators.required,
        Validators.email,
      ]),
      telefone: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(11),
      ]),
    });
  }

  get nome() {
    return this.MedicoForm.get('nome');
  }
  get crm() {
    return this.MedicoForm.get('crm');
  }
  get email() {
    return this.MedicoForm.get('email');
  }
  get telefone() {
    return this.MedicoForm.get('telefone');
  }

  cadastrar() {
    console.log("ola")
    if (this.MedicoForm.invalid) return;

    const novoMedico: InserirMedicoViewModel = this.MedicoForm.value;

    this.MedicoService.cadastrar(novoMedico).subscribe((res) => {
      this.notificacao.sucesso(
        `O registro foi cadastrado com sucesso!`
      );

      this.router.navigate(['/medicos']);
    });
  }
}
