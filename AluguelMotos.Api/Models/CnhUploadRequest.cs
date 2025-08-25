using System.ComponentModel.DataAnnotations;

namespace AluguelMotos.Api.Models
{
    public class CnhUploadRequest
    {
        [Required]
        public required string CourierId { get; set; }
        [Required]
        public required string FileName { get; set; }
        [Required]
        public required string ContentType { get; set; } // image/png, image/bmp
        [Required]
        public required byte[] FileContent { get; set; }
    }
}
