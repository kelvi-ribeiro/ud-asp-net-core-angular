import { Evento } from './Evento';

export interface RedeSocial {
  id: number
  nome: string
  URL: string
  eventoId?: number  
  palestranteId?: number
}
