using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Denso.HotSheet.EntityFrameworkCore.Seed
{
    public static class SQLObjectsHelper
    {
        public static void CreateSQLObjects(HotSheetDbContext context)
        {
            var basePath = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory, string.Empty, SearchOption.AllDirectories)
                 .Where(s => s.ToLower().Contains("sql")).FirstOrDefault();

            if (!string.IsNullOrEmpty(basePath))
            {
                //Clean all sql objects
                if (Directory.Exists(Path.Combine(basePath, "Clean")))
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(basePath, "Clean"), "*.sql"))
                    {
                        // Try to drop proc if its already created
                        // Without this, for new procs, seed method fail on trying to delete
                        try
                        {
                            string strFile = @File.ReadAllText(file).Replace("GO", "").Replace("{", "{{").Replace("}", "}}");
                            context.Database.ExecuteSqlRaw(strFile);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                }

                // Add Functions
                if (Directory.Exists(Path.Combine(basePath, "Functions")))
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(basePath, "Functions"), "*.sql"))
                    {
                        try
                        {
                            string strFile = @File.ReadAllText(file).Replace("GO", "").Replace("{", "{{").Replace("}", "}}");
                            context.Database.ExecuteSqlRaw(strFile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(file);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                // Add Views
                if (Directory.Exists(Path.Combine(basePath, "Views")))
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(basePath, "Views"), "*.sql"))
                    {
                        try
                        {
                            string strFile = @File.ReadAllText(file).Replace("GO", "").Replace("{", "{{").Replace("}", "}}");
                            context.Database.ExecuteSqlRaw(strFile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(file);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                // Add Stored Proceures
                if (Directory.Exists(Path.Combine(basePath, "StoredProcedures")))
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(basePath, "StoredProcedures"), "*.sql"))
                    {
                        try
                        {
                            string strFile = @File.ReadAllText(file).Replace("GO", "").Replace("{", "{{").Replace("}", "}}");
                            context.Database.ExecuteSqlRaw(strFile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(file);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                // Add Tiggers
                if (Directory.Exists(Path.Combine(basePath, "Triggers")))
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(basePath, "Triggers"), "*.sql"))
                    {
                        try
                        {
                            string strFile = @File.ReadAllText(file).Replace("GO", "").Replace("{", "{{").Replace("}", "}}");
                            context.Database.ExecuteSqlRaw(strFile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(file);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
