using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele.Enumerari;

namespace LibrarieModele
{
    public class Jucator
    {

        //constante
        static void Main(string[] args)
        {
            
        }
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const char SEPARATOR_SECUNDAR_FISIER = ' ';
        private const bool SUCCES = true;
        public const int NUMAR_MINIM = 1;
        public const int NUMAR_MAXIM = 99;

        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int CULOARE_KIT = 3;
        private const int POZITIE = 4;
        private const int NUMAR = 5;

        
        int[] numere;

        
        public int IdJucator { get; set; } 
        public string Nume { get; set; }
        public string Prenume { get; set; }

        public string  Poz { get; set; }

        public Class1 Culoare_kit { get; set; }
        public ArrayList Pozitie { get; set; }

        public string PozitieAsString
        {
            get
            {
                return string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), Pozitie.ToArray());
            }
        }

        public int[] GetNumar()
        {
        
            return (int[])numere.Clone();
        }

      
        public Jucator()
        {
            Nume = Prenume = string.Empty;
        }

       
        public Jucator(int idJucator, string nume, string prenume,string pozitie)
        {
            this.IdJucator = idJucator;
            this.Nume = nume;
            this.Prenume = prenume;
            this.Poz = pozitie;
        }

       
        public Jucator(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);
            IdJucator = Convert.ToInt32(dateFisier[ID]);
            Nume = dateFisier[NUME];
            Prenume = dateFisier[PRENUME];
            Poz = dateFisier[POZITIE];
            SetNumar(dateFisier[NUMAR], SEPARATOR_SECUNDAR_FISIER);

            Culoare_kit = (Class1)Enum.Parse(typeof(Class1), dateFisier[CULOARE_KIT]);
            Pozitie = new ArrayList();
    
            Pozitie.AddRange(dateFisier[POZITIE].Split(SEPARATOR_SECUNDAR_FISIER));

        }

      

        public string ConversieLaSir_PentruFisier()
        {
            string sNumar = string.Empty;
            if (numere != null)
            {
                sNumar = string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), numere);
            }

            string obiectJucatorPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdJucator.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Prenume ?? " NECUNOSCUT "),
                Culoare_kit,
                PozitieAsString,
                sNumar);

            return obiectJucatorPentruFisier;
        }

        public void SetNumar(string sirNumar, char delimitator = ' ')
        {
            string[] vectorNumarDupaSplit = sirNumar.Split(delimitator);
            numere = new int[vectorNumarDupaSplit.Length];

            int nrNumar = 0;
            foreach (string numar in vectorNumarDupaSplit)
            {
                bool rezultatConversie = Int32.TryParse(numar, out numere[nrNumar]);
                if (rezultatConversie == SUCCES && ValideazaNumar(numere[nrNumar]) == SUCCES)
                {
                    nrNumar++;
                }
            }

            Array.Resize(ref numere, nrNumar);
        }

        public void SetNumar(int[] _numar)
        {
            numere = new int[_numar.Length];
            _numar.CopyTo(numere, 0);
        }

        private bool ValideazaNumar(int numar)
        {
            if (numar >= NUMAR_MINIM && numar <= NUMAR_MAXIM)
            {
                return true;
            }

            return false;
        }
        
        

    }
}
