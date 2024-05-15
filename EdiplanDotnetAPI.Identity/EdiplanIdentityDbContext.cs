﻿using EdiplanDotnetAPI.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EdiplanDotnetAPI.Identity;
internal class EdiplanIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public EdiplanIdentityDbContext()
    {
        
    }

    public EdiplanIdentityDbContext(DbContextOptions<EdiplanIdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();
}
