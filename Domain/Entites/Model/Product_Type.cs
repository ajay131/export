using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entites.Model
{
    public class Product_Type
    {
         [Key]
        [Column("product_id")]
        public int productId { get; set; }

        [Column("product_type")]
        public string? productType { get; set; }
    }
}