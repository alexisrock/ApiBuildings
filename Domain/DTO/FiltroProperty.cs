using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class FiltroProperty
    {

        public string? CodeInternal { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? IdOwner { get; set; }
        public int Pagina { get; set; }
        public int TamanioPagina { get; set; }

    }
}
