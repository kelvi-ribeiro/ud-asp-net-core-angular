import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  tokenHeader : HttpHeaders
  baseURL = 'http://localhost:5000/api/eventos'

  constructor(private http: HttpClient) { 
    this.tokenHeader = new HttpHeaders({ 'Authorization': `Bearer ${localStorage.getItem('token')}` })
  }

  getAllEventos(): Observable<Evento[]> {
    
    return this.http.get<Evento[]>(this.baseURL, { headers: this.tokenHeader })
  }
  getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`, { headers: this.tokenHeader })
  }
  getEventosById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`, { headers: this.tokenHeader })
  }
  postUpload(file: File) {
    const fileToUpload = <File>file[0]
    const formData = new FormData()
    formData.append('file', fileToUpload, fileToUpload.name)
    return this.http.post(`${this.baseURL}/upload`, formData, { headers: this.tokenHeader })
  }
  postEvento(evento: Evento) {
    return this.http.post(`${this.baseURL}`, evento, { headers: this.tokenHeader })
  }
  putEvento(evento: Evento) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento, { headers: this.tokenHeader })
  }
  deleteEvento(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`, { headers: this.tokenHeader })
  }
}
