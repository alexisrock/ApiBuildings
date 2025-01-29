using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Users")]
    public class Users
    {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Password { get; set; }

    }
}
