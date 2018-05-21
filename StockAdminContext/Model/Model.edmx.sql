
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/08/2013 00:24:30
-- Generated from EDMX file: C:\Users\Jorge\Documents\Visual Studio 2012\Projects\SIGI\SIGI-CodeFirst\SigiApi\Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SIGI];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ALOHA_ArticlesOITM_ALOHA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OITM_ALOHA] DROP CONSTRAINT [FK_ALOHA_ArticlesOITM_ALOHA];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckbookSeriesCheckbook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeriesCheckbook] DROP CONSTRAINT [FK_CheckbookSeriesCheckbook];
GO
IF OBJECT_ID(N'[dbo].[FK_ColorsFilling]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Filling] DROP CONSTRAINT [FK_ColorsFilling];
GO
IF OBJECT_ID(N'[dbo].[FK_ColorsRibbon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ribbon] DROP CONSTRAINT [FK_ColorsRibbon];
GO
IF OBJECT_ID(N'[dbo].[FK_CoverColors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cover] DROP CONSTRAINT [FK_CoverColors];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentsUserAuthorization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAuthorizationSet] DROP CONSTRAINT [FK_DocumentsUserAuthorization];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentsUserDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDocument] DROP CONSTRAINT [FK_DocumentsUserDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceALOHADetailInvoiceALOHA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailInvoiceALOHA] DROP CONSTRAINT [FK_InvoiceALOHADetailInvoiceALOHA];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalUsersUserDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDocument] DROP CONSTRAINT [FK_LocalUsersUserDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_OIGE_GoodsIssuesIGE1_GoodsIssueDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IGE1_GoodsIssueDetail] DROP CONSTRAINT [FK_OIGE_GoodsIssuesIGE1_GoodsIssueDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OIGN_GoodsReceiptIGN1_GoodsReceiptDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IGN1_GoodsReceiptDetail] DROP CONSTRAINT [FK_OIGN_GoodsReceiptIGN1_GoodsReceiptDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OINV_SalesINV1_SalesDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[INV1_SalesDetail] DROP CONSTRAINT [FK_OINV_SalesINV1_SalesDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OINV_SalesINV1_SalesDetail1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DPI1_DownPaymentDetail] DROP CONSTRAINT [FK_OINV_SalesINV1_SalesDetail1];
GO
IF OBJECT_ID(N'[dbo].[FK_OITB_GroupsOITM_Articles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OITM_Articles] DROP CONSTRAINT [FK_OITB_GroupsOITM_Articles];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ALOHAOITM_Articles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OITM_ALOHA] DROP CONSTRAINT [FK_OITM_ALOHAOITM_Articles];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesDPI1_DownPaymentDetail1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DPI1_DownPaymentDetail] DROP CONSTRAINT [FK_OITM_ArticlesDPI1_DownPaymentDetail1];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesIGE1_GoodsIssueDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IGE1_GoodsIssueDetail] DROP CONSTRAINT [FK_OITM_ArticlesIGE1_GoodsIssueDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesIGN1_GoodsReceiptDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IGN1_GoodsReceiptDetail] DROP CONSTRAINT [FK_OITM_ArticlesIGN1_GoodsReceiptDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesINV1_SalesDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[INV1_SalesDetail] DROP CONSTRAINT [FK_OITM_ArticlesINV1_SalesDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesOITW_BranchArticles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OITW_BranchArticles] DROP CONSTRAINT [FK_OITM_ArticlesOITW_BranchArticles];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesPCH1_PurchaseDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PCH1_PurchaseDetail] DROP CONSTRAINT [FK_OITM_ArticlesPCH1_PurchaseDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesRDR1_SpecialOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RDR1_SpecialOrderDetail] DROP CONSTRAINT [FK_OITM_ArticlesRDR1_SpecialOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesRIN1_ClientCreditNoteDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RIN1_ClientCreditNoteDetail] DROP CONSTRAINT [FK_OITM_ArticlesRIN1_ClientCreditNoteDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesRPC1_SupplierCreditNoteDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RPC1_SupplierCreditNoteDetail] DROP CONSTRAINT [FK_OITM_ArticlesRPC1_SupplierCreditNoteDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesWTQ1_TransferRequestDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WTQ1_TransferRequestDetails] DROP CONSTRAINT [FK_OITM_ArticlesWTQ1_TransferRequestDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_OITM_ArticlesWTR1_TransferDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WTR1_TransferDetails] DROP CONSTRAINT [FK_OITM_ArticlesWTR1_TransferDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_OPCH_FacturasCompraPCH1_DetalleFacturasCompra]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PCH1_PurchaseDetail] DROP CONSTRAINT [FK_OPCH_FacturasCompraPCH1_DetalleFacturasCompra];
GO
IF OBJECT_ID(N'[dbo].[FK_ORDR_SpecialOrderATC1_Attachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ATC1_Attachments] DROP CONSTRAINT [FK_ORDR_SpecialOrderATC1_Attachments];
GO
IF OBJECT_ID(N'[dbo].[FK_ORDR_SpecialOrderRDR1_SpecialOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RDR1_SpecialOrderDetail] DROP CONSTRAINT [FK_ORDR_SpecialOrderRDR1_SpecialOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RPC1_SupplierCreditNoteDetail] DROP CONSTRAINT [FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores];
GO
IF OBJECT_ID(N'[dbo].[FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RIN1_ClientCreditNoteDetail] DROP CONSTRAINT [FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores1];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchODPI_DownPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ODPI_DownPayment] DROP CONSTRAINT [FK_OWHS_BranchODPI_DownPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOIGE_GoodsIssues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OIGE_GoodsIssues] DROP CONSTRAINT [FK_OWHS_BranchOIGE_GoodsIssues];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOIGN_GoodsReceipt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OIGN_GoodsReceipt] DROP CONSTRAINT [FK_OWHS_BranchOIGN_GoodsReceipt];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOINV_Sales]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OINV_Sales] DROP CONSTRAINT [FK_OWHS_BranchOINV_Sales];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOITW_BranchArticles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OITW_BranchArticles] DROP CONSTRAINT [FK_OWHS_BranchOITW_BranchArticles];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOPCH_Purchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OPCH_Purchase] DROP CONSTRAINT [FK_OWHS_BranchOPCH_Purchase];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchORDR_SpecialOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORDR_SpecialOrder] DROP CONSTRAINT [FK_OWHS_BranchORDR_SpecialOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchORIN_ClientCreditNotes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORIN_ClientCreditNotes] DROP CONSTRAINT [FK_OWHS_BranchORIN_ClientCreditNotes];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchORPC_SupplierCreditNotes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORPC_SupplierCreditNotes] DROP CONSTRAINT [FK_OWHS_BranchORPC_SupplierCreditNotes];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOWTQ_TransferRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OWTQ_TransferRequest] DROP CONSTRAINT [FK_OWHS_BranchOWTQ_TransferRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_OWHS_BranchOWTR_Transfers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OWTR_Transfers] DROP CONSTRAINT [FK_OWHS_BranchOWTR_Transfers];
GO
IF OBJECT_ID(N'[dbo].[FK_OWTQ_OrdersWTQ1_OrderDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WTQ1_TransferRequestDetails] DROP CONSTRAINT [FK_OWTQ_OrdersWTQ1_OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_OWTR_TransfersWTR1_TransferDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WTR1_TransferDetails] DROP CONSTRAINT [FK_OWTR_TransfersWTR1_TransferDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTypesINV1_SalesDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[INV1_SalesDetail] DROP CONSTRAINT [FK_PaymentTypesINV1_SalesDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTypesPaymentTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentTypes] DROP CONSTRAINT [FK_PaymentTypesPaymentTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_RoyaltiesTypesRel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[INV1_SalesDetail] DROP CONSTRAINT [FK_RoyaltiesTypesRel];
GO
IF OBJECT_ID(N'[dbo].[FK_Series]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeriesCheckbook] DROP CONSTRAINT [FK_Series];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAuthorizationLocalUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAuthorizationSet] DROP CONSTRAINT [FK_UserAuthorizationLocalUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ALOHA_Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ALOHA_Articles];
GO
IF OBJECT_ID(N'[dbo].[ATC1_Attachments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ATC1_Attachments];
GO
IF OBJECT_ID(N'[dbo].[AuthorizationLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthorizationLog];
GO
IF OBJECT_ID(N'[dbo].[CashMissing]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashMissing];
GO
IF OBJECT_ID(N'[dbo].[Checkbook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Checkbook];
GO
IF OBJECT_ID(N'[dbo].[Colors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Colors];
GO
IF OBJECT_ID(N'[dbo].[Cover]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cover];
GO
IF OBJECT_ID(N'[dbo].[Deposits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Deposits];
GO
IF OBJECT_ID(N'[dbo].[DetailInvoiceALOHA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetailInvoiceALOHA];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[DPI1_DownPaymentDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DPI1_DownPaymentDetail];
GO
IF OBJECT_ID(N'[dbo].[Filling]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Filling];
GO
IF OBJECT_ID(N'[dbo].[IGE1_GoodsIssueDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IGE1_GoodsIssueDetail];
GO
IF OBJECT_ID(N'[dbo].[IGN1_GoodsReceiptDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IGN1_GoodsReceiptDetail];
GO
IF OBJECT_ID(N'[dbo].[INV1_SalesDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[INV1_SalesDetail];
GO
IF OBJECT_ID(N'[dbo].[InvoiceALOHA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceALOHA];
GO
IF OBJECT_ID(N'[dbo].[LocalUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocalUsers];
GO
IF OBJECT_ID(N'[dbo].[NNM1_Series]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NNM1_Series];
GO
IF OBJECT_ID(N'[dbo].[OCRD_BusinessPartner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OCRD_BusinessPartner];
GO
IF OBJECT_ID(N'[dbo].[ODPI_DownPayment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ODPI_DownPayment];
GO
IF OBJECT_ID(N'[dbo].[OIGE_GoodsIssues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OIGE_GoodsIssues];
GO
IF OBJECT_ID(N'[dbo].[OIGN_GoodsReceipt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OIGN_GoodsReceipt];
GO
IF OBJECT_ID(N'[dbo].[OINM_Transaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OINM_Transaction];
GO
IF OBJECT_ID(N'[dbo].[OINV_Sales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OINV_Sales];
GO
IF OBJECT_ID(N'[dbo].[OITB_Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OITB_Groups];
GO
IF OBJECT_ID(N'[dbo].[OITM_ALOHA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OITM_ALOHA];
GO
IF OBJECT_ID(N'[dbo].[OITM_Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OITM_Articles];
GO
IF OBJECT_ID(N'[dbo].[OITW_BranchArticles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OITW_BranchArticles];
GO
IF OBJECT_ID(N'[dbo].[OPCH_Purchase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OPCH_Purchase];
GO
IF OBJECT_ID(N'[dbo].[ORDR_SpecialOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORDR_SpecialOrder];
GO
IF OBJECT_ID(N'[dbo].[ORIN_ClientCreditNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORIN_ClientCreditNotes];
GO
IF OBJECT_ID(N'[dbo].[ORPC_SupplierCreditNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORPC_SupplierCreditNotes];
GO
IF OBJECT_ID(N'[dbo].[OWHS_Branch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OWHS_Branch];
GO
IF OBJECT_ID(N'[dbo].[OWTQ_TransferRequest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OWTQ_TransferRequest];
GO
IF OBJECT_ID(N'[dbo].[OWTR_Transfers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OWTR_Transfers];
GO
IF OBJECT_ID(N'[dbo].[Parameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parameters];
GO
IF OBJECT_ID(N'[dbo].[PaymentTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentTypes];
GO
IF OBJECT_ID(N'[dbo].[PCH1_PurchaseDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PCH1_PurchaseDetail];
GO
IF OBJECT_ID(N'[dbo].[RDR1_SpecialOrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RDR1_SpecialOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[Ribbon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ribbon];
GO
IF OBJECT_ID(N'[dbo].[RIN1_ClientCreditNoteDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RIN1_ClientCreditNoteDetail];
GO
IF OBJECT_ID(N'[dbo].[RPC1_SupplierCreditNoteDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RPC1_SupplierCreditNoteDetail];
GO
IF OBJECT_ID(N'[dbo].[SeriesCheckbook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeriesCheckbook];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TransactionLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionLog];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UCakes]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UCakes];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UCategory]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UCategory];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UColor_Covert]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UColor_Covert];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UColor_Flower]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UColor_Flower];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UColor_Ribbon]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UColor_Ribbon];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UColor_top]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UColor_top];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UCover]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UCover];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UEspecialty]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UEspecialty];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UFlower]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UFlower];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UMovements]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UMovements];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UMovements_Acc]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UMovements_Acc];
GO
IF OBJECT_ID(N'[dbo].[UserAuthorizationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuthorizationSet];
GO
IF OBJECT_ID(N'[dbo].[UserDocument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDocument];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UStyle]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UStyle];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[UStyle_filler]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[UStyle_filler];
GO
IF OBJECT_ID(N'[dbo].[WTQ1_TransferRequestDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WTQ1_TransferRequestDetails];
GO
IF OBJECT_ID(N'[dbo].[WTR1_TransferDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WTR1_TransferDetails];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'NNM1_Series'
CREATE TABLE [dbo].[NNM1_Series] (
    [ObjectCode] nvarchar(20)  NOT NULL,
    [Series] smallint  NOT NULL,
    [SeriesName] nvarchar(8)  NOT NULL,
    [Remark] nvarchar(50)  NULL,
    [Locked] char(1)  NULL,
    [StateL] varchar(50)  NULL
);
GO

-- Creating table 'OITM_Articles'
CREATE TABLE [dbo].[OITM_Articles] (
    [ItemCode] nvarchar(20)  NOT NULL,
    [ItemName] nvarchar(100)  NULL,
    [ItmsGrpCod] smallint  NULL,
    [CodeBars] nvarchar(16)  NULL,
    [PrchseItem] char(1)  NULL,
    [SellItem] char(1)  NULL,
    [InvntItem] char(1)  NULL,
    [CardCode] nvarchar(15)  NULL,
    [BuyUnitMsr] nvarchar(20)  NULL,
    [NumInBuy] decimal(19,6)  NULL,
    [SalUnitMsr] nvarchar(20)  NULL,
    [AvgPrice] decimal(19,6)  NULL,
    [PurPackUn] decimal(19,6)  NULL,
    [SalPackUn] decimal(19,6)  NULL,
    [validFor] char(1)  NULL,
    [validFrom] datetime  NULL,
    [validTo] datetime  NULL,
    [ItemType] char(1)  NULL,
    [InvntryUom] nvarchar(20)  NULL,
    [NumInSale] decimal(19,6)  NOT NULL,
    [AssetItem] varchar(1)  NOT NULL,
    [LastSyncDateL] datetime  NULL,
    [TemplateL] nvarchar(max)  NOT NULL,
    [OWHS_BranchWhsCode] nvarchar(8)  NOT NULL,
    [U_LINEA] nvarchar(30)  NULL,
    [U_SUBLINEA] nvarchar(30)  NULL
);
GO

-- Creating table 'OITW_BranchArticles'
CREATE TABLE [dbo].[OITW_BranchArticles] (
    [ItemCode] nvarchar(20)  NOT NULL,
    [WhsCode] nvarchar(8)  NOT NULL,
    [OnHand] decimal(19,6)  NULL,
    [OnHand1] decimal(19,6)  NULL,
    [IsCommited] decimal(19,6)  NULL,
    [OnOrder] decimal(19,6)  NULL,
    [MinStock] decimal(19,6)  NULL,
    [MaxStock] decimal(19,6)  NULL,
    [MinOrder] decimal(19,6)  NULL,
    [Locked] char(1)  NULL,
    [LastSyncDateL] datetime  NOT NULL
);
GO

-- Creating table 'OWHS_Branch'
CREATE TABLE [dbo].[OWHS_Branch] (
    [WhsCode] nvarchar(8)  NOT NULL,
    [WhsName] nvarchar(100)  NULL,
    [Grp_Code] nvarchar(4)  NULL,
    [Locked] char(1)  NULL,
    [LockedForOrderL] bit  NOT NULL
);
GO

-- Creating table 'WTQ1_TransferRequestDetails'
CREATE TABLE [dbo].[WTQ1_TransferRequestDetails] (
    [DocEntry] int  NULL,
    [LineNum] int  NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [OpenQty] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [DocDate] datetime  NULL,
    [LineStatus] nvarchar(1)  NULL,
    [UseBaseUn] nvarchar(1)  NULL,
    [UnitMsr] nvarchar(20)  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedDateL] datetime  NULL,
    [ModifiedByL] nvarchar(max)  NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [IdTransferRequestL] int  NOT NULL,
    [IdTransferRequestDetailL] int  NOT NULL
);
GO

-- Creating table 'LocalUsers'
CREATE TABLE [dbo].[LocalUsers] (
    [UserId] uniqueidentifier  NOT NULL,
    [NickName] nvarchar(15)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Position] nvarchar(max)  NOT NULL,
    [Locked] nvarchar(max)  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [AllowChangePrices] bit  NOT NULL,
    [AllowProcessing] bit  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [ObjType] nvarchar(20)  NOT NULL,
    [DocumentName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OWTR_Transfers'
CREATE TABLE [dbo].[OWTR_Transfers] (
    [DocEntry] int  NULL,
    [DocNum] int  NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [Comments] nvarchar(254)  NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [Series] smallint  NULL,
    [TaxDate] datetime  NULL,
    [Filler] nvarchar(8)  NULL,
    [StateL] int  NOT NULL,
    [DeliveryDateL] datetime  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NULL,
    [LastSyncDateL] datetime  NULL,
    [ObjType] nvarchar(20)  NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [ReceptionDateL] datetime  NOT NULL,
    [ModifiedDateL] datetime  NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NULL,
    [IdTransferL] int  NOT NULL
);
GO

-- Creating table 'WTR1_TransferDetails'
CREATE TABLE [dbo].[WTR1_TransferDetails] (
    [DocEntry] int  NULL,
    [LineNum] int  NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [OpenQty] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [DocDate] datetime  NULL,
    [LineStatus] nvarchar(1)  NULL,
    [UseBaseUn] nvarchar(1)  NULL,
    [UnitMsr] nvarchar(20)  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedDateL] datetime  NULL,
    [ModifiedByL] nvarchar(max)  NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [IdTransferL] int  NOT NULL,
    [IdTransferDetailL] int  NOT NULL
);
GO

-- Creating table 'UserDocument'
CREATE TABLE [dbo].[UserDocument] (
    [ObjType] nvarchar(20)  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [TypeAccess] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'Parameters'
CREATE TABLE [dbo].[Parameters] (
    [IdParameters] int IDENTITY(1,1) NOT NULL,
    [Parameter] nvarchar(max)  NOT NULL,
    [Value1] nvarchar(max)  NOT NULL,
    [Value2] nvarchar(max)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL
);
GO

-- Creating table 'OPCH_Purchase'
CREATE TABLE [dbo].[OPCH_Purchase] (
    [IdPurchase] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [Comments] nvarchar(254)  NULL,
    [Series] smallint  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [NumAtCard] nvarchar(100)  NOT NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'ORPC_SupplierCreditNotes'
CREATE TABLE [dbo].[ORPC_SupplierCreditNotes] (
    [IdSupplierCreditNote] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [Comments] nvarchar(254)  NULL,
    [NumAtCard] nvarchar(100)  NOT NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [VatSum] decimal(18,0)  NOT NULL,
    [Series] smallint  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'PCH1_PurchaseDetail'
CREATE TABLE [dbo].[PCH1_PurchaseDetail] (
    [IdPurchaseDetail] int IDENTITY(1,1) NOT NULL,
    [IdPurchase] int  NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [DocDate] datetime  NULL,
    [UseBaseUn] nvarchar(1)  NOT NULL,
    [TaxCode] nvarchar(50)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'RPC1_SupplierCreditNoteDetail'
CREATE TABLE [dbo].[RPC1_SupplierCreditNoteDetail] (
    [IdSupplierCreditNoteDetail] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [BaseDocNum] int  NOT NULL,
    [BaseEntry] int  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [DocDate] datetime  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [OcrCode] nvarchar(8)  NOT NULL,
    [IdSupplierCreditNote] int  NOT NULL
);
GO

-- Creating table 'OIGN_GoodsReceipt'
CREATE TABLE [dbo].[OIGN_GoodsReceipt] (
    [IdGoodReceiptL] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [Comments] nvarchar(254)  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [Ref2] nvarchar(11)  NOT NULL,
    [Series] smallint  NULL,
    [StateL] time  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'IGN1_GoodsReceiptDetail'
CREATE TABLE [dbo].[IGN1_GoodsReceiptDetail] (
    [IdGoodReceiptDetail] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [LineStatus] nvarchar(1)  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [DocDate] datetime  NULL,
    [UseBaseUn] nvarchar(1)  NOT NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [UnitMsr] nvarchar(20)  NOT NULL,
    [CodeBars] nvarchar(16)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [IdGoodReceiptL] int  NOT NULL,
    [OITM_Articles_ItemCode] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'IGE1_GoodsIssueDetail'
CREATE TABLE [dbo].[IGE1_GoodsIssueDetail] (
    [IdGoodIssueDetail] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [LineStatus] nvarchar(1)  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [DocDate] datetime  NULL,
    [UseBaseUn] nvarchar(1)  NOT NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [UnitMsr] nvarchar(20)  NOT NULL,
    [CodeBars] nvarchar(16)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [IdGoodIssueL] int  NOT NULL
);
GO

-- Creating table 'OIGE_GoodsIssues'
CREATE TABLE [dbo].[OIGE_GoodsIssues] (
    [IdGoodIssueL] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [Comments] nvarchar(254)  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [Ref2] nvarchar(11)  NOT NULL,
    [Series] smallint  NULL,
    [StateL] time  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'Checkbook'
CREATE TABLE [dbo].[Checkbook] (
    [IdCheckbookL] int IDENTITY(1,1) NOT NULL,
    [DateL] datetime  NOT NULL,
    [SerieL] nvarchar(8)  NOT NULL,
    [InitialNumL] int  NOT NULL,
    [NextNumberL] int  NOT NULL,
    [LastNumL] int  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL
);
GO

-- Creating table 'SeriesCheckbook'
CREATE TABLE [dbo].[SeriesCheckbook] (
    [Series] smallint  NOT NULL,
    [IdCheckbookL] int  NOT NULL
);
GO

-- Creating table 'OINV_Sales'
CREATE TABLE [dbo].[OINV_Sales] (
    [IdSaleL] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [NumAtCard] nvarchar(100)  NOT NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [Comments] nvarchar(254)  NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [Series] smallint  NULL,
    [TaxDate] datetime  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [ObjType1] nvarchar(20)  NOT NULL,
    [DocType] nvarchar(max)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'INV1_SalesDetail'
CREATE TABLE [dbo].[INV1_SalesDetail] (
    [IdSaleDetail] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [OcrCode] nvarchar(8)  NOT NULL,
    [IdSaleL] int  NOT NULL,
    [IdPaymentType] int  NOT NULL,
    [IdRoyalty] int  NOT NULL,
    [TaxStatus] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'PaymentTypes'
CREATE TABLE [dbo].[PaymentTypes] (
    [IdPaymentType] int IDENTITY(1,1) NOT NULL,
    [PaymentName] nvarchar(max)  NOT NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [IsRoyalty] bit  NOT NULL,
    [PaymentTypesIdPaymentType] int  NOT NULL
);
GO

-- Creating table 'TransactionLog'
CREATE TABLE [dbo].[TransactionLog] (
    [IdTransactionLog] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(200)  NOT NULL,
    [VerificationCode] smallint  NOT NULL,
    [Code] nvarchar(10)  NOT NULL,
    [Date] datetime  NOT NULL,
    [State] nvarchar(1)  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ORDR_SpecialOrder'
CREATE TABLE [dbo].[ORDR_SpecialOrder] (
    [IdSpecialOrder] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [AtcEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [DocType] nvarchar(max)  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [NumAtCard] nvarchar(100)  NOT NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [Comments] nvarchar(254)  NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [Series] smallint  NULL,
    [Ref2] nvarchar(11)  NOT NULL,
    [Ref1] nvarchar(11)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'ATC1_Attachments'
CREATE TABLE [dbo].[ATC1_Attachments] (
    [IdAtached] int IDENTITY(1,1) NOT NULL,
    [AbsEntry] int  NOT NULL,
    [Line] int  NOT NULL,
    [srcPath] nvarchar(max)  NOT NULL,
    [trgtPath] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [FileExt] nvarchar(8)  NOT NULL,
    [Date] datetime  NOT NULL,
    [UsrID] int  NOT NULL,
    [Copied] nvarchar(1)  NOT NULL,
    [Override] nvarchar(1)  NOT NULL,
    [IdEspecialOrder] int  NOT NULL
);
GO

-- Creating table 'RDR1_SpecialOrderDetail'
CREATE TABLE [dbo].[RDR1_SpecialOrderDetail] (
    [IdSpecialOrderDetail] int IDENTITY(1,1) NOT NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocEntry] int  NOT NULL,
    [Dscription] nvarchar(100)  NULL,
    [ItemCode] nvarchar(20)  NULL,
    [LineNum] int  NOT NULL,
    [LineTotal] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [OcrCode] nvarchar(8)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [IdSpecialOrder] int  NOT NULL
);
GO

-- Creating table 'RIN1_ClientCreditNoteDetail'
CREATE TABLE [dbo].[RIN1_ClientCreditNoteDetail] (
    [IdClientCreditNoteDetailL] int IDENTITY(1,1) NOT NULL,
    [IdClientCreditNoteL] int  NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [BaseDocNum] int  NOT NULL,
    [BaseEntry] int  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [DocDate] datetime  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [OcrCode] nvarchar(8)  NOT NULL
);
GO

-- Creating table 'ORIN_ClientCreditNotes'
CREATE TABLE [dbo].[ORIN_ClientCreditNotes] (
    [IdClientCreditNoteL] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [Comments] nvarchar(254)  NULL,
    [NumAtCard] nvarchar(100)  NOT NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [VatSum] decimal(18,0)  NOT NULL,
    [Series] smallint  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'ODPI_DownPayment'
CREATE TABLE [dbo].[ODPI_DownPayment] (
    [IdDownPayment] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [DocNum] int  NOT NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [NumAtCard] nvarchar(100)  NOT NULL,
    [DocTotal] decimal(18,0)  NOT NULL,
    [Comments] nvarchar(254)  NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [Series] smallint  NULL,
    [TaxDate] datetime  NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [ObjType1] nvarchar(20)  NOT NULL,
    [DocType] nvarchar(max)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'DPI1_DownPaymentDetail'
CREATE TABLE [dbo].[DPI1_DownPaymentDetail] (
    [IdDownPaymentDetail] int IDENTITY(1,1) NOT NULL,
    [DocEntry] int  NOT NULL,
    [LineNum] int  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [Quantity] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [LineTotal] decimal(19,6)  NULL,
    [WhsCode] nvarchar(8)  NULL,
    [AcctCode] nvarchar(15)  NOT NULL,
    [OcrCode] nvarchar(8)  NOT NULL,
    [IdDownPaymentL] int  NOT NULL,
    [IdPaymentType] int  NOT NULL,
    [IdRoyalty] int  NOT NULL
);
GO

-- Creating table 'Deposits'
CREATE TABLE [dbo].[Deposits] (
    [IdDeposits] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Cashier] nvarchar(100)  NOT NULL,
    [Cash] decimal(18,0)  NOT NULL,
    [Cheque] decimal(18,0)  NOT NULL,
    [UserAuthorization] nvarchar(max)  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL,
    [Number] int  NOT NULL
);
GO

-- Creating table 'CashMissing'
CREATE TABLE [dbo].[CashMissing] (
    [IdCashMissing] int IDENTITY(1,1) NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [CardCode] nvarchar(15)  NULL,
    [Date] datetime  NOT NULL,
    [Comments] nvarchar(254)  NULL,
    [UserAuthorization] nvarchar(max)  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [ModifiedByL] nvarchar(max)  NOT NULL,
    [ModifiedDateL] datetime  NOT NULL
);
GO

-- Creating table 'OINM_Transaction'
CREATE TABLE [dbo].[OINM_Transaction] (
    [IdTransaction] int IDENTITY(1,1) NOT NULL,
    [TransNum] int  NOT NULL,
    [TransType] int  NOT NULL,
    [DocDate] datetime  NOT NULL,
    [DocDueDate] datetime  NOT NULL,
    [Ref1] nvarchar(11)  NOT NULL,
    [Ref2] nvarchar(11)  NOT NULL,
    [Comments] nvarchar(254)  NOT NULL,
    [JrnMemo] nvarchar(50)  NOT NULL,
    [DocTime] int  NOT NULL,
    [ItemCode] nvarchar(20)  NULL,
    [Dscription] nvarchar(100)  NULL,
    [InQty] decimal(19,6)  NULL,
    [OutQty1] decimal(19,6)  NULL,
    [Price] decimal(19,6)  NULL,
    [SerialNum] nvarchar(17)  NOT NULL,
    [WareHouse] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'UserAuthorizationSet'
CREATE TABLE [dbo].[UserAuthorizationSet] (
    [ObjType] nvarchar(20)  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [LocalUsers_UserId] uniqueidentifier  NULL
);
GO

-- Creating table 'ALOHA_Articles'
CREATE TABLE [dbo].[ALOHA_Articles] (
    [IdALOHA_Articles] int IDENTITY(1,1) NOT NULL,
    [Categories] nvarchar(max)  NOT NULL,
    [code] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [DetailInvoiceALOHAIdDetailInvoiceALOHA] int  NOT NULL
);
GO

-- Creating table 'OITM_ALOHA'
CREATE TABLE [dbo].[OITM_ALOHA] (
    [IdALOHA_Articles] int  NOT NULL,
    [ItemCode] nvarchar(20)  NOT NULL,
    [OITM_Articles_ItemCode] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'AuthorizationLog'
CREATE TABLE [dbo].[AuthorizationLog] (
    [IdAuthorizationLog] int IDENTITY(1,1) NOT NULL,
    [ObjType] nvarchar(20)  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [DocEntry] int  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'OITB_Groups'
CREATE TABLE [dbo].[OITB_Groups] (
    [ItmsGrpCod] smallint  NOT NULL,
    [ItmsGrpNam] nvarchar(20)  NOT NULL,
    [LastSyncDateL] datetime  NOT NULL,
    [Locked] nvarchar(1)  NOT NULL,
    [StateL] nvarchar(1)  NOT NULL,
    [MandatoryCount] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'InvoiceALOHA'
CREATE TABLE [dbo].[InvoiceALOHA] (
    [IdInvoiceALOHA] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [State] nvarchar(1)  NOT NULL,
    [Credit] float  NOT NULL,
    [Cash] float  NOT NULL,
    [correlative] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DetailInvoiceALOHA'
CREATE TABLE [dbo].[DetailInvoiceALOHA] (
    [IdDetailInvoiceALOHA] int IDENTITY(1,1) NOT NULL,
    [InvoiceALOHAIdInvoiceALOHA] int  NOT NULL,
    [Category] int  NOT NULL,
    [DOB] datetime  NOT NULL,
    [EntryId] nvarchar(7)  NOT NULL,
    [Property1] nvarchar(max)  NOT NULL,
    [Hour] datetime  NOT NULL,
    [Quantity] int  NOT NULL,
    [Sysdate] datetime  NOT NULL,
    [TermId] int  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'Colors'
CREATE TABLE [dbo].[Colors] (
    [IdColors] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CoverIdCover] int  NOT NULL
);
GO

-- Creating table 'Cover'
CREATE TABLE [dbo].[Cover] (
    [IdCover] int IDENTITY(1,1) NOT NULL,
    [U_SUBLINEA] nvarchar(30)  NOT NULL,
    [IdColors] int  NOT NULL
);
GO

-- Creating table 'Ribbon'
CREATE TABLE [dbo].[Ribbon] (
    [IdRibbon] int IDENTITY(1,1) NOT NULL,
    [ColorsIdColors] int  NOT NULL,
    [U_SUBLINEA] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Filling'
CREATE TABLE [dbo].[Filling] (
    [IdFilling] int IDENTITY(1,1) NOT NULL,
    [U_SUBLINEA] nvarchar(30)  NOT NULL,
    [ColorsIdColors] int  NOT NULL
);
GO

-- Creating table 'OCRD_BusinessPartner'
CREATE TABLE [dbo].[OCRD_BusinessPartner] (
    [IdBusinessPartner] int IDENTITY(1,1) NOT NULL,
    [CardCode] nvarchar(15)  NOT NULL,
    [CardName] nvarchar(100)  NOT NULL,
    [GroupCode] int  NOT NULL,
    [AddId] nvarchar(18)  NULL,
    [Notes] nvarchar(100)  NULL,
    [Address] nvarchar(100)  NULL,
    [CardType] nvarchar(1)  NOT NULL,
    [Phone1] nvarchar(20)  NULL,
    [Phone2] nvarchar(20)  NULL,
    [Cellular] nvarchar(50)  NULL,
    [Email] nvarchar(100)  NULL,
    [ValidFor] char(1)  NOT NULL,
    [ValidFrom] datetime  NULL,
    [ValidTo] datetime  NULL,
    [U_NIT] nvarchar(17)  NULL
);
GO

-- Creating table 'OWTQ_TransferRequest'
CREATE TABLE [dbo].[OWTQ_TransferRequest] (
    [DocEntry] int  NULL,
    [DocNum] int  NULL,
    [CANCELED] char(1)  NULL,
    [DocStatus] char(1)  NULL,
    [DocDate] datetime  NULL,
    [DocDueDate] datetime  NULL,
    [CardCode] nvarchar(15)  NULL,
    [Comments] nvarchar(254)  NULL,
    [JrnlMemo] nvarchar(50)  NULL,
    [Series] smallint  NULL,
    [TaxDate] datetime  NULL,
    [Filler] nvarchar(8)  NULL,
    [IdTransferRequestL] int  NOT NULL,
    [StateL] int  NOT NULL,
    [DeliveryDateL] datetime  NOT NULL,
    [CreatedDateL] datetime  NOT NULL,
    [ModifiedByL] nvarchar(max)  NULL,
    [LastSyncDateL] datetime  NULL,
    [ObjType] nvarchar(20)  NULL,
    [ModifiedDateL] datetime  NULL,
    [CreatedByL] nvarchar(max)  NOT NULL,
    [WhsCode] nvarchar(8)  NULL,
    [HasToBeSync] bit  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'UCakes'
CREATE TABLE [dbo].[UCakes] (
    [Code] nvarchar(30)  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [DocEntry] int  NOT NULL,
    [Canceled] char(1)  NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [UserSign] int  NULL,
    [Transfered] char(1)  NULL,
    [CreateDate] datetime  NULL,
    [CreateTime] smallint  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateTime] smallint  NULL,
    [DataSource] char(1)  NULL
);
GO

-- Creating table 'UCategory'
CREATE TABLE [dbo].[UCategory] (
    [Code] nvarchar(30)  NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'UColor_Covert'
CREATE TABLE [dbo].[UColor_Covert] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_Nombre] nvarchar(50)  NULL,
    [U_Estado] nvarchar(10)  NULL
);
GO

-- Creating table 'UColor_Flower'
CREATE TABLE [dbo].[UColor_Flower] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_Nombre] nvarchar(50)  NULL,
    [U_Estado] nvarchar(10)  NULL
);
GO

-- Creating table 'UColor_Ribbon'
CREATE TABLE [dbo].[UColor_Ribbon] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_Nombre] nvarchar(50)  NULL,
    [U_Estado] nvarchar(10)  NULL
);
GO

-- Creating table 'UColor_top'
CREATE TABLE [dbo].[UColor_top] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_Nombre] nvarchar(51)  NULL,
    [U_Estado] nvarchar(10)  NULL
);
GO

-- Creating table 'UCover'
CREATE TABLE [dbo].[UCover] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_Nombre] nvarchar(50)  NULL,
    [U_ESTADO] char(1)  NULL
);
GO

-- Creating table 'UEspecialty'
CREATE TABLE [dbo].[UEspecialty] (
    [Code] nvarchar(30)  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [DocEntry] int  NOT NULL,
    [Canceled] char(1)  NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [UserSign] int  NULL,
    [Transfered] char(1)  NULL,
    [CreateDate] datetime  NULL,
    [CreateTime] smallint  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateTime] smallint  NULL,
    [DataSource] char(1)  NULL,
    [U_NOMBRE] nvarchar(50)  NULL
);
GO

-- Creating table 'UFlower'
CREATE TABLE [dbo].[UFlower] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_Nombre] nvarchar(50)  NULL,
    [U_Estado] nvarchar(10)  NULL
);
GO

-- Creating table 'UMovements'
CREATE TABLE [dbo].[UMovements] (
    [Code] nvarchar(30)  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [DocEntry] int  NOT NULL,
    [Canceled] char(1)  NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [UserSign] int  NULL,
    [Transfered] char(1)  NULL,
    [CreateDate] datetime  NULL,
    [CreateTime] smallint  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateTime] smallint  NULL,
    [DataSource] char(1)  NULL,
    [U_TIPO] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'UMovements_Acc'
CREATE TABLE [dbo].[UMovements_Acc] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_AcctCode] nvarchar(10)  NULL,
    [U_codigo] nvarchar(20)  NOT NULL,
    [U_ITEMCODE] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'UStyle'
CREATE TABLE [dbo].[UStyle] (
    [Code] nvarchar(30)  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [DocEntry] int  NOT NULL,
    [Canceled] char(1)  NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [UserSign] int  NULL,
    [Transfered] char(1)  NULL,
    [CreateDate] datetime  NULL,
    [CreateTime] smallint  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateTime] smallint  NULL,
    [DataSource] char(1)  NULL,
    [U_Estado] char(1)  NULL
);
GO

-- Creating table 'UStyle_filler'
CREATE TABLE [dbo].[UStyle_filler] (
    [Code] nvarchar(30)  NOT NULL,
    [LineId] int  NOT NULL,
    [Object] nvarchar(20)  NULL,
    [LogInst] int  NULL,
    [U_NOMBRE] nvarchar(50)  NOT NULL,
    [U_ESTADO] char(1)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Series] in table 'NNM1_Series'
ALTER TABLE [dbo].[NNM1_Series]
ADD CONSTRAINT [PK_NNM1_Series]
    PRIMARY KEY CLUSTERED ([Series] ASC);
GO

-- Creating primary key on [ItemCode] in table 'OITM_Articles'
ALTER TABLE [dbo].[OITM_Articles]
ADD CONSTRAINT [PK_OITM_Articles]
    PRIMARY KEY CLUSTERED ([ItemCode] ASC);
GO

-- Creating primary key on [ItemCode], [WhsCode] in table 'OITW_BranchArticles'
ALTER TABLE [dbo].[OITW_BranchArticles]
ADD CONSTRAINT [PK_OITW_BranchArticles]
    PRIMARY KEY CLUSTERED ([ItemCode], [WhsCode] ASC);
GO

-- Creating primary key on [WhsCode] in table 'OWHS_Branch'
ALTER TABLE [dbo].[OWHS_Branch]
ADD CONSTRAINT [PK_OWHS_Branch]
    PRIMARY KEY CLUSTERED ([WhsCode] ASC);
GO

-- Creating primary key on [IdTransferRequestDetailL] in table 'WTQ1_TransferRequestDetails'
ALTER TABLE [dbo].[WTQ1_TransferRequestDetails]
ADD CONSTRAINT [PK_WTQ1_TransferRequestDetails]
    PRIMARY KEY CLUSTERED ([IdTransferRequestDetailL] ASC);
GO

-- Creating primary key on [UserId] in table 'LocalUsers'
ALTER TABLE [dbo].[LocalUsers]
ADD CONSTRAINT [PK_LocalUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ObjType] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([ObjType] ASC);
GO

-- Creating primary key on [IdTransferL] in table 'OWTR_Transfers'
ALTER TABLE [dbo].[OWTR_Transfers]
ADD CONSTRAINT [PK_OWTR_Transfers]
    PRIMARY KEY CLUSTERED ([IdTransferL] ASC);
GO

-- Creating primary key on [IdTransferDetailL] in table 'WTR1_TransferDetails'
ALTER TABLE [dbo].[WTR1_TransferDetails]
ADD CONSTRAINT [PK_WTR1_TransferDetails]
    PRIMARY KEY CLUSTERED ([IdTransferDetailL] ASC);
GO

-- Creating primary key on [UserId], [ObjType] in table 'UserDocument'
ALTER TABLE [dbo].[UserDocument]
ADD CONSTRAINT [PK_UserDocument]
    PRIMARY KEY CLUSTERED ([UserId], [ObjType] ASC);
GO

-- Creating primary key on [IdParameters] in table 'Parameters'
ALTER TABLE [dbo].[Parameters]
ADD CONSTRAINT [PK_Parameters]
    PRIMARY KEY CLUSTERED ([IdParameters] ASC);
GO

-- Creating primary key on [IdPurchase] in table 'OPCH_Purchase'
ALTER TABLE [dbo].[OPCH_Purchase]
ADD CONSTRAINT [PK_OPCH_Purchase]
    PRIMARY KEY CLUSTERED ([IdPurchase] ASC);
GO

-- Creating primary key on [IdSupplierCreditNote] in table 'ORPC_SupplierCreditNotes'
ALTER TABLE [dbo].[ORPC_SupplierCreditNotes]
ADD CONSTRAINT [PK_ORPC_SupplierCreditNotes]
    PRIMARY KEY CLUSTERED ([IdSupplierCreditNote] ASC);
GO

-- Creating primary key on [IdPurchaseDetail] in table 'PCH1_PurchaseDetail'
ALTER TABLE [dbo].[PCH1_PurchaseDetail]
ADD CONSTRAINT [PK_PCH1_PurchaseDetail]
    PRIMARY KEY CLUSTERED ([IdPurchaseDetail] ASC);
GO

-- Creating primary key on [IdSupplierCreditNoteDetail] in table 'RPC1_SupplierCreditNoteDetail'
ALTER TABLE [dbo].[RPC1_SupplierCreditNoteDetail]
ADD CONSTRAINT [PK_RPC1_SupplierCreditNoteDetail]
    PRIMARY KEY CLUSTERED ([IdSupplierCreditNoteDetail] ASC);
GO

-- Creating primary key on [IdGoodReceiptL] in table 'OIGN_GoodsReceipt'
ALTER TABLE [dbo].[OIGN_GoodsReceipt]
ADD CONSTRAINT [PK_OIGN_GoodsReceipt]
    PRIMARY KEY CLUSTERED ([IdGoodReceiptL] ASC);
GO

-- Creating primary key on [IdGoodReceiptDetail] in table 'IGN1_GoodsReceiptDetail'
ALTER TABLE [dbo].[IGN1_GoodsReceiptDetail]
ADD CONSTRAINT [PK_IGN1_GoodsReceiptDetail]
    PRIMARY KEY CLUSTERED ([IdGoodReceiptDetail] ASC);
GO

-- Creating primary key on [IdGoodIssueDetail] in table 'IGE1_GoodsIssueDetail'
ALTER TABLE [dbo].[IGE1_GoodsIssueDetail]
ADD CONSTRAINT [PK_IGE1_GoodsIssueDetail]
    PRIMARY KEY CLUSTERED ([IdGoodIssueDetail] ASC);
GO

-- Creating primary key on [IdGoodIssueL] in table 'OIGE_GoodsIssues'
ALTER TABLE [dbo].[OIGE_GoodsIssues]
ADD CONSTRAINT [PK_OIGE_GoodsIssues]
    PRIMARY KEY CLUSTERED ([IdGoodIssueL] ASC);
GO

-- Creating primary key on [IdCheckbookL] in table 'Checkbook'
ALTER TABLE [dbo].[Checkbook]
ADD CONSTRAINT [PK_Checkbook]
    PRIMARY KEY CLUSTERED ([IdCheckbookL] ASC);
GO

-- Creating primary key on [Series], [IdCheckbookL] in table 'SeriesCheckbook'
ALTER TABLE [dbo].[SeriesCheckbook]
ADD CONSTRAINT [PK_SeriesCheckbook]
    PRIMARY KEY CLUSTERED ([Series], [IdCheckbookL] ASC);
GO

-- Creating primary key on [IdSaleL] in table 'OINV_Sales'
ALTER TABLE [dbo].[OINV_Sales]
ADD CONSTRAINT [PK_OINV_Sales]
    PRIMARY KEY CLUSTERED ([IdSaleL] ASC);
GO

-- Creating primary key on [IdSaleDetail] in table 'INV1_SalesDetail'
ALTER TABLE [dbo].[INV1_SalesDetail]
ADD CONSTRAINT [PK_INV1_SalesDetail]
    PRIMARY KEY CLUSTERED ([IdSaleDetail] ASC);
GO

-- Creating primary key on [IdPaymentType] in table 'PaymentTypes'
ALTER TABLE [dbo].[PaymentTypes]
ADD CONSTRAINT [PK_PaymentTypes]
    PRIMARY KEY CLUSTERED ([IdPaymentType] ASC);
GO

-- Creating primary key on [IdTransactionLog] in table 'TransactionLog'
ALTER TABLE [dbo].[TransactionLog]
ADD CONSTRAINT [PK_TransactionLog]
    PRIMARY KEY CLUSTERED ([IdTransactionLog] ASC);
GO

-- Creating primary key on [IdSpecialOrder] in table 'ORDR_SpecialOrder'
ALTER TABLE [dbo].[ORDR_SpecialOrder]
ADD CONSTRAINT [PK_ORDR_SpecialOrder]
    PRIMARY KEY CLUSTERED ([IdSpecialOrder] ASC);
GO

-- Creating primary key on [IdAtached] in table 'ATC1_Attachments'
ALTER TABLE [dbo].[ATC1_Attachments]
ADD CONSTRAINT [PK_ATC1_Attachments]
    PRIMARY KEY CLUSTERED ([IdAtached] ASC);
GO

-- Creating primary key on [IdSpecialOrderDetail] in table 'RDR1_SpecialOrderDetail'
ALTER TABLE [dbo].[RDR1_SpecialOrderDetail]
ADD CONSTRAINT [PK_RDR1_SpecialOrderDetail]
    PRIMARY KEY CLUSTERED ([IdSpecialOrderDetail] ASC);
GO

-- Creating primary key on [IdClientCreditNoteDetailL] in table 'RIN1_ClientCreditNoteDetail'
ALTER TABLE [dbo].[RIN1_ClientCreditNoteDetail]
ADD CONSTRAINT [PK_RIN1_ClientCreditNoteDetail]
    PRIMARY KEY CLUSTERED ([IdClientCreditNoteDetailL] ASC);
GO

-- Creating primary key on [IdClientCreditNoteL] in table 'ORIN_ClientCreditNotes'
ALTER TABLE [dbo].[ORIN_ClientCreditNotes]
ADD CONSTRAINT [PK_ORIN_ClientCreditNotes]
    PRIMARY KEY CLUSTERED ([IdClientCreditNoteL] ASC);
GO

-- Creating primary key on [IdDownPayment] in table 'ODPI_DownPayment'
ALTER TABLE [dbo].[ODPI_DownPayment]
ADD CONSTRAINT [PK_ODPI_DownPayment]
    PRIMARY KEY CLUSTERED ([IdDownPayment] ASC);
GO

-- Creating primary key on [IdDownPaymentDetail] in table 'DPI1_DownPaymentDetail'
ALTER TABLE [dbo].[DPI1_DownPaymentDetail]
ADD CONSTRAINT [PK_DPI1_DownPaymentDetail]
    PRIMARY KEY CLUSTERED ([IdDownPaymentDetail] ASC);
GO

-- Creating primary key on [IdDeposits] in table 'Deposits'
ALTER TABLE [dbo].[Deposits]
ADD CONSTRAINT [PK_Deposits]
    PRIMARY KEY CLUSTERED ([IdDeposits] ASC);
GO

-- Creating primary key on [IdCashMissing] in table 'CashMissing'
ALTER TABLE [dbo].[CashMissing]
ADD CONSTRAINT [PK_CashMissing]
    PRIMARY KEY CLUSTERED ([IdCashMissing] ASC);
GO

-- Creating primary key on [IdTransaction] in table 'OINM_Transaction'
ALTER TABLE [dbo].[OINM_Transaction]
ADD CONSTRAINT [PK_OINM_Transaction]
    PRIMARY KEY CLUSTERED ([IdTransaction] ASC);
GO

-- Creating primary key on [ObjType], [UserId] in table 'UserAuthorizationSet'
ALTER TABLE [dbo].[UserAuthorizationSet]
ADD CONSTRAINT [PK_UserAuthorizationSet]
    PRIMARY KEY CLUSTERED ([ObjType], [UserId] ASC);
GO

-- Creating primary key on [IdALOHA_Articles] in table 'ALOHA_Articles'
ALTER TABLE [dbo].[ALOHA_Articles]
ADD CONSTRAINT [PK_ALOHA_Articles]
    PRIMARY KEY CLUSTERED ([IdALOHA_Articles] ASC);
GO

-- Creating primary key on [ItemCode], [IdALOHA_Articles] in table 'OITM_ALOHA'
ALTER TABLE [dbo].[OITM_ALOHA]
ADD CONSTRAINT [PK_OITM_ALOHA]
    PRIMARY KEY CLUSTERED ([ItemCode], [IdALOHA_Articles] ASC);
GO

-- Creating primary key on [IdAuthorizationLog] in table 'AuthorizationLog'
ALTER TABLE [dbo].[AuthorizationLog]
ADD CONSTRAINT [PK_AuthorizationLog]
    PRIMARY KEY CLUSTERED ([IdAuthorizationLog] ASC);
GO

-- Creating primary key on [ItmsGrpCod] in table 'OITB_Groups'
ALTER TABLE [dbo].[OITB_Groups]
ADD CONSTRAINT [PK_OITB_Groups]
    PRIMARY KEY CLUSTERED ([ItmsGrpCod] ASC);
GO

-- Creating primary key on [IdInvoiceALOHA] in table 'InvoiceALOHA'
ALTER TABLE [dbo].[InvoiceALOHA]
ADD CONSTRAINT [PK_InvoiceALOHA]
    PRIMARY KEY CLUSTERED ([IdInvoiceALOHA] ASC);
GO

-- Creating primary key on [IdDetailInvoiceALOHA] in table 'DetailInvoiceALOHA'
ALTER TABLE [dbo].[DetailInvoiceALOHA]
ADD CONSTRAINT [PK_DetailInvoiceALOHA]
    PRIMARY KEY CLUSTERED ([IdDetailInvoiceALOHA] ASC);
GO

-- Creating primary key on [IdColors] in table 'Colors'
ALTER TABLE [dbo].[Colors]
ADD CONSTRAINT [PK_Colors]
    PRIMARY KEY CLUSTERED ([IdColors] ASC);
GO

-- Creating primary key on [IdCover] in table 'Cover'
ALTER TABLE [dbo].[Cover]
ADD CONSTRAINT [PK_Cover]
    PRIMARY KEY CLUSTERED ([IdCover] ASC);
GO

-- Creating primary key on [IdRibbon] in table 'Ribbon'
ALTER TABLE [dbo].[Ribbon]
ADD CONSTRAINT [PK_Ribbon]
    PRIMARY KEY CLUSTERED ([IdRibbon] ASC);
GO

-- Creating primary key on [IdFilling] in table 'Filling'
ALTER TABLE [dbo].[Filling]
ADD CONSTRAINT [PK_Filling]
    PRIMARY KEY CLUSTERED ([IdFilling] ASC);
GO

-- Creating primary key on [IdBusinessPartner] in table 'OCRD_BusinessPartner'
ALTER TABLE [dbo].[OCRD_BusinessPartner]
ADD CONSTRAINT [PK_OCRD_BusinessPartner]
    PRIMARY KEY CLUSTERED ([IdBusinessPartner] ASC);
GO

-- Creating primary key on [IdTransferRequestL] in table 'OWTQ_TransferRequest'
ALTER TABLE [dbo].[OWTQ_TransferRequest]
ADD CONSTRAINT [PK_OWTQ_TransferRequest]
    PRIMARY KEY CLUSTERED ([IdTransferRequestL] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Code], [DocEntry] in table 'UCakes'
ALTER TABLE [dbo].[UCakes]
ADD CONSTRAINT [PK_UCakes]
    PRIMARY KEY CLUSTERED ([Code], [DocEntry] ASC);
GO

-- Creating primary key on [Code], [Name] in table 'UCategory'
ALTER TABLE [dbo].[UCategory]
ADD CONSTRAINT [PK_UCategory]
    PRIMARY KEY CLUSTERED ([Code], [Name] ASC);
GO

-- Creating primary key on [Code], [LineId] in table 'UColor_Covert'
ALTER TABLE [dbo].[UColor_Covert]
ADD CONSTRAINT [PK_UColor_Covert]
    PRIMARY KEY CLUSTERED ([Code], [LineId] ASC);
GO

-- Creating primary key on [Code], [LineId] in table 'UColor_Flower'
ALTER TABLE [dbo].[UColor_Flower]
ADD CONSTRAINT [PK_UColor_Flower]
    PRIMARY KEY CLUSTERED ([Code], [LineId] ASC);
GO

-- Creating primary key on [Code], [LineId] in table 'UColor_Ribbon'
ALTER TABLE [dbo].[UColor_Ribbon]
ADD CONSTRAINT [PK_UColor_Ribbon]
    PRIMARY KEY CLUSTERED ([Code], [LineId] ASC);
GO

-- Creating primary key on [Code], [LineId] in table 'UColor_top'
ALTER TABLE [dbo].[UColor_top]
ADD CONSTRAINT [PK_UColor_top]
    PRIMARY KEY CLUSTERED ([Code], [LineId] ASC);
GO

-- Creating primary key on [Code], [LineId] in table 'UCover'
ALTER TABLE [dbo].[UCover]
ADD CONSTRAINT [PK_UCover]
    PRIMARY KEY CLUSTERED ([Code], [LineId] ASC);
GO

-- Creating primary key on [Code], [DocEntry] in table 'UEspecialty'
ALTER TABLE [dbo].[UEspecialty]
ADD CONSTRAINT [PK_UEspecialty]
    PRIMARY KEY CLUSTERED ([Code], [DocEntry] ASC);
GO

-- Creating primary key on [Code], [LineId] in table 'UFlower'
ALTER TABLE [dbo].[UFlower]
ADD CONSTRAINT [PK_UFlower]
    PRIMARY KEY CLUSTERED ([Code], [LineId] ASC);
GO

-- Creating primary key on [Code], [DocEntry], [U_TIPO] in table 'UMovements'
ALTER TABLE [dbo].[UMovements]
ADD CONSTRAINT [PK_UMovements]
    PRIMARY KEY CLUSTERED ([Code], [DocEntry], [U_TIPO] ASC);
GO

-- Creating primary key on [Code], [LineId], [U_codigo], [U_ITEMCODE] in table 'UMovements_Acc'
ALTER TABLE [dbo].[UMovements_Acc]
ADD CONSTRAINT [PK_UMovements_Acc]
    PRIMARY KEY CLUSTERED ([Code], [LineId], [U_codigo], [U_ITEMCODE] ASC);
GO

-- Creating primary key on [Code], [DocEntry] in table 'UStyle'
ALTER TABLE [dbo].[UStyle]
ADD CONSTRAINT [PK_UStyle]
    PRIMARY KEY CLUSTERED ([Code], [DocEntry] ASC);
GO

-- Creating primary key on [Code], [LineId], [U_NOMBRE] in table 'UStyle_filler'
ALTER TABLE [dbo].[UStyle_filler]
ADD CONSTRAINT [PK_UStyle_filler]
    PRIMARY KEY CLUSTERED ([Code], [LineId], [U_NOMBRE] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ItemCode] in table 'OITW_BranchArticles'
ALTER TABLE [dbo].[OITW_BranchArticles]
ADD CONSTRAINT [FK_OITM_ArticlesOITW_BranchArticles]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [WhsCode] in table 'OITW_BranchArticles'
ALTER TABLE [dbo].[OITW_BranchArticles]
ADD CONSTRAINT [FK_OWHS_BranchOITW_BranchArticles]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOITW_BranchArticles'
CREATE INDEX [IX_FK_OWHS_BranchOITW_BranchArticles]
ON [dbo].[OITW_BranchArticles]
    ([WhsCode]);
GO

-- Creating foreign key on [ObjType] in table 'UserDocument'
ALTER TABLE [dbo].[UserDocument]
ADD CONSTRAINT [FK_DocumentsUserDocument]
    FOREIGN KEY ([ObjType])
    REFERENCES [dbo].[Documents]
        ([ObjType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentsUserDocument'
CREATE INDEX [IX_FK_DocumentsUserDocument]
ON [dbo].[UserDocument]
    ([ObjType]);
GO

-- Creating foreign key on [UserId] in table 'UserDocument'
ALTER TABLE [dbo].[UserDocument]
ADD CONSTRAINT [FK_LocalUsersUserDocument]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[LocalUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdPurchase] in table 'PCH1_PurchaseDetail'
ALTER TABLE [dbo].[PCH1_PurchaseDetail]
ADD CONSTRAINT [FK_OPCH_FacturasCompraPCH1_DetalleFacturasCompra]
    FOREIGN KEY ([IdPurchase])
    REFERENCES [dbo].[OPCH_Purchase]
        ([IdPurchase])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OPCH_FacturasCompraPCH1_DetalleFacturasCompra'
CREATE INDEX [IX_FK_OPCH_FacturasCompraPCH1_DetalleFacturasCompra]
ON [dbo].[PCH1_PurchaseDetail]
    ([IdPurchase]);
GO

-- Creating foreign key on [IdSupplierCreditNote] in table 'RPC1_SupplierCreditNoteDetail'
ALTER TABLE [dbo].[RPC1_SupplierCreditNoteDetail]
ADD CONSTRAINT [FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores]
    FOREIGN KEY ([IdSupplierCreditNote])
    REFERENCES [dbo].[ORPC_SupplierCreditNotes]
        ([IdSupplierCreditNote])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores'
CREATE INDEX [IX_FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores]
ON [dbo].[RPC1_SupplierCreditNoteDetail]
    ([IdSupplierCreditNote]);
GO

-- Creating foreign key on [IdGoodReceiptL] in table 'IGN1_GoodsReceiptDetail'
ALTER TABLE [dbo].[IGN1_GoodsReceiptDetail]
ADD CONSTRAINT [FK_OIGN_GoodsReceiptIGN1_GoodsReceiptDetail]
    FOREIGN KEY ([IdGoodReceiptL])
    REFERENCES [dbo].[OIGN_GoodsReceipt]
        ([IdGoodReceiptL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OIGN_GoodsReceiptIGN1_GoodsReceiptDetail'
CREATE INDEX [IX_FK_OIGN_GoodsReceiptIGN1_GoodsReceiptDetail]
ON [dbo].[IGN1_GoodsReceiptDetail]
    ([IdGoodReceiptL]);
GO

-- Creating foreign key on [IdGoodIssueL] in table 'IGE1_GoodsIssueDetail'
ALTER TABLE [dbo].[IGE1_GoodsIssueDetail]
ADD CONSTRAINT [FK_OIGE_GoodsIssuesIGE1_GoodsIssueDetail]
    FOREIGN KEY ([IdGoodIssueL])
    REFERENCES [dbo].[OIGE_GoodsIssues]
        ([IdGoodIssueL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OIGE_GoodsIssuesIGE1_GoodsIssueDetail'
CREATE INDEX [IX_FK_OIGE_GoodsIssuesIGE1_GoodsIssueDetail]
ON [dbo].[IGE1_GoodsIssueDetail]
    ([IdGoodIssueL]);
GO

-- Creating foreign key on [Series] in table 'SeriesCheckbook'
ALTER TABLE [dbo].[SeriesCheckbook]
ADD CONSTRAINT [FK_Series]
    FOREIGN KEY ([Series])
    REFERENCES [dbo].[NNM1_Series]
        ([Series])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdCheckbookL] in table 'SeriesCheckbook'
ALTER TABLE [dbo].[SeriesCheckbook]
ADD CONSTRAINT [FK_CheckbookSeriesCheckbook]
    FOREIGN KEY ([IdCheckbookL])
    REFERENCES [dbo].[Checkbook]
        ([IdCheckbookL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckbookSeriesCheckbook'
CREATE INDEX [IX_FK_CheckbookSeriesCheckbook]
ON [dbo].[SeriesCheckbook]
    ([IdCheckbookL]);
GO

-- Creating foreign key on [IdSaleL] in table 'INV1_SalesDetail'
ALTER TABLE [dbo].[INV1_SalesDetail]
ADD CONSTRAINT [FK_OINV_SalesINV1_SalesDetail]
    FOREIGN KEY ([IdSaleL])
    REFERENCES [dbo].[OINV_Sales]
        ([IdSaleL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OINV_SalesINV1_SalesDetail'
CREATE INDEX [IX_FK_OINV_SalesINV1_SalesDetail]
ON [dbo].[INV1_SalesDetail]
    ([IdSaleL]);
GO

-- Creating foreign key on [PaymentTypesIdPaymentType] in table 'PaymentTypes'
ALTER TABLE [dbo].[PaymentTypes]
ADD CONSTRAINT [FK_PaymentTypesPaymentTypes]
    FOREIGN KEY ([PaymentTypesIdPaymentType])
    REFERENCES [dbo].[PaymentTypes]
        ([IdPaymentType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentTypesPaymentTypes'
CREATE INDEX [IX_FK_PaymentTypesPaymentTypes]
ON [dbo].[PaymentTypes]
    ([PaymentTypesIdPaymentType]);
GO

-- Creating foreign key on [IdPaymentType] in table 'INV1_SalesDetail'
ALTER TABLE [dbo].[INV1_SalesDetail]
ADD CONSTRAINT [FK_PaymentTypesINV1_SalesDetail]
    FOREIGN KEY ([IdPaymentType])
    REFERENCES [dbo].[PaymentTypes]
        ([IdPaymentType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentTypesINV1_SalesDetail'
CREATE INDEX [IX_FK_PaymentTypesINV1_SalesDetail]
ON [dbo].[INV1_SalesDetail]
    ([IdPaymentType]);
GO

-- Creating foreign key on [IdRoyalty] in table 'INV1_SalesDetail'
ALTER TABLE [dbo].[INV1_SalesDetail]
ADD CONSTRAINT [FK_RoyaltiesTypesRel]
    FOREIGN KEY ([IdRoyalty])
    REFERENCES [dbo].[PaymentTypes]
        ([IdPaymentType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoyaltiesTypesRel'
CREATE INDEX [IX_FK_RoyaltiesTypesRel]
ON [dbo].[INV1_SalesDetail]
    ([IdRoyalty]);
GO

-- Creating foreign key on [IdEspecialOrder] in table 'ATC1_Attachments'
ALTER TABLE [dbo].[ATC1_Attachments]
ADD CONSTRAINT [FK_ORDR_SpecialOrderATC1_Attachments]
    FOREIGN KEY ([IdEspecialOrder])
    REFERENCES [dbo].[ORDR_SpecialOrder]
        ([IdSpecialOrder])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ORDR_SpecialOrderATC1_Attachments'
CREATE INDEX [IX_FK_ORDR_SpecialOrderATC1_Attachments]
ON [dbo].[ATC1_Attachments]
    ([IdEspecialOrder]);
GO

-- Creating foreign key on [IdSpecialOrder] in table 'RDR1_SpecialOrderDetail'
ALTER TABLE [dbo].[RDR1_SpecialOrderDetail]
ADD CONSTRAINT [FK_ORDR_SpecialOrderRDR1_SpecialOrderDetail]
    FOREIGN KEY ([IdSpecialOrder])
    REFERENCES [dbo].[ORDR_SpecialOrder]
        ([IdSpecialOrder])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ORDR_SpecialOrderRDR1_SpecialOrderDetail'
CREATE INDEX [IX_FK_ORDR_SpecialOrderRDR1_SpecialOrderDetail]
ON [dbo].[RDR1_SpecialOrderDetail]
    ([IdSpecialOrder]);
GO

-- Creating foreign key on [IdClientCreditNoteL] in table 'RIN1_ClientCreditNoteDetail'
ALTER TABLE [dbo].[RIN1_ClientCreditNoteDetail]
ADD CONSTRAINT [FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores1]
    FOREIGN KEY ([IdClientCreditNoteL])
    REFERENCES [dbo].[ORIN_ClientCreditNotes]
        ([IdClientCreditNoteL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores1'
CREATE INDEX [IX_FK_ORPC_NotaCreditoProveedoresRPC1_DetalleNotaCreditoProveedores1]
ON [dbo].[RIN1_ClientCreditNoteDetail]
    ([IdClientCreditNoteL]);
GO

-- Creating foreign key on [IdDownPaymentL] in table 'DPI1_DownPaymentDetail'
ALTER TABLE [dbo].[DPI1_DownPaymentDetail]
ADD CONSTRAINT [FK_OINV_SalesINV1_SalesDetail1]
    FOREIGN KEY ([IdDownPaymentL])
    REFERENCES [dbo].[ODPI_DownPayment]
        ([IdDownPayment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OINV_SalesINV1_SalesDetail1'
CREATE INDEX [IX_FK_OINV_SalesINV1_SalesDetail1]
ON [dbo].[DPI1_DownPaymentDetail]
    ([IdDownPaymentL]);
GO

-- Creating foreign key on [LocalUsers_UserId] in table 'UserAuthorizationSet'
ALTER TABLE [dbo].[UserAuthorizationSet]
ADD CONSTRAINT [FK_UserAuthorizationLocalUsers]
    FOREIGN KEY ([LocalUsers_UserId])
    REFERENCES [dbo].[LocalUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAuthorizationLocalUsers'
CREATE INDEX [IX_FK_UserAuthorizationLocalUsers]
ON [dbo].[UserAuthorizationSet]
    ([LocalUsers_UserId]);
GO

-- Creating foreign key on [OITM_Articles_ItemCode] in table 'OITM_ALOHA'
ALTER TABLE [dbo].[OITM_ALOHA]
ADD CONSTRAINT [FK_OITM_ALOHAOITM_Articles]
    FOREIGN KEY ([OITM_Articles_ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ALOHAOITM_Articles'
CREATE INDEX [IX_FK_OITM_ALOHAOITM_Articles]
ON [dbo].[OITM_ALOHA]
    ([OITM_Articles_ItemCode]);
GO

-- Creating foreign key on [IdALOHA_Articles] in table 'OITM_ALOHA'
ALTER TABLE [dbo].[OITM_ALOHA]
ADD CONSTRAINT [FK_ALOHA_ArticlesOITM_ALOHA]
    FOREIGN KEY ([IdALOHA_Articles])
    REFERENCES [dbo].[ALOHA_Articles]
        ([IdALOHA_Articles])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ALOHA_ArticlesOITM_ALOHA'
CREATE INDEX [IX_FK_ALOHA_ArticlesOITM_ALOHA]
ON [dbo].[OITM_ALOHA]
    ([IdALOHA_Articles]);
GO

-- Creating foreign key on [ObjType] in table 'UserAuthorizationSet'
ALTER TABLE [dbo].[UserAuthorizationSet]
ADD CONSTRAINT [FK_DocumentsUserAuthorization]
    FOREIGN KEY ([ObjType])
    REFERENCES [dbo].[Documents]
        ([ObjType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [WhsCode] in table 'ORIN_ClientCreditNotes'
ALTER TABLE [dbo].[ORIN_ClientCreditNotes]
ADD CONSTRAINT [FK_OWHS_BranchORIN_ClientCreditNotes]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchORIN_ClientCreditNotes'
CREATE INDEX [IX_FK_OWHS_BranchORIN_ClientCreditNotes]
ON [dbo].[ORIN_ClientCreditNotes]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'ODPI_DownPayment'
ALTER TABLE [dbo].[ODPI_DownPayment]
ADD CONSTRAINT [FK_OWHS_BranchODPI_DownPayment]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchODPI_DownPayment'
CREATE INDEX [IX_FK_OWHS_BranchODPI_DownPayment]
ON [dbo].[ODPI_DownPayment]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'OWTR_Transfers'
ALTER TABLE [dbo].[OWTR_Transfers]
ADD CONSTRAINT [FK_OWHS_BranchOWTR_Transfers]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOWTR_Transfers'
CREATE INDEX [IX_FK_OWHS_BranchOWTR_Transfers]
ON [dbo].[OWTR_Transfers]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'OPCH_Purchase'
ALTER TABLE [dbo].[OPCH_Purchase]
ADD CONSTRAINT [FK_OWHS_BranchOPCH_Purchase]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOPCH_Purchase'
CREATE INDEX [IX_FK_OWHS_BranchOPCH_Purchase]
ON [dbo].[OPCH_Purchase]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'ORPC_SupplierCreditNotes'
ALTER TABLE [dbo].[ORPC_SupplierCreditNotes]
ADD CONSTRAINT [FK_OWHS_BranchORPC_SupplierCreditNotes]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchORPC_SupplierCreditNotes'
CREATE INDEX [IX_FK_OWHS_BranchORPC_SupplierCreditNotes]
ON [dbo].[ORPC_SupplierCreditNotes]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'OIGN_GoodsReceipt'
ALTER TABLE [dbo].[OIGN_GoodsReceipt]
ADD CONSTRAINT [FK_OWHS_BranchOIGN_GoodsReceipt]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOIGN_GoodsReceipt'
CREATE INDEX [IX_FK_OWHS_BranchOIGN_GoodsReceipt]
ON [dbo].[OIGN_GoodsReceipt]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'OIGE_GoodsIssues'
ALTER TABLE [dbo].[OIGE_GoodsIssues]
ADD CONSTRAINT [FK_OWHS_BranchOIGE_GoodsIssues]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOIGE_GoodsIssues'
CREATE INDEX [IX_FK_OWHS_BranchOIGE_GoodsIssues]
ON [dbo].[OIGE_GoodsIssues]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'OINV_Sales'
ALTER TABLE [dbo].[OINV_Sales]
ADD CONSTRAINT [FK_OWHS_BranchOINV_Sales]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOINV_Sales'
CREATE INDEX [IX_FK_OWHS_BranchOINV_Sales]
ON [dbo].[OINV_Sales]
    ([WhsCode]);
GO

-- Creating foreign key on [WhsCode] in table 'ORDR_SpecialOrder'
ALTER TABLE [dbo].[ORDR_SpecialOrder]
ADD CONSTRAINT [FK_OWHS_BranchORDR_SpecialOrder]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchORDR_SpecialOrder'
CREATE INDEX [IX_FK_OWHS_BranchORDR_SpecialOrder]
ON [dbo].[ORDR_SpecialOrder]
    ([WhsCode]);
GO

-- Creating foreign key on [ItmsGrpCod] in table 'OITM_Articles'
ALTER TABLE [dbo].[OITM_Articles]
ADD CONSTRAINT [FK_OITB_GroupsOITM_Articles]
    FOREIGN KEY ([ItmsGrpCod])
    REFERENCES [dbo].[OITB_Groups]
        ([ItmsGrpCod])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITB_GroupsOITM_Articles'
CREATE INDEX [IX_FK_OITB_GroupsOITM_Articles]
ON [dbo].[OITM_Articles]
    ([ItmsGrpCod]);
GO

-- Creating foreign key on [DetailInvoiceALOHAIdDetailInvoiceALOHA] in table 'ALOHA_Articles'
ALTER TABLE [dbo].[ALOHA_Articles]
ADD CONSTRAINT [FK_DetailInvoiceALOHAALOHA_Articles]
    FOREIGN KEY ([DetailInvoiceALOHAIdDetailInvoiceALOHA])
    REFERENCES [dbo].[DetailInvoiceALOHA]
        ([IdDetailInvoiceALOHA])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailInvoiceALOHAALOHA_Articles'
CREATE INDEX [IX_FK_DetailInvoiceALOHAALOHA_Articles]
ON [dbo].[ALOHA_Articles]
    ([DetailInvoiceALOHAIdDetailInvoiceALOHA]);
GO

-- Creating foreign key on [InvoiceALOHAIdInvoiceALOHA] in table 'DetailInvoiceALOHA'
ALTER TABLE [dbo].[DetailInvoiceALOHA]
ADD CONSTRAINT [FK_InvoiceALOHADetailInvoiceALOHA]
    FOREIGN KEY ([InvoiceALOHAIdInvoiceALOHA])
    REFERENCES [dbo].[InvoiceALOHA]
        ([IdInvoiceALOHA])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceALOHADetailInvoiceALOHA'
CREATE INDEX [IX_FK_InvoiceALOHADetailInvoiceALOHA]
ON [dbo].[DetailInvoiceALOHA]
    ([InvoiceALOHAIdInvoiceALOHA]);
GO

-- Creating foreign key on [ItemCode] in table 'RDR1_SpecialOrderDetail'
ALTER TABLE [dbo].[RDR1_SpecialOrderDetail]
ADD CONSTRAINT [FK_OITM_ArticlesRDR1_SpecialOrderDetail]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesRDR1_SpecialOrderDetail'
CREATE INDEX [IX_FK_OITM_ArticlesRDR1_SpecialOrderDetail]
ON [dbo].[RDR1_SpecialOrderDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'INV1_SalesDetail'
ALTER TABLE [dbo].[INV1_SalesDetail]
ADD CONSTRAINT [FK_OITM_ArticlesINV1_SalesDetail]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesINV1_SalesDetail'
CREATE INDEX [IX_FK_OITM_ArticlesINV1_SalesDetail]
ON [dbo].[INV1_SalesDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'IGE1_GoodsIssueDetail'
ALTER TABLE [dbo].[IGE1_GoodsIssueDetail]
ADD CONSTRAINT [FK_OITM_ArticlesIGE1_GoodsIssueDetail]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesIGE1_GoodsIssueDetail'
CREATE INDEX [IX_FK_OITM_ArticlesIGE1_GoodsIssueDetail]
ON [dbo].[IGE1_GoodsIssueDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [OITM_Articles_ItemCode] in table 'IGN1_GoodsReceiptDetail'
ALTER TABLE [dbo].[IGN1_GoodsReceiptDetail]
ADD CONSTRAINT [FK_OITM_ArticlesIGN1_GoodsReceiptDetail]
    FOREIGN KEY ([OITM_Articles_ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesIGN1_GoodsReceiptDetail'
CREATE INDEX [IX_FK_OITM_ArticlesIGN1_GoodsReceiptDetail]
ON [dbo].[IGN1_GoodsReceiptDetail]
    ([OITM_Articles_ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'WTQ1_TransferRequestDetails'
ALTER TABLE [dbo].[WTQ1_TransferRequestDetails]
ADD CONSTRAINT [FK_OITM_ArticlesWTQ1_TransferRequestDetails]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesWTQ1_TransferRequestDetails'
CREATE INDEX [IX_FK_OITM_ArticlesWTQ1_TransferRequestDetails]
ON [dbo].[WTQ1_TransferRequestDetails]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'RPC1_SupplierCreditNoteDetail'
ALTER TABLE [dbo].[RPC1_SupplierCreditNoteDetail]
ADD CONSTRAINT [FK_OITM_ArticlesRPC1_SupplierCreditNoteDetail]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesRPC1_SupplierCreditNoteDetail'
CREATE INDEX [IX_FK_OITM_ArticlesRPC1_SupplierCreditNoteDetail]
ON [dbo].[RPC1_SupplierCreditNoteDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'PCH1_PurchaseDetail'
ALTER TABLE [dbo].[PCH1_PurchaseDetail]
ADD CONSTRAINT [FK_OITM_ArticlesPCH1_PurchaseDetail]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesPCH1_PurchaseDetail'
CREATE INDEX [IX_FK_OITM_ArticlesPCH1_PurchaseDetail]
ON [dbo].[PCH1_PurchaseDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'WTR1_TransferDetails'
ALTER TABLE [dbo].[WTR1_TransferDetails]
ADD CONSTRAINT [FK_OITM_ArticlesWTR1_TransferDetails]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesWTR1_TransferDetails'
CREATE INDEX [IX_FK_OITM_ArticlesWTR1_TransferDetails]
ON [dbo].[WTR1_TransferDetails]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'DPI1_DownPaymentDetail'
ALTER TABLE [dbo].[DPI1_DownPaymentDetail]
ADD CONSTRAINT [FK_OITM_ArticlesDPI1_DownPaymentDetail1]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesDPI1_DownPaymentDetail1'
CREATE INDEX [IX_FK_OITM_ArticlesDPI1_DownPaymentDetail1]
ON [dbo].[DPI1_DownPaymentDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [ItemCode] in table 'RIN1_ClientCreditNoteDetail'
ALTER TABLE [dbo].[RIN1_ClientCreditNoteDetail]
ADD CONSTRAINT [FK_OITM_ArticlesRIN1_ClientCreditNoteDetail]
    FOREIGN KEY ([ItemCode])
    REFERENCES [dbo].[OITM_Articles]
        ([ItemCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OITM_ArticlesRIN1_ClientCreditNoteDetail'
CREATE INDEX [IX_FK_OITM_ArticlesRIN1_ClientCreditNoteDetail]
ON [dbo].[RIN1_ClientCreditNoteDetail]
    ([ItemCode]);
GO

-- Creating foreign key on [IdColors] in table 'Cover'
ALTER TABLE [dbo].[Cover]
ADD CONSTRAINT [FK_CoverColors]
    FOREIGN KEY ([IdColors])
    REFERENCES [dbo].[Colors]
        ([IdColors])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CoverColors'
CREATE INDEX [IX_FK_CoverColors]
ON [dbo].[Cover]
    ([IdColors]);
GO

-- Creating foreign key on [ColorsIdColors] in table 'Ribbon'
ALTER TABLE [dbo].[Ribbon]
ADD CONSTRAINT [FK_ColorsRibbon]
    FOREIGN KEY ([ColorsIdColors])
    REFERENCES [dbo].[Colors]
        ([IdColors])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ColorsRibbon'
CREATE INDEX [IX_FK_ColorsRibbon]
ON [dbo].[Ribbon]
    ([ColorsIdColors]);
GO

-- Creating foreign key on [ColorsIdColors] in table 'Filling'
ALTER TABLE [dbo].[Filling]
ADD CONSTRAINT [FK_ColorsFilling]
    FOREIGN KEY ([ColorsIdColors])
    REFERENCES [dbo].[Colors]
        ([IdColors])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ColorsFilling'
CREATE INDEX [IX_FK_ColorsFilling]
ON [dbo].[Filling]
    ([ColorsIdColors]);
GO

-- Creating foreign key on [WhsCode] in table 'OWTQ_TransferRequest'
ALTER TABLE [dbo].[OWTQ_TransferRequest]
ADD CONSTRAINT [FK_OWHS_BranchOWTQ_TransferRequest]
    FOREIGN KEY ([WhsCode])
    REFERENCES [dbo].[OWHS_Branch]
        ([WhsCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWHS_BranchOWTQ_TransferRequest'
CREATE INDEX [IX_FK_OWHS_BranchOWTQ_TransferRequest]
ON [dbo].[OWTQ_TransferRequest]
    ([WhsCode]);
GO

-- Creating foreign key on [IdTransferRequestL] in table 'WTQ1_TransferRequestDetails'
ALTER TABLE [dbo].[WTQ1_TransferRequestDetails]
ADD CONSTRAINT [FK_OWTQ_OrdersWTQ1_OrderDetails]
    FOREIGN KEY ([IdTransferRequestL])
    REFERENCES [dbo].[OWTQ_TransferRequest]
        ([IdTransferRequestL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWTQ_OrdersWTQ1_OrderDetails'
CREATE INDEX [IX_FK_OWTQ_OrdersWTQ1_OrderDetails]
ON [dbo].[WTQ1_TransferRequestDetails]
    ([IdTransferRequestL]);
GO

-- Creating foreign key on [IdTransferL] in table 'WTR1_TransferDetails'
ALTER TABLE [dbo].[WTR1_TransferDetails]
ADD CONSTRAINT [FK_OWTR_TransfersWTR1_TransferDetails]
    FOREIGN KEY ([IdTransferL])
    REFERENCES [dbo].[OWTR_Transfers]
        ([IdTransferL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OWTR_TransfersWTR1_TransferDetails'
CREATE INDEX [IX_FK_OWTR_TransfersWTR1_TransferDetails]
ON [dbo].[WTR1_TransferDetails]
    ([IdTransferL]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------