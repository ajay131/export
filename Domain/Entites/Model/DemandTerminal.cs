using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entites.Model
{
    [Table("Demand_Terminal")]
    public class DemandTerminal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationId { get; set; }

        [Column("MID")]
        public string? MID { get; set; }

        [Column("TID")]
        public string? TID { get; set; }

        [Column("CustCode")]
        public string? CustCode { get; set; }

        public string? CompanyName { get; set; }

        [Column("RepaymentAmount")]
        public decimal? RepaymentAmount { get; set; }

        public decimal? Variance { get; set; }

        public decimal? FeesDue { get; set; }

        public decimal? ChargesDue { get; set; }

        public decimal? NetPaylaterDue { get; set; }

        [Column("PaylaterAppId")]
        public string? PaylaterAppId { get; set; }

        public decimal? TodayPaylaterCollection { get; set; }

        [Column("TodayCollectedPaylaterAppId")]
        public string? TodayCollectedPaylaterAppId { get; set; }

        public bool? NachTriggered { get; set; }

        public bool? ChequeDeposited { get; set; }

        public decimal? TotalDue { get; set; }

        public decimal? FinalDue { get; set; }

        [Column("LastEMIDate")]
        public DateTime? LastEMIDate { get; set; }

        [Column("PaylaterNEFTUnclearAmount")]
        public decimal? PaylaterNEFTUnclearAmount { get; set; }

        [Column("NEFTUnclearAmount")]
        public decimal? NEFTUnclearAmount { get; set; }

        [Column("ParentChildType")]
        public string? ParentChildType { get; set; }

        [Column("ChildID")]
        public string? ChildID { get; set; }

        [Column("ChildEMI")]
        public decimal? ChildEMI { get; set; }

        [Column("CHILDVariance")]
        public decimal? CHILDVariance { get; set; }

        [Column("CHILDFees")]
        public decimal? CHILDFees { get; set; }

        [Column("ChildCharges")]
        public decimal? ChildCharges { get; set; }

        [Column("ParentEMI")]
        public decimal? ParentEMI { get; set; }

        [Column("ParentVariance")]
        public decimal? ParentVariance { get; set; }

        [Column("ParentFees")]
        public decimal? ParentFees { get; set; }

        [Column("ParentCharges")]
        public decimal? ParentCharges { get; set; }

        [Column("ExgratiaAmount")]
        public decimal? ExgratiaAmount { get; set; }
    }
}