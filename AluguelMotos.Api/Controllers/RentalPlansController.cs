using AluguelMotos.Domain.Entities;
using AluguelMotos.Domain.Interfaces;
using AluguelMotos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AluguelMotos.Api.Controllers
{
    [ApiController]
    [Route("api/planos-locacao")]
    public class PlanosLocacaoController : ControllerBase
    {
        private readonly IRentalPlanRepository _repository;
        private readonly ILogger<PlanosLocacaoController> _logger;
        public PlanosLocacaoController(IRentalPlanRepository repository, ILogger<PlanosLocacaoController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                _logger.LogInformation("Consultando todos os planos de locação.");
                var plans = await _repository.GetAllAsync();
                return Ok(plans);
            }

        [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                _logger.LogInformation("Consultando plano de locação por id: {Id}", id);
                var plan = await _repository.GetByIdAsync(id);
                if (plan == null) return NotFound();
                return Ok(plan);
            }

        [HttpPost]
            public async Task<IActionResult> Create([FromBody] RentalPlanEntity plan)
            {
                _logger.LogInformation("Criando novo plano de locação: {Name}", plan.Name);
                var created = await _repository.AddAsync(plan);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

        [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] RentalPlanEntity plan)
            {
                _logger.LogInformation("Atualizando plano de locação id: {Id}", id);
                plan.Id = id;
                var updated = await _repository.UpdateAsync(plan);
                return Ok(updated);
            }

        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                _logger.LogInformation("Removendo plano de locação id: {Id}", id);
                var removed = await _repository.RemoveAsync(id);
                if (!removed) return NotFound();
                return NoContent();
            }
    }
}
