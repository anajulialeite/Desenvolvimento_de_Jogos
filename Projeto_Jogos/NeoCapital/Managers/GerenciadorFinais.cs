using System;

namespace NeoCapitalRPG
{
    public class GerenciadorFinais
    {
        public bool VerificarFinais(Personagem jogador)
        {
            if (jogador.XP < 35 && jogador.CiclosCompletados >= 6)
            {
                FinalMorte();
                return true;
            }

            if (jogador.XP >= 50 && jogador.HP > 0)
            {
                FinalBom();
                return true;
            }

            if (jogador.HP < 48 && jogador.CiclosCompletados > 6)
            {
                FinalRuim();
                return true;
            }

            return false;
        }

        private void FinalBom()
        {
            
            Program.AudioGlobal.Parar();

            
            Program.AudioGlobal.TocarMusica("Assets/Musicas/FinalBom.mp3", loop: false);

            Console.Clear();
            UIHelper.ExibirArte("glitch");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("═══ O ROMPIMENTO DO CICLO ═══\n");
            Console.ResetColor();

            Console.WriteLine("Você retorna à viela, mas algo está diferente...");
            Console.WriteLine("O neon pisca de forma estranha, drones ficam parados no ar...");
            Console.WriteLine("O próprio céu apresenta glitches digitais!");

            Console.WriteLine("\nA realidade começa a se fragmentar ao seu redor...");

            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\"Você não é apenas um caçador de créditos, você é um sobrevivente.");
            Console.WriteLine("Está preso em uma simulação criada pelas megacorporações para");
            Console.WriteLine("silenciar aqueles que eles consideram uma 'pedra no sapato'.");
            Console.WriteLine("Você venceu o ciclo... mas não venceu o mundo.\"");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\"A prisão acabou. Agora começa a guerra.\"");
            Console.ResetColor();

            Console.WriteLine("\n═══ FIM - FINAL BOM ═══");
        }

        private void FinalRuim()
        {
            
            Program.AudioGlobal.Parar();

            
            Program.AudioGlobal.TocarMusica("Assets/Musicas/FinalRuim.mp3", loop: false);

            Console.Clear();
            UIHelper.ExibirArte("fantasma");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("═══ O ETERNO FANTASMA ═══\n");
            Console.ResetColor();

            Console.WriteLine("Cada repetição te enfraquece mais...");
            Console.WriteLine("Seus inimigos ficam mais rápidos, suas armas parecem quebrar");
            Console.WriteLine("antes de cada luta, e até sua memória começa a falhar.");

            Console.WriteLine("\nNo último retorno à viela, você sente que algo está diferente:");
            Console.WriteLine("não há drones, não há neon... apenas silêncio.");

            Console.WriteLine("\nSeu corpo está fragmentado em dados;");
            Console.WriteLine("suas mãos piscam como hologramas falhando.");

            Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\" VOCÊ FOI CONSUMIDO PELO CICLO.\"");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\"Alguns resolvem lutar, outros só aceitam,");
            Console.WriteLine("enquanto você... você acabou se tornando apenas mais um");
            Console.WriteLine("'eco digital' vagando na viela.\"");
            Console.ResetColor();

            Console.WriteLine("\n[Cartucho empty]");
            Console.WriteLine("\n═══ FIM - FINAL RUIM ═══");
        }

        private void FinalMorte()
        {
          
            Program.AudioGlobal.Parar();

            
            Program.AudioGlobal.TocarMusica("Assets/Musicas/Morte.mp3", loop: false);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("═══ GAME OVER ═══\n");
            Console.ResetColor();

            Console.WriteLine("Você sucumbiu nas ruas de Neo-Capital...");
            Console.WriteLine("Seu corpo se desintegra em pixels, voltando ao início do ciclo.");
            Console.WriteLine("Mas desta vez, você não desperta...");

            Console.WriteLine("\n═══ FIM ═══");
        }
    }
}
