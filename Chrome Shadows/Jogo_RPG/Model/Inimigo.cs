using NeoCapitalRPG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoCapitalRPG.Model
{
    public class Inimigo
    {
        public string Nome { get; set; }
        public int HP { get; set; }
        public int Ataque { get; set; }
        public int XPRecompensa { get; set; }
        public int CreditosRecompensa { get; set; }

        public Inimigo(string nome, int hp, int ataque, int xp, int creditos)
        {
            Nome = nome;
            HP = hp;
            Ataque = ataque;
            XPRecompensa = xp;
            CreditosRecompensa = creditos;
        }
    }
}
