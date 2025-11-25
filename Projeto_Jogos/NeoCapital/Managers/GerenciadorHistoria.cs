using System;
using NeoCapitalRPG;
using NAudio.Wave;

namespace NeoCapitalRPG
{
    public class GerenciadorHistoria
    {
        public Personagem CriarPersonagem()
        {
            Console.Clear();
            UIHelper.ExibirArte("personagem");

            Console.WriteLine("═══ CRIAÇÃO DE PERSONAGEM ═══\n");

            string nome = SolicitarNome();
            string genero = SolicitarGenero();

            Personagem jogador = new Personagem(nome, genero);

            ExibirPersonagemCriado(jogador);

            UIHelper.AguardarContinuacao();

            return jogador;
        }

        private string SolicitarNome()
        {
            Console.Write("Digite o nome do seu caçador de créditos: ");
            string nome = Console.ReadLine();
            return string.IsNullOrWhiteSpace(nome) ? "Caçador" : nome;
        }

        private string SolicitarGenero()
        {
            Console.WriteLine("\nEscolha o gênero:");
            Console.WriteLine("1 - Masculino");
            Console.WriteLine("2 - Feminino");

            while (true)
            {
                Console.Write("\nSua escolha: ");
                string escolha = Console.ReadLine();

                if (escolha == "1") return "Masculino";
                if (escolha == "2") return "Feminino";

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Escolha inválida! Digite 1 ou 2.");
                Console.ResetColor();
            }
        }

        private void ExibirPersonagemCriado(Personagem jogador)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{jogador.Nome} foi criado com sucesso!");
            Console.WriteLine($"Gênero: {jogador.Genero}");
            Console.WriteLine($"HP: {jogador.HP} | Ataque: {jogador.Ataque}");
            Console.ResetColor();
        }

        public void ExibirIntroducao()
        {
            Console.Clear();
            UIHelper.ExibirArte("cidade");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Pressione [P] a qualquer momento para pular o prólogo.\n");
            Console.ResetColor();

            Program.AudioGlobal.TocarMusica("Assets/Musicas/Keyboard.mp3", true);

            Console.WriteLine("═══ PRÓLOGO ═══\n");

            string[] introducao =
            {
        "O ano é 2147. A cidade Neo-Capital, construída sobre a antiga São Paulo,",
        "está mais populosa do que nunca. Isso a faz entrar em um ciclo inquebrável de violência.",
        "",
        "Megacorporações, controladas por donos sem rosto, ditam tudo:",
        "a comida, as informações… até o ar que você respira é monitorado.",
        "",
        "Não existem mais becos escuros, pois em cada esquina há um letreiro de neon",
        "tentando chamar sua atenção. Também não existem mais locais seguros:",
        "gangues cibernéticas extremamente violentas disputam o pouco de liberdade restante",
        "contra drones e droids mercenários da polícia.",
        "",
        "É aí que você entra: um(a) caçador(a) de créditos que, após mais uma briga",
        "genérica de bar, se vê preso em um dilema incomum.",
        "",
        "O problema? O mesmo dia se repete. Sempre.",
        "",
        "Toda vez que o sol 'nasce', você desperta na mesma viela suja,",
        "com as mesmas oportunidades de luta.",
        "",
        "Talvez, se ficar forte o bastante, consiga abrir uma brecha e quebrar o ciclo..."
    };

            bool skipado = false;

            foreach (string linha in introducao)
            {
                if (string.IsNullOrEmpty(linha))
                {
                    Console.WriteLine();
                    continue;
                }


                if (UIHelper.EscreverTextoAnimadoSkippavel(linha))
                {
                    skipado = true;
                    break;
                }
            }

            // Parar música do prólogo
            Program.AudioGlobal.Parar();

            if (skipado)
            {
                Console.WriteLine("\nPrólogo pulado!");
                Thread.Sleep(600);
            }

            // garante que o jogador escolha arma inicil mesmo pulando o prólogo
            if (Program.JogadorGlobal.ArmaEquipada == null)
            {
                Console.Clear();
                Console.WriteLine("Antes de começar, escolha sua arma inicial:");
                Program.GerenciadorInventarioGlobal.EscolherArmaInicial(Program.JogadorGlobal);
            }

            // Agora sim pode ir para o menu da viela
            Program.GerenciadorMenuGlobal.MenuViela(Program.JogadorGlobal);
        }

        public void ExibirInicioCiclo(int numeroCiclo)
        {
            Console.Clear();
            UIHelper.ExibirArte("viela");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"═══ CICLO #{numeroCiclo} ═══");
            Console.ResetColor();

            Console.WriteLine("\nVocê desperta na mesma viela suja...");
            Console.WriteLine("Os néons piscam da mesma forma. Os mesmos sons ecoam pelas ruas.");
        }
    }
}
