using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.Models
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RevendedorName { get; set; }
    }
}