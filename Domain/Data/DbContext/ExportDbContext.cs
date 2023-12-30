using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entites.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data.DbContexts
{
    public class ExportDbContext : DbContext
    {
        public ExportDbContext()
        { }

        public ExportDbContext(DbContextOptions<ExportDbContext> options)
            : base(options)
        { }

        public virtual DbSet<DemandTerminal> DemandTerminal { get; set; }
        public virtual DbSet<ExportConfigPageTable> ExportConfigPageTables {get; set;}
        public virtual DbSet<ExportParameterSelectionPage> ExportParameterSelectionPages {get; set;}
        public DbSet<Product_Type> product_type { get; set; }

        public DbSet<Export_Type> export_type { get; set; }

        public DbSet<Export_Format> export_format { get; set; }
        
        public DbSet<Filter_Records_by_Processor> filter_Records_by_Processor { get; set; }

        public DbSet<Email_Template> email_template { get; set; }

    
       
        
    }
}