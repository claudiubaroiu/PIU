using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;
namespace NivelStocareDate1
{
    public class FisierText
    {
        static void Main(string[] args)
        {
        }
        private const int ID_PRIMUL_JUCATOR = 1;
        private const int INCREMENT = 1;

        private string numeFisier;

        public FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddJucator(Jucator jucator)
        {
            jucator.IdJucator = GetId();
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(jucator.ConversieLaSir_PentruFisier());
            }
        }

        public ArrayList GetJucatori()
        {
            ArrayList jucatori = new ArrayList();
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Jucator jucator = new Jucator(linieFisier);
                    jucatori.Add(jucator);
                }
            }

            return jucatori;
        }

        public Jucator GetJucator(string nume, string prenume)
        {
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Jucator jucator = new Jucator(linieFisier);
                    if (jucator.Nume.Equals(nume) && jucator.Prenume.Equals(prenume))
                        return jucator;
                }
            }

            return null;
        }

        private int GetId()
        {
            int IdJucator = ID_PRIMUL_JUCATOR;
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Jucator jucator = new Jucator(linieFisier);
                    IdJucator = jucator.IdJucator + INCREMENT;
                }
            }

            return IdJucator;
        }
    }
}
