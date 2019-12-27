using System;

namespace GameTOP
{
  class Program
  {
    static void Main(string[] args)
    {
      var jogo = new JogoFODA(new Jogador("Ronaldo"));
      jogo.IniciarJogo();
    }
  }
  public class Jogador
  {
    public Jogador(string nome)
    {
      _Nome = nome;
    }
    public readonly string _Nome;

    public void Chuta()
    {
      Console.Write($"{_Nome} está chutando");
    }

    public void Corre()
    {
      Console.Write($"{_Nome} está correndo");
    }

    public void Passa()
    {
      Console.Write($"{_Nome} está passando");
    }
  }
}
