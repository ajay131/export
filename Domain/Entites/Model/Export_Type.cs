using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites.Model
{
    public class Export_Type
    {
        [Key]
        [Column("export_id")]
        public int exportId { get; set; }

        [Column("export_type")]
        public string? exportType { get; set; }
    }
}