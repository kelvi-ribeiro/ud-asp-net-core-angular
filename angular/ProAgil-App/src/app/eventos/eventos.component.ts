import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any = [
    {
      EventoId: 1,
      Tema:'Angular',
      Local:'Rio de Janeiro'
    },
    {
      EventoId: 2,
      Tema:'React',
      Local:'São Paulo'
    },
    {
      EventoId: 3,
      Tema:'C#',
      Local:'Minas Gerais'
    },
  ]

  constructor() { }

  ngOnInit() {
  }

}
