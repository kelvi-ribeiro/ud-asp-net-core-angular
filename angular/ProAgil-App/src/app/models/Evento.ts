import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { Palestrante } from './Palestrante';

export interface Evento {
  id: number
  local: string
  imagemURL: string
  dataEvento: Date
  tema: string
  qtdPessoas: number
  telefone: string
  email: string
  lote: string
  lotes: Lote[]
  redeSociais: RedeSocial[]
  palestrantesEventos: Palestrante[]
}
