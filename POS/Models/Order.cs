using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public float total { get; set; }
        public float tax { get; set; }
        public float sub_total { get; set; }

    }
}
