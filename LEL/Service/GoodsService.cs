using Common;
using DTO.Goods;
using DTO.User;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class GoodsService
    {
        private static ILog log = LogManager.GetLogger(typeof(GoodsService));
        private string GoodsImagePath = ConfigurationManager.AppSettings["goodsimagepath"];
        private SortedList<string, le_sysconfig> GetSysConfigList = SysConfig.Get().values;
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public async Task<GoodsListDto> GetGoodsListAsync(GoodsSeachOptions options)
        {
            var BasePath = GetSysConfigList.Values.Where(s => s.Name == "HeadQuartersDomain").FirstOrDefault().Value;
            GoodsListDto list = new GoodsListDto();
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_goods.Where(s => true);
                if (options.IsHot == 1)
                {
                    tempIq = tempIq.Where(s => s.IsHot == 1);
                }
                if (options.IsHot == 0)
                {
                    tempIq = tempIq.Where(s => s.IsHot == 0);
                }
                if (options.IsNewGoods == 1)
                {
                    tempIq = tempIq.Where(s => s.IsNewGoods == 1);
                }
                if (options.IsNewGoods == 0)
                {
                    tempIq = tempIq.Where(s => s.IsNewGoods == 0);
                }
                if (options.IsSeckill == 1)
                {
                    tempIq = tempIq.Where(s => s.IsSeckill == 1);
                }
                if (options.IsSeckill == 0)
                {
                    tempIq = tempIq.Where(s => s.IsSeckill == 0);
                }
                if (options.IsRecommend == 1)
                {
                    tempIq = tempIq.Where(s => s.IsRecommend == 1);
                }
                if (options.IsRecommend == 0)
                {
                    tempIq = tempIq.Where(s => s.IsRecommend == 0);
                }
                if (options.GoodsGroupID != null && options.GoodsGroupID != 0)
                {
                    int[] GroupID = ctx.le_goodsgroups.Where(s => s.ID == options.GoodsGroupID || s.ParentID == options.GoodsGroupID).Select(s => s.ID).ToArray();

                    tempIq = tempIq.Where(s => GroupID.Contains(s.GoodsGroupsID));
                }
                if (options.IsShelves == 1)
                {
                    tempIq = tempIq.Where(S => S.IsShelves == 1);
                }
                if (options.IsShelves == 0)
                {
                    tempIq = tempIq.Where(S => S.IsShelves == 0);
                }

                if (!string.IsNullOrEmpty(options.KeyWords)) //搜索
                {
                    tempIq = tempIq.Where(s => s.GoodsName.Contains(options.KeyWords)
                      || s.GoodsID.ToString().Contains(options.KeyWords)
                       || s.Describe.Contains(options.KeyWords)
                      || s.GoodsGroupsID.ToString().Contains(options.KeyWords)
                      || s.le_goods_value.Any(k => k.SerialNumber.Contains(options.KeyWords) && k.Enable == 1)
                      ); ;
                }
                if (!string.IsNullOrEmpty(options.SerialNumber))
                {
                    tempIq = tempIq.Where(s => s.le_goods_value.Any(k => k.SerialNumber.Contains(options.SerialNumber) && k.Enable == 1));
                }
                if (options.SupplierID != null)
                {
                    tempIq = tempIq.Where(s => s.le_goods_suppliers.Any(k => k.SuppliersID == options.SupplierID));
                }
                if(options.GoodsID!=null&&options.PageTurning!=null)
                {
                    if (options.PageTurning == 1)
                    {
                        tempIq = tempIq.Where(s => s.GoodsID < options.GoodsID.Value);
                    }
                    if (options.PageTurning == 2)
                    {
                        tempIq = tempIq.Where(s => s.GoodsID > options.GoodsID.Value);
                    }
                }
                IQueryable<GroodsModelDto> result = null;
                result = tempIq.Select(s => new GroodsModelDto
                {
                    GoodsID = s.GoodsID,
                    GoodsName = s.GoodsName,
                    GoodsGroups_ID = s.GoodsGroupsID,
                    Sort = s.Sort,
                    Specifications = s.Specifications,
                    Describe = s.Describe,
                    IsShelves = s.IsShelves,
                    Image = BasePath + s.Image,
                    IsHot = s.IsHot,
                    IsNewGoods = s.IsNewGoods,
                    IsRecommend = s.IsRecommend,
                    IsSeckill = s.IsSeckill,
                    CreateTime = s.CreateTime,
                    SpecialOffer = s.SpecialOffer,
                    OriginalPrice = s.OriginalPrice,
                    GoodsGroupName = s.le_goodsgroups.Name,
                    PackingNumber = s.PackingNumber,
                    SalesVolumes = s.SalesVolumes,
                    TotalSalesVolumes = s.TotalSalesVolume,
                    Stock = s.Stock,
                    Quota = s.Quota,
                    MSRP = s.MSRP,
                    GoodsValueList = s.le_goods_value.Where(k => k.Enable == 1).Select(k => new GoodsValues()
                    {
                        SerialNumber = k.SerialNumber,
                        CategoryType = k.CategoryType,
                        GoodsID = k.GoodsID,
                        GoodsValueID = k.GoodsValueID,
                        GoodsValueName = k.GoodsValue
                    }).ToList(),
                });

                #region 排序              
                switch (options.SortKey)
                {
                    case GoodsSeachOrderByType.CreateTimeAsc:
                        result = result.OrderBy(k => k.CreateTime);
                        break;
                    case GoodsSeachOrderByType.CreateTimeDesc:
                        result = result.OrderByDescending(k => k.CreateTime);
                        break;
                    case GoodsSeachOrderByType.OriginalPriceAsc:
                        result = result.OrderBy(k => k.SpecialOffer);
                        break;
                    case GoodsSeachOrderByType.OriginalPriceDesc:
                        result = result.OrderByDescending(k => k.SpecialOffer);
                        break;
                    case GoodsSeachOrderByType.SortAsc:
                        result = result.OrderBy(k => k.Sort);
                        break;
                    case GoodsSeachOrderByType.SortDesc:
                        result = result.OrderByDescending(k => k.Sort);
                        break;
                    case GoodsSeachOrderByType.SalesVolumesASC:
                        result = result.OrderBy(k => k.SalesVolumes);
                        break;
                    case GoodsSeachOrderByType.SalesVolumesDesc:
                        result = result.OrderByDescending(k => k.SalesVolumes);
                        break;
                    case GoodsSeachOrderByType.TotalSalesVolumesASC:
                        result = result.OrderBy(k => k.TotalSalesVolumes);
                        break;
                    case GoodsSeachOrderByType.TotalSalesVolumesDESC:
                        result = result.OrderByDescending(k => k.TotalSalesVolumes);
                        break;
                    case GoodsSeachOrderByType.GoodsIDAsc:
                        result = result.OrderBy(k => k.GoodsID);
                        break;
                    case GoodsSeachOrderByType.GoodsIDDesc:
                        result = result.OrderByDescending(k => k.GoodsID);
                        break;
                    default:
                        result = result.OrderBy(k => k.Sort);
                        break;
                }
                #endregion

                //result= result.Distinct();
                list.PageCount = result.Count();
                result = result.Skip(options.Offset).Take(options.Rows);

                list.GoodsModel = await result.ToListAsync();
                return list;
            }

        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public async Task<GoodsListDto> GetGoodsListAsync(GoodsSeachOptions options, int AdminID)
        {
            var BasePath = GetSysConfigList.Values.Where(s => s.Name == "HeadQuartersDomain").FirstOrDefault().Value;
            GoodsListDto list = new GoodsListDto();
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_goods.Where(s => true);
                if (options.IsHot == 1)
                {
                    tempIq = tempIq.Where(s => s.IsHot == 1);
                }
                if (options.IsHot == 0)
                {
                    tempIq = tempIq.Where(s => s.IsHot == 0);
                }
                if (options.IsNewGoods == 1)
                {
                    tempIq = tempIq.Where(s => s.IsNewGoods == 1);
                }
                if (options.IsNewGoods == 0)
                {
                    tempIq = tempIq.Where(s => s.IsNewGoods == 0);
                }
                if (options.IsSeckill == 1)
                {
                    tempIq = tempIq.Where(s => s.IsSeckill == 1);
                }
                if (options.IsSeckill == 0)
                {
                    tempIq = tempIq.Where(s => s.IsSeckill == 0);
                }
                if (options.IsRecommend == 1)
                {
                    tempIq = tempIq.Where(s => s.IsRecommend == 1);
                }
                if (options.IsRecommend == 0)
                {
                    tempIq = tempIq.Where(s => s.IsRecommend == 0);
                }
                if (options.GoodsGroupID != null && options.GoodsGroupID != 0)
                {
                    int[] GroupID = ctx.le_goodsgroups.Where(s => s.ID == options.GoodsGroupID || s.ParentID == options.GoodsGroupID).Select(s => s.ID).ToArray();

                    tempIq = tempIq.Where(s => GroupID.Contains(s.GoodsGroupsID));
                }
                if (options.IsShelves == 1)
                {
                    tempIq = tempIq.Where(S => S.IsShelves == 1);
                }
                if (options.IsShelves == 0)
                {
                    tempIq = tempIq.Where(S => S.IsShelves == 0);
                }

                if (!string.IsNullOrEmpty(options.KeyWords)) //搜索
                {
                    tempIq = tempIq.Where(s => s.GoodsName.Contains(options.KeyWords)
                       || s.GoodsID.ToString().Contains(options.KeyWords)
                       || s.Describe.Contains(options.KeyWords)
                       || s.GoodsGroupsID.ToString().Contains(options.KeyWords)
                       || s.le_goods_value.Any(k => k.SerialNumber.Contains(options.KeyWords) && k.Enable == 1)
                       ); ;
                }
                 if(options.GoodsID!=null&&options.PageTurning!=null)
                {
                    if (options.PageTurning == 1)
                    {
                        tempIq = tempIq.Where(s => s.GoodsID < options.GoodsID.Value);
                    }
                    if (options.PageTurning == 2)
                    {
                        tempIq = tempIq.Where(s => s.GoodsID > options.GoodsID.Value);
                    }
                }
                if (!string.IsNullOrEmpty(options.SerialNumber))
                {
                    tempIq = tempIq.Where(s => s.le_goods_value.Any(k => k.SerialNumber.Contains(options.SerialNumber)&&k.Enable==1));
                }
                if (options.SupplierID != null)
                {
                    tempIq = tempIq.Where(s => s.le_goods_suppliers.Any(k => k.SuppliersID == options.SupplierID&&k.IsDeleted==0));
                }

                var tempJoin = tempIq.Join(ctx.le_goods_suppliers, o => o.GoodsID, p => p.GoodsID, (p, o) => new GroodsModelDto
                {
                    GoodsID = o.GoodsID,
                    GoodsName = p.GoodsName,
                    GoodsGroups_ID = p.GoodsGroupsID,
                    Sort = p.Sort,
                    Specifications = p.Specifications,
                    Describe = p.Describe,
                    IsShelves = p.IsShelves,
                    Image = BasePath + p.Image,
                    IsHot = p.IsHot,
                    IsNewGoods = p.IsNewGoods,
                    IsRecommend = p.IsRecommend,
                    IsSeckill = p.IsSeckill,
                    CreateTime = p.CreateTime,
                    SpecialOffer = p.SpecialOffer,
                    OriginalPrice = p.OriginalPrice,
                    GoodsGroupName = p.le_goodsgroups.Name,
                    PackingNumber = p.PackingNumber,
                    SalesVolumes = p.SalesVolumes,
                    TotalSalesVolumes = p.TotalSalesVolume,
                    Stock = p.Stock,
                    Quota = p.Quota,
                    MSRP = p.MSRP,
                    MinimumPurchase=p.MinimumPurchase,
                    GoodsValueList = p.le_goods_value.Where(k => k.Enable == 1).Select(k => new GoodsValues()
                    {
                        SerialNumber = k.SerialNumber,
                        CategoryType = k.CategoryType,
                        GoodsID = k.GoodsID,
                        GoodsValueID = k.GoodsValueID,
                        GoodsValueName = k.GoodsValue
                    }).ToList(),
                    SupplierID = o.SuppliersID
                });
                var AdminRoleSupplier = ctx.lel_admin_suppliers.Where(s => s.AdminID == AdminID).Select(s => s.SupplierID).ToList();
                var tmepGroup = tempJoin.Where(s => AdminRoleSupplier.Contains(s.SupplierID));//.ToList();
              
              
                list.PageCount = await tmepGroup.CountAsync();
                #region 排序              
                switch (options.SortKey)
                {
                    case GoodsSeachOrderByType.CreateTimeAsc:
                        tmepGroup = tmepGroup.OrderBy(s => s.CreateTime);
                        break;
                    case GoodsSeachOrderByType.CreateTimeDesc:
                        tmepGroup = tmepGroup.OrderByDescending(s => s.CreateTime);
                        break;
                    case GoodsSeachOrderByType.OriginalPriceAsc:
                        tmepGroup = tmepGroup.OrderBy(s => s.SpecialOffer);
                        break;
                    case GoodsSeachOrderByType.OriginalPriceDesc:
                        tmepGroup = tmepGroup.OrderByDescending(s => s.SpecialOffer);
                        break;
                    case GoodsSeachOrderByType.SortAsc:
                        tmepGroup = tmepGroup.OrderBy(s => s.Sort);
                        break;
                    case GoodsSeachOrderByType.SortDesc:
                        tmepGroup = tmepGroup.OrderByDescending(s => s.Sort);
                        break;
                    case GoodsSeachOrderByType.SalesVolumesASC:
                        tmepGroup = tmepGroup.OrderBy(s => s.SalesVolumes);
                        break;
                    case GoodsSeachOrderByType.SalesVolumesDesc:
                        tmepGroup = tmepGroup.OrderByDescending(s => s.SalesVolumes);
                        break;
                    case GoodsSeachOrderByType.TotalSalesVolumesASC:
                        tmepGroup = tmepGroup.OrderBy(s => s.TotalSalesVolumes);
                        break;
                    case GoodsSeachOrderByType.TotalSalesVolumesDESC:
                        tmepGroup = tmepGroup.OrderByDescending(s => s.TotalSalesVolumes);
                        break;
                    case GoodsSeachOrderByType.GoodsIDAsc:
                        tmepGroup = tmepGroup.OrderBy(k => k.GoodsID);
                        break;
                    case GoodsSeachOrderByType.GoodsIDDesc:
                        tmepGroup = tmepGroup.OrderByDescending(k => k.GoodsID);
                        break;
                    default:
                        tmepGroup = tmepGroup.OrderBy(s => s.Sort);
                        break;
                }
                #endregion

                tmepGroup = tmepGroup.Skip(options.Offset).Take(options.Rows);
                var kk = await tmepGroup.ToListAsync();

                //List<GroodsModelDto> result = new List<GroodsModelDto>();
                //foreach (var index in kk)
                //{
                //    foreach (var sub in index.ToList<GroodsModelDto>())
                //    {
                //        result.Add(sub);
                //    }
                //}
                list.GoodsModel = kk;//tempii;
                return list;

                //list.PageCount = result.Count();
                //result = result.Skip(options.Offset).Take(options.Rows);

                //list.GoodsModel = await result.ToListAsync();
                //return list;
            }

        }
        //}
        /// <summary>
        /// 获取商品详细
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        public async Task<GoodsDetailedDto> GetGoodDetailedAync(int GoodsID)
        {
            var BasePath = GetSysConfigList.Values.Where(s => s.Name == "HeadQuartersDomain").FirstOrDefault().Value;
            GoodsDetailedDto GDetailed = new GoodsDetailedDto();
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_goods.Where(s => s.GoodsID == GoodsID);
                var result = await temp.Select(s => new
                {
                    s.GoodsName,
                    s.SpecialOffer,
                    s.OriginalPrice,
                    s.Specifications,
                    s.ShelfLife,
                    s.Describe,
                    s.GoodsGroupsID,
                    s.IsHot,
                    s.IsNewGoods,
                    s.IsRecommend,
                    s.IsShelves,
                    s.Sort,
                    s.Image,
                    s.GoodsID,
                    s.PackingNumber,
                    s.le_goodsgroups.Name,
                    s.le_goods_value,
                    s.le_goods_img,
                    s.SalesVolumes,
                    s.TotalSalesVolume,
                    s.le_goods_suppliers,
                    s.IsSeckill,
                    s.Quota,
                    s.Stock,
                    s.MinimumPurchase,
                    s.IsBulkCargo,
                    s.IsDeliverHome,
                    s.MSRP,
                    s.GoodsBarand,
                    s.Initial,
                    s.Integral,
                    s.IsCrossdomain,
                  
                    s.IsReturn,
                    s.PlaceofOrigin,
                    s.PriceFull,
                    s.PriceReduction,
                    s.ProductionDate,
                    s.Remarks,
                    s.SeckillBeginTime,
                    s.SeckillEndTime,
                    s.VirtualNumber,
                    s.UrgentOrder,
                    s.Discount,
                    s.TermOfValidity,
                    s.CountFull,
                    s.CountReduction,
                }).FirstOrDefaultAsync();
                if (result == null || result.GoodsID == 0)
                {
                    return null;
                }
                GDetailed.MinimumPurchase = result.MinimumPurchase;
                GDetailed.IsBulkCargo = result.IsBulkCargo;
                GDetailed.IsDeliverHome = result.IsDeliverHome;
                GDetailed.MSRP = result.MSRP;

                GDetailed.Stock = result.Stock;
                GDetailed.Quota = result.Quota;
                GDetailed.IsSeckill = result.IsSeckill;
                GDetailed.GoodsName = result.GoodsName;
                GDetailed.SpecialOffer = result.SpecialOffer;
                GDetailed.OriginalPrice = result.OriginalPrice;
                GDetailed.ShelfLife = result.ShelfLife;

                GDetailed.Sort = result.Sort;
                GDetailed.Describe = result.Describe;
                GDetailed.IsHot = result.IsHot;
                GDetailed.IsNewGoods = result.IsNewGoods;
                GDetailed.IsRecommend = result.IsRecommend;
                GDetailed.IsShelves = result.IsShelves;
                GDetailed.Specifications = result.Specifications;
                GDetailed.GoodsID = result.GoodsID;
                GDetailed.Image = BasePath + result.Image;
                GDetailed.SalesVolumes = result.SalesVolumes;
                GDetailed.TotalSalesVolumes = result.TotalSalesVolume;


                GDetailed.GoodsBarand       = result.GoodsBarand;
                GDetailed.Initial           = result.Initial;
                GDetailed.Integral          = result.Integral;
                GDetailed.IsCrossdomain     = result.IsCrossdomain;
                GDetailed.IsDeliverHome     = result.IsDeliverHome;
                GDetailed.IsReturn          = result.IsReturn;
                GDetailed.PlaceofOrigin     = result.PlaceofOrigin;
                GDetailed.PriceFull         = result.PriceFull;
                GDetailed.PriceReduction    = result.PriceReduction;
                GDetailed.ProductionDate    = result.ProductionDate;
                GDetailed.Remarks           = result.Remarks;
                GDetailed.SeckillBeginTime  = result.SeckillBeginTime;
                GDetailed.SeckillEndTime    = result.SeckillEndTime;
                GDetailed.VirtualNumber     = result.VirtualNumber;
                GDetailed.UrgentOrder       = result.UrgentOrder;
                GDetailed.Discount          = result.Discount;
                GDetailed.TermOfValidity    = result.TermOfValidity;
                GDetailed.CountFull = result.CountFull;
                GDetailed.CountReduction = result.CountReduction;
                //GDetailed.SupplierID = result.SuppliersID;
                //GDetailed.SupplierName = result.SuppliersName; //GetSupplierByID(GDetailed.SupplierID);

                GDetailed.GoodsGroups_ID = result.GoodsGroupsID;
                GDetailed.GoodsGroupsName = result.Name;//GetGoodsGroupsByID(GDetailed.GoodsGroups_ID);
                GDetailed.PackingNumber = result.PackingNumber;
                List<GoodsValues> ListgoodsValues = new List<GoodsValues>();
                foreach (var GoodValue in result.le_goods_value)
                {
                    if (GoodValue.Enable == 1)
                    {
                        GoodsValues goodsValues = new GoodsValues();
                        goodsValues.CategoryType = GoodValue.CategoryType;
                        goodsValues.GoodsValueID = GoodValue.GoodsValueID;
                        goodsValues.GoodsValueName = GoodValue.GoodsValue;
                        goodsValues.SerialNumber = GoodValue.SerialNumber;

                        ListgoodsValues.Add(goodsValues);
                    }
                }
                List<GoodsImg> ListImg = new List<GoodsImg>();
                foreach (var Img in result.le_goods_img)
                {
                    if (Img.IsDelete == 0)
                    {
                        GoodsImg goodsImg = new GoodsImg();
                        goodsImg.ID = Img.ID;
                        goodsImg.Src = BasePath + Img.Src;
                        ListImg.Add(goodsImg);
                        // GDetailed.ImgList.Add(goodsImg);
                    }

                }
                List<SupplierGoods> supplierGoods = new List<SupplierGoods>();
                foreach (var Supplier in result.le_goods_suppliers)
                {
                    if (Supplier.IsDeleted == 0)
                    {
                        supplierGoods.Add(new SupplierGoods
                        {
                            SupplierID = Supplier.SuppliersID,
                            IsDefalut = Supplier.IsDefalut,
                            SupplierName = Supplier.le_suppliers.SuppliersName,
                            Price = Supplier.Supplyprice,
                            GoodsMappingID = Supplier.GoodsMappingID,
                        });
                    }
                }
                GDetailed.ImgList = ListImg;
                GDetailed.ValuesList = ListgoodsValues;
                GDetailed.SupplierGoodsList = supplierGoods;

                return GDetailed;
            }
        }


        #region 商品分类

        /// <summary>
        /// 获取所有的商品分类
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public List<GoodsGroupDto> GetGoodsGroupList(string KeyWords = "")
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_goodsgroups.Where(s => true);
                if (!string.IsNullOrEmpty(KeyWords))
                {
                    tempIq = tempIq.Where(s => s.Name.Contains(KeyWords));
                }
                var result = tempIq.OrderBy(s => s.Sort).Select(s => new GoodsGroupDto
                {
                    Sort = s.Sort,
                    ID = s.ID,
                    Level = s.Level,
                    Name = s.Name,
                    ParentID = s.ParentID
                }).AsNoTracking().ToList();
                return result;
            }
        }

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddGoodsGroupList(GoodsGroupDto model, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                var dto = ctx.le_goodsgroups.Where(s => s.Name == model.Name).FirstOrDefault();
                if (dto != null)
                {
                    msg = "该记录已存在";
                    return false;
                }
                //if(ctx.le_goodsgroups.Count()>=16)
                //{
                //    msg = "最多只能添加16条商品分类";
                //    return false;
                //}
                le_goodsgroups group = new le_goodsgroups();

                group.Level = model.Level;
                group.Name = model.Name;
                group.Sort = model.Sort;
                group.ParentID = model.ParentID;

                ctx.le_goodsgroups.Add(group);

                if (ctx.SaveChanges() > 0)
                {
                    msg = "SUCCESS";
                    return true;
                }

                msg = "新增失败";
                return false;
            }
        }

        /// <summary>
        /// 修改商品分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool EditGoodsGroupList(GoodsGroupDto model, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                var dto = ctx.le_goodsgroups.Where(s => s.ID == model.ID).FirstOrDefault();
                if (dto == null)
                {
                    msg = "该记录不存在";
                    return false;
                }

                dto.Level = model.Level;
                dto.Name = model.Name;
                dto.Sort = model.Sort;
                dto.ParentID = model.ParentID;

                ctx.Entry<le_goodsgroups>(dto).State = EntityState.Modified;

                if (ctx.SaveChanges() > 0)
                {
                    msg = "SUCCESS";
                    return true;
                }

                msg = "修改失败";
                return false;
            }
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="GoodsGroupID"></param>
        /// <returns></returns>
        public bool DeleteGoodsGroup(int GoodsGroupID)
        {
            using (Entities ctx = new Entities())
            {
                var List = ctx.le_goodsgroups.Where(s => s.ParentID == GoodsGroupID || s.ID == GoodsGroupID).ToList();
                if (List.Count == 0)
                {
                    return false;
                }

                //le_goodsgroups entity = new le_goodsgroups { ID = GoodsGroupID };

                //ctx.le_goodsgroups.Attach(entity);
                ctx.le_goodsgroups.RemoveRange(List);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {

                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    return false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int AddGoods(AddGoodsDto dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    le_goods model = new le_goods();

                    if (string.IsNullOrEmpty(dto.HeadImage))
                    {
                        msg = "参数错误，未接受到有效参数";
                        return 0;
                    }
                    else
                    {
                        //model.Image = AttachmentUrl(dto.HeadImageFlow);
                        model.Image = dto.HeadImage;
                    }
                    model.GoodsBarand = dto.GoodsBarand;
                    model.Initial = dto.Initial;
                    model.Integral = dto.Integral;
                    model.IsCrossdomain = dto.IsCrossdomain;
                    model.IsDeliverHome = dto.IsDeliverHome;
                    model.IsReturn = dto.IsReturn;
                    model.PlaceofOrigin = dto.PlaceofOrigin;

                    model.PriceFull = dto.PriceFull;
                    model.PriceReduction = dto.PriceReduction;

                    model.CountFull = dto.CountFull;
                    model.CountReduction = dto.CountReduction;

                    model.ProductionDate = dto.ProductionDate;
                    model.Remarks = dto.Remarks;
                    model.SeckillBeginTime = dto.SeckillBeginTime;
                    model.SeckillEndTime = dto.SeckillEndTime;
                    model.VirtualNumber = dto.VirtualNumber;
                    model.UrgentOrder = dto.UrgentOrder;
                    model.Discount = dto.Discount;
                    model.TermOfValidity = dto.TermOfValidity;
                    model.CountFull = dto.CountFull;
                    model.CountReduction = dto.CountReduction;

                    model.Describe = dto.Describe;
                    model.GoodsGroupsID = dto.GoodsGroups_ID;
                    model.GoodsName = dto.GoodsName;
                    model.SpecialOffer = dto.SpecialOffer;                
                    model.IsHot = dto.IsHot;
                    model.IsNewGoods = dto.IsNewGoods;
                    model.IsRecommend = dto.IsRecommend;
                    model.IsShelves = dto.IsShelves;
                    model.Sort = dto.Sort;
                    model.Specifications = dto.Specifications;
                    model.UpdateTime = DateTime.Now;
                    model.CreateTime = DateTime.Now;
                    model.ShelfLife = dto.ShelfLife;
                    model.PackingNumber = dto.PackingNumber;
                    model.IsSeckill = dto.IsSeckill;
                    model.Stock = dto.Stock;
                    model.Quota = dto.Quota.Value;
                    model.RowVersion = DateTime.Now;

                    model.IsBulkCargo = dto.IsBulkCargo;
                    model.IsDeliverHome = dto.IsDeliverHome;
                    model.MSRP = dto.MSRP;
                    model.MinimumPurchase = dto.MinimumPurchase;

                    model.Province = dto.Province;
                    model.City = dto.City;
                    model.Area = dto.Area;
                    model.PiecePrice = dto.PiecePrice;
                    model.MinimumPrice = dto.MinimumPrice;
                    model.BusinessValue = dto.BusinessValue;
                    model.NewPeriod = dto.NewPeriod;
                    model.Unit = dto.Unit;
                    model.IsRandomDistribution = dto.IsRandomDistribution;

                    model.PriceScheme1 = dto.PriceScheme1.HasValue ? dto.PriceScheme1.Value : 0;
                    model.PriceScheme2 = dto.PriceScheme2.HasValue ? dto.PriceScheme2.Value : 0;
                    model.PriceScheme3 = dto.PriceScheme3.HasValue ? dto.PriceScheme3.Value : 0;

                    model.IsParcel = dto.IsParcel;



                    #region 添加属性
                    int p = 1;
                    List<le_goods_value> List = new List<le_goods_value>();
                    foreach (var index in dto.GoodsValueList)
                    {

                        le_goods_value goods_Value = new le_goods_value();
                        goods_Value.CategoryType = index.CategoryType;
                        goods_Value.Enable = 1;
                        goods_Value.GoodsID = dto.GoodsID;
                        goods_Value.SerialNumber = index.SerialNumber;
                        goods_Value.IsBulkCargo = model.IsBulkCargo;

                        if (string.IsNullOrEmpty(goods_Value.SerialNumber))
                        {
                            goods_Value.IsAuto = 1;
                            if (model.IsBulkCargo == 0)
                            {

                                goods_Value.SerialNumber = BarcodeGeneration(0);
                            }
                            else
                            {
                                goods_Value.SerialNumber = BarcodeGeneration(1);
                            }
                        }


                        goods_Value.GoodsValue = index.GoodsValueName;
                        List.Add(goods_Value);
                        model.le_goods_value.Add(goods_Value);
                        p++;
                    }
                    #endregion

                    #region 添加图片
                    foreach (var imgsrc in dto.GoodsImgList)
                    {
                        le_goods_img imgModel = new le_goods_img();
                        imgModel.CreatTime = DateTime.Now;
                        imgModel.GoodsID = dto.GoodsID;
                        imgModel.Src = imgsrc;
                        imgModel.IsDelete = 0;
                        imgModel.UpdateTime = DateTime.Now;
                        model.le_goods_img.Add(imgModel);
                    }

                    #endregion

                    #region 添加供应商
                    foreach (var Supplier in dto.GoodsSuplierPriceList)
                    {
                        le_goods_suppliers le_Goods_Suppliers = new le_goods_suppliers();
                        le_Goods_Suppliers.CreatTime = DateTime.Now;
                        le_Goods_Suppliers.IsDefalut = Supplier.IsDefalut;
                        //le_Goods_Suppliers.IsDeleted = Supplier.is;
                        le_Goods_Suppliers.SuppliersID = Supplier.SupplierID;
                        le_Goods_Suppliers.Supplyprice = Supplier.Price;
                        le_Goods_Suppliers.UpdateTime = DateTime.Now;
                        model.le_goods_suppliers.Add(le_Goods_Suppliers);
                    }
                    #endregion

                    ctx.le_goods.Add(model);


                    // string addimagemsg;

                    if (ctx.SaveChanges() > 0)
                    {
                        //添加商品图片
                        //if (AddGoodsImage(dto.GoodsImageList, model.GoodsID, out addimagemsg, out object obj))
                        //{
                        //    //if (AddGoodsValueMapping(dto.GoodsValueMappingList, model.GoodsID, out string msg))
                        //    //{
                        //    //    return model.GoodsID;
                        //    //}
                        msg = "SUCCESS";
                        return model.GoodsID;
                        //}
                    }
                    msg = "数据库保存失败";
                    return 0;
                }
                catch (DbEntityValidationException ex)
                {
                    string kk = "";
                    foreach (var index in ex.EntityValidationErrors)
                    {
                        kk += index.ValidationErrors;
                    }
                    msg = kk;
                    log.Error(dto, ex);
                    return 0;
                }
                catch (Exception ex)
                {
                    //log.Debug(dto, ex);

                    log.Error(dto, ex);
                    msg = ExceptionHelper.GetInnerExceptionMsg(ex);
                    //   msg = ex.Message;
                    return 0;
                }

            }
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        public bool UnShelvesGoods(int GoodsID, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                var dto = ctx.le_goods.Where(s => s.GoodsID == GoodsID).FirstOrDefault();
                if (dto == null)
                {
                    msg = "该记录不存在，请确认后重试";
                    return false;
                }
                if (dto.IsShelves == 0)
                {
                    dto.IsShelves = 1;
                }
                else
                {
                    dto.IsShelves = 0;
                }
                ctx.Entry<le_goods>(dto).State = EntityState.Modified;
                if (ctx.SaveChanges() > 0)
                {
                    msg = "SUCCESS";
                    return true;
                }

                msg = "下架失败";
                return true;
            }
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool EditGoods(AddGoodsDto dto, LoginInfo loginInfo, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                le_goods_log GoodLogModel = new le_goods_log();

                var model = ctx.le_goods.Where(s => s.GoodsID == dto.GoodsID).FirstOrDefault();
                GoodLogModel.AdminID = loginInfo.UserID;
                GoodLogModel.BeforeGoodsName = model.GoodsName;
                GoodLogModel.BeforeQuota = model.Quota;
                GoodLogModel.BeforeSheLvesStatus = model.IsShelves;
                GoodLogModel.BeforeSpecialOffer = model.SpecialOffer;
                GoodLogModel.BeforeStock = model.Stock;
                GoodLogModel.GoodsID = model.GoodsID;

                if (model == null)
                {
                    msg = "该记录不存在，请确认后重试";
                    return false;
                }

                model.GoodsGroupsID = dto.GoodsGroups_ID;
                model.GoodsName = dto.GoodsName;

                model.Specifications = dto.Specifications;
                model.IsShelves = dto.IsShelves;
                model.IsRecommend = dto.IsRecommend;
                model.IsNewGoods = dto.IsNewGoods;
                model.IsHot = dto.IsHot;
                model.IsSeckill = dto.IsSeckill;
                model.Describe = dto.Describe;
                model.Sort = dto.Sort;
                model.OriginalPrice = dto.OriginalPrice;
                model.SpecialOffer = dto.SpecialOffer;
                model.PackingNumber = dto.PackingNumber;
                model.Quota = dto.Quota.Value;
                model.Stock = dto.Stock;
                model.RowVersion = DateTime.Now;

                model.MSRP = dto.MSRP;
                model.IsDeliverHome = dto.IsDeliverHome;
                model.IsBulkCargo = dto.IsBulkCargo;
                model.MinimumPurchase = dto.MinimumPurchase;

                model.GoodsBarand = dto.GoodsBarand;
                model.Initial = dto.Initial;
                model.Integral = dto.Integral;
                model.IsCrossdomain = dto.IsCrossdomain;
                model.IsDeliverHome = dto.IsDeliverHome;
                model.IsReturn = dto.IsReturn;
                model.PlaceofOrigin = dto.PlaceofOrigin;
                model.PriceFull = dto.PriceFull;
                model.PriceReduction = dto.PriceReduction;
                model.ProductionDate = dto.ProductionDate;
                model.Remarks = dto.Remarks;
                model.SeckillBeginTime = dto.SeckillBeginTime;
                model.SeckillEndTime = dto.SeckillEndTime;
                model.VirtualNumber = dto.VirtualNumber;
                model.UrgentOrder = dto.UrgentOrder;
                model.Discount = dto.Discount;
                model.TermOfValidity = dto.TermOfValidity;
                model.CountFull = dto.CountFull;
                model.CountReduction = dto.CountReduction;

                model.Province = dto.Province;
                model.City = dto.City;
                model.Area = dto.Area;
                model.PiecePrice = dto.PiecePrice;
                model.MinimumPrice = dto.MinimumPrice;
                model.BusinessValue = dto.BusinessValue;
                model.NewPeriod = dto.NewPeriod;
                model.Unit = dto.Unit;
                model.IsRandomDistribution = dto.IsRandomDistribution;
                model.PriceScheme1 = dto.PriceScheme1.HasValue ? dto.PriceScheme1.Value : 0;
                model.PriceScheme2 = dto.PriceScheme2.HasValue ? dto.PriceScheme2.Value : 0;
                model.PriceScheme3 = dto.PriceScheme3.HasValue ? dto.PriceScheme3.Value : 0;
                model.IsParcel = dto.IsParcel;

                GoodLogModel.AfterGoodsName = model.GoodsName;
                GoodLogModel.AfterQuota = model.Quota;
                GoodLogModel.AfterSheLvesStatus = model.IsShelves;
                GoodLogModel.AfterSpecialOffer = model.SpecialOffer;
                GoodLogModel.AfterStock = model.Stock;
                if (!string.IsNullOrEmpty(dto.ShelfLife))
                {
                    model.ShelfLife = dto.ShelfLife;
                }
                if (!string.IsNullOrEmpty(dto.HeadImage))
                {
                    model.Image = dto.HeadImage;
                }

                ctx.le_goods_log.Add(GoodLogModel);
                if (ctx.SaveChanges() > 0)
                {
                    msg = "SUCCESS";
                    return true;
                }
                else
                {
                    msg = "修改失败，请稍后重试";
                    return false;
                }
            }
        }

        /// <summary>
        /// 添加供货商商品价格
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="GoodsID"></param>
        /// <param name="Price"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public int AddSupplierGoodsPrice(int SupplierID, int GoodsID, decimal Price, int IsDefalut, out string Msg)
        {
            using (Entities ctx = new Entities())
            {
                if (ctx.le_goods_suppliers.Any(s => s.SuppliersID == SupplierID && s.GoodsID == GoodsID && s.IsDeleted == 0))
                {
                    Msg = "已存在供应商价格,请勿重复添加";
                    return 0;
                }
                le_goods_suppliers model = new le_goods_suppliers();
                model.SuppliersID = SupplierID;
                model.GoodsID = GoodsID;
                model.Supplyprice = Price;
                model.CreatTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.IsDeleted = 0;
                model.IsDefalut = IsDefalut;
                ctx.le_goods_suppliers.Add(model);
                if (ctx.SaveChanges() > 0)
                {
                    Msg = "SUCCESS";
                    return model.GoodsMappingID;
                }
                else
                {
                    Msg = "添加商品";
                    return 0;
                }
            }
        }

        /// <summary>
        /// 修改供应商价格
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Price"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public bool UpdateSupplierPrice(int ID, decimal Price, int IsDeleted, int IsDefault, int? AdminID = null)
        {
            using (Entities ctx = new Entities())
            {

                var Model = ctx.le_goods_suppliers.Where(s => s.GoodsMappingID == ID).FirstOrDefault();
                if (Model != null)
                {
                    if (AdminID != null)
                    {
                        if (!ctx.lel_admin_suppliers.Any(s => s.AdminID == AdminID && s.SupplierID == Model.SuppliersID))
                        {
                            ///无权限
                            return false;
                        }
                    }
                    if (IsDefault == 1)
                    {
                        var List = ctx.le_goods_suppliers.Where(s => s.GoodsID == Model.GoodsID && s.IsDefalut == 1 && s.GoodsMappingID != Model.GoodsMappingID).FirstOrDefault();
                        if (List != null)
                        {
                            List.IsDefalut = 0;
                            ctx.Entry<le_goods_suppliers>(List).State = EntityState.Modified;
                            if (ctx.SaveChanges() <= 0)
                            {
                                return false;
                            }
                        }
                    }
                    Model.Supplyprice = Price;
                    Model.IsDeleted = IsDeleted;
                    Model.IsDefalut = IsDefault;
                    ctx.Entry<le_goods_suppliers>(Model).State = EntityState.Modified;
                    int SaveCount = ctx.SaveChanges();
                    if (SaveCount > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取供应商商品价格
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <param name="Offset"></param>
        /// <param name="Rows"></param>
        /// <param name="GoodsID"></param>
        /// <param name="SuppliersID"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<SupplierGoodsPriceDto> GetSupplierGoodsPriceList(string KeyWords, int Offset, int Rows, int? GoodsID, int? SuppliersID, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_goods_suppliers.Where(s => s.IsDeleted == 0);
                if (!string.IsNullOrEmpty(KeyWords))
                {
                    tempIq = tempIq.Where(s => s.le_suppliers.SuppliersName.Contains(KeyWords));
                }
                if (GoodsID != null && GoodsID != 0)
                {
                    tempIq = tempIq.Where(s => s.GoodsID == GoodsID.Value);
                }
                if (SuppliersID != null && GoodsID != 0)
                {
                    tempIq = tempIq.Where(s => s.SuppliersID == SuppliersID.Value);
                }
                tempIq = tempIq.OrderByDescending(s => s.CreatTime);
                var result = tempIq.Select(s => new SupplierGoodsPriceDto
                {
                    CreateTime = s.CreatTime,
                    SuppliersID = s.SuppliersID,
                    SuppliersName = s.le_suppliers.SuppliersName,
                    GoodsID = s.GoodsID,
                    GoodsName = s.le_goods.GoodsName,
                    Price = s.Supplyprice,
                    SuppliersPhone = s.le_suppliers.MobilePhone,
                    GoodsMappingID = s.GoodsMappingID
                });
                Count = tempIq.Count();
                result = result.Skip(Offset).Take(Rows);
                return result.ToList();

            }
        }

        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="List"></param>
        /// <param name="GoodsID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public bool AddGoodsImage(List<GoodsImage> List,int GoodsID,out string msg, out object obj)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        List<GoodsImage> grouplist = new List<GoodsImage>();
        //        try {
        //            if (List.Count <= 0)
        //            {
        //                msg = "请选择图片信息";
        //                obj = grouplist;
        //                return false;
        //            }

        //            foreach (var eh in List)
        //            {
        //                le_goods_img model = new le_goods_img();
        //                model.GoodsID = GoodsID;
        //                model.Src = AttachmentUrl(eh.Base64Src);
        //                model.CreatTime = DateTime.Now;
        //                ctx.le_goods_img.Add(model);

        //                if (ctx.SaveChanges() > 0)
        //                {
        //                    eh.ID = model.ID;

        //                }
        //                else {
        //                    obj = grouplist;
        //                    msg = "新增商品图片失败";
        //                    return false;
        //                }

        //                grouplist.Add(eh);
        //            }


        //            obj = grouplist;
        //            msg = "SUCCESS";
        //            return true;
        //        } catch (Exception ex)
        //        {
        //            obj = grouplist;
        //            msg = "新增商品图片异常，ERROR:"+ ex.ToString();
        //            log.Error(List, ex);
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// 添加商品图片
        /// </summary>        
        public int AddGoodsImage(List<AddGoodsAttachImg1to> List, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (List.Count <= 0)
                    {
                        msg = "请选择图片信息";
                        return 0;
                    }
                    le_goods_img model = new le_goods_img();
                    foreach (var eh in List)
                    {
                        model.GoodsID = eh.GoodsID;
                        model.Src = eh.Src;
                        model.CreatTime = DateTime.Now;
                        ctx.le_goods_img.Add(model);
                    }
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return model.ID;
                    }
                    else
                    {
                        msg = "新增商品图片失败";
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    // obj = grouplist;
                    msg = "新增商品图片异常，ERROR:" + ex.ToString();
                    log.Error(List, ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="List"></param>
        /// <param name="GoodsID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// 
        public bool DeleteGoodsImage(List<GoodsImage> List, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (List.Count <= 0)
                    {
                        msg = "请选择图片信息";
                        return false;
                    }

                    foreach (var eh in List)
                    {
                        le_goods_img model = new le_goods_img() { ID = eh.ID };
                        ctx.le_goods_img.Attach(model);
                        ctx.le_goods_img.Remove(model);
                    }

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }

                    msg = "删除商品图片失败";
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "删除商品图片异常，ERROR:" + ex.ToString();
                    log.Error(List, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="ListImgID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteGoodsImg(List<int> ListImgID, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (ListImgID.Count <= 0)
                    {
                        msg = "请选择图片信息";
                        return false;
                    }
                    foreach (var id in ListImgID)
                    {
                        var entity = ctx.le_goods_img.Find(id);
                        if (entity != null)
                        {
                            entity.IsDelete = 1;
                            //entity.UpdateTime = DateTime.Now;
                            ctx.Entry<le_goods_img>(entity).State = EntityState.Modified;

                            //ctx.le_goods_img(model);
                        }
                    }

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }

                    msg = "删除商品图片失败";
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "删除商品图片异常，ERROR:" + ex.ToString();
                    log.Error(ListImgID, ex);
                    return false;
                }
            }
        }

        #region 商品属性操作

        public le_goods_value AddGoodsValueList(List<GoodsValues> List,int IsBulkCargo, out string Msg)
        {
            using (Entities ctx = new Entities())
            {
                List<Object> list = new List<Object>();

                if (List.Count <= 0)
                {
                    Msg = "参数错误，未获取到有效值";
                    return null;
                }
                try
                {
                    foreach (var index in List)
                    {
                        
                        var exitModel = ctx.le_goods_value.Where(s => s.GoodsValueID == index.GoodsValueID).FirstOrDefault();
                        if (exitModel != null)
                        {
                            if (string.IsNullOrEmpty(index.SerialNumber))
                            {
                                exitModel.IsBulkCargo = IsBulkCargo;
                                exitModel.IsAuto = 1;
                                if (IsBulkCargo == 0)
                                {

                                    index.SerialNumber = BarcodeGeneration(0);
                                }
                                else
                                {
                                    index.SerialNumber = BarcodeGeneration(1);
                                }
                            }
                            exitModel.GoodsID = index.GoodsID;
                            exitModel.GoodsValue = index.GoodsValueName;
                            exitModel.UpdateTime = DateTime.Now;
                            //exitModel.Enable=
                            exitModel.CategoryType = index.CategoryType;
                            exitModel.SerialNumber = index.SerialNumber;
                            ctx.Entry<le_goods_value>(exitModel).State = EntityState.Modified;
                            if (ctx.SaveChanges() <= 0)
                            {
                                log.Error(string.Format("属性修改失败id={0}", exitModel.GoodsValueID));
                                Msg = "SUCCESS";
                                return exitModel;
                            }
                        }
                        else
                        {
                          
                            le_goods_value goods_Value = new le_goods_value();
                            if (string.IsNullOrEmpty(index.SerialNumber))
                            {
                                goods_Value.IsAuto = 1;
                                if (IsBulkCargo == 0)
                                {

                                    index.SerialNumber = BarcodeGeneration(0);
                                }
                                else
                                {
                                    index.SerialNumber = BarcodeGeneration(1);
                                }
                            }
                            goods_Value.IsBulkCargo = IsBulkCargo;
                            goods_Value.GoodsID = index.GoodsID;
                            goods_Value.GoodsValue = index.GoodsValueName;
                            //exitModel.Enable=
                            goods_Value.CategoryType = index.CategoryType;
                            goods_Value.SerialNumber = index.SerialNumber;
                            goods_Value.Enable = 1;
                            goods_Value.CreateTime = DateTime.Now;
                            goods_Value.UpdateTime = DateTime.Now;
                            ctx.le_goods_value.Add(goods_Value);
                            if (ctx.SaveChanges() <= 0)
                            {
                                log.Error(string.Format("属性添加失败GoodsValue={0}", index.GoodsValueName));
                            }
                            Msg = "SUCCESS";
                            return goods_Value;
                        }
                    }
                    Msg = "SUCCESS";
                    return null;
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;
                    return null;
                }
                //Object group = new Object();
                //List<GoodsValue> Valuelist = new List<GoodsValue>();
                //Valuelist.Add(dto);

                //var exit = ctx.le_goods_value_mapping.Where(s => s.CategoryType == dto.CategoryType && s.GoodsID == dto.GoodsID).FirstOrDefault();
                //if (exit != null)
                //{
                //    //已存在属性大类，直接添加
                //    AddGoodsValue(Valuelist, exit.ID,out object valueobj);
                //    list.Add(valueobj);
                //}
                //else {
                //    //先添加属性大类，在添加属性明细
                //    le_goods_value_mapping lgvm = new le_goods_value_mapping();
                //    lgvm.GoodsID = dto.GoodsID;
                //    lgvm.CategoryType = dto.CategoryType;
                //    ctx.le_goods_value_mapping.Add(lgvm);

                //    if (ctx.SaveChanges() > 0)
                //    {
                //        AddGoodsValue(Valuelist, lgvm.ID, out object valueobj);
                //        list.Add(valueobj);
                //    }
                //}
            }
            //obj = list;
            //return true;

        }

        /// <summary>
        /// 增加商品属性
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        //public bool AddGoodsValue(List<GoodsValues> GoodsValueList,int CoodsValueMappingID, out object obj)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        List<GoodsValue> list = new List<GoodsValue>();
        //        foreach (var index in GoodsValueList)
        //        {
        //            var exit = ctx.le_goods_value.Where(s=>s.GoodsValueID==index.GoodsValueID).FirstOrDefault();
        //            if (exit != null)
        //            {
        //                exit.Enable = 1;
        //                exit.SerialNumber = index.SerialNumber;
        //                exit.GoodsValue = index.GoodsValueName;
        //                exit.CategoryType = index.CategoryType;
        //                ctx.Entry<le_goods_value>(exit).State = EntityState.Modified;

        //                if (ctx.SaveChanges() > 0)
        //                {
        //                    index.GoodsValueID = exit.GoodsValueID;
        //                }
        //                else {
        //                    obj = list;
        //                    return false;
        //                }
        //            }
        //            else {
        //                le_goods_value model = new le_goods_value();
        //                model.GoodsValue = index.GoodsValueName;
        //               // model.CoodsValueMappingID = CoodsValueMappingID;
        //                model.SerialNumber = index.SerialNumber;
        //                model.CategoryType = index.CategoryType;
        //                model.GoodsID = index.GoodsValueID;
        //                model.Enable = 1;
        //                ctx.le_goods_value.Add(model);

        //                if (ctx.SaveChanges() > 0)
        //                {
        //                    index.GoodsValueID = model.GoodsValueID;
        //                }
        //                else
        //                {
        //                    obj = list;
        //                    return false;
        //                }
        //            }

        //            list.Add(index);
        //        }
        //        //if (ctx.SaveChanges() > 0)
        //        //{
        //        //    return true;
        //        //}
        //        obj = list;
        //        return true;
        //    }
        //}

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="List"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteGoodsValue(List<int> List, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (List.Count <= 0)
                    {
                        msg = "请选择商品属性信息";
                        return false;
                    }

                    foreach (var eh in List)
                    {
                        var dto = ctx.le_goods_value.Where(s => s.GoodsValueID == eh).FirstOrDefault();
                        if (dto == null)
                        {
                            msg = "该记录不存在，请确认后重试";
                            return false;
                        }
                        dto.Enable = 0;
                        //dto.UpdateTime = DateTime.Now;
                        ctx.Entry<le_goods_value>(dto).State = EntityState.Modified;
                    }

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }

                    msg = "删除商品属性失败";
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "删除商品属性异常，ERROR:" + ex.ToString();
                    log.Error(List, ex);
                    return false;
                }
            }
        }

        //public bool AddGoodsValueMapping(List<GoodsValueMapping> List,int GoodsID,out string msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        try
        //        {
        //            msg = "";
        //            if (List.Count <= 0)
        //            {
        //                msg = "请添加商品属性";
        //                return false;
        //            }

        //            foreach (var mod in List)
        //            {
        //                le_goods_value_mapping lgvm = new le_goods_value_mapping();
        //                lgvm.GoodsID = GoodsID;
        //                lgvm.CategoryType = mod.CategoryType;
        //                ctx.le_goods_value_mapping.Add(lgvm);

        //                if (ctx.SaveChanges() > 0)
        //                {
        //                    AddGoodsValue(mod.GoodsValueList, lgvm.ID, out object obj);
        //                }
        //            }
        //            msg = "SUCCESS";
        //            return true;
        //        }
        //        catch (Exception ex){
        //            msg = "新增失败，信息：" + ex.ToString();
        //            return false;
        //        }
        //    }
        //}
        #endregion

        /// <summary>
        /// 文件流图片处理方法
        /// </summary>
        /// <param name="filebyte">文件流</param>
        /// <returns></returns>
        //public string AttachmentUrl(Byte[] filebyte)
        //{
        //    try
        //    {
        //        var ImageName = DateTime.Now.ToFileTimeUtc().ToString() + ".jpg";

        //        var WebPath = ConfigurationManager.AppSettings["WebPath"];
        //        var SavePath = ConfigurationManager.AppSettings["SavePath"] + ImageName;
        //        var ReturnPath = WebPath + ImageName;

        //        //写入目录
        //        File.WriteAllBytes(SavePath, filebyte);
        //        return ReturnPath;
        //    }
        //    catch (Exception ex)
        //    {
        //        //HX.BPM.Common.LogWriter.Write("日志", "BaggageMonitorAdd_Request", "行李监控流程发起处理异常", ex.Message);
        //        //aUrl = null;
        //        return  null;
        //    }
        //}
        public string AttachmentUrl(string imgsrc)
        {
            try
            {

                string FileHead = imgsrc.Split(',')[0];
                string FileContent = imgsrc.Split(',')[1];
                string FileType = FileHead.Substring(FileHead.IndexOf("/") + 1, FileHead.IndexOf(";") - FileHead.IndexOf("/") - 1);

                string fileFilt = "gif|jpg|bmp|jpeg|png";
                if (fileFilt.IndexOf(fileFilt) <= -1)
                {
                    Exception ex = new Exception("图片上传格式错误,允许上传格式为：" + FileHead);
                    log.Error(imgsrc, ex);
                    throw ex;
                }


                var filebyte = Convert.FromBase64String(FileContent);
                var ImageName = DateTime.Now.ToFileTimeUtc().ToString() + ".jpg";

                var WebPath = ConfigurationManager.AppSettings["WebPath"];
                var SavePath = ConfigurationManager.AppSettings["SavePath"] + ImageName;


                var ReturnPath = WebPath + ImageName;

                //写入目录
                File.WriteAllBytes(SavePath, filebyte);
                return ReturnPath;
            }
            catch (Exception ex)
            {
                //HX.BPM.Common.LogWriter.Write("日志", "BaggageMonitorAdd_Request", "行李监控流程发起处理异常", ex.Message);
                //aUrl = null;
                return "错误";
            }
        }

        /// <summary>
        /// 查询商品条码是否存在
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <returns></returns>
        public bool IsSerialNumberExit(string SerialNumber)
        {
            using (Entities ctx = new Entities())
            {
                return ctx.le_goods_value.Any(s => s.SerialNumber == SerialNumber);
            }
        }

        /// <summary>
        /// 生成条码 
        /// </summary>
        /// <param name="Count">位数</param>
        /// <param name="Offest">偏移量</param>
        /// <returns></returns>
        public string BarcodeGeneration(int IsBulkCargo)
        {
            string SerialNumber = "";
            using (Entities ctx = new Entities())
            {
                var Model = ctx.le_goods_value.Where(s => s.IsBulkCargo == IsBulkCargo && s.IsAuto == 1).OrderByDescending(s => s.SerialNumber).Skip(0).Take(1).FirstOrDefault();

                if (Model != null)
                {
                    SerialNumber = (Model.SerialNumber).ToString();
                }
                else
                {
                    SerialNumber = "0";
                }
                string NonceStr = "";
                int StrCount;
                var IsSuccess = long.TryParse(SerialNumber, out long Number);
                if (!IsSuccess)
                {
                    new Exception("自动生成条码出错 转化出错");
                }
                NonceStr = (Number + 1).ToString();
                StrCount = NonceStr.Length;
                if (IsBulkCargo == 0 && NonceStr.Length < 6) //非散货不足6位补0
                {
                    for (int i = 0; i < 6 - StrCount; i++)
                    {
                        NonceStr = NonceStr.Insert(0, "0");
                    }
                }
                else if (IsBulkCargo == 1 && NonceStr.Length < 5)//散货不足5位补0
                {
                    for (int i = 0; i < 5 - StrCount; i++)
                    {
                        NonceStr = NonceStr.Insert(0, "0");
                    }
                }
                else
                {


                }
                //var kk = NonceStr.LastIndexOf('0');
                //var sub = NonceStr.Substring(kk);
                //var str= NonceStr.Split(sub)
                // int StrCount = NonceStr.Length;


                return NonceStr;
            }

        }

        /// <summary>
        /// 获取商品当前最大排序数自动加一
        /// </summary>
        /// <returns></returns>
        public int GetGoodsMaxSort()
        {
            using (Entities ctx=new Entities())
            {
                int result = ctx.le_goods.Max(s => s.Sort);
                return result + 1;
            }
        }
        /// <summary>
        /// 判断当前排序是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExitGetGoodsSort(int Sort)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_goods.Any(s=>s.Sort== Sort);
                return result;
            }
        }

        /// <summary>
        /// 清空月销量
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ClearSalesVolumesAsync()
        {
            string sql = "Update le_goods set SalesVolumes=0 ";
            using (Entities ctx = new Entities())
            {
                //var count = 0;
                var count = await ctx.Database.ExecuteSqlCommandAsync(sql);
                if (count > 0)
                {
                    log.Debug(string.Format("清空月销量,影响行数：{0}", count));
                    return true;
                }
                else
                {
                    log.Error(string.Format("清空月销量错误"));
                    return false;
                }
            }

        }

    }
}
