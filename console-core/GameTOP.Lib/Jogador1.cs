
using GameTOP.Interface;

namespace GameTOP.Lib
{
    public class Jogador1 : iJogador
  {
    public Jogador1(string nome)
    {
      _Nome = nome;
    }
    public readonly string _Nome;

    public string Chuta()
    {
      return $"{_Nome} está chutando \n";
    }

    public string Corre()
    {
      return $"{_Nome} está correndo \n";
    }

    public string Passa()
    {
      return $"{_Nome} está passando \n";
    }
  }
}
