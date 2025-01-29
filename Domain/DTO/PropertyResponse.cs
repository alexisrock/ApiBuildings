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
    public record PropertyResponse
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }     
        public int IdOwner { get; set; }
 
    }
}
