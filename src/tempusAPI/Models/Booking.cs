using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tempusAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("Bookings")]
        public Project Project { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Bookings")]
        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        }
}
