using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationAPI.Models;
using VacationData;

namespace VacationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly VacationContext _context;
        public RequestController(VacationContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<RequestVac>> Get()
            => await _context.Requests.Include("User").ToListAsync();


        [HttpGet("{UserId}")] 
        [ProducesResponseType(typeof(RequestVac), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestByUserId(int UserId)
        {
            var request = await _context.Requests.Where(a => a.UserId == UserId).ToListAsync();
            return Ok(request); 
        }

        [HttpGet("request-id/{RequestId}")] 
        [ProducesResponseType(typeof(RequestVac), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestById(int RequestId)
        {
            var request = await _context.Requests.Where(a => a.Id == RequestId).ToListAsync();
            return Ok(request);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var requestToDelete = await _context.Requests
                .Include(r => r.User) 
                .FirstOrDefaultAsync(r => r.Id == id);
            if (requestToDelete == null) return NotFound();

            var fromDate = requestToDelete.FromDate;
            var toDate = requestToDelete.ToDate;
            int daysDifference = toDate.DayNumber - fromDate.DayNumber + 1;

            var user = requestToDelete.User;
            if (user == null) return NotFound();

            if (requestToDelete.TypeId == 1)
            {
                user.CasualDays += daysDifference;
            }
            else if (requestToDelete.TypeId == 2)
            {
                user.AnnualDays += daysDifference;
            }
            else
            {
                return BadRequest("Invalid request type.");
            }
            _context.Users.Update(user);
            _context.Requests.Remove(requestToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.Error.WriteLine($"An error occurred while saving changes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddVacationRequest(RequestVac request)
        { 
            var fromDate = request.FromDate;
            var toDate = request.ToDate;
            int daysDifference = toDate.DayNumber - fromDate.DayNumber + 1;

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) return NotFound();
            
            if (request.TypeId == 1)
            {
                if (user.CasualDays < daysDifference)
                {
                    return BadRequest("Insufficient casual leave balance.");
                }
                user.CasualDays -= daysDifference;
            }
            else if (request.TypeId == 2)
            {
                if (user.AnnualDays < daysDifference)
                {
                    return BadRequest("Insufficient annual leave balance.");
                }
                user.AnnualDays -= daysDifference;
            }
            else
            {
                return BadRequest("Invalid request type.");
            }
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            await _context.Requests.AddAsync(request);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.Error.WriteLine($"An error occurred while saving changes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

            return CreatedAtAction(nameof(GetRequestByUserId), new { UserId = request.UserId }, request);
        }

        [HttpPost("edit-request/{Requestid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditVacationRequest(int Requestid, RequestVac request)
        {
            if (Requestid != request.Id) return BadRequest("ID mismatch");

            var existingRequest = await _context.Requests.FindAsync(Requestid);
            if (existingRequest == null) return NotFound();

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) return NotFound();

            
            var fromDate = existingRequest.FromDate;
            var toDate = existingRequest.ToDate;
            int previousDaysDiff = toDate.DayNumber - fromDate.DayNumber + 1;

            
            if (existingRequest.TypeId == 1)
            {
                user.CasualDays += previousDaysDiff;
            }
            else if (existingRequest.TypeId == 2)
            {
                user.AnnualDays += previousDaysDiff;
            }

            
            var newFromDate = request.FromDate;
            var newToDate = request.ToDate;
            int newDaysDiff = newToDate.DayNumber - newFromDate.DayNumber + 1;

            
            if (request.TypeId == 1)
            {
                if (user.CasualDays < newDaysDiff)
                {
                    return BadRequest("Insufficient casual days balance.");
                }
                user.CasualDays -= newDaysDiff;
            }
            else if (request.TypeId == 2)
            {
                if (user.AnnualDays < newDaysDiff)
                {
                    return BadRequest("Insufficient annual days balance.");
                }
                user.AnnualDays -= newDaysDiff;
            }

            
            existingRequest.TypeId = request.TypeId;
            existingRequest.FromDate = request.FromDate;
            existingRequest.ToDate = request.ToDate;


            _context.Entry(existingRequest).Property(r => r.TypeId).IsModified = true;
            _context.Entry(existingRequest).Property(r => r.FromDate).IsModified = true;
            _context.Entry(existingRequest).Property(r => r.ToDate).IsModified = true;


            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Approve/{requestId}/{statusUpdate}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveRequest(int requestId, int statusUpdate)
        {

            var request = await _context.Requests.FindAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }

            var fromDate = request.FromDate;
            var toDate = request.ToDate;
            int daysDifference = toDate.DayNumber - fromDate.DayNumber + 1;

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) {
                return NotFound();
            }

            if (statusUpdate == 3 && request.TypeId == 1)
            {
                user.CasualDays += daysDifference;
            }
            if(statusUpdate == 3 && request.TypeId == 2)
            {
                user.AnnualDays += daysDifference;
            }

            request.StatusId = statusUpdate;
            _context.Users.Update(user);
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpGet("RequestsForManager/{id}")]
        [ProducesResponseType(typeof(RequestVac), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetManagerRequests(int id)
        {
            var users = await _context.Users.Where(a => a.ManagerId == id).ToListAsync();
            var userIds = users.Select(u => u.Id).ToList();
            var requests = await _context.Requests
                                         .Where(r => userIds.Contains(r.UserId))
                                         .ToListAsync();
            return Ok(requests);
        }
    }
}
