import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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

  constructor(private http: HttpClient) { }

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
    this.http.get('http://localhost:5000/api/eventos')
      .subscribe(response => {
        this.eventos = response
      }, error => {
        console.log(error);
      })
  }
}
