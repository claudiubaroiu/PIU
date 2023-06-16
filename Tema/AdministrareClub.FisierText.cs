
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp3

{
    public class AdministrareCluburi_FisierText
    {
        private const int NR_MAX_CLUBURI = 50;
        private string numeFisier;

        public AdministrareCluburi_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // se incearca deschiderea fisierului in modul OpenOrCreate
            // astfel incat sa fie creat daca nu exista
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddClub(Club club)
        {
            // instructiunea 'using' va apela la final streamWriterFisierText.Close();
            // al doilea parametru setat la 'true' al constructorului StreamWriter indica
            // modul 'append' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(club.ConversieLaSir_PentruFisier());
            }
        }

        public Club[] GetCluburi(out int nrCluburi)
        {
            Club[] cluburi = new Club[NR_MAX_CLUBURI];

            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrCluburi = 0;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    cluburi[nrCluburi++] = new Club(linieFisier);
                }
            }

            return cluburi;
        }

        public ArrayList GetJucatori()
        {
            ArrayList jucatori = new ArrayList();

            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Club Jucatori = new Club(linieFisier);
                    jucatori.Add(Jucatori);
                }
            }

            return jucatori;
        }
    }
}