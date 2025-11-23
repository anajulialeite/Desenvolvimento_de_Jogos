using System;
using System.Threading;

namespace NeoCapitalRPG
{
    public static class UIHelper
    {
        public static void ExibirTituloJogo()
        {
            
            AudioService audio = new AudioService();
            audio.TocarMusica("Assets/Musicas/opening.mp3", true);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(@"
    ███╗   ██╗███████╗ ██████╗       ██████╗ █████╗ ██████╗ ██╗████████╗ █████╗ ██╗     
    ████╗  ██║██╔════╝██╔═══██╗     ██╔════╝██╔══██╗██╔══██╗██║╚══██╔══╝██╔══██╗██║     
    ██╔██╗ ██║█████╗  ██║   ██║     ██║     ███████║██████╔╝██║   ██║   ███████║██║     
    ██║╚██╗██║██╔══╝  ██║   ██║     ██║     ██╔══██║██╔═══╝ ██║   ██║   ██╔══██║██║     
    ██║ ╚████║███████╗╚██████╔╝     ╚██████╗██║  ██║██║     ██║   ██║   ██║  ██║███████╗
    ╚═╝  ╚═══╝╚══════╝ ╚═════╝       ╚═════╝╚═╝  ╚═╝╚═╝     ╚═╝   ╚═╝   ╚═╝  ╚═╝╚══════╝
    ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("                            ═══ CAÇADOR DE CRÉDITOS ═══");
            Console.ResetColor();
            Console.WriteLine("\n                              Neo-Capital - Ano 2147");
            Console.WriteLine("\n                          Pressione ENTER para começar...");

            Console.ReadLine();

            
            audio.Parar();
        }

        public static void EscreverTextoAnimado(string texto, int delay = 30)
        {
            foreach (char c in texto)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
        public static bool EscreverTextoAnimadoSkippavel(string texto, int delay = 30)
        {
            foreach (char c in texto)
            {
                Console.Write(c);

              
                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(true).Key;

                    if (tecla == ConsoleKey.P)
                    {
                        Console.Write(texto.Substring(texto.IndexOf(c)));
                        Console.WriteLine();
                        return true; 
                    }
                }

                Thread.Sleep(delay);
            }

            Console.WriteLine();
            return false;
        }


        public static void ExibirArte(string tipo)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

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
    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
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

        public static void AguardarContinuacao()
        {
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        public static void LimparTela()
        {
            Console.Clear();
        }
    }
}
