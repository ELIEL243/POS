using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Qts { get; set; }
        public string Date { get; set; }
        public float Purchase_price { get; set; }
        public float Total { get; set; }

    }
}
