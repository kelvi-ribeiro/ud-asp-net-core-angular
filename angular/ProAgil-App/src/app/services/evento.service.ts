import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'http://localhost:5000/api/eventos'

  constructor(private http:HttpClient) { }

  getEventos(){
    return this.http.get(this.baseURL)
  }

}
