using SQLite;
using System;
using System.Collections.Generic;

namespace QuotesMobile.Models
{
    public partial class Quote
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public int AcrossTheFloor { get; set; }
        public int HeightFromTheFloor { get; set; }
        public int Angle { get; set; }
        public double Time { get; set; }
        public int Price { get; set; }
        public string JobType { get; set; }
        public bool Agreed { get; set; }
        public Nullable<System.DateTime> QuoteDate { get; set; }
        public Nullable<System.DateTime> AgreedDate { get; set; }
        public bool Finished { get; set; }
        public Nullable<System.DateTime> finishDate { get; set; }
        public Nullable<double> timeTaken { get; set; }
        public Nullable<bool> payedByCash { get; set; }
        public Nullable<bool> payedByTransfer { get; set; }
    }

    public class JobType
    {
        public string jtype { get; set; }

        public JobType()
        {
        }

        public JobType(string jtype)
        {
            this.jtype = jtype;
        }

        public IList<JobType> GetJobTypes()
        {
            string[] jtypes = { "3dr/tall", "6dr/tall", "3dr", "6dr" };
            var f = new List<JobType>();
            foreach (var jtype in jtypes)
            {
                f.Add(new JobType(jtype));
            }            
            return f;
        }
    }

    public class Supplier
    {
        public string supplierName { get; set; }

        public Supplier()
        {
        }

        public Supplier(string _supplierName)
        {
            this.supplierName = _supplierName;
        }

        public IList<Supplier> GetSuppliers()
        {
            string[] sups = { "Noyaks", "Strahans", "Barretts", "Clearys", "Deisel" };
            var f = new List<Supplier>();
            foreach (var sup in sups)
            {
                f.Add(new Supplier(sup));
            }
            return f;
        }
    }
}
