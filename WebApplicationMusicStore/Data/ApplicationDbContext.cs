﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationMusicStore.Models;

namespace WebApplicationMusicStore.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<WebApplicationMusicStore.Models.Music> Music { get; set; }
		public DbSet<WebApplicationMusicStore.Models.Song> Song { get; set; }
		public DbSet<WebApplicationMusicStore.Models.Customer> Customers { get; set; }
	}
}
