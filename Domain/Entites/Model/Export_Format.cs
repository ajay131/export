using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites.Model
{
    public class Export_Format
    {
        [Key]
        [Column("export_format_id")]
        public int exportFormatId { get; set; }

        [Column("export_format")]
        public string? exportFormat { get; set; }
    }
}