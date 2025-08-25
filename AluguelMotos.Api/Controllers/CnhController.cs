using System;
using System.IO;
using AluguelMotos.Domain.Entities;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CnhController : ControllerBase
    {
        private readonly AluguelMotosDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CnhController(AluguelMotosDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] CnhUploadRequest request)
        {
            if (request.ContentType != "image/png" && request.ContentType != "image/bmp")
                return BadRequest(new { message = "Invalid file type. Only PNG or BMP allowed." });

            var courier = await _context.Couriers.FindAsync(Guid.Parse(request.CourierId));
            if (courier == null) return NotFound();

            var fileName = $"cnh_{courier.Id}_{DateTime.UtcNow.Ticks}{System.IO.Path.GetExtension(request.FileName)}";
            var filePath = System.IO.Path.Combine(_env.ContentRootPath, "cnh_images", fileName);
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath)!);
            await System.IO.File.WriteAllBytesAsync(filePath, request.FileContent);

            courier.CnhImagePath = filePath;
            await _context.SaveChangesAsync();
            return Ok(new { message = "CNH image uploaded", path = filePath });
        }
    }
}
