using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 答应列表
    /// </summary>
    public class PrintingService
    {
        public  List<le_orders_lines_mapping> GetLinesMappingList(string OutNo,int SupplierID)
        {
            using (Entities ctx=new Entities())
            {
                string SupplierIDstr = SupplierID.ToString();
                if(string.IsNullOrEmpty(OutNo))
                {
                    var result = ctx.le_orders_lines_mapping.Where(s => s.OutTradeNo == OutNo && s.A == SupplierIDstr).ToList();
                    return result;
                }
                else
                {
                    var result = ctx.le_orders_lines_mapping.Where(s=> s.A == SupplierIDstr).ToList();
                    return result;
                }
               
                
            }
        }

        public bool CreateOrUpdate(string OutNo,int SupplierID,string A,string B,string C,string Remarks,string Types)
        {
            using (Entities ctx=new Entities())
            {
                string SupplierIDstr = SupplierID.ToString();
                var Model = ctx.le_orders_lines_mapping.Where(s => s.OutTradeNo == OutNo && s.A == SupplierIDstr).FirstOrDefault();
                bool IsAdd;
                if(Model==null)
                {
                    Model = new le_orders_lines_mapping();
                    Model.A = SupplierIDstr;
                    Model.B = B;
                    Model.C = C;
                    Model.Remarks = Remarks;
                    Model.Types= Types;
                    Model.PrintingTimes = 1;
                    IsAdd = true;
                    Model.OutTradeNo = OutNo;
                }
                else
                {
                    Model.PrintingTimes += 1;
                    Model.A = SupplierIDstr;
                    Model.B = B;
                    Model.C = C;
                    Model.Remarks = Remarks;
                    Model.Types = Types;
                    
                    IsAdd = false;
                    Model.OutTradeNo = OutNo;
                }
                if(IsAdd)
                {
                    ctx.le_orders_lines_mapping.Add(Model);
                }
                else
                {
                    ctx.Entry<le_orders_lines_mapping>(Model).State = System.Data.Entity.EntityState.Modified;

                }
                if(ctx.SaveChanges()>0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
