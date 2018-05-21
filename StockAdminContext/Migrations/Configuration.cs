using System.Collections.Generic;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

namespace StockAdminContext.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StockAdminContext.Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StockAdminContext.Models.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var documents = new List<Document>()
                              {
                                  new Document()
                                  {
                                      ObjType = "13",
                                      DocumentName = "Facturas Clientes",
                                      DocumentType = typeof(OINV_Sales)
                                  },
                                  new Document()
                                  {
                                      ObjType = "14",
                                      DocumentName = "Nota de Credito Deudores",
                                      DocumentType = typeof(ORIN_ClientCreditNotes),
                                  },
                                  new Document()
                                  {
                                      ObjType = "18",
                                      DocumentName= "Factura de Proveedores",
                                      DocumentType = typeof(OPCH_Purchase),
                                  },
                                  new Document()
                                  {
                                      ObjType = "19",
                                      DocumentName = "Nota de Credito proveedores",
                                      DocumentType = typeof(ORPC_SupplierCreditNotes),
                                  },

                                  new Document()
                                  {
                                      ObjType = "59",
                                      DocumentName = "Entradas de Mercancia",
                                      DocumentType = typeof(OIGN_GoodsReceipt),
                                  },
                                  new Document()
                                  {
                                    ObjType    = "60",
                                    DocumentName = "Salida de Mercancia",
                                    DocumentType = typeof(OIGE_GoodsIssues),
                                  },
                                  new Document()
                                  {
                                      ObjType = "67",
                                      DocumentName = "Transferencia de Stock",
                                      DocumentType = typeof(OWTR_Transfers),
                                  },
                                  new Document()
                                  {
                                      ObjType = "1250000001",
                                      DocumentName = "Solicitud de Traslado",
                                      DocumentType = typeof(OWTQ_TransferRequest)
                                  },
                                  new Document()
                                  {
                                      ObjType = "2",
                                      DocumentName = "Socios de Negocio",
                                      DocumentType = typeof(OCRD_BusinessPartner),
                                  },  
                                  new Document()
                                  {
                                      ObjType = "4",
                                      DocumentName = "Articulo",
                                      DocumentType = typeof(OITM_Articles),
                                  },
                                  new Document()
                                  {
                                      ObjType = "203",
                                      DocumentName = "Aticipos De Clientes",
                                      DocumentType = typeof(ODPI_DownPayment),
                                  }
                              };

            // Get New Documets to add in database
            var newDocuments = documents.Where(d => !context.Documents.Any(doc => doc.ObjType == d.ObjType)).ToList();

            newDocuments.ForEach(d => context.Documents.Add(d));

            context.SaveChanges();


            var users = Membership.CreateDefaultUsers();

            var newUsers = users.Where(u => !context.LocalUsers.Any(user => user.NickName == u.NickName)).ToList();

            newUsers.ForEach(u=> context.LocalUsers.Add(u));

            context.SaveChanges();


            // Report Types

            var reportMapping = new List<ReportMapping>()
                                {
                                    new ReportMapping()
                                    {
                                        DisplayName = "Ajustes", 
                                        ReportFileName = "rptAjustedeInventarios.rpt",
                                        StoredProcedureName = "SP_INV_REP_AJUSTES",
                                    }

                                };

            var newMappings =
                reportMapping.Where(m => !context.ReportMappings.Any(r => r.DisplayName == m.DisplayName)).ToList();

            newMappings.ForEach(m=> context.ReportMappings.Add(m));

            context.SaveChanges();

        }
    } 
}
