using System;

namespace NeoCapitalRPG
{
    public class Jogo
    {
        private Personagem jogador;
        private bool jogoAtivo;

        
        private GerenciadorHistoria gerenciadorHistoria;
        private GerenciadorInventario gerenciadorInventario;
        private SistemaBatalha sistemaBatalha;
        private GerenciadorCenarios gerenciadorCenarios;
        private GerenciadorFinais gerenciadorFinais;
        private GerenciadorMenu gerenciadorMenu;

        public Jogo()
        {
            InicializarGerenciadores();
            jogoAtivo = true;
        }

        private void InicializarGerenciadores()
        {
            gerenciadorHistoria = new GerenciadorHistoria();
            gerenciadorInventario = new GerenciadorInventario();
            sistemaBatalha = new SistemaBatalha();
            gerenciadorCenarios = new GerenciadorCenarios(sistemaBatalha);
            gerenciadorFinais = new GerenciadorFinais();
            gerenciadorMenu = new GerenciadorMenu(gerenciadorCenarios, gerenciadorInventario);
        }

        public void IniciarJogo()
        {
            UIHelper.ExibirTituloJogo();

            jogador = gerenciadorHistoria.CriarPersonagem();

            Program.JogadorGlobal = jogador;
            Program.GerenciadorMenuGlobal = gerenciadorMenu;
            Program.GerenciadorInventarioGlobal = gerenciadorInventario;

            gerenciadorHistoria.ExibirIntroducao();

            ExecutarLoopPrincipal();
        }



        private void ExecutarLoopPrincipal()
        {
            while (jogoAtivo && jogador.HP > 0)
            {
                LoopCiclo();
            }
        }

        private void LoopCiclo()
        {
            jogador.CiclosCompletados++;

            gerenciadorHistoria.ExibirInicioCiclo(jogador.CiclosCompletados);

            // Escolher arma no primeiro ciclo
            if (jogador.ArmaEquipada == null)
            {
                gerenciadorInventario.EscolherArmaInicial(jogador);
            }

            jogador.ExibirStatus();

            // Verificar condições de final
            if (gerenciadorFinais.VerificarFinais(jogador))
            {
                jogoAtivo = false;
                return;
            }

            // Exibir menu e processar ações
            gerenciadorMenu.MenuViela(jogador);
        }
    }
}
