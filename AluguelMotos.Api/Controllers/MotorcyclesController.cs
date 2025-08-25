using AluguelMotos.Domain.Entities;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly AluguelMotosDbContext _context;
        public MotorcyclesController(AluguelMotosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotorcycleCreateRequest request)
        {
            if (await _context.Motorcycles.AnyAsync(m => m.Plate == request.Plate))
                return Conflict(new { message = "Plate already exists" });

            var moto = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Year = request.Year,
                Model = request.Model,
                Plate = request.Plate
            };
            _context.Motorcycles.Add(moto);
            await _context.SaveChangesAsync();
            // Simulação de evento de moto cadastrada
            // TODO: Integrar mensageria
            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? plate)
        {
            var query = _context.Motorcycles.AsQueryable();
            if (!string.IsNullOrEmpty(plate))
                query = query.Where(m => m.Plate == plate);
            var result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        [HttpPut("{id}/plate")]
        public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] string newPlate)
        {
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return NotFound();
            if (await _context.Motorcycles.AnyAsync(m => m.Plate == newPlate))
                return Conflict(new { message = "Plate already exists" });
            moto.Plate = newPlate;
            await _context.SaveChangesAsync();
            return Ok(moto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return NotFound();
            // TODO: Validar se existe locação vinculada antes de remover
            _context.Motorcycles.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
