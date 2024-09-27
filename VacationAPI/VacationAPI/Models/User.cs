using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CasualDays { get; set; } = 6;
        public int AnnualDays { get; set; } = 24;
        public required string Email { get; set; }
        public  required string Password { get; set; }
        public int? ManagerId { get; set; }
    }
}
