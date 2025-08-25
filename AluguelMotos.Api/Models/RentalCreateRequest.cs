using System.ComponentModel.DataAnnotations;
using AluguelMotos.Domain.Entities;

namespace AluguelMotos.Api.Models
{
    public class RentalCreateRequest
    {
        [Required]
        public Guid CourierId { get; set; }
        [Required]
        public Guid MotorcycleId { get; set; }
        [Required]
        public RentalPlan Plan { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpectedEndDate { get; set; }
    }
}
