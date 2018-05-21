using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;


namespace StockAdminContext.Models
{
    public partial class ApplicationContext : DbContext
    {
        //static SIGIContext()
        //{
        //    Database.SetInitializer<SIGIContext>(null);
        //} 
        public ApplicationContext()
            : base("Name=SIGIContext")
        {
            //Database.SetInitializer<SIGIContext>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
            Configuration.AutoDetectChangesEnabled = true;
          
        }

          public ApplicationContext(string connectionStringName)
            : base(connectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }

          public ApplicationContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }

        //public override int SaveChanges()
        //{
        //    var changedEntries = this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

        //    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
        //    {
        //        foreach (var property in entry.CurrentValues.PropertyNames)
        //        {
        //           entry.Property(property).CurrentValue = 
        //        }
        //    }


        //    return base.SaveChanges();
        //}

        public DbSet<ALOHA_Articles> ALOHA_Articles { get; set; }
        public DbSet<ATC1_Attachments> ATC1_Attachments { get; set; }
        public DbSet<AuthorizationLog> AuthorizationLogs { get; set; }
        public DbSet<CashMissing> CashMissings { get; set; }
        public DbSet<Checkbook> Checkbooks { get; set; }
        public DbSet<CanceledCheckBook> CanceledCheckbooks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DetailInvoiceALOHA> DetailInvoiceALOHAs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DPI1_DownPaymentDetail> DPI1_DownPaymentDetail { get; set; }
        public DbSet<IGE1_GoodsIssueDetail> IGE1_GoodsIssueDetail { get; set; }
        public DbSet<IGN1_GoodsReceiptDetail> IGN1_GoodsReceiptDetail { get; set; }
        public DbSet<INV1_SalesDetail> INV1_SalesDetail { get; set; }
        public DbSet<InvoiceALOHA> InvoiceALOHAs { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<NNM1_Series> NNM1_Series { get; set; }
        public DbSet<OCRD_BusinessPartner> OCRD_BusinessPartner { get; set; }
        public DbSet<ODPI_DownPayment> ODPI_DownPayment { get; set; }
        public DbSet<OIGE_GoodsIssues> OIGE_GoodsIssues { get; set; }
        public DbSet<OIGN_GoodsReceipt> OIGN_GoodsReceipt { get; set; }
        public DbSet<OINM_Transaction> OINM_Transaction { get; set; }
        public DbSet<OINV_Sales> OINV_Sales { get; set; }
        public DbSet<OITB_Groups> OITB_Groups { get; set; }
        public DbSet<OITM_ALOHA> OITM_ALOHA { get; set; }
        public DbSet<OITM_Articles> OITM_Articles { get; set; }
        public DbSet<OITW_BranchArticles> OITW_BranchArticles { get; set; }
        public DbSet<OPCH_Purchase> OPCH_Purchase { get; set; }
        public DbSet<ORDR_SpecialOrder> ORDR_SpecialOrder { get; set; }
        public DbSet<ORIN_ClientCreditNotes> ORIN_ClientCreditNotes { get; set; }
        public DbSet<ORPC_SupplierCreditNotes> ORPC_SupplierCreditNotes { get; set; }
        public DbSet<OWHS_Branch> OWHS_Branch { get; set; }
        public DbSet<OWTQ_TransferRequest> OWTQ_TransferRequest { get; set; }
        public DbSet<OWTR_Transfers> OWTR_Transfers { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PCH1_PurchaseDetail> PCH1_PurchaseDetail { get; set; }
        public DbSet<RDR1_SpecialOrderDetail> RDR1_SpecialOrderDetail { get; set; }
        public DbSet<RIN1_ClientCreditNoteDetail> RIN1_ClientCreditNoteDetail { get; set; }
        public DbSet<RPC1_SupplierCreditNoteDetail> RPC1_SupplierCreditNoteDetail { get; set; }
        public DbSet<InventoryCount> InventoryCount { get; set; }
        public DbSet<InventoryCountDetail> InventoryCountDetail { get; set; }


        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<UCake> UCakes { get; set; }
        public DbSet<UCategory> UCategories { get; set; }
        public DbSet<UWarehouseOrder> UWarehouseOrders { get; set; }
        public DbSet<UColor_Covert> UColor_Covert { get; set; }
        public DbSet<UColor_Flower> UColor_Flower { get; set; }
        public DbSet<UColor_Ribbon> UColor_Ribbon { get; set; }
        public DbSet<UColor_top> UColor_top { get; set; }
        public DbSet<UCover> UCovers { get; set; }
        public DbSet<UEspecialty> UEspecialties { get; set; }
        public DbSet<UFlower> UFlowers { get; set; }
        public DbSet<UMovement> UMovements { get; set; }
        public DbSet<UMovements_Acc> UMovements_Acc { get; set; }
        public DbSet<UserAuthorizationSet> UserAuthorizationSets { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<UStyle> UStyles { get; set; }
        public DbSet<Ufiller> Ufillers { get; set; }
        public DbSet<UColor_Height> UColor_Height { get; set; }
        public DbSet<ULaza> ULaza { get; set; }
        public DbSet<UGuirnalda> UGuirnalda { get; set; }
        public DbSet<WTQ1_TransferRequestDetails> WTQ1_TransferRequestDetails { get; set; }
        public DbSet<WTR1_TransferDetails> WTR1_TransferDetails { get; set; }
        public DbSet<LOG_CHANGES> LOG_CHANGES { get; set; }
        public DbSet<PaymentInvoiceALOHA> PaymentInvoiceALOHA { get; set; }

        public DbSet<ReportMapping> ReportMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, StockAdminContext.Migrations.Configuration>());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
             
            //modelBuilder.Entity<ALOHA_Articles>().HasRequired(t => t.DetailInvoiceALOHA)
            //   .WithMany(t => t.ALOHA_Articles)
            //   .HasForeignKey(d => d.DetailInvoiceALOHAIdDetailInvoiceALOHA);


            modelBuilder.Entity<ATC1_Attachments>().HasRequired(t => t.ORDR_SpecialOrder)
                .WithMany(t => t.ATC1_Attachments)
                .HasForeignKey(d => d.IdEspecialOrder);


            modelBuilder.Entity<Checkbook>().HasMany(t => t.SeriesCheckbook)
                .WithMany(t => t.Checkbooks)
                .Map(m =>
                    {
                        m.ToTable("SeriesCheckbook");
                        m.MapLeftKey("IdCheckbookL");
                        m.MapRightKey("Series");
                    });

            modelBuilder.Entity<DetailInvoiceALOHA>().HasRequired(t => t.InvoiceALOHA)
                .WithMany(t => t.DetailInvoiceALOHAs)
                .HasForeignKey(d => d.IdInvoiceALOHA);
            modelBuilder.Entity<DetailInvoiceALOHA>().HasRequired(t => t.ALOHA_Articles)
               .WithMany(t => t.DetailInvoiceALOHAs)
               .HasForeignKey(d => d.IdALOHA_Articles).WillCascadeOnDelete(false);

            modelBuilder.Entity<DPI1_DownPaymentDetail>()
            .HasRequired(t => t.ODPI_DownPayment)
               .WithMany(t => t.DPI1_DownPaymentDetail)
               .HasForeignKey(d => d.IdDownPaymentL);
           

            modelBuilder.Entity<IGE1_GoodsIssueDetail>()
            .HasRequired(t => t.OIGE_GoodsIssues)
               .WithMany(t => t.IGE1_GoodsIssueDetail)
               .HasForeignKey(d => d.IdGoodIssueL);

            modelBuilder.Entity<IGE1_GoodsIssueDetail>()
            .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.IGE1_GoodsIssueDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<IGN1_GoodsReceiptDetail>()
            .HasRequired(t => t.OIGN_GoodsReceipt)
                .WithMany(t => t.IGN1_GoodsReceiptDetail)
                .HasForeignKey(d => d.IdGoodReceiptL);
            modelBuilder.Entity<IGN1_GoodsReceiptDetail>()
            .HasRequired(t => t.OITM_Articles)
                .WithMany(t => t.IGN1_GoodsReceiptDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);

            //modelBuilder.Entity<IGN1_GoodsReceiptDetail>()
            //.HasOptional(d => d.BranchArticle)
            //.WithMany()
            //.HasForeignKey(r => new { r.ItemCode, r.WhsCode });
            

            modelBuilder.Entity<OINV_Sales>()
            .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OINV_Sales)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);


            modelBuilder.Entity<INV1_SalesDetail>()
                .HasRequired(t => t.OINV_Sales)
                .WithMany(t => t.INV1_SalesDetail)
                .HasForeignKey(d => d.IdSaleL);
            modelBuilder.Entity<INV1_SalesDetail>()
            .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.INV1_SalesDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<OINV_Sales>()
            .HasOptional(t => t.PaymentType)
                .WithMany(t => t.OINV_Sales)
                .HasForeignKey(d => d.IdPaymentType).WillCascadeOnDelete(false);
            modelBuilder.Entity<OINV_Sales>()
            .HasOptional(t => t.RoyaltyPaymentType)
                .WithMany(t => t.OINV_RoyaltiesSales)
                .HasForeignKey(d => d.IdRoyalityPayementType).WillCascadeOnDelete(false);


            modelBuilder.Entity<ODPI_DownPayment>()
            .HasOptional(t => t.OWHS_Branch)
               .WithMany(t => t.ODPI_DownPayment)
               .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OIGE_GoodsIssues>()
                .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OIGE_GoodsIssues)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OIGE_GoodsIssues>()
             .HasOptional(t => t.Movement)
             .WithMany()
             .HasForeignKey(t => t.IdMovement).WillCascadeOnDelete(false);

            modelBuilder.Entity<OIGE_GoodsIssues>()
               .HasOptional(t => t.Group)
               .WithMany()
               .HasForeignKey(t => t.ItmsGrpCod).WillCascadeOnDelete(false);

            modelBuilder.Entity<OIGN_GoodsReceipt>()
                 .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OIGN_GoodsReceipt)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OIGN_GoodsReceipt>()
                .HasOptional(t => t.Movement)
                .WithMany()
                .HasForeignKey(t => t.IdMovement).WillCascadeOnDelete(false);

            modelBuilder.Entity<OIGN_GoodsReceipt>()
                .HasOptional(t => t.Group)
                .WithMany()
                .HasForeignKey(t => t.ItmsGrpCod).WillCascadeOnDelete(false);

             modelBuilder.Entity<OITM_ALOHA>()
            .HasRequired(t => t.ALOHA_Articles)
              .WithMany(t => t.OITM_ALOHA)
              .HasForeignKey(d => d.IdALOHA_Articles).WillCascadeOnDelete(false);
             modelBuilder.Entity<OITM_ALOHA>()
                 .HasRequired(t => t.OITM_Articles)
                 .WithMany(t => t.OITM_ALOHA)
                 .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OITM_Articles>()
            .HasOptional(t => t.OITB_Groups)
                .WithMany(t => t.OITM_Articles)
                .HasForeignKey(d => d.ItmsGrpCod).WillCascadeOnDelete(false);

            modelBuilder.Entity<OITW_BranchArticles>()
                .HasRequired(t => t.OITM_Articles)
               .WithMany(t => t.OITW_BranchArticles)
               .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<OITW_BranchArticles>()
                .HasRequired(t => t.OWHS_Branch)
                .WithMany(t => t.OITW_BranchArticles)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OPCH_Purchase>()
                .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OPCH_Purchase)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                 .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.ORDR_SpecialOrder)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);
            
            //modelBuilder.Entity<ORIN_ClientCreditNotes>()
            //    .HasOptional(t => t.OWHS_Branch)
            //    .WithMany(t => t.ORIN_ClientCreditNotes)
            //    .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORPC_SupplierCreditNotes>()
            .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.ORPC_SupplierCreditNotes)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OWTQ_TransferRequest>()
            .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OWTQ_TransferRequest)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<OWTR_Transfers>()
                .HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OWTR_Transfers)
                .HasForeignKey(d => d.WhsCode).WillCascadeOnDelete(false);

            modelBuilder.Entity<PCH1_PurchaseDetail>()
                .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.PCH1_PurchaseDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<PCH1_PurchaseDetail>()
                .HasRequired(t => t.OPCH_Purchase)
                .WithMany(t => t.PCH1_PurchaseDetail)
                .HasForeignKey(d => d.IdPurchase);

            modelBuilder.Entity<RDR1_SpecialOrderDetail>()
                .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.RDR1_SpecialOrderDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<RDR1_SpecialOrderDetail>()
                .HasRequired(t => t.ORDR_SpecialOrder)
                .WithMany(t => t.RDR1_SpecialOrderDetail)
                .HasForeignKey(d => d.IdSpecialOrder);
            
            modelBuilder.Entity<RIN1_ClientCreditNoteDetail>()
            .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.RIN1_ClientCreditNoteDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<RIN1_ClientCreditNoteDetail>()
                .HasRequired(t => t.ORIN_ClientCreditNotes)
                .WithMany(t => t.RIN1_ClientCreditNoteDetail)
                .HasForeignKey(d => d.IdClientCreditNoteL);


            modelBuilder.Entity<RPC1_SupplierCreditNoteDetail>()
                .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.RPC1_SupplierCreditNoteDetail)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<RPC1_SupplierCreditNoteDetail>()
                .HasRequired(t => t.ORPC_SupplierCreditNotes)
                .WithMany(t => t.RPC1_SupplierCreditNoteDetail)
                .HasForeignKey(d => d.IdSupplierCreditNote);

            modelBuilder.Entity<WTQ1_TransferRequestDetails>()
            .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.WTQ1_TransferRequestDetails)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<WTQ1_TransferRequestDetails>()
                .HasRequired(t => t.OWTQ_TransferRequest)
                .WithMany(t => t.WTQ1_TransferRequestDetails)
                .HasForeignKey(d => d.IdTransferRequestL);

            modelBuilder.Entity<WTR1_TransferDetails>()
                .HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.WTR1_TransferDetails)
                .HasForeignKey(d => d.ItemCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<WTR1_TransferDetails>()
                .HasRequired(t => t.OWTR_Transfers)
                .WithMany(t => t.WTR1_TransferDetails)
                .HasForeignKey(d => d.IdTransferL);
            modelBuilder.Entity<InventoryCountDetail>()
              .HasRequired(t => t.InventoryCount)
              .WithMany(t => t.InventoryCountDetail)
              .HasForeignKey(d => d.IdInventoryCountL);

            modelBuilder.Entity<CanceledCheckBook>()
                .HasRequired(t => t.Checkbook)
                .WithMany(t => t.CanceledCheckBooks)
                .HasForeignKey(t => t.IdCheckBookL).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Deposit>()
            //    .HasRequired(t => t.Serie)
            //    .WithMany(t => t.Desposits)
            //    .HasForeignKey(t => t.IdSerie).WillCascadeOnDelete(false);

            modelBuilder.Entity<Deposit>()
                .HasOptional(t => t.Cashier)
                .WithMany(t => t.Deposits)
                .HasForeignKey(t => t.IdCashier).WillCascadeOnDelete(false);

            modelBuilder.Entity<Deposit>()
                .HasOptional(t => t.ShopManager)
                .WithMany(t => t.AuthoriezdDeposits)
                .HasForeignKey(t => t.IdShopManager).WillCascadeOnDelete(false);

            modelBuilder.Entity<CashMissing>().HasOptional(t=> t.Cashier)
                        .WithMany(t=> t.CashMissings).HasForeignKey(t=> t.IdCashier).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.Cake).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdCake).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.Category).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdCategory).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.ColorCovert).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdColorCovert).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.ColorFlower).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdColorFlower).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.ColorRibbon).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdRibbon).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.ColorTop).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdColorTop).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.Cover).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdCover).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
               .HasOptional(t => t.Laza).WithMany(t => t.SpecialOrder)
               .HasForeignKey(t => t.IdLaza).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
               .HasOptional(t => t.Guirnalda).WithMany(t => t.SpecialOrder)
               .HasForeignKey(t => t.IdGuirnalda).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.Especiality).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdEspeciality).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.Flower).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdFlower).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.Style).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.filler).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdFiller).WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDR_SpecialOrder>()
                .HasOptional(t => t.ColorHeight).WithMany(t => t.SpecialOrder)
                .HasForeignKey(t => t.IdColorHeight).WillCascadeOnDelete(false);

          
            modelBuilder.Entity<UColor_Covert>()
              .HasOptional(t => t.Style).WithMany(t => t.ColorCover)
              .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<Ufiller>()
              .HasOptional(t => t.Style).WithMany(t => t.filler)
              .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);
            modelBuilder.Entity<Ufiller>()
              .HasOptional(t => t.specialty).WithMany(t => t.FillerList)
              .HasForeignKey(t => t.IdSpecialty).WillCascadeOnDelete(false);

            modelBuilder.Entity<UCover>()
              .HasOptional(t => t.Style).WithMany(t => t.covert)
              .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<ULaza>()
             .HasOptional(t => t.Style).WithMany(t => t.laza)
             .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<UGuirnalda>()
           .HasOptional(t => t.Style).WithMany(t => t.guirnalda)
           .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<UColor_Flower>()
             .HasOptional(t => t.Style).WithMany(t => t.colorFlower)
             .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<UFlower>()
             .HasOptional(t => t.Style).WithMany(t => t.colorflowerl)
             .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<UColor_top>()
             .HasOptional(t => t.Style).WithMany(t => t.colorTop)
             .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<UColor_Ribbon>()
             .HasOptional(t => t.Style).WithMany(t => t.colorRibbon)
             .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Entity<UColor_Height>()
             .HasOptional(t => t.Style).WithMany(t => t.colorHeight)
             .HasForeignKey(t => t.IdStyle).WillCascadeOnDelete(false);

            modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(18, 4));
            modelBuilder.Properties<decimal?>().Configure(config => config.HasPrecision(18, 4));

            modelBuilder.Entity<UserDocument>()
                .HasRequired(t=> t.Document).WithMany(d=> d.UserDocuments)
                .HasForeignKey(d=> d.ObjType).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserDocument>()
                .HasRequired(d => d.LocalUser).WithMany(d => d.UserDocuments)
                .HasForeignKey(t => t.IdUser).WillCascadeOnDelete(false);

            modelBuilder.Entity<OITW_BranchArticles>()
            .Property(t => t.RowVersion).IsRowVersion();
            modelBuilder.Entity<LOG_CHANGES>()
           .Property(t => t.RowVersion).IsRowVersion();

            base.OnModelCreating(modelBuilder);
        }
    }
}
