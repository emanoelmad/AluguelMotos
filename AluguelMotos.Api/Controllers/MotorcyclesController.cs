using AluguelMotos.Domain.Entities;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("motos")]
    public class MotosController : ControllerBase
    {
        private readonly AluguelMotosDbContext _context;
        private readonly ILogger<MotosController> _logger;
        public MotosController(AluguelMotosDbContext context, ILogger<MotosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarMoto([FromBody] MotorcycleCreateRequest request)
        {
            _logger.LogInformation("Tentando cadastrar moto: {Plate}", request.Plate);
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
            return CreatedAtAction(nameof(ConsultarMotoPorId), new { id = moto.Id }, moto);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarMotos([FromQuery] string? plate)
        {
            _logger.LogInformation("Consultando motos. Filtro placa: {Plate}", plate);
            var query = _context.Motorcycles.AsQueryable();
            if (!string.IsNullOrEmpty(plate))
                query = query.Where(m => m.Plate == plate);
            var result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarMotoPorId(Guid id)
        {
            _logger.LogInformation("Consultando moto por id: {Id}", id);
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> ModificarPlaca(Guid id, [FromBody] string novaPlaca)
        {
            _logger.LogInformation("Modificando placa da moto {Id} para {NovaPlaca}", id, novaPlaca);
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return NotFound();
            if (await _context.Motorcycles.AnyAsync(m => m.Plate == novaPlaca))
                return Conflict(new { message = "Plate already exists" });
            moto.Plate = novaPlaca;
            await _context.SaveChangesAsync();
            return Ok(moto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverMoto(Guid id)
        {
            _logger.LogInformation("Removendo moto id: {Id}", id);
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return NotFound();
            // TODO: Validar se existe locação vinculada antes de remover
            _context.Motorcycles.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
