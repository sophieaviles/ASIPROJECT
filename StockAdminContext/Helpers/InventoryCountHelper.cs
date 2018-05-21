using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class InventoryCountHelper
    {
        public static    ObservableCollection<InventoryCount> GetInventoryCount(OITB_Groups selecteGroup,
            DateTime? startDate, DateTime? endDate, string keywords, LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<InventoryCount> count = db.InventoryCount.Include("InventoryCountDetail");

                if (selecteGroup!=null) count = count.Where(c => c.ItmsGrpCod == selecteGroup.ItmsGrpCod);
                if (startDate.HasValue) count = count.Where(c => c.DocDate >= startDate.Value);
                if (endDate.HasValue) count = count.Where(c => c.DocDate <= endDate.Value);
                if (!string.IsNullOrEmpty(keywords)) count = count.Where(c => c.Comments.Contains(keywords));
                if (status.HasValue) count = count.Where(c => c.StateL == status.Value);

                var grpNames = (from item in count
                    join grp in db.OITB_Groups on item.ItmsGrpCod equals grp.ItmsGrpCod
                    select new
                           {
                               Code = item.ItmsGrpCod,
                               GroupName = grp.ItmsGrpNam,
                           }).ToList();
                var collection = count.ToList();

               

                collection.ForEach(c =>
                                   {
                                       var grp = grpNames.FirstOrDefault(g => g.Code == c.ItmsGrpCod);
                                       if (grp != null) c.GroupName = grp.GroupName;
                                   });

                return new ObservableCollection<InventoryCount>(collection);
            }

        }

        public static void AddOrUpdate(InventoryCount selectedInventoryCount)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                if (selectedInventoryCount.IdInventoryCountL == 0)
                {
                    db.InventoryCount.Add(selectedInventoryCount);
                }

                else db.Entry(selectedInventoryCount).State = EntityState.Modified;
                }
            }

        public static void Delete(InventoryCount selectedInventoryCount)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (selectedInventoryCount.IdInventoryCountL <= 0) return;
                var sic = db.InventoryCount.FirstOrDefault(
                    ic => ic.IdInventoryCountL == selectedInventoryCount.IdInventoryCountL);
                if (sic != null) db.InventoryCount.Remove(sic);
                //selectedInventoryCount.ChangeEntityState(EntityState.Deleted);
            }
        }

        public static ObservableCollection<InventoryCountDetail> GetDetalCollection(OITB_Groups selectedGroup ,InventoryCount inventory)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<OITM_Articles> dbArticles = db.OITM_Articles
                                                      .OrderByDescending(p => p.TemplateL);

                if (selectedGroup != null)
                {
                    dbArticles = dbArticles.Where(i => i.ItmsGrpCod == selectedGroup.ItmsGrpCod);
                }
                  var details = (

                   from br in db.OITW_BranchArticles.Where(b=> b.WhsCode == Config.WhsCode)
                   join  i in dbArticles on br.ItemCode equals i.ItemCode
                   join cat in db.UCategories on i.U_categoria equals  cat.Code into scategorires
                   from scat in scategorires.DefaultIfEmpty() 
                   //Validaciones de articulos
                   where i.InvntItem.Contains("Y") && i.validFor.Contains("Y") 
                   && br.Locked.Contains("N")

                  
                  // where  i.ItmsGrpCod == selectedGroup.ItmsGrpCod
                   
                    select new 
                    {
                        ItemCode =  i.ItemCode,
                        Dscription =  i.ItemName,
                        Quantity = br.OnHand1.HasValue? br.OnHand1.Value:0,
                        Unit =  i.InvntryUom,
                        OnHand =  br.OnHand1.HasValue? br.OnHand1.Value:0,
                        OITM_Articles = i,
                        Category = (scat == null? string.Empty:scat.Name),
                        template = i.TemplateL,
                    }).OrderByDescending(p=> p.template)
                    .ThenByDescending(c=> c.Category).ThenBy(e=> e.Dscription).ToList();
                  
                var branchDetails = details.Select(i =>
                                                         new InventoryCountDetail()
                                                         {
                                                             ItemCode = i.ItemCode,
                                                             Dscription = i.Dscription,
                                                             Quantity = i.Quantity,
                                                             Unit = i.Unit,
                                                             OnHand = i.OnHand,
                                                             OITM_Articles = i.OITM_Articles,
                                                             Category = i.Category,
                                                         }
                                                     );                              

                //// Add articles with category null
                //var productsIds = branchDetails.Select(b => b.ItemCode).ToList();
                // IQueryable<OITM_Articles> pendingItems =
                //    dbArticles.Where(p => !productsIds.Contains(p.ItemCode) && string.IsNullOrEmpty(p.U_categoria));

                //var pendingProducts = ( from br in branch.OITW_BranchArticles
                //                        join article in  pendingItems on br.ItemCode equals  article.ItemCode
                //                        select new InventoryCountDetail()
                //                        {
                //                            ItemCode = article.ItemCode,
                //                            Dscription = article.ItemName,
                //                            Quantity = br.OnHand1.HasValue ? br.OnHand1.Value : 0,
                //                            Unit = article.InvntryUom,
                //                            OnHand = br.OnHand1.HasValue ? br.OnHand1.Value : 0,
                //                            OITM_Articles = article,
                                            
                //                        }).ToList();

                //branchDetails.AddRange(pendingProducts);

                inventory.InventoryCountDetail.ForEach(d =>
                                                       {
                                                         
                                                           var product =  branchDetails.FirstOrDefault(i => i.ItemCode == d.ItemCode);
                                                           if (product != null)
                                                           {
                                                               d.OnHand = product.OnHand;
                                                               d.Category = product.Category;
                                                               //d.Quantity = product.Quantity;
                                                               d.Unit = product.Unit;
                                                               //branchDetails.Remove(product);
                                                               //branchDetails.Add(d);
                                                           }
                                                          
                                                       });
                if(!inventory.InventoryCountDetail.Any()) return new ObservableCollection<InventoryCountDetail>( branchDetails);
                else return new ObservableCollection<InventoryCountDetail>(inventory.InventoryCountDetail);
            }
        }


        public static void Process(InventoryCount selectedInventoryCount)
        {            
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                ContextFactory.SaveChanges();

                 var diferencias = (
                 from t0 in db.InventoryCountDetail// selectedInventoryCount.InventoryCountDetail //Conteo fisico
                 join t1 in  db.OITW_BranchArticles on t0.ItemCode equals t1.ItemCode 
                 join t2 in db.UMovements_Acc on t0.ItemCode equals t2.U_ITEMCODE 
                 where t1.WhsCode.Equals(Config.WhsCode) && (t0.Quantity - t1.OnHand1) != 0
                 && t0.IdInventoryCountL == selectedInventoryCount.IdInventoryCountL
                 && (t2.U_IDMOVIMIENTO.Equals("1") || t2.U_IDMOVIMIENTO.Equals("2"))
                    select new {
                    itemcode = t0.ItemCode,
                    whscode = t1.WhsCode,
                    Acc = t2.U_CUENTA,
                    dif = t0.Quantity - t1.OnHand1,
                    tipo = t2.U_IDMOVIMIENTO
                    }
                ).ToList();

                var entradas = diferencias.Where(c => c.dif > 0 && c.tipo.Equals("1") ).ToList();
                var salidas = diferencias.Where(c => c.dif < 0 && c.tipo.Equals("2") ).ToList();

                //Crear entradas y salidas


                var receipt = new OIGN_GoodsReceipt()
                {
                    Comments = "Ajuste Entrada por Conteo Fisico - SIGI",
                    IdMovement = "1",
                    ItmsGrpCod = selectedInventoryCount.ItmsGrpCod
                };

                var issue = new OIGE_GoodsIssues()
                {
                    Comments = "Ajuste de Salida por Conteo Fisico - SIGI",
                    IdMovement = "2",
                    ItmsGrpCod = selectedInventoryCount.ItmsGrpCod
                };  


                if (salidas.Any())
                {
                    
                    //agregar detalles
                    salidas.ForEach(d=> {
                        var article =db.OITM_Articles.FirstOrDefault(c => c.ItemCode == d.itemcode);
                       
                        issue.IGE1_GoodsIssueDetail.Add(new IGE1_GoodsIssueDetail()
                                                {
                                                    ItemCode = article.ItemCode,
                                                    Dscription = article.ItemName,
                                                    Quantity = d.dif * -1,
                                                    UnitMsr = article.InvntryUom,
                                                    AcctCode = d.Acc ?? Config.DefaultAcc,
                                                });
                        });
                    db.OIGE_GoodsIssues.Add(issue);
                    ContextFactory.SaveChanges();                   
                }
                if (entradas.Any())
                {                   
                    //agregar detalles
                     entradas.ForEach(d=> {
                        var article =db.OITM_Articles.FirstOrDefault(c => c.ItemCode == d.itemcode);
                       
                        receipt.IGN1_GoodsReceiptDetail.Add(new IGN1_GoodsReceiptDetail()
                                                {
                                                    ItemCode = article.ItemCode,
                                                    Dscription = article.ItemName,
                                                    Quantity = d.dif,
                                                    UnitMsr = article.InvntryUom,
                                                    AcctCode = d.Acc ?? Config.DefaultAcc,
                                                });
                        });
                     db.OIGN_GoodsReceipt.Add(receipt);
                     ContextFactory.SaveChanges();               
                }

                //Luego de creados lo ajustes marcar como procesado el conteo

                selectedInventoryCount.StateL = LocalStatus.Procesado;
                ContextFactory.SaveChanges();   
                
                //procesar entradas
                if (receipt.IGN1_GoodsReceiptDetail.Any()) Synchronization.Synchronize(receipt);

                //Procesar salida
                if (issue.IGE1_GoodsIssueDetail.Any()) Synchronization.Synchronize(issue);

                ContextFactory.SaveChanges();                       
            }
        }
    }
}
