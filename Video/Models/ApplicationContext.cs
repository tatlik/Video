﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Video.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Infovideo> Infovideos { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
           Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
