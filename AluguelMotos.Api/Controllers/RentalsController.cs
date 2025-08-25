using AluguelMotos.Domain.Entities;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly AluguelMotosDbContext _context;
        public RentalsController(AluguelMotosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentalCreateRequest request)
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
            return CreatedAtAction(nameof(GetById), new { id = rental.Id }, rental);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Rentals.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        private decimal CalculateRentalCost(RentalPlan plan, int days)
        {
            return plan switch
            {
                RentalPlan.SevenDays => 30m * days,
                RentalPlan.FifteenDays => 28m * days,
                RentalPlan.ThirtyDays => 22m * days,
                RentalPlan.FortyFiveDays => 20m * days,
                RentalPlan.FiftyDays => 18m * days,
                _ => 0m
            };
        }
    }
}
