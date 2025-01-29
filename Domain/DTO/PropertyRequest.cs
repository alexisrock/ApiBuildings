using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DTO
{
    public record PropertyRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }     
        public int IdOwner { get; set; }
        public string FileImage { get; set; } = string.Empty;
        public bool? Enabled { get; set; }
        public DateTime? DateSale { get; set; }
        public string NameTrace { get; set; } = string.Empty;
        public decimal? Value { get; set; }
        public decimal? Tax { get; set; }

    }

    public record PropertyUpdateRequest
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }

    public record PropertyImagesUpdateRequest     
    {
        public int IdPropertyImage { get; set; }      
        public int IdProperty { get; set; }    
        public string FileImage { get; set; } = string.Empty;
        public bool? Enabled { get; set; }

    }

    public record PropertyTraceUpdateRequest
    {
        public int IdPropertyTrace { get; set; }      
        public int IdProperty { get; set; }
        public DateTime? DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Value { get; set; }
        public decimal? Tax { get; set; }
    }

}
