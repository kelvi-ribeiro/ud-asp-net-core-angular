import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { FormGroup, Validators, FormBuilder, } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from "ngx-bootstrap";
defineLocale('pt-br', ptBrLocale)

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  evento: Evento
  bodyDeletarEvento = ''
  metodoSalvar = 'postEvento'
  eventosFiltrados: Evento[]
  eventos: Evento[]
  imagemLargura = 50
  imagemMargem = 2
  mostrarImagem = false
  registerForm: FormGroup

  constructor(
    private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
  ) {
    this.localeService.use('pt-br')
  }

  _filtroLista: string
  get filtroLista() {
    return this._filtroLista
  }
  set filtroLista(value: string) {
    this._filtroLista = value
    this.eventosFiltrados = this.filtroLista ?
      this.filtrarEventos(this.filtroLista) : this.eventos
  }

  novoEvento(template: any) {
    this.metodoSalvar = 'postEvento'
    this.openModal(template)
  }

  editarEvento(evento: Evento, template: any) {
    this.metodoSalvar = 'putEvento'
    this.openModal(template)
    this.evento = evento
    this.registerForm.patchValue(evento)
  }

  openModal(template: any) {
    this.registerForm.reset()
    template.show()
  }

  ngOnInit() {
    this.validation()
    this.getEventos()
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      this.evento = Object.assign(this.evento.id ? { id: this.evento.id } : {}, this.registerForm.value)
      this.eventoService[this.metodoSalvar](this.evento).subscribe(
        () => {
          template.hide()
          this.getEventos()
        }, error => {
          console.log(error);
        }
      )
    }
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }
  
  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
    );
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50)
        ]
      ],

      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imagemURL: ['', Validators.required],
      qtdPessoas: ['',
        [
          Validators.required,
          Validators.max(120000),
        ]
      ],
      telefone: ['', Validators.required],
      email: ['',
        [
          Validators.required,
          Validators.email
        ]
      ],
    })
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase()
    return this.eventos.filter(evento => {
      return evento.tema.toLocaleLowerCase().includes(filtrarPor)
    })
  }

  getEventos() {
    this.eventoService.getAllEventos()
      .subscribe((_eventos: Evento[]) => {
        this.eventos = _eventos
      }, error => {
        console.log(error);
      })
  }
}
