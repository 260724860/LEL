using DTO.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StoreBulkCargoService
    {
        /// <summary>
        /// 新增门店散货表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool AddStoreBulkCargo(StoreBulkCargoDto dto)
        {
            using (Entities ctx = new Entities())
            {
                le_store_bulkcargo model = new le_store_bulkcargo();
                model.a = dto.a;
                model.b = dto.b;
                model.BarCode = dto.BarCode;
                model.c = dto.c;
                model.CreateTime = DateTime.Now;
                model.d = dto.d;
                model.GoodsID = dto.GoodsID;
                model.Name = dto.Name;
                model.UpdateTime = DateTime.Now;
                model.UserID = dto.UserID;

                ctx.le_store_bulkcargo.Add(model);
                if (ctx.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
           // return false;
        }

        /// <summary>
        /// 获取门店散货
        /// </summary>
        /// <param name="Barcode"></param>
        /// <param name="Name"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<StoreBulkCargoDto> GetStoreBulkCargoDtos(string Barcode,string Name,int? UserID)
        {
            using (Entities ctx=new Entities())
            {
                var temp = ctx.le_store_bulkcargo.Where(s => true);
                if(!string.IsNullOrEmpty(Barcode))
                {
                    temp = temp.Where(s => s.BarCode== Barcode);
                }
                if(!string.IsNullOrEmpty(Name))
                {
                    temp = temp.Where(s => s.Name == Name);
                }
                if(UserID.HasValue)
                {
                    temp = temp.Where(s => s.UserID == UserID);
                }
                var result = temp.Select(s => new StoreBulkCargoDto
                {
                    a = s.a,
                    StoreCode=s.StoreCode,
                    b=s.b,
                    BarCode=s.BarCode,
                    c=s.c,
                    d =s.d,
                    GoodsID =s.GoodsID,
                    ID =s.ID,
                    Name =s.Name,
                    UserID =s.UserID
                }).ToList();

                return result;

            }
        }
    }
}
