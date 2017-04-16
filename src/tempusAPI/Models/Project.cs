using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tempusAPI.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
