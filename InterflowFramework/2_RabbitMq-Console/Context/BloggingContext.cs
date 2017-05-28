using _2_RabbitMq_Console.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_RabbitMq_Console.Context
{
	public class BloggingContext : DbContext
	{
		public BloggingContext()
		{ }

		public BloggingContext(DbContextOptions<BloggingContext> options)
			: base(options)
		{ }

		public DbSet<Blog> Blogs { get; set; }

		#region OnConfiguring
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
			}
		}
		#endregion
	}
}
