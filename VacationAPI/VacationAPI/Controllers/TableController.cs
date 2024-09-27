using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationAPI.Models;
using VacationData;


namespace VacationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly VacationContext _context;
        public TableController(VacationContext context) => _context = context;

    }
}
