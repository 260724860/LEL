﻿using Common;
using DTO.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LEL.Controllers
{
    /// <summary>
    /// 商城API地址
    /// </summary>
    public class ShoppingMallController : ApiController
    {
        private Service.GoodsService GoodsService = new Service.GoodsService();

        /// <summary>
        /// 获取所有商品分类
        /// </summary>
        /// <param name="KeyWords">搜索关键字</param>
        /// <returns></returns>
        [HttpGet, Route("api/ShoppingMall/GetGoodsGroupList/")]
        public IHttpActionResult  GetGoodsGroupList(string KeyWords = "")
        {
             var result=  GoodsService.GetGoodsGroupList(KeyWords);
             return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        [HttpPost, Route("api/ShoppingMall/GetGoodsList/")]
        public async Task<IHttpActionResult> GetGoodsList([FromBody]GoodsSeachOptions options)
        {
            if(options==null)
            {
                return Json(JRpcHelper.AjaxResult(1, "未接收到有效参数", options));
            }
            var result = await GoodsService.GetGoodsListAsync(options,null);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 获取商品详细
        /// </summary>
        /// <param name="GoodsID">商品ID</param>
        /// <returns></returns>
        [HttpGet, Route("api/ShoppingMall/GetGoodDetailed/")]
        public async Task<IHttpActionResult> GetGoodDetailed(int GoodsID)
        {
            var result = await GoodsService.GetGoodDetailedAync(GoodsID);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
    }
}
