using AluguelMotos.Domain.Entities;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("locacao")]
    public class LocacaoController : ControllerBase
    {
        private readonly AluguelMotosDbContext _context;
    public LocacaoController(AluguelMotosDbContext context)
        {
            _context = context;
        }

    [HttpPost]
    public async Task<IActionResult> AlugarMoto([FromBody] RentalCreateRequest request)
        {
            var courier = await _context.Couriers.FindAsync(request.CourierId);
            if (courier == null) return NotFound(new { message = "Courier not found" });
            if (courier.CnhType != "A" && courier.CnhType != "A+B")
                return BadRequest(new { message = "Courier not allowed to rent motorcycle" });
            var moto = await _context.Motorcycles.FindAsync(request.MotorcycleId);
            if (moto == null) return NotFound(new { message = "Motorcycle not found" });
            // TODO: Validar se a moto está disponível

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                CourierId = request.CourierId,
                MotorcycleId = request.MotorcycleId,
                Plan = request.Plan,
                StartDate = request.StartDate,
                ExpectedEndDate = request.ExpectedEndDate,
                EndDate = request.ExpectedEndDate,
                TotalCost = CalculateRentalCost(request.Plan, request.ExpectedEndDate.Subtract(request.StartDate).Days)
            };
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ConsultarLocacaoPorId), new { id = rental.Id }, rental);
        }

    [HttpGet]
    public async Task<IActionResult> ConsultarLocacoes()
        {
            var result = await _context.Rentals.ToListAsync();
            return Ok(result);
        }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarLocacaoPorId(Guid id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        private decimal CalculateRentalCost(RentalPlan plan, int days)
        {
            switch (plan)
            {
                case RentalPlan.SevenDays: return 30m * days;
                case RentalPlan.FifteenDays: return 28m * days;
                case RentalPlan.ThirtyDays: return 22m * days;
                case RentalPlan.FortyFiveDays: return 20m * days;
                case RentalPlan.FiftyDays: return 18m * days;
                default: return 0m;
            }
        }
    }
}
