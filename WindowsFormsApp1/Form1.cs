using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using LibrarieModele;
using LibrarieModele.Enumerari;
using NivelStocareDate1;
using System.Configuration;
using System.Net.WebSockets;
using System.Diagnostics.Eventing.Reader;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        FisierText adminJucatori;

        private System.Windows.Forms.Label lblHeaderNume;
        private System.Windows.Forms.Label lblHeaderPrenume;
        private System.Windows.Forms.Label lblHeaderNumar;
        private System.Windows.Forms.Label lblHeaderPozitie;

        private System.Windows.Forms.Label[] lblsNume;
        private System.Windows.Forms.Label[] lblsPrenume;
        private System.Windows.Forms.Label[] lblsNumar;
        private System.Windows.Forms.Label[] lblsPoz;

        private const int LATIME_CONTROL = 100;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 120;
        private const int OFFSET_X = 600;

        ArrayList pozitieSelectata = new ArrayList();

        public Form1()
        {

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;
            adminJucatori = new FisierText(caleCompletaFisier);

            InitializeComponent();

            //setare proprietati
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.LimeGreen;
            this.Text = "Informatii jucatori";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AfiseazaJucatori();
        }

        private void AfiseazaJucatori()
        {
            //adaugare control de tip Label pentru 'Nume';
            lblHeaderNume = new System.Windows.Forms.Label();
            lblHeaderNume.Width = LATIME_CONTROL;
            lblHeaderNume.Text = "Nume";
            lblHeaderNume.Left = OFFSET_X + 0;
            lblHeaderNume.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderNume);



            //adaugare control de tip Label pentru 'Prenume';
            lblHeaderPrenume = new System.Windows.Forms.Label();
            lblHeaderPrenume.Width = LATIME_CONTROL;
            lblHeaderPrenume.Text = "Prenume";
            lblHeaderPrenume.Left = OFFSET_X + DIMENSIUNE_PAS_X;
            lblHeaderPrenume.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderPrenume);

            //adaugare control de tip Label pentru 'Numar';
            lblHeaderNumar = new System.Windows.Forms.Label();
            lblHeaderNumar.Width = LATIME_CONTROL;
            lblHeaderNumar.Text = "Numar";
            lblHeaderNumar.Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
            lblHeaderNumar.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderNumar);

            ArrayList jucatori = adminJucatori.GetJucatori();

            int nrJucatori = jucatori.Count;
            lblsNume = new System.Windows.Forms.Label[nrJucatori];
            lblsPrenume = new System.Windows.Forms.Label[nrJucatori];
            lblsNumar = new System.Windows.Forms.Label[nrJucatori];
           
            int i = 0;
            foreach (Jucator jucator in jucatori)
            {
                //adaugare control de tip Label pentru numele jucatorilor;
                lblsNume[i] = new System.Windows.Forms.Label();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = jucator.Nume;
                lblsNume[i].Left = OFFSET_X + 0;
                lblsNume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNume[i]);

                //adaugare control de tip Label pentru prenumele jucatorilor
                lblsPrenume[i] = new System.Windows.Forms.Label();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = jucator.Prenume;
                lblsPrenume[i].Left = OFFSET_X + DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsPrenume[i]);

                //adaugare control de tip Label pentru numerele jucatorilor
                lblsNumar[i] = new System.Windows.Forms.Label();
                lblsNumar[i].Width = LATIME_CONTROL;
                lblsNumar[i].Text = string.Join(" ", jucator.GetNumar());
                lblsNumar[i].Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
                lblsNumar[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNumar[i]);

               
                i++;

            }
             
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            if (DateIntrareValide())
            {

                Jucator s = new Jucator(0, txtNume.Text, txtPrenume.Text,pozitieSelectata.ToString());
                s.SetNumar(txtNumar.Text);

                Class1 culoareSelectata = GetCuloareSelectata();
                s.Culoare_kit = culoareSelectata;

                s.Pozitie = new ArrayList();
                s.Pozitie.AddRange(pozitieSelectata);

                adminJucatori.AddJucator(s);

                ResetareControale();
            }
        }

 
       private bool DateIntrareValide()

        {
            if (string.IsNullOrEmpty(txtNume.Text))
            {
                MessageBox.Show("Introduceti un nume valid", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(txtNumar.Text) )
            {
                MessageBox.Show("Introduceti un numar valid", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (ContainsOnlyDigits())
            {
                MessageBox.Show("Introduceti un numar valid", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(txtPrenume.Text))
            {
                MessageBox.Show("Introduceti un prenume valid", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

       
            private bool ContainsOnlyDigits()
            {
                string text = txtNumar.Text;
                int number;
                bool isNumber = int.TryParse(text, out number);

                return !isNumber;
            }

        


        private void CkbPozitie_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBoxControl = sender as CheckBox; //operator 'as'
      
            string pozitiaSelectata = checkBoxControl.Text;
            if (checkBoxControl.Checked == true)
                pozitieSelectata.Add(pozitiaSelectata);
            else
                pozitieSelectata.Remove(pozitiaSelectata);
        }

        private void ResetareControale()
        {
            txtNume.Text = txtPrenume.Text = txtNumar.Text = string.Empty;

            rdbRosu.Checked = false;
            rdbGalben.Checked = false;
            rdbNegru.Checked = false;

            ckbFundas.Checked = false;
            ckbMijlocas.Checked = false;
            ckbAtacant.Checked = false;

            pozitieSelectata.Clear();
        }

        private Class1 GetCuloareSelectata()
        {
            if (rdbRosu.Checked)
                return Class1.Rosu;
            if (rdbGalben.Checked)
                return Class1.Galben;
            if (rdbNegru.Checked)
                return Class1.Negru;

            return Class1.Rosu;
        }

        private void BtnAfiseaza_Click(object sender, EventArgs e)
        {
            AfiseazaJucatori();
            this.Width = 1000;
        }
    


        private void txtNume_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
