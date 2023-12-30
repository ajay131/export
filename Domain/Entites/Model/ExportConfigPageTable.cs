using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entites.Model
{
    [Table("ExportConfigPage")]
    public class ExportConfigPageTable
    {
        [Key]
        [Column("ConfigId")]
        public int ConfigId { get; set; }

        [Column("product_id")]
        public int? ProductType { get; set; }

         [Column("export_id")]
        public int? ExportType { get; set; }

        [Column("export_format_id")]
        public int? ExportFormat { get; set; }

        [Column("Template_Name")]
        public string? TemplateName { get; set; } 

        [Column("Delimiter")]
        public string? Delimiter { get; set; }

        [Column("Blank_Lines_From_Top")]
        public int? blankLines { get; set; }

        [Column("records_id")]
        public int? filterRecords { get; set; }

         [Column("email_template_id")]
        public int? EmailTemplate { get; set; }

        [Column("ColumnNamesInFirstDataRow")]
        public bool? columnNamesInFirstRow { get; set; }

        [Column("Is_Header")]
        public bool? IsHeader { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; }

        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }

        [Column("created_by")]
        public Guid? CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Column("updated_by")]
        public Guid? UpdatedBy { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Column("authorized_by")]
        public Guid? AuthorizedBy { get; set; }

        [Column("authorized_date")]
        public DateTime? AuthorizedDate { get; set; }

        [Column("status")]
        public int? Status { get; set; }

        [Column("sequence")]
        public int? Sequence { get; set; }

        [Column("version")]
        public int? Version { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("dateCheckbox")]
        public bool? isIncludedDate { get; set; }

        [Column("Date")]
        public int? dateSequence { get; set; }

        [Column("transactionCheckbox")]
        public bool? isIncludedNoOfTransactions { get; set; }

        [Column("transactionSequenceNoInput")]
        public int? noOfTransactionSequence { get; set; }

        [Column("totalTransactionCheckbox")]
        public bool? isIncludedTotalTransactionValue { get; set; }

        [Column("transactionValue")]
        public int? totalTransactionValueSequence { get; set; }

        // public static implicit operator ExportConfigPageTable(int v)
        // {
        //     throw new NotImplementedException();
        // }
        // public ICollection<ExportParameterSelectionPage> ExportParameterSelectionPage { get; set; }

    }
}