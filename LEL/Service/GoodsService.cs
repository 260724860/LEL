using DTO.Goods;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 商品管理
    /// </summary>
   public class GoodsService
    {
        private static ILog log = LogManager.GetLogger(typeof(StoreUserService));
        private string GoodsImagePath = ConfigurationManager.AppSettings["goodsimagepath"];

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public async Task<GroodsListDto> GetGoodsListAsync(GoodsSeachOptions options)
        {
            GroodsListDto list = new GroodsListDto();
            using (Entities ctx = new Entities())
            {               
                var tempIq = ctx.le_goods.Where(s => true).Select(s => new GroodsModelDto {
                    GoodsID=s.GoodsID,
                    GoodsName = s.GoodsName,
                    GoodsGroups_ID = s.GoodsGroups_ID,
                    SerialNumber = s.SerialNumber,
                    Sort = s.Sort,
                    Specifications = s.Specifications,
                    Describe = s.Describe,
                    IsShelves=s.IsShelves,
                    Image=s.Image, 
                    IsHot=s.IsHot,
                    IsNewGoods=s.IsNewGoods,
                    IsRecommend=s.IsRecommend ,
                    CreateTime=s.CreateTime,
                    SpecialOffer=s.SpecialOffer,
                    OriginalPrice=s.OriginalPrice
                });
                if(options.IsHot!=null&&options.IsHot==1)
                {
                    tempIq = tempIq.Where(s => s.IsHot == 1);
                }
                if(options.IsNewGoods!=null && options.IsNewGoods == 1)
                {
                    tempIq = tempIq.Where(s => s.IsNewGoods == 1);
                }
                if(options.IsRecommend!=null && options.IsRecommend == 1)
                {
                    tempIq = tempIq.Where(s => s.IsRecommend == 1);
                }
                if (options.GoodsGroupID != null && options.GoodsGroupID != 0)
                {
                    tempIq = tempIq.Where(s => s.GoodsGroups_ID == options.GoodsGroupID);
                }
                if(options.IsShelves!=null && options.IsShelves == 1)
                {
                    tempIq = tempIq.Where(S => S.IsShelves == 1);
                }
                if (options.IsShelves == null || options.IsShelves == 0)
                {
                    tempIq = tempIq.Where(S => S.IsShelves == 0);
                }
                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.GoodsName.Contains(options.KeyWords)
                      || s.SerialNumber.Contains(options.KeyWords)
                      || s.Describe.Contains(options.KeyWords)
                      || s.GoodsGroups_ID.ToString() == options.KeyWords);
                }
                ///排序              
                switch(options.SortKey)
                {
                    case GoodsSeachOrderByType.CreateTimeAsc:   
                        tempIq = tempIq.OrderBy(s => s.CreateTime);
                        break;
                    case GoodsSeachOrderByType.CreateTimeDesc:
                        tempIq = tempIq.OrderByDescending(s => s.CreateTime);
                        break;
                    case GoodsSeachOrderByType.SpecialOfferAsc:                           
                        tempIq = tempIq.OrderBy(s => s.SpecialOffer);                          
                        break;
                    case GoodsSeachOrderByType.SpecialOfferDesc:
                        tempIq = tempIq.OrderByDescending(s => s.SpecialOffer);
                        break;
                    case GoodsSeachOrderByType.SortAsc:
                        tempIq = tempIq.OrderBy(s => s.Sort);
                        break;
                    case GoodsSeachOrderByType.SortDesc:
                        tempIq = tempIq.OrderByDescending(s => s.Sort);
                        break;
                    default:                          
                        break;
                }
                list.PageCount = await tempIq.CountAsync();
                tempIq = tempIq.Skip(options.Offset).Take(options.Rows);
              
                list.GroodsModel = await tempIq.ToListAsync();
                return list;
            }
            
        }
        
        /// <summary>
        /// 获取商品详细
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        public async Task<GoodsDetailedDto> GetGoodDetailedAync(int GoodsID)
        {
            GoodsDetailedDto GDetailed = new GoodsDetailedDto();
            using (Entities ctx = new Entities())
            {
               var temp= ctx.le_goods_value_mapping
                    .Include(H => H.le_goods)
                    .Include(h => h.le_goods_value);
                temp = temp.Where(s => s.GoodsID == GoodsID);
                var result = await temp.Select(s => new {
                    s.le_goods.GoodsName,
                    s.le_goods.SerialNumber,
                    s.le_goods_value.GoodsValue,
                    s.le_goods.SpecialOffer,
                    s.CategoryType,
                    s.GoodsValueID
                }).ToListAsync();
                GDetailed.SerialNumber =  result[0].SerialNumber;
                GDetailed.GoodsName =  result[0].GoodsName;
                GDetailed.ValuesList = result.Select(s => new GoodsValues { CategoryType = s.CategoryType, Price = s.SpecialOffer, GoodsValueName = s.GoodsValue ,GoodsValueID=s.GoodsValueID})
                    .OrderBy(s=>s.CategoryType)
                    .ToList();
                GDetailed.ImgList = await ctx.le_goods_img.Where(s => s.GoodsID == GoodsID).Select(s => new GoodsImg { Src=s.Src,ID=s.ID}).ToListAsync();
                return GDetailed;
            }
        }

        /// <summary>
        /// 获取所有的商品分类
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public List<GoodsGroupDto> GetGoodsGroupList(string KeyWords="")
        {
            using (Entities ctx = new Entities())
            {
               // ctx.le_
                var tempIq = ctx.le_goodsgroups.Where(s => true).AsNoTracking();
                if (string.IsNullOrEmpty(KeyWords))
                {
                    var reuslt = tempIq.OrderBy(s => s.Sort).Select(s=>new GoodsGroupDto
                    {
                        Sort=s.Sort,
                        ID=s.ID,
                        Level=s.Level,
                        Name=s.Name,
                        ParentID=s.ParentID
                    }).ToList();
                    return reuslt;
                }
                else
                {
                    var result = tempIq.Where(s => s.Name.Contains(KeyWords)).Select(s => new GoodsGroupDto
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
        }
               
        /// <summary>
        /// 增加商品属性
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public int AddGoodsValue(string Name)
        {
            using (Entities ctx = new Entities())
            {
                var exit= ctx.le_goods_value.Where(s => s.GoodsValue == Name).Select(s => s.ID).FirstOrDefault();
                 if(exit!=0)
                {
                    return exit;
                }
                le_goods_value model = new le_goods_value();
                model.GoodsValue = Name;
             
                ctx.le_goods_value.Add(model);
                if(ctx.SaveChanges()>0)
                {
                    return model.ID;
                }
                return 0;
            }
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int AddGoods(AddGoodsDto dto)
        {        
            using (Entities ctx = new Entities())
            {
                le_goods model = new le_goods();
                model.Category1 = dto.Category1;
                model.Category2 = dto.Category2;
                model.Category3 = dto.Category3;
                model.Category4 = dto.Category4;
                model.Category5 = dto.Category5;
                model.Describe = dto.Describe;
                model.GoodsGroups_ID = dto.GoodsGroups_ID;
                model.GoodsName = dto.GoodsName;
                model.SerialNumber = dto.SerialNumber;
                model.SpecialOffer = dto.SpecialOffer;
                model.OriginalPrice = dto.OriginalPrice;
                model.Image = dto.Image;
                model.IsHot = dto.IsHot;
                model.IsNewGoods = dto.IsNewGoods;
                model.IsRecommend = dto.IsRecommend;
                model.IsShelves = dto.IsShelves;
                model.SerialNumber = dto.SerialNumber;
                model.Sort = dto.Sort;
                model.Specifications = dto.Specifications;
                model.UpdateTime = DateTime.Now;
                model.CreateTime = DateTime.Now;
                model.SupplierID = dto.SupplierID;

                ctx.le_goods.Add(model);

                try
                {
                    string msg;
                    if (AddGoodsImage(dto.GoodsImageList, model.GoodsID , out msg))
                    {
                        if (ctx.SaveChanges() > 0)
                        {
                            return model.GoodsID;
                        }
                    }
                    
                    return 0;
                }
                catch (Exception ex)
                {
                    //log.Debug(dto, ex);
                    log.Error(dto,ex);
                    return 0;
                }

            }
        }

        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="List"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddGoodsImage(List<GoodsImage> List,int GoodsID,out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try {
                    if (List.Count <= 0)
                    {
                        msg = "请选择图片信息";
                        return false;
                    }

                    foreach (var eh in List)
                    {
                        le_goods_img model = new le_goods_img();
                        model.GoodsID = GoodsID;
                        model.Src = AttachmentUrl(eh.ImageFlow);

                        ctx.le_goods_img.Add(model);
                    }

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }

                    msg = "新增商品图片失败";
                    return false;
                } catch (Exception ex)
                {
                    msg = "新增商品图片异常，ERROR:"+ ex.ToString();
                    log.Error(List, ex);
                    return false;
                }
            }
        }


        /// <summary>
        /// 图片处理方法
        /// </summary>
        /// <param name="fileNameAttachment">文件流</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileExt">文件格式</param>
        /// <returns></returns>
        public string AttachmentUrl(Byte[] filebyte)
        {
            try
            {
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
                return  null;
            }
        }
    }
}
