using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiblioFrontOffice.Models;

namespace BiblioFrontOffice.Data
{
    public class BiblioFrontOfficeContext : DbContext
    {
        public BiblioFrontOfficeContext (DbContextOptions<BiblioFrontOfficeContext> options)
            : base(options)
        {
        }

        public DbSet<BiblioFrontOffice.Models.Reservation> Reservation { get; set; } = default!;
        public DbSet<BiblioFrontOffice.Models.Adherent> Adherent { get; set; } = default!;
        public DbSet<BiblioFrontOffice.Models.Document> Document { get; set; } = default!;
        public DbSet<BiblioFrontOffice.Models.Panier> Panier { get; set; } = default!;
        public DbSet<BiblioFrontOffice.Models.Detail> Detail { get; set; } = default!; 
        public DbSet<BiblioFrontOffice.Models.Status_Reservation> Status_Reservation { get; set; } = default!; 
    }
}
