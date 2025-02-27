using Microsoft.AspNetCore.Mvc;
using MyDotNetApi.Models;

namespace MyDotNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult AddNumbers([FromBody] InputModel model)
        {
            if (model == null || !double.TryParse(model.Input1, out double number1) || !double.TryParse(model.Input2, out double number2))
            {
                return BadRequest("Geçersiz veri. Lütfen sayısal değerler girin.");
            }

            double result = number1 + number2;

            // Gelen verileri işleyebilirsiniz (örneğin, veritabanına kaydedebilirsiniz).
            Console.WriteLine($"Input1: {model.Input1}, Input2: {model.Input2}, result: {result}");

            // Sonucu döndürün.
            return Ok(new { Message = "Sayılar başarıyla toplandı!", Result = result });
        }
    }
}