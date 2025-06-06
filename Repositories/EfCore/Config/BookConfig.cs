﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EfCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, CategoryId =1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Book { Id = 2,CategoryId =2, Title = "Mesnevi", Price = 175 },
                new Book { Id = 3,CategoryId=1 , Title = "Dede Korkut", Price = 95 }

                );

        }
    }
}
