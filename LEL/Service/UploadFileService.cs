using Common;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace Service
{
    public class UploadFileService
    {
        /// <summary>
        /// 导出供应商列表
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable ExportSupplierDt(string fileName)
        {
            using (Entities ctx = new Entities())
            {
                var Supplier = ctx.le_suppliers.Where(s => s.Status == 1).Select(s => new
                {
                    s.SuppliersID,
                    s.SuppliersName,
                    s.ResponPeople,
                    s.MobilePhone
                }).ToList();

                var dt = DataTableToListHelper.ToDataTable(Supplier);
                return dt;
                //DataRow dr = dt.NewRow();
                //dr["SuppliersID"] = "供应商ID";
                //dr["SuppliersName"] = "供应商名";
                //dr["SuppliersResponPeople"] = "供应商负责人";
                //dr["SuppliersMobilePhone"] = "供应商联系电话";
                //dt.Rows.InsertAt(dr, 0);

                //int count =ExcelHelper.DataTableToExcel(fileName,dt , "供应商表",true);              
                //return count;
            }

        }
        /// <summary>
        /// 导出商品分类表
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable ExportGoodsGroupDt(string fileName)
        {
            using (Entities ctx = new Entities())
            {
                var GoodsList = ctx.le_goodsgroups.Select(s => new
                {
                    s.Name,
                    s.ID,
                }).ToList();
                var dt = DataTableToListHelper.ToDataTable(GoodsList);
                return dt;
                //int count = ExcelHelper.DataTableToExcel(fileName, dt, "商品分类表", true);
                //return count;
                //  return 0;
            }
        }

        /// <summary>
        /// 导出供应商价格
        /// </summary>
        /// <returns></returns>
        public DataTable ExportGoodsSupplierPrice()
        {
            using (Entities ctx = new Entities())
            {
                var GoodSupplier = ctx.le_goods_suppliers.Select(s => new
                {
                    s.le_goods.GoodsName,
                    s.le_goods.GoodsID,
                    s.GoodsMappingID,
                    s.SuppliersID,
                    s.Supplyprice,
                    s.IsDefalut,
                    s.IsDeleted,
                    s.le_suppliers.SuppliersName
                }).ToList();
                var dt = DataTableToListHelper.ToDataTable(GoodSupplier);
                return dt;
            }
        }

        /// <summary>
        /// 导出商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable ExportGoodsInfo()
        {
            using (Entities ctx = new Entities())
            {
                var GoodsInfo = ctx.le_goods.Select(s => new
                {
                    s.GoodsID,
                    s.GoodsName,

                }).ToList();
                var dt = DataTableToListHelper.ToDataTable(GoodsInfo);
                return dt;
            }
        }

        /// <summary>
        /// 从Excel导入数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool InsetDb(string fileName, out string Msg)
        {
            //DataColumn[] GoodsColumns = { new DataColumn("Status"), new DataColumn("ErrorMessage"), new DataColumn("Index") };
            //DataColumn[] GoodsBusinessImgolumns = { new DataColumn("Status"), new DataColumn("ErrorMessage"), new DataColumn("Index") };
            //DataColumn[] GoodsValueColumns = { new DataColumn("Status"), new DataColumn("ErrorMessage"), new DataColumn("Index") };
            //DataColumn[] GoodsSupperColumns = { new DataColumn("Status"), new DataColumn("ErrorMessage"), new DataColumn("Index") };

            string GoodsNumber;
            var GoodsDT = ExcelHelper.DataReaderExcelFile(fileName, "商品录入");
            var GoodsAttachImg1T = ExcelHelper.DataReaderExcelFile(fileName, "商品图片");
            var GoodsValueDT = ExcelHelper.DataReaderExcelFile(fileName, "商品属性");
            var GoodsSupperDT = ExcelHelper.DataReaderExcelFile(fileName, "供应商价格表");
            Random rd = new Random();
            using (Entities ctx = new Entities())
            {
                for (int i = 1; i < GoodsDT.Rows.Count; i++) //跳过第一行示例
                {
                    GoodsNumber = GoodsDT.Rows[i]["商品序列号"].ToString();
                    try
                    {
                        le_goods GoodsModel = new le_goods();

                        #region 数据格式检查

                        if (!int.TryParse(GoodsDT.Rows[i]["装箱数"].ToString(), out int a))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的装箱数格式错误", GoodsNumber);
                            return false;
                        }
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["建议零售价"].ToString(), out decimal bb))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的建议零售价格式错误", GoodsNumber);
                            return false;
                        }
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["划线价"].ToString(), out decimal c))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的划线价格式错误", GoodsNumber);
                            return false;
                        }
                        #endregion

                        GoodsModel.Describe = GoodsDT.Rows[i]["商品描述"].ToString();
                        if (string.IsNullOrEmpty(GoodsModel.Describe))
                        {
                            Msg = string.Format("未能在【商品录入】获取到商品序列号为：{0},的商品描述", GoodsNumber);
                            return false;
                        }
                        GoodsModel.GoodsGroupsID = Convert.ToInt32(GoodsDT.Rows[i]["商品分类ID"].ToString());
                        GoodsModel.GoodsName = GoodsDT.Rows[i]["商品名称"].ToString();
                        GoodsModel.Image = "/GoodImg/" + GoodsDT.Rows[i]["商品主图"].ToString();
                        GoodsModel.SpecialOffer = Convert.ToDecimal(GoodsDT.Rows[i]["建议零售价"].ToString());
                        GoodsModel.OriginalPrice = Convert.ToDecimal(GoodsDT.Rows[i]["划线价"].ToString());
                        GoodsModel.MSRP = Convert.ToDecimal(GoodsDT.Rows[i]["特价"].ToString());
                        GoodsModel.PackingNumber = Convert.ToInt32(GoodsDT.Rows[i]["装箱数"].ToString());
                        GoodsModel.ShelfLife = string.IsNullOrEmpty(GoodsDT.Rows[i]["保质期"].ToString()) ? "0" : GoodsDT.Rows[i]["保质期"].ToString();
                        GoodsModel.Sort = 999;
                        GoodsModel.Specifications = GoodsDT.Rows[i]["商品规格（例：盒/箱/件/个）"].ToString();
                        GoodsModel.Stock = Convert.ToInt32(GoodsDT.Rows[i]["库存"].ToString());
                        GoodsModel.Quota = Convert.ToInt32(GoodsDT.Rows[i]["每人限购"].ToString());
                        GoodsModel.IsBulkCargo = Convert.ToInt32(GoodsDT.Rows[i]["是否为散货(1是,0不是)"].ToString()) == 1 ? 1 : 0; ;
                        GoodsModel.MinimumPurchase = Convert.ToInt32(GoodsDT.Rows[i]["最小采购量"].ToString());
                        GoodsModel.RowVersion = DateTime.Now;
                        GoodsModel.IsShelves = Convert.ToInt32(GoodsDT.Rows[i]["上架(0不上架/1上架)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.IsRecommend = Convert.ToInt32(GoodsDT.Rows[i]["是否推荐(0否/1是)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.IsHot = Convert.ToInt32(GoodsDT.Rows[i]["是否热门(0否/1是)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.IsSeckill = Convert.ToInt32(GoodsDT.Rows[i]["是否秒杀(0否/1是)"].ToString()) == 1 ? 1 : 0;

                        //model.Province = dto.Province;
                        //model.City = dto.City;
                        //model.Area = dto.Area;
                        //model.PiecePrice = dto.PiecePrice;
                        //model.MinimumPrice = dto.MinimumPrice;
                        //model.BusinessValue = dto.BusinessValue;
                        //model.NewPeriod = dto.NewPeriod;
                        //model.Unit = dto.Unit;
                        //model.IsRandomDistribution = dto.IsRandomDistribution;
                        GoodsModel.PriceScheme1 = 0;
                        GoodsModel.PriceScheme2 = 0;
                        GoodsModel.PriceScheme3 = 0;
                        GoodsModel.IsParcel =0;

                        //获取商品图片
                        DataRow[] FileterImg = GoodsAttachImg1T.Select("商品序列号= '" + GoodsNumber + "'");
                        if (FileterImg == null || FileterImg.Length <= 0) //为设置图片
                        {
                            le_goods_img GoodsImg = new le_goods_img();
                            GoodsImg.CreatTime = DateTime.Now;
                            GoodsImg.Src = "/GoodImg/0.jpg";
                            GoodsImg.IsDelete = 0;
                            GoodsModel.le_goods_img.Add(GoodsImg);
                            //Msg = string.Format("未能在【商品图片】获取到商品序列号为：{0},的图片", GoodsNumber);
                            //return false;
                        }
                        else
                        {
                            foreach (var ImgModel in FileterImg)
                            {
                                le_goods_img GoodsImg = new le_goods_img();
                                GoodsImg.CreatTime = DateTime.Now;
                                GoodsImg.Src = "/GoodImg/" + ImgModel["商品图片名"];
                                GoodsImg.IsDelete = 0;
                                GoodsModel.le_goods_img.Add(GoodsImg);
                            }
                        }

                        //获取供应商价格
                        DataRow[] FileterSupplier = GoodsSupperDT.Select("商品序列号= '" + GoodsNumber + "'");
                        var b = FileterSupplier[0].Field<object>("是否为默认供应商(1是,0不是)");
                        if (FileterSupplier == null || FileterSupplier.Length <= 0)
                        {
                            Msg = string.Format("未能在【供应商价格表】获取到商品序列号为：{0},的供应商价格", GoodsNumber);
                            return false;
                        }
                        DataRow[] RepeatSupplier = FileterSupplier.Where(s => s.Field<string>("是否为默认供应商(1是,0不是)") == "1").ToArray();
                        if (RepeatSupplier.Length > 1 || RepeatSupplier.Length < 1)
                        {
                            Msg = string.Format("必须且只能有一个默认供货商！在【供应商价格表】,商品序列号为{0}", GoodsNumber);
                            return false;
                        }
                        var result = FileterSupplier.GroupBy(s => s["供应商ID"]).Select(s => s.ToList()).Count();
                        if (FileterSupplier.GroupBy(s => s["供应商ID"]).Select(s => s.ToList()).Count() > 1)
                        {
                            Msg = string.Format("同商品供应商不可重复！在【供应商价格表】,商品序列号为{0}", GoodsNumber);
                            return false;
                        }

                        foreach (var GoodsValue in FileterSupplier)
                        {
                            string SupplierID = GoodsValue["供应商ID"].ToString();
                            int Count = FileterSupplier.Where(s => s.Field<string>("供应商ID") == SupplierID).ToArray().Count();
                            if (Count > 1)
                            {
                                Msg = string.Format("同商品供应商不可重复！在【供应商价格表】,商品序列号为{0}", GoodsNumber);
                                return false;
                            }
                            le_goods_suppliers model = new le_goods_suppliers();
                            model.IsDeleted = 0;
                            model.SuppliersID = Convert.ToInt32(GoodsValue["供应商ID"]);
                            model.Supplyprice = Convert.ToDecimal(GoodsValue["供应商价格"]);
                            model.IsDefalut = Convert.ToInt32(GoodsValue["是否为默认供应商(1是,0不是)"]);

                            model.CreatTime = DateTime.Now;
                            GoodsModel.le_goods_suppliers.Add(model);
                        }

                        //获取属性
                        DataRow[] FileterValues = GoodsValueDT.Select("商品序列号= '" + GoodsNumber + "'");
                        if (FileterValues == null || FileterValues.Length <= 0)
                        {
                            Msg = string.Format("未能在【商品属性】Sheet获取到商品序列号为：{0},的商品属性", GoodsNumber);
                            return false;
                        }
                        int k = 0;
                        for (int p = 0; p < FileterValues.Length; p++)
                        {

                            le_goods_value GVmodel = new le_goods_value();
                            GVmodel.CategoryType = Convert.ToInt32(FileterValues[p]["属性类型（例：口味）2（例：颜色）3（例：尺寸）4未定 5 未定"]);
                            if (GVmodel.CategoryType < 0 || GVmodel.CategoryType > 5)
                            {
                                Msg = string.Format("未能在【商品属性】获取到商品序列号为：{0},的商品属性类型范围错误限定范围【12345】", GoodsNumber);
                                return false;
                            }
                            GVmodel.Enable = 1;
                            GVmodel.GoodsValue = FileterValues[p]["属性值"].ToString();
                            GVmodel.SerialNumber = FileterValues[p]["条码"].ToString();
                            GVmodel.CreateTime = DateTime.Now;
                            GVmodel.UpdateTime = DateTime.Now;
                            if (string.IsNullOrEmpty(GVmodel.SerialNumber))
                            {
                                GVmodel.IsAuto = 1;
                                GVmodel.SerialNumber = new GoodsService().BarcodeGeneration(GoodsModel.IsBulkCargo);

                            }
                            GVmodel.IsBulkCargo = GoodsModel.IsBulkCargo;
                            GoodsModel.le_goods_value.Add(GVmodel);
                            k++;
                            //ctx.le_goods_value.Add(model);
                        }
                        ctx.le_goods.Add(GoodsModel);
                    }
                  
                    catch (Exception ex)
                    {
                        Msg = "数据格式错误请检查数据源,请勿随意添加修改列,详细：" + ex.Message + "| 在【商品录入】中商品序列号为:"+ GoodsNumber + "";
                        return false;
                        ///throw ex;
                    }
                }
                try
                {
                    if (ctx.SaveChanges() > 0)
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
                catch (DbEntityValidationException ex)
                {
                   
                    Msg = "数据类型错误:" + ExceptionHelper.GetInnerExceptionMsg(ex);
                    return false;
                }
                catch(Exception ex)
                {
                    Msg ="数据库保存失败:" + ExceptionHelper.GetInnerExceptionMsg(ex);
                    return false;
                }
            }



            return true;
        }

        /// <summary>
        /// 商品价格信息从Excel导入数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool GoodsSupplierPriceInsertDb(string fileName, out string Msg)
        {
            string GoodsNumber;
            var GoodSupplierDT = ExcelHelper.DataReaderExcelFile(fileName, "商品供应价格");

            using (Entities ctx = new Entities())
            {
                for (int i = 0; i < GoodSupplierDT.Rows.Count; i++) //跳过第一行示例
                {

                    le_goods_suppliers model = new le_goods_suppliers();

                    model.Supplyprice = Convert.ToDecimal(GoodSupplierDT.Rows[i]["供应价格"]);
                    model.IsDefalut = Convert.ToInt32(GoodSupplierDT.Rows[i]["是否为默认供应商（1 为默认）"]);
                    model.SuppliersID = Convert.ToInt32(GoodSupplierDT.Rows[i]["供应商ID"]);
                    model.GoodsID = Convert.ToInt32(GoodSupplierDT.Rows[i]["商品ID"]);
                    model.CreatTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    if (ctx.le_goods_suppliers.Any(s => s.GoodsID == model.GoodsID && s.SuppliersID == model.SuppliersID))
                    {
                        Msg = string.Format("商品ID为[{0}]得商品已存在相同得供应商，请检查数据源", model.GoodsID);
                        return false;

                    }
                    ctx.le_goods_suppliers.Add(model);

                }
                try
                {
                    if (ctx.SaveChanges() > 0)
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
                catch (Exception ex)
                {
                    Msg = ex.Message;
                    return false;
                    throw ex;
                }
            }

            return true;
        }
    }
}
