using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SalesService.Models
{
    public class Revendedor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}