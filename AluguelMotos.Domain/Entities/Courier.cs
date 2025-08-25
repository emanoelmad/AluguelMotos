using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelMotos.Domain.Entities
{
    public class Courier
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public required string CnhNumber { get; set; }
        public required string CnhType { get; set; } // A, B, A+B
        public string? CnhImagePath { get; set; }
    }
}
