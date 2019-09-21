using Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.Common.Enum;

namespace Service
{
   public class BackOrderService
    {
        private static ILog log = LogManager.GetLogger(typeof(BackOrderService));
        public bool AddBackOrder(string Barcode,string GoodsName,string PurchasePrice,string SellingPrice,
            string Specifications,int GoodsCount,string Merchant,string MerchantCode,string Classify,string ClassifyCode
            ,int UsersID,string Flag,string Remark,string InStock,int  ID,out string Msg)
        {
            log.Error(Barcode + GoodsName + PurchasePrice + SellingPrice + Specifications + GoodsCount + Merchant + MerchantCode + Classify + ClassifyCode);
            using (Entities ctx=new Entities())
            {
                bool IsAdd = true;
                le_backorder model = new le_backorder();
                if (ID!=0)
                {
                    model = ctx.le_backorder.Where(s => s.ID == ID).FirstOrDefault();
                    if(model==null)
                    {
                        Msg = "ID输入错误";
                        return false;
                    }
                }
                else
                {
                    string start = DateTime.Now.ToString("yyyy-MM-dd ") + "00:00:00";
                    string end = DateTime.Now.ToString("yyyy-MM-dd ") + "23:59:59";
                    var startDate = Convert.ToDateTime(start);
                    var endDate = Convert.ToDateTime(end);
                    model = ctx.le_backorder.Where(s => s.UsersID == UsersID && s.Flag == Flag && s.CreateTime> startDate&&s.CreateTime<= endDate && s.BarCode == Barcode).FirstOrDefault();
                    if(model!=null)
                    {
                        IsAdd = false;
                    }
                    else
                    {
                        model = new le_backorder();
                    }
                }
                
                
                model.Classify = Classify;
                model.ClassifyCode = ClassifyCode;
                model.BarCode = Barcode;
                model.GoodsCount = GoodsCount;
                model.GoodsName = GoodsName;
                model.Merchant = Merchant;
                model.MerchantCode = MerchantCode;
                model.PurchasePrice = PurchasePrice;
                model.SellingPrice = SellingPrice;
                model.Specifications = Specifications;
                model.UpdateTime = DateTime.Now;
                model.CreateTime = DateTime.Now;
                model.UsersID = UsersID;
                model.Flag = Flag;
                model.Remark = Remark;
                model.InStock = InStock;
                int count;
                if (IsAdd)
                {
                    ctx.le_backorder.Add(model);
                    count = ctx.SaveChanges();
                }
                else
                {
                    ctx.Entry<le_backorder>(model).State = EntityState.Modified;
                    count = ctx.SaveChanges();
                }
                if(count>0)
                {
                    Msg = "SUCCESS";
                    return true;
                }
                else
                {
                    Msg = "FAIL";
                    return false;
                }
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Flag"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="InStock"></param>
        /// <returns></returns>
        public List<le_backorder> GetbackOrderList(int UserID,string Flag, DateTime? BeginTime, DateTime? EndTime, string MerchantCode, BackOrderOrderByType OrderByType,string InStock="" )
        {
            using (Entities ctx=new Entities())
            {
                var result = ctx.le_backorder.Where(s => true);
                 result = result.Where(s => s.UsersID == UserID);
                //if(!string.IsNullOrEmpty(Flag))
                //{ 
                //    result=result.Where(s=>s.Flag==Flag);
                //}
                if(!string.IsNullOrEmpty(InStock))
                {
                    result = result.Where(s => s.InStock == InStock);
                }
                if(!string.IsNullOrEmpty(MerchantCode))
                {
                    result = result.Where(s => s.MerchantCode == MerchantCode);
                }
                if(BeginTime!=null)
                {
                    result = result.Where(s => s.CreateTime >= BeginTime);
                }
                if(EndTime!=null)
                {
                    result = result.Where(s => s.CreateTime <= EndTime);
                }
                switch( OrderByType )
                {
                    case BackOrderOrderByType.BarCodeAsc:
                        result = result.OrderBy(s => s.BarCode);
                        break;
                    case BackOrderOrderByType.BarCodeDesc:
                        result = result.OrderByDescending(s => s.BarCode);
                        break;
                    case BackOrderOrderByType.MerchantAsc:
                        result = result.OrderBy(s => s.Merchant);
                        break;
                    case BackOrderOrderByType.MerchantDesc:
                        result = result.OrderByDescending(s => s.Merchant);
                        break;
                    case BackOrderOrderByType.UpdateTimeAsc:
                        result = result.OrderBy(s => s.UpdateTime);
                        break;
                    case BackOrderOrderByType.UpdateTimeDesc:
                        result = result.OrderByDescending(s => s.UpdateTime);
                        break;
                    default:
                        result = result.OrderByDescending(s => s.UpdateTime);
                        break;
                }
                
                var tolist= result.ToList();

                tolist = tolist.GroupBy(s => s.BarCode).Select(s => new le_backorder
                {
                    SellingPrice = s.Max(k => k.SellingPrice),
                    BarCode = s.Key,
                    Specifications = s.Max(k => k.Specifications),
                    InStock = s.Max(k => k.InStock),
                    Classify = s.Max(k => k.Classify),
                    ClassifyCode = s.Max(k => k.ClassifyCode),
                    CreateTime = s.Max(k => k.CreateTime),
                    Flag = s.Max(k => k.Flag),
                    GoodsCount = s.Sum(k => k.GoodsCount),
                    GoodsName = s.Max(k => k.GoodsName),
                    Merchant = s.Max(k => k.Merchant),
                    MerchantCode = s.Max(k => k.MerchantCode),
                    PurchasePrice = s.Max(k => k.PurchasePrice),
                    Remark = s.Max(k => k.Remark),
                    UpdateTime = s.Max(k => k.UpdateTime),
                    UsersID = s.Max(k => k.UsersID),
                }).ToList();
                return tolist;

            }

        }
        
    }
}
