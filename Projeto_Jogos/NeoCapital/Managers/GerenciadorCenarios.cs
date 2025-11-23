using System;

namespace NeoCapitalRPG
{
    public class GerenciadorCenarios
    {
        private SistemaBatalha sistemaBatalha;

        public GerenciadorCenarios(SistemaBatalha sistemaBatalha)
        {
            this.sistemaBatalha = sistemaBatalha;
        }

        public void IrParaFerroVelho(Personagem jogador)
        {
            Console.Clear();
            UIHelper.ExibirArte("ferro_velho");

            Console.WriteLine("═══ FERRO-VELHO ═══");
            Console.WriteLine("Você entra em um ferro-velho repleto de sucata tecnológica...");
            Console.WriteLine("Drones policiais antigos patrulham o local, mas parecem defeituosos.");

            Inimigo drone = new Inimigo("Drone Policial Antigo", 30, 8, 5, 10);

            if (sistemaBatalha.IniciarBatalha(jogador, drone))
            {
                RecompensaFerroVelho(jogador, drone);
            }

            Console.WriteLine("\nVocê retorna para a viela...");
            UIHelper.AguardarContinuacao();
        }

        private void RecompensaFerroVelho(Personagem jogador, Inimigo drone)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVocê encontra peças úteis entre os destroços!");
            jogador.PecasColetadas++;
            jogador.GanharXP(drone.XPRecompensa);
            jogador.Creditos += drone.CreditosRecompensa;
            Console.WriteLine($"Peças coletadas: +1 | XP: +{drone.XPRecompensa} | Créditos: +{drone.CreditosRecompensa}");
            Console.ResetColor();
        }

        public void IrParaMercadoAbandonado(Personagem jogador)
        {
            Console.Clear();
            UIHelper.ExibirArte("mercado");

            Console.WriteLine("═══ MERCADO ABANDONADO ═══");
            Console.WriteLine("O mercado está tomado pela gangue Chrome Shadows...");
            Console.WriteLine("Cyberpunks com implantes brilhantes te cercam!");

            Inimigo gangster = new Inimigo("Membro Chrome Shadows", 45, 12, 8, 25);

            if (sistemaBatalha.IniciarBatalha(jogador, gangster))
            {
                RecompensaMercado(jogador, gangster);
            }

            Console.WriteLine("\nVocê retorna para a viela...");
            UIHelper.AguardarContinuacao();
        }

        private void RecompensaMercado(Personagem jogador, Inimigo gangster)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVocê saqueia os créditos do gangster!");
            jogador.GanharXP(gangster.XPRecompensa);
            jogador.Creditos += gangster.CreditosRecompensa;
            Console.WriteLine($"XP: +{gangster.XPRecompensa} | Créditos: +{gangster.CreditosRecompensa}");
            Console.ResetColor();
        }
    }
}
