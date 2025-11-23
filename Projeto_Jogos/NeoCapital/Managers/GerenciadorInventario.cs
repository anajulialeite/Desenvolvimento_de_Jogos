using System;

namespace NeoCapitalRPG
{
    public class GerenciadorInventario
    {
        private Arma canoFerro;
        private Arma punhal;
        private Arma canoBlindsado;

        public GerenciadorInventario()
        {
            InicializarArmas();
        }

        private void InicializarArmas()
        {
            canoFerro = new Arma("Cano de Ferro", 5, "Uma arma improvisada, pesada mas eficaz");
            punhal = new Arma("Punhal", 3, "Rápido e silencioso, perfeito para ataques furtivos");
            canoBlindsado = new Arma("Cano Blindado", 10, "Melhoramento do cano de ferro com peças coletadas");
        }

        public void EscolherArmaInicial(Personagem jogador)
        {
            Console.WriteLine("\n═══ ESCOLHA SUA ARMA ═══");
            Console.WriteLine("Você encontra duas opções no chão da viela:");
            Console.WriteLine($"1 - {canoFerro.Nome} (+{canoFerro.Bonus} ATK) - {canoFerro.Descricao}");
            Console.WriteLine($"2 - {punhal.Nome} (+{punhal.Bonus} ATK) - {punhal.Descricao}");

            while (true)
            {
                Console.Write("\nSua escolha: ");
                string escolha = Console.ReadLine();

                if (escolha == "1")
                {
                    jogador.ArmaEquipada = canoFerro;
                    ExibirArmaEquipada(canoFerro);
                    break;
                }
                else if (escolha == "2")
                {
                    jogador.ArmaEquipada = punhal;
                    ExibirArmaEquipada(punhal);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Escolha inválida! Digite 1 ou 2.");
                    Console.ResetColor();
                }
            }
        }

        private void ExibirArmaEquipada(Arma arma)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nVocê equipou: {arma.Nome}");
            Console.ResetColor();
        }

        public void VerificarInventario(Personagem jogador)
        {
            Console.WriteLine("\n═══ INVENTÁRIO ═══");
            Console.WriteLine($"Arma equipada: {jogador.ArmaEquipada.Nome} (+{jogador.ArmaEquipada.Bonus} ATK)");
            Console.WriteLine($"Créditos: {jogador.Creditos}");
            Console.WriteLine($"Peças coletadas: {jogador.PecasColetadas}");
            Console.WriteLine($"XP: {jogador.XP}");
            Console.WriteLine($"Ciclos completados: {jogador.CiclosCompletados}");
        }

        public void MelhorarArma(Personagem jogador)
        {
            if (jogador.PecasColetadas >= 3 && jogador.ArmaEquipada != canoBlindsado)
            {
                ExibirOpcaoMelhoramento(jogador);
            }
            else
            {
                ExibirMensagemErroMelhoramento(jogador);
            }
        }

        private void ExibirOpcaoMelhoramento(Personagem jogador)
        {
            Console.WriteLine("\n═══ MELHORAMENTO DE ARMA ═══");
            Console.WriteLine("Você pode melhorar sua arma usando 3 peças!");
            Console.WriteLine($"Sua arma atual: {jogador.ArmaEquipada.Nome} (+{jogador.ArmaEquipada.Bonus} ATK)");
            Console.WriteLine($"Nova arma: {canoBlindsado.Nome} (+{canoBlindsado.Bonus} ATK)");

            Console.Write("Deseja melhorar? (s/n): ");
            string escolha = Console.ReadLine().ToLower();

            if (escolha == "s" || escolha == "sim")
            {
                RealizarMelhoramento(jogador);
            }
        }

        private void RealizarMelhoramento(Personagem jogador)
        {
            jogador.PecasColetadas -= 3;
            jogador.ArmaEquipada = canoBlindsado;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Arma melhorada com sucesso!");
            Console.ResetColor();
        }

        private void ExibirMensagemErroMelhoramento(Personagem jogador)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (jogador.ArmaEquipada == canoBlindsado)
            {
                Console.WriteLine("Sua arma já está no máximo!");
            }
            else
            {
                Console.WriteLine("Você precisa de 3 peças para melhorar sua arma!");
            }
            Console.ResetColor();
        }
    }
}
