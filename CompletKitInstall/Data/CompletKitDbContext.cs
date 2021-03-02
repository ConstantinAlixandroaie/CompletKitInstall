using CompletKitInstall.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompletKitInstall.Data
{
    public class CompletKitDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public CompletKitDbContext(DbContextOptions<CompletKitDbContext> options)
            : base(options)
        {
        }
    }
}
