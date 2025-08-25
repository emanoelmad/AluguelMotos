using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelMotos.Domain.Entities
{
    public class Motorcycle
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; } // Property with uniqueness constraint
    }
}
