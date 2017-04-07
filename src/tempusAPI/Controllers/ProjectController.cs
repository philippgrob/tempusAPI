﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tempusAPI.Models;

namespace tempusAPI.Controllers
{
    [Route( ("api/[controller]"))]
    public class ProjectController:Controller
    {
        [HttpGet()]
        public List<Project> GetAll()
        {
            var repo = new BookingEfRepository();
            return repo.FindAllProjects();
        }

    }
}
