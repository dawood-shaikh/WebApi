using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class EntityDBContext : DbContext
    {
        public EntityDBContext(DbContextOptions options)
              : base(options)
        {
        }

        public DbSet<PaymentStatustbl> paymentStatustbls { get; set; }
    }
}
