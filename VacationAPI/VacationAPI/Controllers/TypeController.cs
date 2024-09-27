using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationAPI.Models;
using VacationData;

namespace VacationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly VacationContext _context;
        public TypeController(VacationContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<VacationType>> GetVactionTypes()
            => await _context.Types.ToListAsync();
    }
}
