using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class OINM_Transaction : BaseModel
    {

        public OINM_Transaction(OWTR_Transfers obj, WTR1_TransferDetails detail)
        {
            this.TransNum = obj.DocEntry??0;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = obj.JrnlMemo;
            this.ItemCode = detail.ItemCode;
            this.Dscription = detail.Dscription;
            if (obj.Filler == Config.WhsCode) // se le esta sacando producto
            {
                this.InQty = 0;
                this.OutQty1 = detail.Quantity;
            }
            else {
                if (detail.WhsCode == Config.WhsCode) this.InQty = detail.Quantity;
                else this.InQty = 0;                
                this.OutQty1 = 0;
            }
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;           
        }

        public OINM_Transaction(OPCH_Purchase obj, PCH1_PurchaseDetail detail)
        {
            this.TransNum = obj.DocEntry;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = "";
            this.ItemCode = detail.ItemCode;
            this.Dscription = detail.Dscription;
            this.InQty = detail.Quantity;
            this.OutQty1 = 0;
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;  
        }

        public OINM_Transaction(OIGN_GoodsReceipt obj, IGN1_GoodsReceiptDetail detail)
        {
            this.TransNum = obj.DocEntry ?? 0;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = "";
            this.ItemCode = detail.ItemCode;
            this.Dscription = detail.Dscription;
            this.InQty = detail.Quantity;
            this.OutQty1 = 0;
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;  
        }

        public OINM_Transaction(OIGE_GoodsIssues obj, IGE1_GoodsIssueDetail detail)
        {
            this.TransNum = obj.DocEntry ?? 0;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = "";
            this.ItemCode = detail.ItemCode;
            this.Dscription = detail.Dscription;
            this.InQty = 0;
            this.OutQty1 = detail.Quantity;
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;  
        }

        public OINM_Transaction(OINV_Sales obj, INV1_SalesDetail detail)
        {
            this.TransNum = obj.DocEntry;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = "";
            this.ItemCode = detail.ItemCode??"";
            this.Dscription = detail.Dscription;
            this.InQty = 0;
            this.OutQty1 = detail.Quantity??0;
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;  
        }

        public OINM_Transaction(ORIN_ClientCreditNotes obj, RIN1_ClientCreditNoteDetail detail)
        {
            this.TransNum = obj.DocEntry;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = "";
            this.ItemCode = string.IsNullOrEmpty( detail.ItemCode) ? null: detail.ItemCode;
            this.Dscription = detail.Dscription;
            this.InQty = detail.Quantity ?? 0;
            this.OutQty1 = 0;
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;  
        }

        public OINM_Transaction(ORPC_SupplierCreditNotes obj, RPC1_SupplierCreditNoteDetail detail)
        {
            this.TransNum = obj.DocEntry;
            this.Comments = obj.Comments;
            this.TransType = int.Parse(obj.ObjType);
            this.DocDate = obj.DocDate;
            this.DocDueDate = obj.DocDueDate;
            this.Ref1 = "";
            this.Ref2 = "";
            this.Comments = obj.Comments;
            this.JrnMemo = "";
            this.ItemCode = detail.ItemCode??null;
            this.Dscription = detail.Dscription;
            this.InQty = 0;
            this.OutQty1 = detail.Quantity??0;
            this.Price = detail.Price;
            this.SerialNum = "";
            this.WareHouse = detail.WhsCode;  
        }

        [Key]
        public int IdTransaction { get; set; }
        public int TransNum { get; set; }
        public int TransType { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Comments { get; set; }
        public string JrnMemo { get; set; }
        public int DocTime { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public decimal? InQty { get; set; }
        public decimal? OutQty1 { get; set; }
        public decimal? Price { get; set; }
        public string SerialNum { get; set; }
        public string WareHouse { get; set; }
        public bool HasToBeSync { get; set; }
    }
}
