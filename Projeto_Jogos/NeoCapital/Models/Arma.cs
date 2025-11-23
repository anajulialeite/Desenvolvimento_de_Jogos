namespace NeoCapitalRPG
{
    public class Arma
    {
        public string Nome { get; set; }
        public int Bonus { get; set; }
        public string Descricao { get; set; }

        public Arma(string nome, int bonus, string descricao)
        {
            Nome = nome;
            Bonus = bonus;
            Descricao = descricao;
        }
    }
}
