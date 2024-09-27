using System.ComponentModel.DataAnnotations;

namespace VacationAPI.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string? StatusType { get; set; }
    }
}
