using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public class BackOrderService
    {
        public bool AddBackOrder(string Barcode,string GoodsName,string PurchasePrice,string SellingPrice,
            string Specifications,int GoodsCount,string Merchant,string MerchantCode,string Classify,string ClassifyCode
            ,int UsersID,string Flag,string Remark,string InStock,int  ID,out string Msg)
        {
            using (Entities ctx=new Entities())
            {
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
                ctx.le_backorder.Add(model);
                int count = ctx.SaveChanges();
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
        public List<le_backorder> GetbackOrderList(int UserID,string Flag, DateTime? BeginTime, DateTime? EndTime)
        {
            using (Entities ctx=new Entities())
            {
                var result = ctx.le_backorder.Where(s => true);
                 result = result.Where(s => s.UsersID == UserID);
                if(!string.IsNullOrEmpty(Flag))
                { 
                    result=result.Where(s=>s.Flag==Flag);

                }
                if(BeginTime!=null)
                {
                    result = result.Where(s => s.CreateTime >= BeginTime);
                }
                if(EndTime!=null)
                {
                    result = result.Where(s => s.CreateTime <= EndTime);
                }
                return result.ToList();
            }
        }
        
    }
}
