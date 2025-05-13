using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SwaggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartFormController : ControllerBase
    {

        [HttpPost("validate")]
        public IActionResult ValidateForm([FromBody] Dictionary<string, string> formData)
        { 
            var validator = new SmartFormValidator<Dictionary<string, string>>();

            bool isValid = validator.Validate(formData);

            if (!isValid)
            {
                return BadRequest(new { Errors = validator.GetErrors() });
            }
            
            return Ok(new { Message = "Form is valid" });
        }
    }

}
