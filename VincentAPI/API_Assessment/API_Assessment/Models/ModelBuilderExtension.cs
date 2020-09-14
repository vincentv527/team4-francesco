using API_Assessment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_API.Model
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Author = "Martin", Price = 21, Title = "The" },
                new Book { Id = 2, Author = "Shawn", Price = 27, Title = "quick" },
                new Book { Id = 3, Author = "Kate", Price = 20, Title = "brown" },
                new Book { Id = 4, Author = "Josh", Price = 29, Title = "fox" },
                new Book { Id = 5, Author = "Vin", Price = 31, Title = "jumped" },
                new Book { Id = 6, Author = "Ashton", Price = 42, Title = "over" });

        }
    }
}