using ConsoleApp1;
using System;
using System.Threading;

namespace NeoCapitalRPG
{
    public class Jogo
    {
        private Personagem jogador;
        private Random random = new Random();
        private bool jogoAtivo = true;

        // Armas disponíveis
        private Arma canoFerro = new Arma("Cano de Ferro", 5, "Uma arma improvisada, pesada mas eficaz");
        private Arma punhal = new Arma("Punhal", 3, "Rápido e silencioso, perfeito para ataques furtivos");
        private Arma canoBlindsado = new Arma("Cano Blindado", 10, "Melhoramento do cano de ferro com peças coletadas");

        public void IniciarJogo()
        {
            ExibirTituloJogo();
            CriarPersonagem();
            ExibirIntroducao();

            while (jogoAtivo && jogador.HP > 0)
            {
                LoopPrincipal();
            }
        }

        private void ExibirTituloJogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
    ███╗   ██╗███████╗ ██████╗       ██████╗ █████╗ ██████╗ ██╗████████╗ █████╗ ██╗     
    ████╗  ██║██╔════╝██╔═══██╗     ██╔════╝██╔══██╗██╔══██╗██║╚══██╔══╝██╔══██╗██║     
    ██╔██╗ ██║█████╗  ██║   ██║     ██║     ███████║██████╔╝██║   ██║   ███████║██║     
    ██║╚██╗██║██╔══╝  ██║   ██║     ██║     ██╔══██║██╔═══╝ ██║   ██║   ██╔══██║██║     
    ██║ ╚████║███████╗╚██████╔╝     ╚██████╗██║  ██║██║     ██║   ██║   ██║  ██║███████╗
    ╚═╝  ╚═══╝╚══════╝ ╚═════╝       ╚═════╝╚═╝  ╚═╝╚═╝     ╚═╝   ╚═╝   ╚═╝  ╚═╝╚══════╝
    ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                            ═══ CAÇADOR DE CRÉDITOS ═══");
            Console.ResetColor();
            Console.WriteLine("\n                              Neo-Capital - Ano 2147");
            Console.WriteLine("\n                          Pressione ENTER para começar...");
            Console.ReadLine();
        }

        private void CriarPersonagem()
        {
            Console.Clear();
            ExibirArte("personagem");

            Console.WriteLine("═══ CRIAÇÃO DE PERSONAGEM ═══\n");

            Console.Write("Digite o nome do seu caçador de créditos: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome)) nome = "Caçador";

            Console.WriteLine("\nEscolha o gênero:");
            Console.WriteLine("1 - Masculino");
            Console.WriteLine("2 - Feminino");

            string genero = "";
            while (true)
            {
                Console.Write("\nSua escolha: ");
                string escolha = Console.ReadLine();

                if (escolha == "1")
                {
                    genero = "Masculino";
                    break;
                }
                else if (escolha == "2")
                {
                    genero = "Feminino";
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Escolha inválida! Digite 1 ou 2.");
                    Console.ResetColor();
                }
            }

            jogador = new Personagem(nome, genero);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nome} foi criado com sucesso!");
            Console.WriteLine($"Gênero: {genero}");
            Console.WriteLine($"HP: {jogador.HP} | Ataque: {jogador.Ataque}");
            Console.ResetColor();

            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private void ExibirIntroducao()
        {
            Console.Clear();
            ExibirArte("cidade");

            Console.WriteLine("═══ PRÓLOGO ═══\n");

            string[] introducao = {
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

            foreach (string linha in introducao)
            {
                if (string.IsNullOrEmpty(linha))
                {
                    Console.WriteLine();
                }
                else
                {
                    EscreverTextoAnimado(linha);
                }
            }

            Console.WriteLine("\n\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private void LoopPrincipal()
        {
            // Início de um novo ciclo
            jogador.CiclosCompletados++;

            Console.Clear();
            ExibirArte("viela");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"═══ CICLO #{jogador.CiclosCompletados} ═══");
            Console.ResetColor();

            Console.WriteLine("\nVocê desperta na mesma viela suja...");
            Console.WriteLine("Os néons piscam da mesma forma. Os mesmos sons ecoam pelas ruas.");

            // Se é o primeiro ciclo, escolher arma
            if (jogador.ArmaEquipada == null)
            {
                EscolherArmaInicial();
            }

            ExibirStatus();

            // Verificar condições de final
            if (VerificarFinais()) return;

            // Menu principal da viela
            MenuViela();
        }

        private void EscolherArmaInicial()
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nVocê equipou: {canoFerro.Nome}");
                    Console.ResetColor();
                    break;
                }
                else if (escolha == "2")
                {
                    jogador.ArmaEquipada = punhal;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nVocê equipou: {punhal.Nome}");
                    Console.ResetColor();
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

        private void MenuViela()
        {
            while (true)
            {
                Console.WriteLine("\n═══ VIELA INICIAL ═══");
                Console.WriteLine("Para onde você quer ir?");
                Console.WriteLine("1 - Ferro-Velho (Batalhar contra drones antigos)");
                Console.WriteLine("2 - Mercado Abandonado (Enfrentar gangue Chrome Shadows)");
                Console.WriteLine("3 - Verificar inventário");
                Console.WriteLine("4 - Melhorar arma (requer peças)");

                Console.Write("\nSua escolha: ");
                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        IrParaFerroVelho();
                        return;
                    case "2":
                        IrParaMercadoAbandonado();
                        return;
                    case "3":
                        VerificarInventario();
                        break;
                    case "4":
                        MelhorarArma();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opção inválida!");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void IrParaFerroVelho()
        {
            Console.Clear();
            ExibirArte("ferro_velho");

            Console.WriteLine("═══ FERRO-VELHO ═══");
            Console.WriteLine("Você entra em um ferro-velho repleto de sucata tecnológica...");
            Console.WriteLine("Drones policiais antigos patrulham o local, mas parecem defeituosos.");

            // Criar inimigo
            Inimigo drone = new Inimigo("Drone Policial Antigo", 30, 8, 5, 10);

            if (IniciarBatalha(drone))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVocê encontra peças úteis entre os destroços!");
                jogador.PecasColetadas++;
                jogador.GanharXP(drone.XPRecompensa);
                jogador.Creditos += drone.CreditosRecompensa;
                Console.WriteLine($"Peças coletadas: +1 | XP: +{drone.XPRecompensa} | Créditos: +{drone.CreditosRecompensa}");
                Console.ResetColor();
            }

            Console.WriteLine("\nVocê retorna para a viela...");
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }

        private void IrParaMercadoAbandonado()
        {
            Console.Clear();
            ExibirArte("mercado");

            Console.WriteLine("═══ MERCADO ABANDONADO ═══");
            Console.WriteLine("O mercado está tomado pela gangue Chrome Shadows...");
            Console.WriteLine("Cyberpunks com implantes brilhantes te cercam!");

            // Criar inimigo mais forte
            Inimigo gangster = new Inimigo("Membro Chrome Shadows", 45, 12, 8, 25);

            if (IniciarBatalha(gangster))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVocê saqueia os créditos do gangster!");
                jogador.GanharXP(gangster.XPRecompensa);
                jogador.Creditos += gangster.CreditosRecompensa;
                Console.WriteLine($"XP: +{gangster.XPRecompensa} | Créditos: +{gangster.CreditosRecompensa}");
                Console.ResetColor();
            }

            Console.WriteLine("\nVocê retorna para a viela...");
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }

        private bool IniciarBatalha(Inimigo inimigo)
        {
            Console.WriteLine($"\n═══ BATALHA INICIADA ═══");
            Console.WriteLine($"Você enfrenta: {inimigo.Nome}");
            Console.WriteLine($"HP do Inimigo: {inimigo.HP}");

            while (inimigo.HP > 0 && jogador.HP > 0)
            {
                // Turno do jogador
                Console.WriteLine($"\nSeu HP: {jogador.HP}/{jogador.HPMaximo} | Inimigo HP: {inimigo.HP}");
                Console.WriteLine("1 - Atacar");
                Console.WriteLine("2 - Defender (reduz dano recebido pela metade)");

                Console.Write("Sua ação: ");
                string acao = Console.ReadLine();

                bool defendendo = false;

                if (acao == "1")
                {
                    // Atacar
                    int dano = random.Next(jogador.AtaqueTotal() - 2, jogador.AtaqueTotal() + 3);
                    inimigo.HP -= dano;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Você causa {dano} de dano!");
                    Console.ResetColor();

                    if (inimigo.HP <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{inimigo.Nome} foi derrotado!");
                        Console.ResetColor();
                        return true;
                    }
                }
                else if (acao == "2")
                {
                    defendendo = true;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Você se prepara para defender!");
                    Console.ResetColor();
                }

                // Turno do inimigo
                int danoInimigo = random.Next(inimigo.Ataque - 1, inimigo.Ataque + 2);
                if (defendendo) danoInimigo /= 2;

                jogador.ReceberDano(danoInimigo);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{inimigo.Nome} causa {danoInimigo} de dano em você!");
                Console.ResetColor();

                if (jogador.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Você foi derrotado...");
                    Console.ResetColor();
                    return false;
                }

                Thread.Sleep(1000);
            }

            return true;
        }

        private void VerificarInventario()
        {
            Console.WriteLine("\n═══ INVENTÁRIO ═══");
            Console.WriteLine($"Arma equipada: {jogador.ArmaEquipada.Nome} (+{jogador.ArmaEquipada.Bonus} ATK)");
            Console.WriteLine($"Créditos: {jogador.Creditos}");
            Console.WriteLine($"Peças coletadas: {jogador.PecasColetadas}");
            Console.WriteLine($"XP: {jogador.XP}");
            Console.WriteLine($"Ciclos completados: {jogador.CiclosCompletados}");
        }

        private void MelhorarArma()
        {
            if (jogador.PecasColetadas >= 3 && jogador.ArmaEquipada != canoBlindsado)
            {
                Console.WriteLine("\n═══ MELHORAMENTO DE ARMA ═══");
                Console.WriteLine("Você pode melhorar sua arma usando 3 peças!");
                Console.WriteLine($"Sua arma atual: {jogador.ArmaEquipada.Nome} (+{jogador.ArmaEquipada.Bonus} ATK)");
                Console.WriteLine($"Nova arma: {canoBlindsado.Nome} (+{canoBlindsado.Bonus} ATK)");

                Console.Write("Deseja melhorar? (s/n): ");
                string escolha = Console.ReadLine().ToLower();

                if (escolha == "s" || escolha == "sim")
                {
                    jogador.PecasColetadas -= 3;
                    jogador.ArmaEquipada = canoBlindsado;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Arma melhorada com sucesso!");
                    Console.ResetColor();
                }
            }
            else
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

        private bool VerificarFinais()
        {
            // Final Bom - Romper o ciclo
            if (jogador.XP >= 100 && jogador.HP > 0)
            {
                FinalBom();
                return true;
            }

            // Final Ruim - Ser consumido pelo ciclo
            if (jogador.XP < 20 && jogador.CiclosCompletados >= 10)
            {
                FinalRuim();
                return true;
            }

            // Morte
            if (jogador.HP <= 0)
            {
                FinalMorte();
                return true;
            }

            return false;
        }

        private void FinalBom()
        {
            Console.Clear();
            ExibirArte("glitch");

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
            jogoAtivo = false;
        }

        private void FinalRuim()
        {
            Console.Clear();
            ExibirArte("fantasma");

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
            jogoAtivo = false;
        }

        private void FinalMorte()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("═══ GAME OVER ═══\n");
            Console.ResetColor();

            Console.WriteLine("Você sucumbiu nas ruas de Neo-Capital...");
            Console.WriteLine("Seu corpo se desintegra em pixels, voltando ao início do ciclo.");
            Console.WriteLine("Mas desta vez, você não desperta...");

            Console.WriteLine("\n═══ FIM ═══");
            jogoAtivo = false;
        }

        private void ExibirStatus()
        {
            Console.WriteLine($"\n[STATUS] {jogador.Nome} | HP: {jogador.HP}/{jogador.HPMaximo} | ATK: {jogador.AtaqueTotal()} | XP: {jogador.XP}");
            Console.WriteLine($"[RECURSOS] Créditos: {jogador.Creditos} | Peças: {jogador.PecasColetadas} | Ciclo: {jogador.CiclosCompletados}");
        }

        private void EscreverTextoAnimado(string texto)
        {
            foreach (char c in texto)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
        }

        private void ExibirArte(string tipo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            switch (tipo)
            {
                case "personagem":
                    Console.WriteLine(@"
       ╔══════════════╗
       ║   ◐    ◑     ║  
       ║      ▼       ║  <- Caçador de Créditos
       ╠══════════════╣
       ║  ████████    ║
       ║  ████████    ║  
       ╚══════════════╝
                    ");
                    break;

                case "cidade":
                    Console.WriteLine(@"
    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
    ▓    NEO-CAPITAL - 2147     ▓
    ▓  ╔═══╗ ╔═══╗ ╔═══╗      ▓
    ▓  ║▓▓▓║ ║▓▓▓║ ║▓▓▓║      ▓
    ▓  ║▓▓▓║ ║▓▓▓║ ║▓▓▓║      ▓
    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                    ");
                    break;

                case "viela":
                    Console.WriteLine(@"
    ░░░░░░░░ VIELA INICIAL ░░░░░░░░
    ░                             ░
    ░  ╔════════════════════════╗ ░
    ░  ║     ◊ NEON SIGNS ◊    ║ ░
    ░  ╚════════════════════════╝ ░
    ░  [Você está aqui]          ░
    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                    ");
                    break;

                case "ferro_velho":
                    Console.WriteLine(@"
    ████ FERRO-VELHO ████
    ████ ▼ ▼ ▼ ▼ ▼  ████
    ████   SUCATA    ████
    ████ ◊ DRONES ◊  ████
    ██████████████████████
                    ");
                    break;

                case "mercado":
                    Console.WriteLine(@"
    ♦♦♦ MERCADO ABANDONADO ♦♦♦
    ♦   CHROME SHADOWS GANG   ♦
    ♦  ╔══════════════════╗   ♦
    ♦  ║ ◊ CYBERPUNKS ◊  ║   ♦
    ♦  ╚══════════════════╝   ♦
    ♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦
                    ");
                    break;

                case "glitch":
                    Console.WriteLine(@"
    ╔══G═L═I═T═C═H══╗
    ║▓▓░░▓▓░░▓▓░░▓▓║
    ║░░▓▓░░▓▓░░▓▓░░║
    ║▓▓░░ ERROR ░░▓▓║
    ║░░▓▓░░▓▓░░▓▓░░║
    ╚═══════════════╝
                    ");
                    break;

                case "fantasma":
                    Console.WriteLine(@"
    ░░░░ FANTASMA DIGITAL ░░░░
    ░     ◊ ◊ ◊ ◊ ◊     ░
    ░   ╔═══════════════╗   ░
    ░   ║  ░▓░▓░▓░▓░▓░  ║   ░
    ░   ║  ▓░▓░▓░▓░▓░▓  ║   ░
    ░   ╚═══════════════╝   ░
    ░░░░░░░░░░░░░░░░░░░░░░░░░
                    ");
                    break;
            }

            Console.ResetColor();
        }
    }
}