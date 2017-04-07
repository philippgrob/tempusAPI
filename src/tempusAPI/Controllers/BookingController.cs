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
        
        [HttpGet("/id/{id}")]
        public Booking GetBookingById(int id)
        {
            var repo = new BookingEfRepository();
            return repo.GetBookingById(id);
        }

        //http://localhost:5001/api/booking?employeeId=2&beginDate=2013-04-23T18:25:43.511Z&completed=true
        [HttpGet()]
        public List<Booking> GetBookingsById([FromQuery]int employeeId, [FromQuery] string beginDate, [FromQuery] Boolean completed)
        {
           var repo = new BookingEfRepository();
           return repo.FindBookingByEmployeeIdDateRestrictedByDateAndCompletion(employeeId, beginDate, completed);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            var repo = new BookingEfRepository();
            repo.SaveBooking(booking);

            return CreatedAtAction(nameof(GetBookingById), new {id = booking.BookingId},booking);
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] Booking booking)
        {
            var repo = new BookingEfRepository();
            repo.PatchBooking(booking);

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
        }

        [HttpDelete]
        public void Remove(int id)
        {
            var repo = new BookingEfRepository();
            repo.RemoveBooking(id);
        }

    }
}
