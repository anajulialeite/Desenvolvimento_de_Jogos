using NeoCapitalRPG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoCapitalRPG.Model
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
