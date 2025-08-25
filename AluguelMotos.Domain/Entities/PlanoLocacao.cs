using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelMotos.Domain.Entities
{
    public class RentalPlanEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Days { get; set; }
        public decimal DailyRate { get; set; }
        public decimal? FinePercent { get; set; }
        public decimal? ExtraDailyValue { get; set; }
    }
}
