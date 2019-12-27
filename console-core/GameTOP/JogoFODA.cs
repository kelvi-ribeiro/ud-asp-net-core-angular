namespace GameTOP
{
  public class JogoFODA
  {
    private readonly iJogador _jogador;

    public JogoFODA(iJogador jogador)
    {
      _jogador = jogador;
    }
    public void IniciarJogo()
    {
      _jogador.Corre();
      _jogador.Chuta();
      _jogador.Passa();
    }
  }
}

