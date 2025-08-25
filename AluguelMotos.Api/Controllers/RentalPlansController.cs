using AluguelMotos.Domain.Entities;
using AluguelMotos.Domain.Interfaces;
using AluguelMotos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalPlansController : ControllerBase
    {
        private readonly IRentalPlanRepository _repository;
        public RentalPlansController(IRentalPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _repository.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentalPlanEntity plan)
        {
            var created = await _repository.AddAsync(plan);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RentalPlanEntity plan)
        {
            plan.Id = id;
            var updated = await _repository.UpdateAsync(plan);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _repository.RemoveAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
