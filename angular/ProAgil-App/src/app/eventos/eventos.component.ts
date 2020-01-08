import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  _filtroLista: string
  get filtroLista() {
    return this._filtroLista
  }
  set filtroLista(value: string) {
    this._filtroLista = value
    this.eventosFiltrados = this.filtroLista ?
      this.filtrarEventos(this.filtroLista) : this.eventos
  }
  eventosFiltrados: Evento[]
  eventos: Evento[]
  imagemLargura = 50
  imagemMargem = 2
  mostrarImagem = false

  constructor(private eventoService: EventoService) { }

  ngOnInit() {
    this.getEventos()
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem
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
