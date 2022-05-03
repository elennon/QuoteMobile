using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuotesMobile.Models
{
    public partial class Receipt
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string supplier { get; set; }
        public Nullable<double> spent { get; set; }
        public Nullable<System.DateTime> dateBought { get; set; }
    }
}
