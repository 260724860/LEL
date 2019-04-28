using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DTO.Goods;

namespace Service.Tests
{
    [TestClass()]
    public class GoodsServiceTests
    {
      
        /// <summary>
        /// 获取所有的商品分类
        /// </summary>
        GoodsService GoodsService = new GoodsService();
        [TestMethod()]
        public void GetGoodsGroupListTest()
        {
            var result = GoodsService.GetGoodsGroupList();
            var keyresult = GoodsService.GetGoodsGroupList("酒");
            if (result.Count <= 0 && keyresult.Count <= 0)
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// 增加商品属性
        /// </summary>
        [TestMethod()]
        public void AddGoodsValueTest()
        {
            //int result = GoodsService.AddGoodsValue(RandomUtils.GetNames(1)[0], 10.00m);
            //if (result <= 0)
            //{
            //    Assert.Fail();
            //}
        }

        /// <summary>
        /// 增加商品
        /// </summary>
        [TestMethod()]
        public void AddGoodsTest()
        {
            Random rm = new Random();
            AddGoodsDto model = new AddGoodsDto();
            //model.Category1 = GoodsService.AddGoodsValue("酸", 10.0m).ToString() + "," + GoodsService.AddGoodsValue("甜", 10.0m).ToString()+","+ GoodsService.AddGoodsValue("辣", 10.0m).ToString();
            //model.Category2 = GoodsService.AddGoodsValue("红", 10.0m).ToString() + "," + GoodsService.AddGoodsValue("蓝", 10.0m).ToString() + "," + GoodsService.AddGoodsValue("黄", 10.0m).ToString();
            //model.Category3 = GoodsService.AddGoodsValue("大", 20.0m).ToString() + "," + GoodsService.AddGoodsValue("中", 10.0m).ToString() + "," + GoodsService.AddGoodsValue("小", 10.0m).ToString();
            //model.Describe = string.Join(",",RandomUtils.GenerateChineseWords(50));
            model.GoodsGroups_ID = rm.Next(1, 110);
            model.GoodsName= string.Join(",", RandomUtils.GenerateChineseWords(12));
            model.Image = string.Join(",", RandomUtils.GenerateChineseWords(50));
            model.IsHot = 1;
            model.IsNewGoods = 1;
            model.IsRecommend = 1;
            model.IsShelves = 0;
            model.SerialNumber = RandomUtils.GenerateOutTradeNo("TEST");
            model.Sort = rm.Next(1, 99);
            model.Specifications = "/箱";
            var result= GoodsService.AddGoods(model);
            if(result<=0)
            {
                Assert.Fail();
            }
           
        }

       
    }
}