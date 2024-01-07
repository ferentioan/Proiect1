using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Proiect1.Models
{
    public class Produse
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(250), Unique]
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(typeof(Warehouse))]
        public int WarehouseID { get; set; }

    }
}
