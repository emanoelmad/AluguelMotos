using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelMotos.Domain.Entities
{
    public enum RentalPlan
    {
        SevenDays,
        FifteenDays,
        ThirtyDays,
        FortyFiveDays,
        FiftyDays
    }

    public class Rental
    {
        public Guid Id { get; set; }
        public Guid CourierId { get; set; }
        public Guid MotorcycleId { get; set; }
        public RentalPlan Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? Fine { get; set; }

        public decimal CalculateReturnCost(DateTime actualReturnDate)
        {
            int rentedDays = (ExpectedEndDate - StartDate).Days;
            int usedDays = (actualReturnDate - StartDate).Days;
            decimal dailyRate = Plan switch
            {
                RentalPlan.SevenDays => 30m,
                RentalPlan.FifteenDays => 28m,
                RentalPlan.ThirtyDays => 22m,
                RentalPlan.FortyFiveDays => 20m,
                RentalPlan.FiftyDays => 18m,
                _ => 0m
            };

            if (actualReturnDate < ExpectedEndDate)
            {
                int unusedDays = rentedDays - usedDays;
                decimal finePercent = Plan switch
                {
                    RentalPlan.SevenDays => 0.20m,
                    RentalPlan.FifteenDays => 0.40m,
                    _ => 0m
                };
                decimal fine = unusedDays * dailyRate * finePercent;
                return (usedDays * dailyRate) + fine;
            }
            else if (actualReturnDate > ExpectedEndDate)
            {
                int extraDays = (actualReturnDate - ExpectedEndDate).Days;
                decimal extraCost = extraDays * 50m;
                return (rentedDays * dailyRate) + extraCost;
            }
            else
            {
                return rentedDays * dailyRate;
            }
        }
    }
}
