using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using ntu.xzmcwjzs.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace ntu.xzmcwjzs.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<xzmcwjzs.EntityFramework.xzmcwjzsDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "xzmcwjzs";
        }

        protected override void Seed(xzmcwjzs.EntityFramework.xzmcwjzsDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();

                new DefaultTestDataForTask(context).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
