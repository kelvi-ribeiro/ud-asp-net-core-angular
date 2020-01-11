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
  postUpload(file: File) {
    const fileToUpload = <File>file[0]
    const formData = new FormData()
    formData.append('file', fileToUpload, fileToUpload.name)
    return this.http.post(`${this.baseURL}/upload`, formData)
  }
  postEvento(evento: Evento) {
    return this.http.post(`${this.baseURL}`, evento)
  }
  putEvento(evento: Evento) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento)
  }
  deleteEvento(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`)
  }
}
