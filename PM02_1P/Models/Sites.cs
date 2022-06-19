using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM02_1P.Models
{
    public class Sites
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Double Lat { get; set; }
        public Double Lon { get; set; }
        public String Descrip { get; set; }
        public Byte[] Photo { get; set; }
    }
}
