using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.MyConsoleApp.Data
{
    public class TodoAppDbContext : AbpDbContext<TodoAppDbContext>
    {

        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

          
            /* Configure your own entities here */
            /* Configure your own tables/entities inside here */
            builder.Entity<TodoItem>(b =>
            {
                b.ToTable("TodoItems");
            });
        }
    }
}
