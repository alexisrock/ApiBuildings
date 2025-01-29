using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Configuration")]
    public class Configuration
    {

        [Key]
        public string Id { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;

    }
}
