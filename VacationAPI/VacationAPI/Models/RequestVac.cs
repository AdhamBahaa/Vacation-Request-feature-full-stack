using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VacationAPI.Models
{
    public class RequestVac
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("VacationType")]
        public int TypeId { get; set; }
        public VacationType? VacationType { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; } = 1;
        public Status? Status { get; set; }
    }
}
