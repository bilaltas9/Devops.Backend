using Microsoft.AspNetCore.Mvc;
using MyDotNetApi.Models;
using MyDotNetApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyDotNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNumbers([FromBody] InputModel model)
        {
            if (model == null || !double.TryParse(model.Input1, out double number1) || !double.TryParse(model.Input2, out double number2))
            {
                return BadRequest("Geçersiz veri. Lütfen sayısal değerler girin.");
            }

            double result = number1 + number2;

            // Veritabanına kaydet
            var calculation = new Calculation
            {
                Number1 = number1,
                Number2 = number2,
                Result = result
            };

            // Gelen verileri işleyebilirsiniz (örneğin, veritabanına kaydedebilirsiniz).
            Console.WriteLine($"Input1: {model.Input1}, Input2: {model.Input2}, result: {result}");

            _context.Calculations.Add(calculation);
            await _context.SaveChangesAsync();

            // Sonucu döndür
            return Ok(new { Message = "Sayılar başarıyla toplandı ve veritabanına kaydedildi!", Result = result });
        }
    }
}