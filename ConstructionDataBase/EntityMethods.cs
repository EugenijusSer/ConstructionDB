using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace ConstructionDataBase
{
    class EntityMethods
    {
        ConstructionDBEntities entities = new ConstructionDBEntities();

        //SELECT

        public DbSet<Statybviete> SelectStatybvietes()
        {
            return entities.Statybvietes;
        }

        public DbSet<Imone> SelectImones()
        {
            return entities.Imones;
        }

        public DbSet<Darbuotojas> SelectDarbuotojai()
        {
            return entities.Darbuotojai;
        }

        public DbSet<Statybininkas> SelectStatybininkai()
        {
            return entities.Statybininkai;
        }

        public DbSet<Priziuretojas> SelectPriziuretojai()
        {
            return entities.Priziuretojai;
        }

        //INSERT
        public void InsertStatybviete(Statybviete stat)
        {
            entities.Statybvietes.Add(stat);
            entities.SaveChanges();
        }

        public void InsertImone(Imone im)
        {
            entities.Imones.Add(im);
            entities.SaveChanges();
        }

        public void InsertDarbuotojas(Darbuotojas darb)
        {
            entities.Darbuotojai.Add(darb);
            entities.SaveChanges();
        }

        public void InsertStatybininkas(Statybininkas statyb)
        {
            entities.Statybininkai.Add(statyb);
            entities.SaveChanges();
        }

        public void InsertPriziuretojas(Priziuretojas priz)
        {
            entities.Priziuretojai.Add(priz);
            entities.SaveChanges();
        }

        //UPDATE

        public void UpdateStatybviete(long field, Statybviete updated)
        {
            Statybviete stat = entities.Statybvietes.Where(temp => temp.Id == field).FirstOrDefault();
            stat.Tipas = updated.Tipas;
            stat.Plotas = updated.Plotas;
            stat.Adresas = updated.Adresas;
            stat.Pradzia = updated.Pradzia;
            stat.Pabaiga = updated.Pabaiga;
            entities.SaveChanges();
        }

        public void UpdateImone(long field, Imone updated)
        {
            Imone im = entities.Imones.Where(temp => temp.Id == field).FirstOrDefault();
            im.Pavadinimas = updated.Pavadinimas;
            im.Adresas = updated.Adresas;
            entities.SaveChanges();
        }

        public void UpdateDarbuotojas(long field, Darbuotojas updated)
        {
            Darbuotojas darb = entities.Darbuotojai.Where(temp => temp.AK == field).FirstOrDefault();
            darb.Vardas = updated.Vardas;
            darb.Pavarde = updated.Pavarde;
            darb.Tel_nr = updated.Tel_nr;
            darb.Alga = updated.Alga;
            darb.Statybviete = updated.Statybviete;
            entities.SaveChanges();
        }

        public void UpdateStatybininkas(long field, Statybininkas updated)
        {
            Statybininkas statyb = entities.Statybininkai.Where(temp => temp.AK == field).FirstOrDefault();
            statyb.Kvalifikacija = updated.Kvalifikacija;
            entities.SaveChanges();
        }

        public void UpdatePriziuretojas(long field, Priziuretojas updated)
        {
            Priziuretojas priz = entities.Priziuretojai.Where(temp => temp.AK == field).FirstOrDefault();
            priz.El_pastas = updated.El_pastas;
            priz.Imone = updated.Imone;
            entities.SaveChanges();
        }

        //DELETE

        public void DeleteStatybviete(int id)
        {
            if (entities.Darbuotojai.Any(temp => temp.Statybviete == id))
            {
                throw new Exception("Action aborted. Firsly delete dependencies.");
            }
            Statybviete a = entities.Statybvietes.Where(emt => emt.Id == id).FirstOrDefault();
            entities.Statybvietes.Remove(a);
            entities.SaveChanges();
        }

        public void DeleteImone(int id)
        {
            if (entities.Priziuretojai.Any(temp => temp.Imone == id))
            {
                throw new Exception("Action aborted. Firsly delete dependencies.");
            }
            Imone a = entities.Imones.Where(emt => emt.Id == id).FirstOrDefault();
            entities.Imones.Remove(a);
            entities.SaveChanges();
        }

        public void DeleteDarbuotojas(long ak)
        {
            if (entities.Statybininkai.Any(temp => temp.AK == ak) || entities.Priziuretojai.Any(temp => temp.AK == ak))
            {
                throw new Exception("Action aborted. Firsly delete dependencies.");
            }
            Darbuotojas a = entities.Darbuotojai.Where(emt => emt.AK == ak).FirstOrDefault();
            entities.Darbuotojai.Remove(a);
            entities.SaveChanges();
        }

        public void DeleteStatybininkas(long ak)
        {
            Statybininkas a = entities.Statybininkai.Where(temp => temp.AK == ak).FirstOrDefault();
            entities.Statybininkai.Remove(a);
            entities.SaveChanges();
        }

        public void DeletePriziuretojas(long ak)
        {
            Priziuretojas a = entities.Priziuretojai.Where(temp => temp.AK == ak).FirstOrDefault();
            entities.Priziuretojai.Remove(a);
            entities.SaveChanges();
        }

        public DataTable JoinedTables()
        {
            var query =
                from stat in entities.Statybininkai
                join darb in entities.Darbuotojai on stat.AK equals darb.AK
                select new { Statybininkas = stat, Darbuotojas = darb };

            DataTable data = new DataTable();

            data.Columns.Add("AK", typeof(long));
            data.Columns.Add("Vardas", typeof(string));
            data.Columns.Add("Pavarde", typeof(string));
            data.Columns.Add("Kvalifikacija", typeof(string));
            data.Columns.Add("Alga", typeof(int));
            data.Columns.Add("Statybviete", typeof(int));

            foreach (var element in query)
            {
                var row = data.NewRow();
                row["AK"] = element.Statybininkas.AK;
                row["Vardas"] = element.Darbuotojas.Vardas;
                row["Pavarde"] = element.Darbuotojas.Pavarde;
                row["Kvalifikacija"] = element.Statybininkas.Kvalifikacija;
                row["Alga"] = element.Darbuotojas.Alga;
                row["Statybviete"] = element.Darbuotojas.Statybviete;
                data.Rows.Add(row);
            }
           
            return data;
        }

    }
}
