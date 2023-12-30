using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entites.Model
{
    [Table("ExportParameterSelectionPage")]
    public class ExportParameterSelectionPage
    {
        [Key]
        [Column("ParameterId")]
        public int ParameterId { get; set; }

        [Column("ExportType")]
        public string? exportTypeParameter { get; set; }

        [Column("FieldName")]
        public string transactionDataParameter { get; set; }

        [Column("Include")]
        public bool? includeParameter { get; set; }

        [Column("Sequence")]
        public int? sequenceExportType { get; set; }

        [Column("ConfigId")]
        public int ConfigId { get; set; }

        // [ForeignKey("ConfigId")]
        // public ExportConfigPageTable ExportConfigPageTable { get; set; }



    }
}