using System;

namespace Proiect_IA
{
    public class Regula
    {
        public string[] Premise { get; set; }
        public string Concluzie { get; set; }
        public bool Activata { get; set; }

        public Regula(string[] p, string c)
        {
            this.Premise = p;
            this.Concluzie = c;
            this.Activata = false;
        }
    }
}