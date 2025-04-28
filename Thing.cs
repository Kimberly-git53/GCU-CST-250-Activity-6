using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmInventory
{
    public class Thing
    {
        /// Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        /// Constructor
        public override string ToString()
        {
            // concatenate three properties
            return "Id = " + Id + ", Name = " + Name + ", Value = " + Value;

        }
    }
}
