using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VacationAPI.Models
{
    public class VacationType
    {
        [Key]
        public int Id { get; set; }
        public string? VacType { get; set; }
    }
}
