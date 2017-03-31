using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tempusAPI.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectDescription { get; set; }
        public string AccountingNumber { get; set; }

        public string ConfidentialStatus { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
