using Denso.HotSheet.Catalogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Denso.HotSheet.EntityFrameworkCore.Seed.Tenants
{
    public class TenantCatalogsBuilder
    {
        private readonly HotSheetDbContext _context;
        private readonly int _tenantId;

        public TenantCatalogsBuilder(HotSheetDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateDocumentTypes();
            CreatePaymentTerms();
            CreateDocumentStatuses();
            CreateHotSheetTerms();
            CreatePaidBy();
            CreateCatalogsBySqlScripts();
        }

        private void CreateDocumentTypes()
        {
            var airDocumentType = _context.DocumentTypes.IgnoreQueryFilters().FirstOrDefault(r => r.Id == 1);
            if (airDocumentType == null)
            {
                _context.DocumentTypes.Add(new DocumentType { Name = "Air Shipping Instruction/Instrucción de Embarque Aereo", IsActive = true });
            }

            var groundCountry = _context.DocumentTypes.IgnoreQueryFilters().FirstOrDefault(r => r.Id == 2);
            if (groundCountry == null)
            {
                _context.DocumentTypes.Add(new DocumentType { Name = "Ground Shipping Instruction/Instrucción de Embarque Terrestre", IsActive = true });
            }

            var seaCountry = _context.DocumentTypes.IgnoreQueryFilters().FirstOrDefault(r => r.Id == 3);
            if (seaCountry == null)
            {
                _context.DocumentTypes.Add(new DocumentType { Name = "Sea Shipping Instruction/Instrucción de Embarque Maritimo", IsActive = true });
            }

            _context.SaveChanges();
        }

        private void CreatePaymentTerms()
        {
            var paymentTerm1 = _context.PaymentTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Id == 1);
            if (paymentTerm1 == null)
            {
                _context.PaymentTerms.Add(new PaymentTerm { Name = "Remittence", TenantId = _tenantId, IsActive = true });
            }

            var paymentTerm2 = _context.PaymentTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Id == 2);
            if (paymentTerm2 == null)
            {
                _context.PaymentTerms.Add(new PaymentTerm { Name = "No Payment", TenantId = _tenantId, IsActive = true });
            }

            _context.SaveChanges();
        }

        private void CreateDocumentStatuses()
        {
            var documentStatuses1 = _context.DocumentStatuses.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "Draft");
            if (documentStatuses1 == null)
            {
                _context.DocumentStatuses.Add(new DocumentStatus { Name = "Draft" });
            }

            var documentStatuses2 = _context.DocumentStatuses.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "Circulating");
            if (documentStatuses2 == null)
            {
                _context.DocumentStatuses.Add(new DocumentStatus { Name = "Circulating" });
            }

            var documentStatuses3 = _context.DocumentStatuses.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "Approved");
            if (documentStatuses3 == null)
            {
                _context.DocumentStatuses.Add(new DocumentStatus { Name = "Approved" });
            }

            var documentStatuses4 = _context.DocumentStatuses.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "Cancelled");
            if (documentStatuses4 == null)
            {
                _context.DocumentStatuses.Add(new DocumentStatus { Name = "Cancelled" });
            }

            var documentStatuses5 = _context.DocumentStatuses.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "PendingForApproval");
            if (documentStatuses5 == null)
            {
                _context.DocumentStatuses.Add(new DocumentStatus { Name = "PendingForApproval" });
            }

            var documentStatuses6 = _context.DocumentStatuses.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "Rejected");
            if (documentStatuses6 == null)
            {
                _context.DocumentStatuses.Add(new DocumentStatus { Name = "Rejected" });
            }

            _context.SaveChanges();
        }

        private void CreateHotSheetTerms()
        {
            var item1 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "FOB");
            if (item1 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm {
                    Name = "FOB",
                    Description = "COSTO DE FLETE TERRESTRE HASTA EL PUERTO Y TRAMITE DE ADUANA MEXICANA CUBIERTO POR DNMX, RESPONSABILIDAD DE LA CARGA HASTA QUE SEA CARGADA EN EL BARCO (GROUND TRANSPORTATION, MEXICAN BROKER PAID BY DNMX)",
                    TenantId = _tenantId,
                    IsActive = true
                });
            }

            var item2 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "FCA");
            if (item2 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm { 
                    Name = "FCA", 
                    Description = "SOLO COSTO DE ADUANA MEXICANA PAGADA POR DNMX, FLETE Y ADUANA AMERICANA A CARGO DEL DESTINATARIO (MEXICAN BROKER PAID BY DNMX ONLY)", 
                    TenantId = _tenantId,
                    IsActive = true
                });
            }

            var item3 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "EXW");
            if (item3 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm { 
                    Name = "EXW", 
                    Description = "VENDIDO EN PLANTA, FLETE E IMPUESTOS DE IMPORTACION PAGADOS POR EL DESTINATARIO (SOLD AT DNMX PLANT, FREIGHT COLLECT)",
                    TenantId = _tenantId, 
                    IsActive = true });
            }

            var item4 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DDP");
            if (item4 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm {
                    Name = "DDP",
                    Description = "COSTO DE FLETE PAGADO POR DNMX, INCLUYENDO IMPUESTOS DE IMPORTACION EN DESTINO (SOLD AT CUSTOMER'S LOCATION INCLUDING IMPORT TAXES)",
                    TenantId = _tenantId,
                    IsActive = true
                });
            }

            var item5 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DAT");
            if (item5 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm {
                    Name = "DAT",
                    Description = "COSTO DE FLETE Y ADUANA MEXICANA PAGADOS POR DNMX (DNMX HotSheet RESPONSIBILITY UNTIL SEA TERMINAL) IMPORT TAXES AD DESTINATION NOT INCLUDED",
                    TenantId = _tenantId,
                    IsActive = true
                });
            }

            var item6 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DAP");
            if (item6 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm {
                    Name = "DAP",
                    Description = "COSTO DE FLETE Y ADUANA MEXICANA PAGADOS POR DNMX (DNMX FREIGHT RESPONSIBILITY UNTIL THE BORDER) IMPORT TAXES AT DESTINATION NOT INCLUDED",
                    TenantId = _tenantId,
                    IsActive = true
                });
            }

            var item7 = _context.HotSheetTerms.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "CIF");
            if (item7 == null)
            {
                _context.HotSheetTerms.Add(new HotSheetTerm {
                    Name = "CIF", 
                    Description = "COSTO DE FLETE TERRESTRE, SEGURO, ADUANA MEX. Y FLETE MARITIMO PAGADO POR DNMX(COST, INSURANCE AND FREIGHT PAID BY DNMX) OUR RESP. FINISH UNTIL DESTINATION PORT", 
                    TenantId = _tenantId,
                    IsActive = true
                });
            }

            _context.SaveChanges();
        }

        private void CreatePaidBy()
        {
            var item1 = _context.PaidBy.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DIAM");
            if (item1 == null)
            {
                _context.PaidBy.Add(new PaidBy { Name = "DIAM", TenantId = _tenantId, IsActive = true });
            }

            var item2 = _context.PaidBy.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DMMI");
            if (item2 == null)
            {
                _context.PaidBy.Add(new PaidBy { Name = "DMMI", TenantId = _tenantId, IsActive = true });
            }

            var item3 = _context.PaidBy.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DMTN");
            if (item3 == null)
            {
                _context.PaidBy.Add(new PaidBy { Name = "DMTN", TenantId = _tenantId, IsActive = true });
            }

            var item4 = _context.PaidBy.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DNJP");
            if (item4 == null)
            {
                _context.PaidBy.Add(new PaidBy { Name = "DNJP", TenantId = _tenantId, IsActive = true });
            }

            var item5 = _context.PaidBy.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "DNMX");
            if (item5 == null)
            {
                _context.PaidBy.Add(new PaidBy { Name = "DNMX", TenantId = _tenantId, IsActive = true });
            }

            var item6 = _context.PaidBy.IgnoreQueryFilters().FirstOrDefault(r => r.Name == "OTHER");
            if (item6 == null)
            {
                _context.PaidBy.Add(new PaidBy { Name = "OTHER", TenantId = _tenantId, IsActive = true });
            }

            _context.SaveChanges();
        }

        private void CreateCatalogsBySqlScripts()
        {
            var basePath = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory, string.Empty, SearchOption.AllDirectories)
                 .Where(s => s.ToLower().Contains("sql")).FirstOrDefault();

            if (!string.IsNullOrEmpty(basePath))
            {
                if (Directory.Exists(Path.Combine(basePath, "Seed")))
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(basePath, "Seed"), "*.sql"))
                    {
                        try
                        {
                            string strFile = System.IO.@File.ReadAllText(file).Replace("GO", "").Replace("{", "{{").Replace("}", "}}");
                            _context.Database.ExecuteSqlRaw(strFile);
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

    public class CatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
