using System.ComponentModel.DataAnnotations;

namespace AluguelMotos.Api.Models
{
    public class MotorcycleCreateRequest
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public required string Model { get; set; }
        [Required]
        public required string Plate { get; set; }
    }
}
