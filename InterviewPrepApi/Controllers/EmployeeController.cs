using InterviewPrepApi.DTO;
using InterviewPrepApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace InterviewPrepApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AuthContext _context;
        public EmployeeController(AuthContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("verify-employment")]
        public async Task<IActionResult> verifyEmployment([FromBody] EmployeeRequest employeeRequest)
        {
            var employeeVerification = await _context.EmployeeRequest.SingleOrDefaultAsync(x => x.EmployeeId == employeeRequest.EmployeeId);
            if (employeeVerification != null)
            {
                if (employeeVerification?.EmployeeId == employeeRequest.EmployeeId && employeeVerification?.CompanyName == employeeRequest.CompanyName && employeeVerification?.VerificationCode == employeeRequest.VerificationCode)
                    return Ok(employeeVerification);
                else
                    return BadRequest();
            }
            else
            { return BadRequest(); }

        }
    }
}
