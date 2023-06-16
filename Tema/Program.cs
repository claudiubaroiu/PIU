using System;
using System.Collections.Generic;
using System.Configuration;
namespace ConsoleApp3
{
    class Program
    {
        
        static void Main(string[] args)
        {


            int nrJucatori = 0;
            Club jucatorNou = new Club();
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareCluburi_FisierText adminCluburi = new AdministrareCluburi_FisierText(numeFisier);

            string optiune;
            


           
            do
            {
                Console.WriteLine("I.Introducere informatii club");
                Console.WriteLine("A.Afisare");
                Console.WriteLine("S.Salvare in fisier");
                Console.WriteLine("F.Afisare din fisier");

                optiune = Console.ReadLine();
                switch (optiune.ToUpper())
                {
                    case "I":
                        Console.WriteLine("Introdu numele jucatorului:");
                        string nume = Console.ReadLine();
                        Console.WriteLine("Introdu prenumele jucatorului:");
                        string prenume = Console.ReadLine();
                        Console.WriteLine("Introdu numarul jucatorului:");
                        int Nr_jucator = Convert.ToInt32(Console.ReadLine());
                        jucatorNou = new Club(nume, prenume,Nr_jucator);
                        nrJucatori++;
                        break;
                    case "A":
                        string infoClub = jucatorNou.Info();
                        Console.WriteLine("{0}", infoClub);
                        break;
                    case "S":

                        nrJucatori++;
                        adminCluburi.AddClub(jucatorNou);

                        break;
                    case "F":
                        Club[] cluburi = adminCluburi.GetCluburi(out nrJucatori);
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida");
                        break;
                }
            } while (optiune.ToUpper() != "X");

        }
        public static void AfisareCluburi(Club[] cluburi, int nrCluburi)
        {
            Console.WriteLine("Cluburile sunt:");
            for (int contor = 0; contor < nrCluburi; contor++)
            {
                string infoClub = string.Format("Jucatorul are numele : {1}\n" +
                    "Prenumele:{2}\n Numarul: {3}", cluburi[contor].GetNume() ?? " NECUNOSCUT ",
                    cluburi[contor].GetPrenume() ?? " NECUNOSCUT ",
                   cluburi[contor].GetNrJucator()
                );


                Console.WriteLine(infoClub);
            }
        }

    }
}
