using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConstructionDataBase
{
    public partial class Form1 : Form
    {
        EntityMethods enMet = new EntityMethods();
        DBMethods dbMet = new DBMethods();

        public Form1()
        { 
            InitializeComponent();
            insertDarbuotojasStatybviete.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            insertStatybininkasAK.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            insertPriziuretojasAK.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            insertPriziuretojasImone.DataSource = dbMet.FillComboBox("Imone", "ID");
            updateDarbuotojasStatybviete.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            updateStatybininkasAK.DataSource = dbMet.FillComboBox("Statybininkas", "AK");
            updatePriziuretojasAK.DataSource = dbMet.FillComboBox("Priziuretojas", "AK");
            updatePriziuretojasImone.DataSource = dbMet.FillComboBox("Imone", "ID");
            updateStatybvieteID.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            updateImoneID.DataSource = dbMet.FillComboBox("Imone", "ID");
            updateDarbuotojasAK.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            deleteStatybviete.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            deleteImone.DataSource = dbMet.FillComboBox("Imone", "ID");
            deleteDarbuotojas.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            deleteStatybininkas.DataSource = dbMet.FillComboBox("Statybininkas", "AK");
            deletePriziuretojas.DataSource = dbMet.FillComboBox("Priziuretojas", "AK");
            
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (selectComboBox.Text == "Statybvietės")
            {
                dataGridView.DataSource = enMet.SelectStatybvietes().ToList();
                dataGridView.Columns.Remove("Darbuotojas");
            }

            else if (selectComboBox.Text == "Įmonės")
            {
                dataGridView.DataSource = dbMet.Select("Imone");
            }

            else if (selectComboBox.Text == "Darbuotojai")
            {
                dataGridView.DataSource = enMet.SelectDarbuotojai().ToList();
                dataGridView.Columns.Remove("StatybvietesID");
                dataGridView.Columns.Remove("PriziuretojoAK");
                dataGridView.Columns.Remove("StatybininkoAK");
            }

            else if (selectComboBox.Text == "Statybininkai")
            {
                dataGridView.DataSource = enMet.SelectStatybininkai().ToList();
                dataGridView.Columns.Remove("DarbuotojoAK");
            }

            else if (selectComboBox.Text == "Prižiūrėtojai")
            {
                dataGridView.DataSource = enMet.SelectPriziuretojai().ToList();
                dataGridView.Columns.Remove("DarbuotojoAK");
                dataGridView.Columns.Remove("ImonesID");
            }
        }

        private void insertStatybvieteButton_Click(object sender, EventArgs e)
        {
            Statybviete stat = new Statybviete();
            try
            {
                stat.Id = Convert.ToInt32(insertStatybvieteID.Text);
                stat.Tipas = insertStatybvieteTipas.Text;
                stat.Plotas = Convert.ToInt32(insertStatybvietePlotas.Text);
                stat.Adresas = insertStatybvieteAdresas.Text;
                stat.Pradzia = pradziaTime.Value.Date;
                stat.Pabaiga = pabaigaTime.Value.Date;

                enMet.InsertStatybviete(stat);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }   
        }

        private void insertImoneButton_Click(object sender, EventArgs e)
        {
            Imone im = new Imone();
            try
            {
                im.Id = Convert.ToInt32(insertImoneID.Text);
                im.Pavadinimas = insertImonePavadinimas.Text;
                im.Adresas = insertImoneAdresas.Text;

                dbMet.InsertImone(im);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void insertDarbuotojasButton_Click(object sender, EventArgs e)
        {
            Darbuotojas darb = new Darbuotojas();
            try
            {
                darb.AK = Convert.ToInt64(insertDarbuotojasAK.Text);
                darb.Vardas = insertDarbuotojasVardas.Text;
                darb.Pavarde = insertDarbuotojasPavarde.Text;
                darb.Tel_nr = insertDarbuotojasTelnr.Text;
                darb.Alga = Convert.ToInt32(insertDarbuotojasAlga.Text);
                darb.Statybviete = Convert.ToInt32(insertDarbuotojasStatybviete.Text);

                enMet.InsertDarbuotojas(darb);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void insertStatybininkasButton_Click(object sender, EventArgs e)
        {
            Statybininkas statyb = new Statybininkas();
            try
            {
                statyb.AK = Convert.ToInt64(insertStatybininkasAK.Text);
                statyb.Kvalifikacija = insertStatybininkasKvalifikacija.Text;

                enMet.InsertStatybininkas(statyb);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void insertPriziuretojasButton_Click(object sender, EventArgs e)
        {
            Priziuretojas priz = new Priziuretojas();
            try
            {
                priz.AK = Convert.ToInt64(insertPriziuretojasAK.Text);
                priz.El_pastas = insertPriziuretojasElpastas.Text;
                priz.Imone = Convert.ToInt32(insertPriziuretojasImone.Text);

                enMet.InsertPriziuretojas(priz);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void updateStatybvieteButton_Click(object sender, EventArgs e)
        {
            Statybviete stat = new Statybviete();
            int id = Convert.ToInt32(updateStatybvieteID.Text);
            stat.Tipas = updateStatybvieteTipas.Text;
            stat.Plotas = Convert.ToInt32(updateStatybvietePlotas.Text);
            stat.Adresas = updateStatybvieteAdresas.Text;
            stat.Pradzia = updatePradziaDate.Value.Date;
            stat.Pabaiga = updatePabaigaDate.Value.Date;

            enMet.UpdateStatybviete(id, stat);
        }

        private void updateImoneButton_Click(object sender, EventArgs e)
        {
            Imone im = new Imone();
            int id = Convert.ToInt32(updateImoneID.Text);
            im.Pavadinimas = updateImonePavadinimas.Text;
            im.Adresas = updateImoneAdresas.Text;

            dbMet.UpdateImone(id, im);
        }

        private void updateDarbuotojasButton_Click(object sender, EventArgs e)
        {
            Darbuotojas darb = new Darbuotojas();
            long ak = Convert.ToInt64(updateDarbuotojasAK.Text);
            darb.Vardas = updateDarbuotojasVardas.Text;
            darb.Pavarde = updateDarbuotojasPavarde.Text;
            darb.Tel_nr = updateDarbuotojasTelnr.Text;
            darb.Alga = Convert.ToInt32(updateDarbuotojasAlga.Text);
            darb.Statybviete = Convert.ToInt32(updateDarbuotojasStatybviete.Text);

            enMet.UpdateDarbuotojas(ak, darb);
        }

        private void updatePriziuretojasButton_Click(object sender, EventArgs e)
        {
            Priziuretojas priz = new Priziuretojas();
            long ak = Convert.ToInt64(updatePriziuretojasAK.Text);
            priz.El_pastas = updatePriziuretojasElpastas.Text;
            priz.Imone = Convert.ToInt32(updatePriziuretojasImone.Text);

            enMet.UpdatePriziuretojas(ak, priz);
        }

        private void updateStatybininkasButton_Click(object sender, EventArgs e)
        {
            Statybininkas statyb = new Statybininkas();
            long ak = Convert.ToInt64(updateStatybininkasAK.Text);
            statyb.Kvalifikacija = updateStatybininkasKvalifikacija.Text;

            enMet.UpdateStatybininkas(ak, statyb);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            insertDarbuotojasStatybviete.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            insertStatybininkasAK.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            insertPriziuretojasAK.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            insertPriziuretojasImone.DataSource = dbMet.FillComboBox("Imone", "ID");
            updateDarbuotojasStatybviete.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            updateStatybininkasAK.DataSource = dbMet.FillComboBox("Statybininkas", "AK");
            updatePriziuretojasAK.DataSource = dbMet.FillComboBox("Priziuretojas", "AK");
            updatePriziuretojasImone.DataSource = dbMet.FillComboBox("Imone", "ID");
            updateStatybvieteID.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            updateImoneID.DataSource = dbMet.FillComboBox("Imone", "ID");
            updateDarbuotojasAK.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            deleteStatybviete.DataSource = dbMet.FillComboBox("Statybviete", "ID");
            deleteImone.DataSource = dbMet.FillComboBox("Imone", "ID");
            deleteDarbuotojas.DataSource = dbMet.FillComboBox("Darbuotojas", "AK");
            deleteStatybininkas.DataSource = dbMet.FillComboBox("Statybininkas", "AK");
            deletePriziuretojas.DataSource = dbMet.FillComboBox("Priziuretojas", "AK");
        }

        private void deleteStatybvieteButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deleteStatybviete.Text);
            try
            {
                enMet.DeleteStatybviete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteImoneButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deleteImone.Text);
            try
            {
                dbMet.DeleteImone(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteDarbuotojasButton_Click(object sender, EventArgs e)
        {
            long ak = Convert.ToInt32(deleteDarbuotojas.Text);
            try
            {
                enMet.DeleteDarbuotojas(ak);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteStatybininkasButton_Click(object sender, EventArgs e)
        {
            long ak = Convert.ToInt32(deleteStatybininkas.Text);
            enMet.DeleteStatybininkas(ak);

        }

        private void deletePriziuretojasButton_Click(object sender, EventArgs e)
        {
            long ak = Convert.ToInt32(deletePriziuretojas.Text);
            enMet.DeletePriziuretojas(ak);
        }

        private void joinButton_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = enMet.JoinedTables();
        }

        private void skipAndTakeButton_Click(object sender, EventArgs e)
        {
            int skip = Convert.ToInt32(skipBox.Text);
            int take = Convert.ToInt32(takeBox.Text);

            DataTable data = (DataTable)dataGridView.DataSource;
            dataGridView.DataSource = dbMet.SkipAndTake(skip, take, data);
        }

        private void groupButton_Click(object sender, EventArgs e)
        {
            DataTable data = (DataTable)dataGridView.DataSource;
            dataGridView.DataSource = dbMet.GroupAndSum(data);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable data = (DataTable)dataGridView.DataSource;
            dataGridView.DataSource = dbMet.Top5(data);
        }

        
    }
}
