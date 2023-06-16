using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{

    public class Club
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const int NUME = 0;
        private const int NUME2 = 0;
        private const int NR_JUCATORI = 1;
        private string nume;
        private string prenume;
        private int nr_jucator;

        public void citire()
        {
            Console.WriteLine("Introduceti numele jucatorului: ");
            nume = Console.ReadLine();
            Console.WriteLine("Introduceti prenumele jucatorului: ");
            prenume = Console.ReadLine();
            Console.WriteLine("Introduceti numarul jucatorului: ");
            nr_jucator = Convert.ToInt32(Console.ReadLine());

        }

        public string Nume { get; set; }
        public int Nr_jucator { get; set; }

        public string Prenume { get; set; }


        public Club(string nume, string prenume, int nrJucator)
        {
            this.nume = nume;
            this.nr_jucator = nrJucator;
            this.prenume = prenume;
        }
        public Club()
        {

        }


        public void Afisare(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(i.ToString() + Nume + Prenume);
            }

        }

        public string Info()
        {
            string info = string.Format("\nNumele este: {1}\nPrenumele este: {2} \nNumarul este: {0}\n",
                nr_jucator.ToString(),
                nume ?? "NECUNOSCUT",
                prenume ?? "NECUNOSCUT");
            return info;

        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectClubPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                nr_jucator.ToString(),
                (nume ?? " NECUNOSCUT "),
                (prenume ?? "NECUNOSCUT"));

            return obiectClubPentruFisier;
        }
        public Club(string linieFisier)
        {

            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);
            foreach (var data in dateFisier)

                Console.WriteLine(data);


        }
        public int GetNrJucator()
        {
            return nr_jucator;
        }

        public string GetNume()
        {
            return nume;
        }
        public string GetPrenume()
        {
            return prenume;
        }

    }
}