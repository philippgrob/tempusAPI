using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tempusAPI.Models;

namespace tempusAPI.Controllers
{
    [Route("api/[controller]")]
        public class EmployeeController : Controller
        {
            [HttpGet("{userName}")]
            public Employee GeEmployeeByUserName(string userName)
            {
                var repo = new BookingEfRepository();
                return repo.GetEmployeeByUserName(userName);
            }
        }
}
