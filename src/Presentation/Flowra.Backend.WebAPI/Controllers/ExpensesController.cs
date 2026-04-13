using Flowra.Backend.Application.DTOs.Expense;
using Flowra.Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseDto dto) // Parametre DTO oldu
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _expenseService.AddExpenseAsync(dto);
            return Ok(new { message = "Gider başarıyla eklendi." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExpenseDto dto) // Parametre DTO oldu
        {
            if (id != dto.Id)
                return BadRequest("URL'deki ID ile gönderilen ID uyuşmuyor.");

            try
            {
                await _expenseService.UpdateExpenseAsync(dto);
                return Ok(new { message = "Gider başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                // Servisten fırlatılan "bulunamadı" hatasını yakala
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _expenseService.DeleteExpenseAsync(id);
            return Ok(new { message = "Gider başarıyla silindi." });
        }
    }
}
