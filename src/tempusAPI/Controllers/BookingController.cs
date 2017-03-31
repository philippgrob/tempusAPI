using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tempusAPI.Models;

namespace tempusAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookingController:Controller
    {
        [HttpGet()]
        public List<Booking> GetAll()
        {
            var repo = new BookingEFRepository();
            return repo.FindAllBookings();
        }

        [HttpGet("employee")]
        public List<Employee> GetEmployees()
        {
            var repo = new BookingEFRepository();
            return repo.FindAllEmployees();
        }
        
        [HttpGet("project")]
        public List<Project> GetProjects()
        {
            var repo = new BookingEFRepository();
            return repo.FindAllProjects();
        }

        [HttpGet("employee/id/{id}")]
        public Employee GetEmployee(int id)
        {
            var repo = new BookingEFRepository();
            return repo.GetEmployeeById(id);
        }

        [HttpGet("booking/{id}")]
        public Booking GetBookingById(int id)
        {
            var repo = new BookingEFRepository();
            return repo.GetBookingById(id);
        }


        [HttpGet("employee/userName/{userName}")]
        public Employee GetEmployee(String userName)
        {
            var repo = new BookingEFRepository();
            return repo.GetEmployeeByUserName(userName);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            var repo = new BookingEFRepository();
            repo.SaveBooking(booking);

            return CreatedAtAction(nameof(GetBookingById), new {id = booking.BookingId},booking);
        }

    }
}
