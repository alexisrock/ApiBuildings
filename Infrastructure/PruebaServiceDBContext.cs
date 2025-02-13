using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PruebaServiceDBContext : DbContext
    {
        public PruebaServiceDBContext(DbContextOptions<PruebaServiceDBContext> options) : base(options) { }


   

        public virtual DbSet<Configuration>? Configuraciones { get; set; }
        public virtual DbSet<Users>? Users { get; set; }
        public virtual DbSet<Owner>? Owners { get; set; }
        public virtual DbSet<Property>? Propertys { get; set; }
        public virtual DbSet<PropertyImage>? PropertyImages { get; set; }
        public virtual DbSet<PropertyTrace>? PropertyTraces { get; set; }



    }

}

