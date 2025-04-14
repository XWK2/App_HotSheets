using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Denso.HotSheet.EntityFrameworkCore
{
    public static class HotSheetDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HotSheetDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HotSheetDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
