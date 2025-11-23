using NeoCapitalRPG;

class Program
{
    public static AudioService AudioGlobal = new AudioService();

    public static GerenciadorMenu GerenciadorMenuGlobal;
    public static Personagem JogadorGlobal;
    public static GerenciadorInventario GerenciadorInventarioGlobal;

    static void Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Neo-Capital: Caçador de Créditos";

            Jogo jogo = new Jogo();
            jogo.IniciarJogo();

            Console.WriteLine("\nObrigado por jogar Neo-Capital: Caçador de Créditos!");
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro no jogo: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
