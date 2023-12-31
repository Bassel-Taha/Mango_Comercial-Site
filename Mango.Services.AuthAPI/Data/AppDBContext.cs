﻿using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUsers>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        //the EF wont create a new table for the application user it will alter the user table in the database cuz it know that this class inherit from the identity user class
        
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
