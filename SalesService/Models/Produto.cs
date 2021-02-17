using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesService.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        // Chave Estrangeira
        public int RevendedorId { get; set; }
        
        public virtual Revendedor Revendedor { get; set; }

    }
}