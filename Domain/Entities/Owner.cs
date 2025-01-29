using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    [Table("Owner")]
    public class Owner
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOwner { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string? Address { get; set; }
        public string? Photo { get; set; } 
        public DateTime? Birthday { get; set;}       
    }
}
