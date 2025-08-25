using AluguelMotos.Domain.Entities;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("entregadores")]
    public class EntregadoresController : ControllerBase
    {
        private readonly AluguelMotosDbContext _context;
    public EntregadoresController(AluguelMotosDbContext context)
        {
            _context = context;
        }

    [HttpPost]
    public async Task<IActionResult> CadastrarEntregador([FromBody] CourierCreateRequest request)
        {
            if (await _context.Couriers.AnyAsync(c => c.Cnpj == request.Cnpj))
                return Conflict(new { message = "CNPJ already exists" });
            if (await _context.Couriers.AnyAsync(c => c.CnhNumber == request.CnhNumber))
                return Conflict(new { message = "CNH number already exists" });
            if (request.CnhType != "A" && request.CnhType != "B" && request.CnhType != "A+B")
                return BadRequest(new { message = "Invalid CNH type" });

            var courier = new Courier
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cnpj = request.Cnpj,
                BirthDate = request.BirthDate,
                CnhNumber = request.CnhNumber,
                CnhType = request.CnhType
            };
            _context.Couriers.Add(courier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ConsultarEntregadorPorId), new { id = courier.Id }, courier);
        }

    [HttpGet]
    public async Task<IActionResult> ConsultarEntregadores()
        {
            var result = await _context.Couriers.ToListAsync();
            return Ok(result);
        }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarEntregadorPorId(Guid id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            if (courier == null) return NotFound();
            return Ok(courier);
        }
    }
}
