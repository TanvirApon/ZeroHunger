using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class RequestDTO
    {
        public int id { get; set; }

        public string request_time { get; set; }

        public string collection_time { get; set; }

        public string status { get; set; }

        public Nullable<int> restaurant_id { get; set; }

        public Nullable<int> employee_id { get; set; }
    }
}