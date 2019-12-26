using System;

namespace GameTOP
{
  class Program
  {
    static void Main(string[] args)
    {
      var jogo = new JogoFODA("Cristiano Ronaldo");
      jogo.IniciarJogo();
    }
  }
  class JogoFODA
  {
    private readonly string _NomeJogador;

    public JogoFODA(string nome)
    {
      _NomeJogador = nome;
    }
    public void IniciarJogo()
    {
      Console.Write($"{_NomeJogador} deu um passe");
    }
  }
}
