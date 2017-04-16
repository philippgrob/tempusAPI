using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tempusAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Completed { get; set; }
    }
}
