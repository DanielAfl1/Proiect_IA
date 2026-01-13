using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proiect_IA
{
    public class MotorInferenta
    {
        private List<Regula> bazaDeCunostinte = new List<Regula>();
        public HashSet<string> memoriaDeLucru = new HashSet<string>();

        public void AdaugaFapt(string fapt)
        {
            memoriaDeLucru.Add(fapt);
        }

        public void ResetareMemorie()
        {
            memoriaDeLucru.Clear();
            foreach (Regula r in bazaDeCunostinte)
            {
                r.Activata = false;
            }
        }

        public void IncarcaReguli(string scenariu)
        {
            bazaDeCunostinte.Clear();

            if (scenariu.Contains("Geometrie"))
            {
                bazaDeCunostinte.Add(new Regula(new string[] { "Latura(AB)=Latura(AC)" }, "Triunghi_Isoscel"));
                bazaDeCunostinte.Add(new Regula(new string[] { "Latura(AB)=Latura(BC)" }, "Triunghi_Isoscel"));
                bazaDeCunostinte.Add(new Regula(new string[] { "Latura(AC)=Latura(BC)" }, "Triunghi_Isoscel"));

                string[] premiseEchi = { "Latura(AB)=Latura(AC)", "Latura(AC)=Latura(BC)" };
                bazaDeCunostinte.Add(new Regula(premiseEchi, "Triunghi_Echilateral"));
            }
            else if (scenariu.Contains("Aritmetica"))
            {
                bazaDeCunostinte.Add(new Regula(new string[] { "UltimaCifra(0)" }, "Divizibil(5)"));
                bazaDeCunostinte.Add(new Regula(new string[] { "UltimaCifra(0)" }, "Par"));
                bazaDeCunostinte.Add(new Regula(new string[] { "Par" }, "Divizibil(2)"));
                bazaDeCunostinte.Add(new Regula(new string[] { "SumaCifre(Div3)" }, "Divizibil(3)"));

                string[] p6 = { "Divizibil(2)", "Divizibil(3)" };
                bazaDeCunostinte.Add(new Regula(p6, "Divizibil(6)"));

                string[] p30 = { "Divizibil(6)", "Divizibil(5)" };
                bazaDeCunostinte.Add(new Regula(p30, "Divizibil(30)"));
            }
        }

        // Returneaza un string cu toti pasii demonstratiei pentru a fi afisat in Form
        public string ExecutaInferenta(string faptTinta)
        {
            StringBuilder log = new StringBuilder();
            bool schimbare = true;
            int pas = 1;

            while (schimbare)
            {
                schimbare = false;
                foreach (Regula r in bazaDeCunostinte)
                {
                    if (!r.Activata)
                    {
                        bool toateOk = true;
                        foreach (string p in r.Premise)
                        {
                            if (!memoriaDeLucru.Contains(p)) { toateOk = false; break; }
                        }

                        if (toateOk)
                        {
                            memoriaDeLucru.Add(r.Concluzie);
                            r.Activata = true;
                            schimbare = true;

                            log.AppendLine($" Pas {pas}: {string.Join(" & ", r.Premise)} -> {r.Concluzie}");
                            pas++;
                        }
                    }
                }
            }

            if (memoriaDeLucru.Contains(faptTinta))
                log.AppendLine($"REZULTAT: {faptTinta} confirmat.");
            else
                log.AppendLine("REZULTAT: Nu s-a putut demonstra.");

            return log.ToString();
        }
    }
}