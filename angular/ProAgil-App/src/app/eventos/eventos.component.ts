import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, FormControl, } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventosFiltrados: Evento[]
  eventos: Evento[]
  imagemLargura = 50
  imagemMargem = 2
  mostrarImagem = false
  modalRef: BsModalRef
  registerForm:FormGroup

  constructor(
      private eventoService: EventoService,
      private modalService:BsModalService
      ) { }

  _filtroLista: string
  get filtroLista() {
    return this._filtroLista
  }
  set filtroLista(value: string) {
    this._filtroLista = value
    this.eventosFiltrados = this.filtroLista ?
      this.filtrarEventos(this.filtroLista) : this.eventos
  }


  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template)
  }

  ngOnInit() {
    this.getEventos()
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem
  }

  salvarAlteracao(){
    
  }

  validation(){
    this.registerForm = new FormGroup({
      tema: new FormControl,
      local: new FormControl,
      dataEvento: new FormControl,
      qtdPessoas: new FormControl,
      telefone: new FormControl,
      email: new FormControl,      
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
