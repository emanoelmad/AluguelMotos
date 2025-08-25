using System.ComponentModel.DataAnnotations;

namespace AluguelMotos.Api.Models
{
    public class CourierCreateRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Cnpj { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public required string CnhNumber { get; set; }
        [Required]
        public required string CnhType { get; set; } // A, B, A+B
    }
}
