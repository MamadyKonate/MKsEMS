using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class LeaveAllowance
    {
        [Key]
        public int Id { get; set; }
        public double Allowance { get; set; }
    }
}
