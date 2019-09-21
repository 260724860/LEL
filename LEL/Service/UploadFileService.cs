using Common;
using System;
using System.Collections.Generic;
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
        /// 搜索商品信息并且导出
        /// </summary>
        /// <returns></returns>
        //public DataTable ExportGoodsInfoBySerach()
        //{
            
        //}
        
        /// <summary>
        /// 从Excel导入数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool InsertGoodsBaseInfo(string fileName, out string Msg)
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
            if (GoodsDT.Rows.Count <= 1)
            {
                Msg = string.Format("未检索到任何行!");
                return false;
            }
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
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["（金额满减）满"].ToString(), out decimal PriceFull))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的（金额满减）满格式错误", GoodsNumber);
                            return false;
                        }
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["（金额满减）减"].ToString(), out decimal PriceReduction))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的【（金额满减）减】格式错误", GoodsNumber);
                            return false;
                        }
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["（数量满减）满"].ToString(), out decimal CountFull))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的【（数量满减）满】格式错误", GoodsNumber);
                            return false;
                        }
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["（数量满减）减"].ToString(), out decimal CountReduction))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的（数量满减）减式错误", GoodsNumber);
                            return false;
                        }
                        if (!int.TryParse(GoodsDT.Rows[i]["积分"].ToString(), out int Integral))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的【积分】格式错误", GoodsNumber);
                            return false;
                        }
                        if (!Decimal.TryParse(GoodsDT.Rows[i]["折扣"].ToString(), out decimal Discount))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的【折扣】格式错误", GoodsNumber);
                            return false;
                        }

                        DateTime? SeckillBeginTime = null;
                        DateTime? SeckillEndTime = null;
                        DateTime? QuotaBeginTime = null;
                        DateTime? QuotaEndTime = null;
                        if (!string.IsNullOrEmpty(GoodsDT.Rows[i]["秒杀开始时间"].ToString()))
                        {
                            try {
                                SeckillBeginTime = DateTime.ParseExact(GoodsDT.Rows[i]["秒杀开始时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                            }
                            catch(Exception ex)
                            {
                                Msg = string.Format("在【商品录入】中商品序列号：{0}的【秒杀开始时间】格式错误", GoodsNumber);
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(GoodsDT.Rows[i]["秒杀结束时间"].ToString()))
                        {
                            try
                            {
                                SeckillEndTime = DateTime.ParseExact(GoodsDT.Rows[i]["秒杀结束时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (Exception ex)
                            {
                                Msg = string.Format("在【商品录入】中商品序列号：{0}的【秒杀结束时间】格式错误", GoodsNumber);
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(GoodsDT.Rows[i]["限购开始时间"].ToString()))
                        {
                            try
                            {
                                QuotaBeginTime = DateTime.ParseExact(GoodsDT.Rows[i]["限购开始时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (Exception ex)
                            {
                                Msg = string.Format("在【商品录入】中商品序列号：{0}的【限购开始时间】格式错误", GoodsNumber);
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(GoodsDT.Rows[i]["限购结束时间"].ToString()))
                        {
                            try
                            {
                                QuotaEndTime = DateTime.ParseExact(GoodsDT.Rows[i]["限购结束时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (Exception ex)
                            {
                                Msg = string.Format("在【商品录入】中商品序列号：{0}的【限购结束时间】格式错误", GoodsNumber);
                                return false;
                            }
                        }
                        if(string.IsNullOrEmpty(GoodsDT.Rows[i]["库存"].ToString()))
                        {
                            Msg = string.Format("在【商品录入】中商品序列号：{0}的【库存】格式错误", GoodsNumber);
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
                        if(GoodsModel.MinimumPurchase<=0)
                        {
                            Msg = string.Format("在【商品录入】中到商品序列号为：{0},得最小起配数设置错误,不能小于或等于0", GoodsNumber);
                            return false;
                        }
                        GoodsModel.RowVersion = DateTime.Now;
                        GoodsModel.IsShelves = Convert.ToInt32(GoodsDT.Rows[i]["上架(0不上架/1上架)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.IsRecommend = Convert.ToInt32(GoodsDT.Rows[i]["是否推荐(0否/1是)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.IsHot = Convert.ToInt32(GoodsDT.Rows[i]["是否热门(0否/1是)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.IsSeckill = Convert.ToInt32(GoodsDT.Rows[i]["是否秒杀(0否/1是)"].ToString()) == 1 ? 1 : 0;
                        GoodsModel.CountFull = CountFull;
                        GoodsModel.CountReduction = CountReduction;
                        GoodsModel.PriceFull = PriceFull;
                        GoodsModel.PriceReduction = PriceReduction;
                        GoodsModel.Integral = Integral;
                        GoodsModel.Discount = Discount;

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
                        GoodsModel.SeckillBeginTime = SeckillBeginTime;
                        GoodsModel.SeckillEndTime = SeckillEndTime;
                        GoodsModel.QuotaBeginTime = QuotaBeginTime;
                        GoodsModel.QuotaEndTime = QuotaEndTime;

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
                                GVmodel.IsBulkCargo = 1;
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
                    string validationMsg = "";
                    foreach (var index in ex.EntityValidationErrors)
                    {
                        validationMsg += index.ValidationErrors+"|";
                    }

                    Msg = "数据类型错误:"+ validationMsg;
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


        public bool UpdateGoodsInfoByExcel(string fileName,string SheetName,out string Msg)
        {
            ExcelHelper abc = new ExcelHelper(fileName);
            var GoodsInfoDt = abc.ExcelToDataTable(SheetName, true);
          
            // var GoodsInfoDt = ExcelHelper.DataReaderExcelFile(fileName,"Sheet1");
            using (Entities ctx = new Entities())
            {
                var GoodsIdList = new List<int>();
                var GoodsBarCodeList = new List<string>();
                for (int i = 0; i < GoodsInfoDt.Rows.Count; i++)
                {
                    if(string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["序号"].ToString()))
                    {
                        continue;
                    }
                    if (int.TryParse(GoodsInfoDt.Rows[i]["商品ID"].ToString(), out int GoodsID))
                    {
                        GoodsIdList.Add(GoodsID);
                    }
                    else
                    {
                        GoodsBarCodeList.Add(GoodsInfoDt.Rows[i]["商品条码"].ToString());
                    }
                }
                var GoodsInfoList = new List<le_goods>();
                if (GoodsIdList.Count > 0)
                {
                    GoodsInfoList = ctx.le_goods.Where(s => GoodsIdList.Contains(s.GoodsID)).ToList();
                }
                else
                {
                    GoodsInfoList = ctx.le_goods.Where(s => s.le_goods_value.Any(k => GoodsBarCodeList.Contains(k.SerialNumber) && k.Enable == 1)).ToList();
                }
                for (int i = 0; i < GoodsInfoDt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["序号"].ToString()))
                    {
                        continue;
                    }
                    var GoodsModel = new le_goods();
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["商品id"].ToString(),out int GoodsId))
                    {
                        string SerialNumber = GoodsInfoDt.Rows[i]["商品条码"].ToString();
                        GoodsId = ctx.le_goods_value.Where(s => s.SerialNumber == SerialNumber && s.Enable == 1).Select(s=>s.GoodsID).FirstOrDefault();
                        GoodsModel = GoodsInfoList.Where(s => s.GoodsID == GoodsId) .FirstOrDefault();
                    }
                    else
                    {
                        GoodsModel = GoodsInfoList.Where(s => s.GoodsID == GoodsId).FirstOrDefault();
                    }
                    if(GoodsModel!=null)
                    {
                        if (int.TryParse(GoodsInfoDt.Rows[i]["上架"].ToString(), out int IsShelves))
                        {
                            GoodsModel.IsShelves = Convert.ToInt32(GoodsInfoDt.Rows[i]["上架"]);
                        }
                        GoodsModel.IsShelves = 0;
                        if (int.TryParse(GoodsInfoDt.Rows[i]["起配数"].ToString(), out int MinimumPurchase))
                        {
                            GoodsModel.MinimumPurchase = MinimumPurchase;
                        }
                        if(int.TryParse(GoodsInfoDt.Rows[i]["装箱数"].ToString(), out int PackingNumber))
                        {
                            GoodsModel.PackingNumber = PackingNumber;
                        }
                        if (GoodsInfoDt.Columns.Contains("是否默认"))
                        {

                            if (!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["是否默认"].ToString()) && !int.TryParse(GoodsInfoDt.Rows[i]["是否默认"].ToString(), out int IsDefaulSuppplier))
                            {
                                Msg = "【是否默认设置错误】取值范围0/1";
                                return false;
                            }
                            if (decimal.TryParse(GoodsInfoDt.Rows[i]["供应商价格"].ToString(), out decimal SupplierPrice) && !string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["供应商名称"].ToString()))
                            {
                                string SuppliersName = GoodsInfoDt.Rows[i]["供应商名称"].ToString();
                                int SupplierID = ctx.le_suppliers.Where(s => s.SuppliersName == SuppliersName).Select(s => s.SuppliersID).FirstOrDefault();

                                var GoodsSupplierList = ctx.le_goods_suppliers.Where(s => s.GoodsID == GoodsModel.GoodsID && s.IsDeleted == 0 && s.SuppliersID == SupplierID).ToList();
                                if (GoodsSupplierList == null) throw new Exception("【" + GoodsModel.GoodsName + "】价格设置错误");

                                foreach (var GoodsSupplier in GoodsSupplierList)
                                {
                                    if (GoodsSupplier.Supplyprice != SupplierPrice)
                                    {
                                        GoodsSupplier.IsDeleted = 1;
                                        GoodsSupplier.UpdateTime = DateTime.Now;

                                        ctx.Entry<le_goods_suppliers>(GoodsSupplier).State = System.Data.Entity.EntityState.Modified;
                                        // ctx.SaveChanges();

                                    }

                                }
                                le_goods_suppliers model = new le_goods_suppliers();
                                model.IsDefalut = 1;//需要及时修改
                                model.IsDeleted = 0;
                                model.SuppliersID = SupplierID;
                                model.GoodsID = GoodsModel.GoodsID;
                                model.UpdateTime = DateTime.Now;
                                model.CreatTime = DateTime.Now;
                                model.Supplyprice = SupplierPrice;
                                ctx.le_goods_suppliers.Add(model);

                            }
                        }
                        ctx.Entry<le_goods>(GoodsModel).State=System.Data.Entity.EntityState.Modified;
                    }
                }
                int Count = ctx.SaveChanges();
                if (Count>0)
                {
                    Msg = "修改成功";
                    return true;
                }
            }
            Msg = "未知错误";
            return false;
        }

        public bool AddGoodsInfoByExcel(string fileName, out string Msg)
        {
            ExcelHelper abc = new ExcelHelper(fileName);
            var GoodsInfoDt = abc.ExcelToDataTable("", true);
          
            List<string> GoodsGourpName = new List<string>();
            using (Entities ctx = new Entities())
            {
                int IsShelves=0; int IsBulkCargo=0; int PackingNumber=0; int MinimumPurchase=0; decimal SupplierPric=0;
                string GoodsName=""; string Unit;
                decimal SpecialOffer = 0;
               
              //  var query = GoodsInfoDt.AsEnumerable().GroupBy(t => new { GoodsName = t.Field<string>("商品名称"), SerialNumber = t.Field<string>("商品条码") })
              //      .Where(g => g.Count() > 1) //找出重复行
              //.Select(g => new { g.Key.GoodsName, g.Key.SerialNumber,  c = g.Count() }).ToList();
                List<string> NoExitGoods = new List<string>();
                for (int i = 0; i < GoodsInfoDt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["序号"].ToString()))
                    {
                        continue;
                    }
                    #region 参数检查
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["上架"].ToString(), out IsShelves))
                    {
                        Msg = "上架字段错误只能输入0/1，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】";
                        return false;
                    }
                    //if (!int.TryParse(GoodsInfoDt.Rows[i]["散货"].ToString(), out IsBulkCargo))
                    //{
                    //    Msg = "散货字段错误只能输入0/1，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】";
                    //    return false;
                    //}
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["装箱数"].ToString(), out PackingNumber))
                    {
                        Msg = "装箱数字段错误只能输入正整数，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】";
                        return false;
                    }
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["起配数"].ToString(), out MinimumPurchase))
                    {
                        Msg = "起配数字段错误只能输入正整数，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】";
                        return false;
                    }
                    if (!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["保质期"].ToString()))
                    {
                        //Msg = "保质期不能为空";
                        //return false;
                    }
                    if (!decimal.TryParse(GoodsInfoDt.Rows[i]["供应商价格"].ToString(), out SupplierPric))
                    {
                        Msg = "供应商价格填写错误";
                        return false;
                    }
                    if (!decimal.TryParse(GoodsInfoDt.Rows[i]["平台售价"].ToString(), out SpecialOffer))
                    {
                        Msg = "平台价格填写错误";
                        return false;
                    }
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["是否默认"].ToString(), out int IsDefaultSuplier))
                    {
                        if (IsDefaultSuplier <0|| IsDefaultSuplier > 1)
                        {
                            Msg = "是否为默认供应商标志错误，取值范围[0,1]";
                            return false;
                        }
                    }
                    var ShelfLife = GoodsInfoDt.Rows[i]["保质期"].ToString();
                    GoodsName = GoodsInfoDt.Rows[i]["商品名称"].ToString();
                    var GoodsGroup = GoodsInfoDt.Rows[i]["商品分类"].ToString();
                    string  SerialNumber = GoodsInfoDt.Rows[i]["商品条码"].ToString();
                    string SupplierName= GoodsInfoDt.Rows[i]["供应商名称"].ToString();
                    Unit = GoodsInfoDt.Rows[i]["商品单位"].ToString();
                    le_goods GoodsModel = new le_goods();
                    le_goods_value GoodsValueModel = new le_goods_value();
                    le_goods_img GoodsImgModel = new le_goods_img();
                    le_goods_suppliers GoodsSupplierModel = new le_goods_suppliers();

                    if (GoodsInfoDt.Rows[i]["商品条码"] == null && IsBulkCargo == 0)
                    {
                        Msg = "商品条码不可为空，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】";
                        return false;
                    }

                    //GoodsModel.IsBulkCargo = IsBulkCargo;
                    GoodsValueModel.CategoryType = 1;
                    GoodsValueModel.IsBulkCargo = IsBulkCargo;
                    GoodsValueModel.CreateTime = DateTime.Now;
                    GoodsValueModel.UpdateTime = DateTime.Now;

                    if (IsBulkCargo == 1 && string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["商品条码"].ToString()))//是散货并且条码为空则自动生成条码
                    {
                        GoodsValueModel.IsAuto = 1;
                        GoodsValueModel.SerialNumber = new GoodsService().BarcodeGeneration(GoodsModel.IsBulkCargo);
                    }
                    else
                    {
                        GoodsValueModel.SerialNumber = GoodsInfoDt.Rows[i]["商品条码"].ToString();
                    }
                    
                   // GoodsModel.le_goods_value = GoodsValueModel;
                    GoodsModel.GoodsName = GoodsName;
                   // GoodsModel.GoodsGroupsID = GoodsGroupModel.ID;
                    GoodsModel.Unit = Unit;
                    GoodsModel.PackingNumber = PackingNumber;
                    GoodsModel.MinimumPurchase = MinimumPurchase;
                    GoodsModel.ShelfLife = ShelfLife;

                    //if (query.Any(s => s.GoodsName == GoodsName && s.SerialNumber == SerialNumber))//多供应商
                    //{
                    var ExitGoodsModelList = ctx.le_goods.Where(s => s.GoodsName == GoodsName && s.le_goods_value.Any(k => k.SerialNumber == SerialNumber)).ToList();//.FirstOrDefault();
                    if (ExitGoodsModelList != null && ExitGoodsModelList.Count > 0)//如果存在有重复得商品
                    {
                        //目前存在上架得商品
                        var ShelvesGoodsList = ExitGoodsModelList.Where(s => s.IsShelves == 1).ToList();
                        if (ShelvesGoodsList == null|| ShelvesGoodsList.Count<=0)
                        {
                            throw new Exception("该商品平台存在多条重复记录但未下架，在序号【" + GoodsInfoDt.Rows[i]["商品名称"] + "】");
                        }
                           
                        if (ShelvesGoodsList.Count > 1) continue; //throw new Exception("该商品平台存在个多重复上架，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】");

                        var ShelvesGoods = ShelvesGoodsList.FirstOrDefault();
                        ShelvesGoods.Unit = Unit;
                        ShelvesGoods.PackingNumber = PackingNumber;
                        ShelvesGoods.MinimumPrice = MinimumPurchase;
                        ShelvesGoods.ShelfLife = ShelfLife;
                        ShelvesGoods.SpecialOffer = SpecialOffer;
                        //ExitGoodsModelList=
                        var GoodsValue= ShelvesGoods.le_goods_value.Where(s=>s.Enable==1).FirstOrDefault();
                        if (GoodsValue == null) throw new Exception("该商品未存在任何属性");

                        GoodsValue.CategoryType = 1;
                        GoodsValue.IsBulkCargo = IsBulkCargo;
                        GoodsValue.SerialNumber = SerialNumber;
                        //GoodsValue.CreateTime = DateTime.Now;
                       // GoodsValue.UpdateTime = DateTime.Now;

                        ctx.Entry<le_goods_value>(GoodsValue).State= System.Data.Entity.EntityState.Modified;
                        ctx.Entry<le_goods>(ShelvesGoods).State = System.Data.Entity.EntityState.Modified;

                        var SupplierID = ctx.le_suppliers.Where(s => s.SuppliersName == SupplierName).Select(s => s.SuppliersID).FirstOrDefault();
                        var GoodsSupplierList= ShelvesGoods.le_goods_suppliers.Where(s=>s.IsDeleted==0).ToList();

                        if(GoodsSupplierList.Count>1)
                        {
                            var RemoveSuppliers= GoodsSupplierList.Skip(1).FirstOrDefault();
                            GoodsSupplierList.Remove(RemoveSuppliers);
                            ctx.le_goods_suppliers.Remove(RemoveSuppliers);
                        }


                        foreach(var GoodsSupplier in GoodsSupplierList)
                        {                           
                            if(GoodsSupplier.SuppliersID== SupplierID && GoodsSupplier.Supplyprice!=SupplierPric)
                            {
                                GoodsSupplier.IsDeleted = 1;
                                GoodsSupplier.UpdateTime = DateTime.Now;                                
                                ctx.Entry<le_goods_suppliers>(GoodsSupplier).State = System.Data.Entity.EntityState.Modified;
                                le_goods_suppliers NewSupplierPrice = new le_goods_suppliers();
                                NewSupplierPrice.CreatTime = DateTime.Now;
                                NewSupplierPrice.SuppliersID = SupplierID;
                                NewSupplierPrice.Supplyprice = SupplierPric;
                                NewSupplierPrice.GoodsID = ShelvesGoods.GoodsID;
                                NewSupplierPrice.IsDefalut = IsDefaultSuplier;
                                ctx.le_goods_suppliers.Add(NewSupplierPrice);

                                if(NewSupplierPrice.IsDefalut==1)
                                {
                                    var NoDefaultSupplier = GoodsSupplierList.Where(s => s.IsDefalut == 1 && s.IsDeleted == 0&&s.SuppliersID!=SupplierID).ToList();
                                    foreach(var item in NoDefaultSupplier)
                                    {
                                        item.IsDefalut = 0;
                                        item.UpdateTime = DateTime.Now;
                                        ctx.Entry<le_goods_suppliers>(item).State = System.Data.Entity.EntityState.Modified;
                                    }
                                      
                                }
                            }
                        }

                        var NoShelvesGoods = ExitGoodsModelList.Where(s => s.IsShelves == 0).ToList();//目前存在下架得商品
                        foreach (var GoodsItem in NoShelvesGoods)
                        {
                            var OrderLineList = GoodsItem.le_orders_lines.ToList();
                            foreach(var OrderLine in OrderLineList)
                            {
                                OrderLine.GoodsID = ShelvesGoods.GoodsID;                                
                                ctx.Entry<le_orders_lines>(OrderLine).State = System.Data.Entity.EntityState.Modified;
                                var updateOrderLinesql = ctx.Database.ExecuteSqlCommand("update le_orderline_goodsvalue set GoodsValueid='" + GoodsValue.GoodsValueID + "'  where orderlineid=" + OrderLine.OrdersLinesID + " ");
                            }
                            ctx.le_goods_img.RemoveRange(GoodsItem.le_goods_img);
                            ctx.le_goods_log.RemoveRange(GoodsItem.le_goods_log);
                            ctx.le_goods_suppliers.RemoveRange(GoodsItem.le_goods_suppliers);
                            ctx.le_shop_cart.RemoveRange(GoodsItem.le_shop_cart);

                            
                            ctx.le_goods_value.RemoveRange(GoodsItem.le_goods_value);
                            ctx.le_goods.Remove(GoodsItem);
                            // GoodsItem.
                            //ExitShelvesGoods=
                        }
                    }

                    else
                    {
                       
                        NoExitGoods.Add(GoodsInfoDt.Rows[i]["商品条码"].ToString()+"|"+ GoodsInfoDt.Rows[i]["商品名称"].ToString());
                    }
                    int ks = ctx.SaveChanges();
                    var result = NoExitGoods;
                    //}
                    //else//单供应商
                    //{

                    //}
                }
               


                //var GoodsGroupModel = ctx.le_goodsgroups.Where(s => s.Name == GoodsGroup).FirstOrDefault();
                //if (GoodsGroupModel == null)
                //{
                //    Msg = "请输入正确得商品分类名，在序号【" + GoodsInfoDt.Rows[i]["序号"] + "】";
                //    return false;
                //}

                #endregion





            }
            Msg = "未完成具体功能";
            return false;
        }

        public bool AddGoodsInfoByExcel(string fileName, out string Msg,int g)
        {
            Msg = "未知错误";
          //  return false;
            ExcelHelper abc = new ExcelHelper(fileName);
            var GoodsInfoDt = abc.ExcelToDataTable("", true);
            bool IsAdd = false;
            int GoodsID=0;
            int GoodsGroupID = 0;
            string Serialnumber;
            using (Entities ctx = new Entities())
            {
                for(int i= 0; i< GoodsInfoDt.Rows.Count;i++)
                {
                    if (string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["序号"].ToString()))
                    {
                        continue;
                    }
                    #region 数据格式检查
                    string GoodsNumber = GoodsInfoDt.Rows[i]["序号"].ToString();
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["装箱数"].ToString(), out int a))
                    {
                        Msg = string.Format("在【商品录入】中商品序列号：{0}的装箱数格式错误", GoodsNumber);
                        return false;
                    }
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["建议零售价"].ToString(), out decimal bb))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的建议零售价格式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["划线价"].ToString(), out decimal c))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的划线价格式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["（金额满减）满"].ToString(), out decimal PriceFull))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的（金额满减）满格式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["（金额满减）减"].ToString(), out decimal PriceReduction))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的【（金额满减）减】格式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["（数量满减）满"].ToString(), out decimal CountFull))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的【（数量满减）满】格式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["（数量满减）减"].ToString(), out decimal CountReduction))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的（数量满减）减式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!int.TryParse(GoodsInfoDt.Rows[i]["积分"].ToString(), out int Integral))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的【积分】格式错误", GoodsNumber);
                    //    return false;
                    //}
                    //if (!Decimal.TryParse(GoodsInfoDt.Rows[i]["折扣"].ToString(), out decimal Discount))
                    //{
                    //    Msg = string.Format("在【商品录入】中商品序列号：{0}的【折扣】格式错误", GoodsNumber);
                    //    return false;
                    //}

                    DateTime? SeckillBeginTime = null;
                    DateTime? SeckillEndTime = null;
                    DateTime? QuotaBeginTime = null;
                    DateTime? QuotaEndTime = null;
                    //if (!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["秒杀开始时间"].ToString()))
                    //{
                    //    try
                    //    {
                    //        SeckillBeginTime = DateTime.ParseExact(GoodsInfoDt.Rows[i]["秒杀开始时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Msg = string.Format("在【商品录入】中商品序列号：{0}的【秒杀开始时间】格式错误", GoodsNumber);
                    //        return false;
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["秒杀结束时间"].ToString()))
                    //{
                    //    try
                    //    {
                    //        SeckillEndTime = DateTime.ParseExact(GoodsInfoDt.Rows[i]["秒杀结束时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Msg = string.Format("在【商品录入】中商品序列号：{0}的【秒杀结束时间】格式错误", GoodsNumber);
                    //        return false;
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["限购开始时间"].ToString()))
                    //{
                    //    try
                    //    {
                    //        QuotaBeginTime = DateTime.ParseExact(GoodsInfoDt.Rows[i]["限购开始时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Msg = string.Format("在【商品录入】中商品序列号：{0}的【限购开始时间】格式错误", GoodsNumber);
                    //        return false;
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["限购结束时间"].ToString()))
                    //{
                    //    try
                    //    {
                    //        QuotaEndTime = DateTime.ParseExact(GoodsInfoDt.Rows[i]["限购结束时间"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Msg = string.Format("在【商品录入】中商品序列号：{0}的【限购结束时间】格式错误", GoodsNumber);
                    //        return false;
                    //    }
                    //}
                   
                    #endregion
                   
                    if (GoodsInfoDt.Rows[i]["商品名称"].ToString()==string.Empty)
                    {
                        Msg = "商品名称不能为空!";
                        return false;
                    }
                    if (!int.TryParse(GoodsInfoDt.Rows[i]["分类id"].ToString(), out GoodsGroupID))
                    {
                        if (GoodsGroupID == 0)
                        {
                            Msg = "请输入正确的分类id,在商品【"+ GoodsInfoDt.Rows[i]["商品名称"].ToString() + "】";
                            return false;
                        }
                    }
                    //GoodsGroupID = 147;
                    Serialnumber = GoodsInfoDt.Rows[i]["商品条码"].ToString();
                    //if(string.IsNullOrEmpty(Serialnumber))
                    //{

                    //    Msg = "商品条码输入错误";
                    //    return false;
                    //}

                    le_goods le_Goods = new le_goods();
                    if(GoodsInfoDt.Rows[i]["商品ID"].ToString()!=string.Empty)
                    {
                        GoodsID = Convert.ToInt32(GoodsInfoDt.Rows[i]["商品ID"].ToString());                      
                    }
                    string GoodsName= GoodsInfoDt.Rows[i]["商品名称"].ToString();
                    //存在同条码同商品名则修改
                    le_Goods = ctx.le_goods.Where(s => s.GoodsName == GoodsName && s.le_goods_value.Any(k => k.SerialNumber == Serialnumber)).FirstOrDefault();
                    if (le_Goods==null)
                    {
                        le_Goods = new le_goods();
                        IsAdd = true;
                    }
                    else
                    {
                        IsAdd = false;
                    }
                    //if (ctx.le_goods.Any(s => s.GoodsName == le_Goods.GoodsName && s.le_goods_value.Any(k => k.SerialNumber == Serialnumber)))
                    //{

                    //    //Msg = "已经存在相同的商品名请修改！在" + le_Goods.GoodsName;
                    //    //return false;
                    //}
                    if (GoodsInfoDt.Columns.Contains("库存"))
                    {
                        if (string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["库存"].ToString()))
                        {
                            le_Goods.Stock = 100000;
                            //Msg = string.Format("在【商品录入】中商品序列号：{0}的【库存】格式错误", GoodsNumber);
                            //return false;
                        }
                    }
                    else
                    {
                        le_Goods.Stock = 100000;
                    }
                    le_Goods.GoodsName = GoodsInfoDt.Rows[i]["商品名称"].ToString();
                    le_Goods.GoodsGroupsID = GoodsGroupID;
                    le_Goods.Image = "/GoodImg/" + Serialnumber + ".jpg";
                    if (GoodsInfoDt.Columns.Contains("描述"))
                    {
                        le_Goods.Describe = GoodsInfoDt.Rows[i]["描述"].ToString();
                        if (string.IsNullOrEmpty(le_Goods.Describe))
                        {
                            le_Goods.Describe = "无";
                            //Msg = string.Format("未能在【商品录入】获取到商品序列号为：{0},的商品描述", GoodsNumber);
                            //return false;
                        }
                    }
                    else
                    {
                        le_Goods.Describe = "无";
                    }
                    le_Goods.GoodsGroupsID = Convert.ToInt32(GoodsInfoDt.Rows[i]["分类id"].ToString());
                    le_Goods.GoodsName = GoodsInfoDt.Rows[i]["商品名称"].ToString();

                    if (IsAdd)
                    {
                        le_Goods.Image = "/GoodImg/" + GoodsInfoDt.Rows[i]["商品条码"].ToString() + ".jpg";
                    }
                    if(GoodsInfoDt.Rows[i]["平台价格"].ToString()==string.Empty)
                    {
                        le_Goods.SpecialOffer = Convert.ToDecimal(GoodsInfoDt.Rows[i]["供应商价格"].ToString());
                    }else
                    {
                        le_Goods.SpecialOffer = Convert.ToDecimal(GoodsInfoDt.Rows[i]["平台价格"].ToString());
                    }
                    //2684
                    le_Goods.OriginalPrice = 0;// Convert.ToDecimal(GoodsInfoDt.Rows[i]["划线价"].ToString());
                    le_Goods.MSRP = 0; //Convert.ToDecimal(GoodsInfoDt.Rows[i]["特价"].ToString());
                    le_Goods.PackingNumber = Convert.ToInt32(GoodsInfoDt.Rows[i]["装箱数"].ToString());
                    le_Goods.ShelfLife = string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["保质期"].ToString()) ? "0" : GoodsInfoDt.Rows[i]["保质期"].ToString();
                    le_Goods.Sort = 999;
                    if(!string.IsNullOrEmpty(GoodsInfoDt.Rows[i]["商品单位"].ToString()))
                    {
                        le_Goods.Specifications = GoodsInfoDt.Rows[i]["商品单位"].ToString();
                    }
                   
                    //le_Goods.Stock = Convert.ToInt32(GoodsInfoDt.Rows[i]["库存"].ToString());
                    if (GoodsInfoDt.Columns.Contains("限购"))
                    {
                        if (GoodsInfoDt.Rows[i]["限购"].ToString() != string.Empty)
                        {
                            le_Goods.Quota = Convert.ToInt32(GoodsInfoDt.Rows[i]["限购"].ToString());
                        }
                        else
                        {
                            le_Goods.Quota = -1;
                        }
                    }
                    else
                    {
                        le_Goods.Quota = -1;
                    }
                    if (GoodsInfoDt.Columns.Contains("散货"))
                    {
                        if (GoodsInfoDt.Rows[i]["散货"].ToString() != string.Empty)
                        {
                            le_Goods.IsBulkCargo = Convert.ToInt32(GoodsInfoDt.Rows[i]["散货"].ToString()) == 1 ? 1 : 0; ;
                        }
                    }
                    
                    le_Goods.MinimumPurchase = Convert.ToInt32(GoodsInfoDt.Rows[i]["起配数"].ToString());
                    if (le_Goods.MinimumPurchase <= 0)
                    {
                        Msg = string.Format("在【商品录入】中到商品序列号为：{0},得最小起配数设置错误,不能小于或等于0", GoodsNumber);
                        return false;
                    }
                    le_Goods.RowVersion = DateTime.Now;
                    if (GoodsInfoDt.Rows[i]["上架"].ToString() != string.Empty)
                    {
                        le_Goods.IsShelves = Convert.ToInt32(GoodsInfoDt.Rows[i]["上架"].ToString()) == 1 ? 1 : 0;
                    }
                    if (GoodsInfoDt.Rows[i]["推荐"].ToString() != string.Empty)
                    {
                        le_Goods.IsRecommend = Convert.ToInt32(GoodsInfoDt.Rows[i]["推荐"].ToString()) == 1 ? 1 : 0;
                    }
                    if (GoodsInfoDt.Rows[i]["热门"].ToString() != string.Empty)
                    {
                        le_Goods.IsHot = Convert.ToInt32(GoodsInfoDt.Rows[i]["热门"].ToString()) == 1 ? 1 : 0;
                    }
                    if (GoodsInfoDt.Rows[i]["秒杀"].ToString() != string.Empty)
                    {
                        le_Goods.IsSeckill = Convert.ToInt32(GoodsInfoDt.Rows[i]["秒杀"].ToString()) == 1 ? 1 : 0;
                    }
                    //le_Goods.IsRecommend = Convert.ToInt32(GoodsInfoDt.Rows[i]["推荐"].ToString()) == 1 ? 1 : 0;
                    //le_Goods.IsHot = Convert.ToInt32(GoodsInfoDt.Rows[i]["热门"].ToString()) == 1 ? 1 : 0;
                    //le_Goods.IsSeckill = Convert.ToInt32(GoodsInfoDt.Rows[i]["秒杀"].ToString()) == 1 ? 1 : 0;
                    le_Goods.CountFull = 0;// CountFull;
                    le_Goods.CountReduction = 0; //CountReduction;
                    le_Goods.PriceFull = 0; //PriceFull;
                    le_Goods.PriceReduction = 0; //PriceReduction;
                    le_Goods.Integral = 0;//Integral;
                    le_Goods.Discount = 0; //Discount;

                    //model.Province = dto.Province;
                    //model.City = dto.City;
                    //model.Area = dto.Area;
                    //model.PiecePrice = dto.PiecePrice;
                    //model.MinimumPrice = dto.MinimumPrice;
                    //model.BusinessValue = dto.BusinessValue;
                    //model.NewPeriod = dto.NewPeriod;
                    //model.Unit = dto.Unit;
                    //model.IsRandomDistribution = dto.IsRandomDistribution;
                    le_Goods.PriceScheme1 = 0;
                    le_Goods.PriceScheme2 = 0;
                    le_Goods.PriceScheme3 = 0;
                    le_Goods.IsParcel = 0;
                    le_Goods.SeckillBeginTime = SeckillBeginTime;
                    le_Goods.SeckillEndTime = SeckillEndTime;
                    le_Goods.QuotaBeginTime = QuotaBeginTime;
                    le_Goods.QuotaEndTime = QuotaEndTime;
                    int IsDefaultSuplier;
                    if (GoodsInfoDt.Rows[i]["是否默认"].ToString()==string.Empty&& IsAdd)
                    {
                        IsDefaultSuplier = 1;
                    }
                   
                    else
                    {
                        IsDefaultSuplier = Convert.ToInt32(GoodsInfoDt.Rows[i]["是否默认"]);
                    }
                       
                    if (IsAdd)
                    {
                        le_goods_value GoodsValuemodel = new le_goods_value();
                        GoodsValuemodel.CategoryType = 1;
                        GoodsValuemodel.CreateTime = DateTime.Now;
                        GoodsValuemodel.UpdateTime = DateTime.Now;
                        GoodsValuemodel.SerialNumber = Serialnumber;
                        GoodsValuemodel.GoodsValue=GoodsInfoDt.Rows[i]["属性"].ToString();
                        GoodsValuemodel.Enable = 1;
                        if (string.IsNullOrEmpty(Serialnumber))
                        {
                            le_Goods.IsBulkCargo = 1;
                            GoodsValuemodel.IsAuto = 1;
                            GoodsValuemodel.IsBulkCargo = 1;
                            GoodsValuemodel.SerialNumber = new GoodsService().BarcodeGeneration(le_Goods.IsBulkCargo);

                        }

                        le_goods_img le_Goods_Img = new le_goods_img();
                        le_Goods_Img.CreatTime = DateTime.Now;
                        le_Goods_Img.UpdateTime = DateTime.Now;
                        le_Goods_Img.Src= "/GoodImg/" + Serialnumber + ".jpg";
                        le_Goods.le_goods_img.Add(le_Goods_Img);

                        le_goods_suppliers model = new le_goods_suppliers();
                        model.IsDeleted = 0;
                        model.SuppliersID = Convert.ToInt32(GoodsInfoDt.Rows[i]["供应商id"]);
                        model.Supplyprice = Convert.ToDecimal(GoodsInfoDt.Rows[i]["供应商价格"]);
                        model.IsDefalut = IsDefaultSuplier;//Convert.ToInt32(GoodsInfoDt.Rows[i]["是否默认"]);

                        model.CreatTime = DateTime.Now;
                        le_Goods.le_goods_suppliers.Add(model);
                        le_Goods.le_goods_img.Add(le_Goods_Img);
                        le_Goods.le_goods_value.Add(GoodsValuemodel);
                        
                        ctx.le_goods.Add(le_Goods);
                        int counts = ctx.SaveChanges();
                    }
                    else
                    {
                        //le_Goods.UpdateTime = DateTime.Now;
                        var GoodsValueList = le_Goods.le_goods_value.Where(s => s.Enable == 1).OrderByDescending(s => s.CreateTime).ToList();
                        if (GoodsValueList.Count > 1)
                        {
                            new Exception(string.Format("在{0} 存在多个重复属性值", GoodsInfoDt.Rows[i]["商品名称"].ToString()));
                        }
                        var  GoodsValue = GoodsValueList.FirstOrDefault();
                        if(!string.IsNullOrEmpty(Serialnumber))
                        {
                            GoodsValue.SerialNumber = Serialnumber;
                        }
                     
                        GoodsValue.GoodsValue = GoodsInfoDt.Rows[i]["属性"].ToString();
                        
                        var NoShelvesGoods = ctx.le_goods.Where(s=>s.GoodsName==GoodsName&&s.GoodsID!=le_Goods.GoodsID).ToList();//目前存在下架得商品
                        foreach (var GoodsItem in NoShelvesGoods)
                        {
                            var OrderLineList = GoodsItem.le_orders_lines.ToList();
                            foreach (var OrderLine in OrderLineList)
                            {
                                OrderLine.GoodsID = le_Goods.GoodsID;
                                ctx.Entry<le_orders_lines>(OrderLine).State = System.Data.Entity.EntityState.Modified;
                                var updateOrderLinesql = ctx.Database.ExecuteSqlCommand("update le_orderline_goodsvalue set GoodsValueid='" + GoodsValue.GoodsValueID + "'  where orderlineid=" + OrderLine.OrdersLinesID + " ");
                            }
                            var RemoveList = GoodsItem.le_shop_cart.ToList();
                            foreach(var carshop in RemoveList)
                            {
                                ctx.le_cart_goodsvalue.RemoveRange(carshop.le_cart_goodsvalue);
                            }
                            //ctx.le_cart_goodsvalue.RemoveRange(Go)
                            ctx.le_shop_cart.RemoveRange(GoodsItem.le_shop_cart);
                            ctx.le_goods_img.RemoveRange(GoodsItem.le_goods_img);
                            ctx.le_goods_log.RemoveRange(GoodsItem.le_goods_log);
                            ctx.le_goods_suppliers.RemoveRange(GoodsItem.le_goods_suppliers);
                            //ctx.le_shop_cart.RemoveRange(GoodsItem.le_shop_cart);


                            ctx.le_goods_value.RemoveRange(GoodsItem.le_goods_value);
                            ctx.le_goods.Remove(GoodsItem);
                            // GoodsItem.
                            //ExitShelvesGoods=
                        }

                        int SupplierID = Convert.ToInt32(GoodsInfoDt.Rows[i]["供应商id"]);
                        decimal SupplierPrice = Convert.ToDecimal(GoodsInfoDt.Rows[i]["供应商价格"]);

                        ///删除所有的供应商价格
                        ctx.le_goods_suppliers.RemoveRange(le_Goods.le_goods_suppliers);

                        le_goods_suppliers model = new le_goods_suppliers();
                        model.IsDeleted = 0;
                        model.SuppliersID = Convert.ToInt32(GoodsInfoDt.Rows[i]["供应商id"]);
                        model.Supplyprice = Convert.ToDecimal(GoodsInfoDt.Rows[i]["供应商价格"]);
                        model.IsDefalut = 1; //IsDefaultSuplier;//Convert.ToInt32(GoodsInfoDt.Rows[i]["是否默认"]);

                        model.CreatTime = DateTime.Now;
                        le_Goods.le_goods_suppliers.Add(model);

                        //var GoodsSupplierList = le_Goods.le_goods_suppliers.Where(s => s.IsDeleted == 0&& s.SuppliersID== SupplierID).ToList();

                        //if(GoodsSupplierList==null|| GoodsSupplierList.Count<=0)
                        //{
                        //    new Exception("");
                        //}
                        //if (GoodsSupplierList.Count > 1)
                        //{
                        //    var RemoveSuppliers = GoodsSupplierList.Skip(1).FirstOrDefault();
                        //    GoodsSupplierList.Remove(RemoveSuppliers);
                        //    ctx.le_goods_suppliers.Remove(RemoveSuppliers);
                        //}

                        //foreach (var GoodsSupplier in GoodsSupplierList)
                        //{
                        //    if (GoodsSupplier.SuppliersID == SupplierID && GoodsSupplier.Supplyprice != SupplierPrice)
                        //    {
                        //        GoodsSupplier.IsDeleted = 1;
                        //        GoodsSupplier.UpdateTime = DateTime.Now;

                        //        ctx.Entry<le_goods_suppliers>(GoodsSupplier).State = System.Data.Entity.EntityState.Modified;

                        //        le_goods_suppliers NewSupplierPrice = new le_goods_suppliers();
                        //        NewSupplierPrice.CreatTime = DateTime.Now;
                        //        NewSupplierPrice.SuppliersID = Convert.ToInt32(GoodsInfoDt.Rows[i]["供应商id"]);
                        //        NewSupplierPrice.Supplyprice = Convert.ToDecimal(GoodsInfoDt.Rows[i]["供应商价格"]);
                        //        NewSupplierPrice.GoodsID = le_Goods.GoodsID;
                        //        NewSupplierPrice.IsDefalut = IsDefaultSuplier;
                        //        ctx.le_goods_suppliers.Add(NewSupplierPrice);

                        //        if (NewSupplierPrice.IsDefalut == 1)
                        //        {
                        //            var NoDefaultSupplier = GoodsSupplierList.Where(s => s.IsDefalut == 1 && s.IsDeleted == 0 && s.SuppliersID != SupplierID).ToList();
                        //            foreach (var item in NoDefaultSupplier)
                        //            {
                        //                item.IsDefalut = 0;
                        //                item.UpdateTime = DateTime.Now;
                        //                ctx.Entry<le_goods_suppliers>(item).State = System.Data.Entity.EntityState.Modified;
                        //            }

                        //        }
                        //    }
                        //}
                        ctx.Entry<le_goods>(le_Goods).State = System.Data.Entity.EntityState.Modified;
                        int counts = ctx.SaveChanges();
                        //if (counts > 0)
                        //{
                        //    Msg = "SUCCESS";
                        //    return true;
                        //}
                        //else
                        //{
                        //    Msg = "新增失败";
                        //    return false;
                        //}
                        //Msg = "修改失败";
                        //return false;
                    }

                }
                int count = ctx.SaveChanges();
                if (count > 0)
                {
                    Msg = "SUCCESS";
                    return true;
                }
                else
                {
                    Msg = "新增失败";
                    return false;
                }
            }
            return false;
        }
    }
}
