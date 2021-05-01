using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database
{
    public sealed class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=pwcAssignment;User ID=sa;Password=123456789");

            return new Context(builder.Options, new IdentityService(new HttpContextAccessor()));
        }
    }
}
