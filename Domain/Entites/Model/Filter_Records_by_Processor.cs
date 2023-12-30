using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites.Model
{
    public class Filter_Records_by_Processor
    {
        [Key]
        [Column("records_id")]
        public int recordId { get; set; }

        [Column("record_type")]
        public string? recordType { get; set; }
    }
}