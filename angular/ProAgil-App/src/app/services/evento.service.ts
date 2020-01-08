import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'http://localhost:5000/api/eventos'

  constructor(private http: HttpClient) { }

  getAllEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL)
  }
  getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`)
  }
  getEventosById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`)
  }
  postEvento(evento: Evento) {
    return this.http.post(`${this.baseURL}`, evento)
  }
}
