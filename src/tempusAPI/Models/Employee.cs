using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tempusAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public String UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
