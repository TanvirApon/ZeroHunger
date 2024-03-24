using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class ResturantDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string location { get; set; }
        public string password { get; set; }
    }
}