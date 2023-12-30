using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entites.Model
{
    public class Email_Template
    {
        [Key]
        [Column("email_template_id")]
       // [JsonPropertyName("gd")]
        public int emailTemplateId { get; set; }

        [Column("email_template")]
        public string? emailTemplate { get; set; }
    }
}