using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.Models
{
    public class ProdutoDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string RevendedorName { get; set; }
    }
}