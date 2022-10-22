using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Unit { get; set; }
        public int Qts { get; set; }
        public string Marque { get; set; }
        public string Category { get; set; }
    }
}
