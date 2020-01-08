import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';

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
  eventosFiltrados: any = []
  eventos: any = []
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

  filtrarEventos(filtrarPor: string) {
    filtrarPor = filtrarPor.toLocaleLowerCase()
    return this.eventos.filter(evento => {
      return evento.tema.toLocaleLowerCase().includes(filtrarPor)
    })
  }

  getEventos() {
    this.eventoService.getEventos()
      .subscribe(response => {
        this.eventos = response
      }, error => {
        console.log(error);
      })
  }
}
