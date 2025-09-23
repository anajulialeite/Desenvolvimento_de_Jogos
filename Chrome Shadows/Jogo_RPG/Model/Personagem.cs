﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Personagem
    {
        public string Nome { get; set; }
        public string Genero { get; set; }
        public int HP { get; set; }
        public int HPMaximo { get; set; }
        public int Ataque { get; set; }
        public int XP { get; set; }
        public Arma ArmaEquipada { get; set; }
        public int Creditos { get; set; }
        public int PecasColetadas { get; set; }
        public int CiclosCompletados { get; set; }

        public Personagem(string nome, string genero)
        {
            Nome = nome;
            Genero = genero;
            HP = 100;
            HPMaximo = 100;
            Ataque = 15;
            XP = 0;
            Creditos = 0;
            PecasColetadas = 0;
            CiclosCompletados = 0;
        }

        public void CurarHP(int quantidade)
        {
            HP = Math.Min(HP + quantidade, HPMaximo);
        }

        public void ReceberDano(int dano)
        {
            HP = Math.Max(0, HP - dano);
        }

        public void GanharXP(int xp)
        {
            XP += xp;
            if (XP >= 20 && (XP - xp) < 20)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[SISTEMA] {Nome} evoluiu! HP e Ataque aumentaram!");
                Console.ResetColor();
                HPMaximo += 10;
                HP = HPMaximo;
                Ataque += 3;
            }
        }

        public int AtaqueTotal()
        {
            return Ataque + (ArmaEquipada?.Bonus ?? 0);
        }
    }

}

