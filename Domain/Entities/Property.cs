using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Property")]
    public class Property
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }
        [Required]
        [ForeignKey("Owner")]
        public int IdOwner { get; set; }
        [ForeignKey("IdOwner")]
        public Owner? Owner { get; set; }

    }


    [Table("PropertyImage")]
    public class PropertyImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyImage { get; set; }
        [ForeignKey("Property")]
        public int IdProperty { get; set; }
        [ForeignKey("IdProperty")]
        public Property? Property { get; set; }
        public string FileImage { get; set; } = string.Empty;
        public bool? Enabled { get; set; } 

    }



    [Table("PropertyTrace")]
    public class PropertyTrace
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyTrace { get; set; }
        [ForeignKey("Property")]
        public int IdProperty { get; set; }
        [ForeignKey("IdProperty")]
        public Property? Property { get; set; }
        public DateTime? DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Value { get; set; }
        public decimal? Tax { get; set; }


    }






}

