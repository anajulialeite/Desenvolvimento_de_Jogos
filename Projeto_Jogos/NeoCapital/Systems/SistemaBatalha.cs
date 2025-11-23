using System;
using System.Threading;

namespace NeoCapitalRPG
{
    public class SistemaBatalha
    {
        private Random random;
        private AudioService audio = new AudioService(); 

        public SistemaBatalha()
        {
            random = new Random();
        }

        public bool IniciarBatalha(Personagem jogador, Inimigo inimigo)
        {
            
            audio.TocarMusica("Assets/Musicas/Batalha.mp3", true);


            Console.WriteLine($"\n═══ BATALHA INICIADA ═══");
            Console.WriteLine($"Você enfrenta: {inimigo.Nome}");
            Console.WriteLine($"HP do Inimigo: {inimigo.HP}");

            while (inimigo.HP > 0 && jogador.HP > 0)
            {
                ExibirStatusBatalha(jogador, inimigo);

                bool defendendo = TurnoJogador(jogador, inimigo);

                if (inimigo.HP <= 0)
                {
                    audio.Parar(); 
                    VitoriaJogador(jogador, inimigo);
                    return true;
                }

                TurnoInimigo(jogador, inimigo, defendendo);

                if (jogador.HP <= 0)
                {
                    audio.Parar(); 
                    return false;
                }

                Thread.Sleep(1000);
            }

            audio.Parar(); 
            return true;
        }

        private void ExibirStatusBatalha(Personagem jogador, Inimigo inimigo)
        {
            Console.WriteLine($"\nSeu HP: {jogador.HP}/{jogador.HPMaximo} | Inimigo HP: {inimigo.HP}");
            Console.WriteLine("1 - Atacar");
            Console.WriteLine("2 - Defender (reduz dano recebido pela metade)");
        }

        private bool TurnoJogador(Personagem jogador, Inimigo inimigo)
        {
            Console.Write("Sua ação: ");
            string acao = Console.ReadLine();

            if (acao == "1")
            {
                return ExecutarAtaque(jogador, inimigo);
            }
            else if (acao == "2")
            {
                return ExecutarDefesa();
            }

            return false;
        }

        private bool ExecutarAtaque(Personagem jogador, Inimigo inimigo)
        {
            int dano = random.Next(jogador.AtaqueTotal() - 2, jogador.AtaqueTotal() + 3);
            inimigo.HP -= dano;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Você causa {dano} de dano!");
            Console.ResetColor();

            return false;
        }

        private bool ExecutarDefesa()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Você se prepara para defender!");
            Console.ResetColor();
            return true;
        }

        private void TurnoInimigo(Personagem jogador, Inimigo inimigo, bool jogadorDefendendo)
        {
            int danoInimigo = random.Next(inimigo.Ataque - 1, inimigo.Ataque + 2);

            if (jogadorDefendendo)
            {
                danoInimigo /= 2;
            }

            jogador.ReceberDano(danoInimigo);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{inimigo.Nome} causa {danoInimigo} de dano em você!");
            Console.ResetColor();
        }

        private void VitoriaJogador(Personagem jogador, Inimigo inimigo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{inimigo.Nome} foi derrotado!");
            Console.ResetColor();
        }
    }
}
