using Common;
using DTO.Goods;
using System.Threading.Tasks;
using System.Web.Http;

namespace LEL.Controllers
{
    /// <summary>
    /// 商城API地址
    /// </summary>
    [Authorize]
    public class ShoppingMallController : BaseApiController
    {
        private Service.GoodsService GoodsService = new Service.GoodsService();

        /// <summary>
        /// 获取所有商品分类
        /// </summary>
        /// <param name="KeyWords">搜索关键字</param>
        /// <returns></returns>
        [HttpGet, Route("api/ShoppingMall/GetGoodsGroupList/")]
        public IHttpActionResult GetGoodsGroupList(string KeyWords = "")
        {
            var result = GoodsService.GetGoodsGroupList(KeyWords);
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
            if (options == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "未接收到有效参数", options));
            }
            string Classify = GetLoginInfo().Classify; 
            
            //string Environment = "";
            //string url= Request.RequestUri.Host.ToString();
            //var SubdomainArrty = url.Split('.');
            //if (SubdomainArrty.Length > 0)
            //{
            //    Environment = SubdomainArrty[0];
            //    if(Environment=="lelshoptest"||Environment== "lelshoptest2"||Environment=="localhost")
            //    {
            //        Environment = "";
            //    }
            //}
            var result = await GoodsService.GetGoodsListAsync(options, Classify);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Classify));
        }

        /// <summary>
        /// 获取商品详细
        /// </summary>
        /// <param name="GoodsID">商品ID</param>
        /// <returns></returns>
        [HttpGet, Route("api/ShoppingMall/GetGoodDetailed/")]
        public async Task<IHttpActionResult> GetGoodDetailed(int GoodsID)
        {
            string Environment = "";
            string url = Request.RequestUri.Host.ToString();
            var SubdomainArrty = url.Split('.');
            if (SubdomainArrty.Length > 0)
            {
                Environment = SubdomainArrty[0];
                if (Environment == "lelshoptest" || Environment == "lelshoptest2")
                {
                    Environment = "";
                }
            }
            var result = await GoodsService.GetGoodDetailedAync(GoodsID, Environment);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
    }
}
