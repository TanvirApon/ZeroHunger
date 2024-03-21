using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class ItemDTO
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        [Required]
        public int quantity { get; set; }

        // will be add some verification
        public string expiredate { get; set; }


        [Required]
        public Nullable<int> request_id { get; set; }
    }
}