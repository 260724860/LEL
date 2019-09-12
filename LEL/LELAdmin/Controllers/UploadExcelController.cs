using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    [Authorize]
    public class UploadExcelController : ApiController
    {
        UploadFileService UploadFileBLL = new UploadFileService();
        /// <summary>
        /// 批量导入商品数据 支持格式.xlsx
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/UploadExcel/UploadExcel/")]
        public IHttpActionResult UploadExcel()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //string serverPath = System.Configuration.ConfigurationSettings.AppSettings["serverPath"];
            //string localPath = System.Configuration.ConfigurationSettings.AppSettings["localPath"];
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                System.Web.HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型
                    string fileFilt = ".xlsx";
                    if (fileFilt.IndexOf(fileExt) <= -1)
                    {
                        return Json(JRpcHelper.AjaxResult(1, "上传文件类型错误,允许上传格式.xlsx", null));
                    }
                    DateTime dt = DateTime.Now;
                    string folderPath = "UploadFile\\";
                    string serverFolderPath = "/Upload/File";
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("/") + folderPath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("/") + folderPath);
                    }
                    string fileName = RandomUtils.GenerateOutTradeNo("Excel") + Path.GetExtension(hpf.FileName);
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("/") + folderPath + fileName;
                    hpf.SaveAs(filePath);

                    return Json(JRpcHelper.AjaxResult(0, "上传成功", System.Web.HttpContext.Current.Server.MapPath("/") + serverFolderPath + fileName));

                }
            }
            return Json(JRpcHelper.AjaxResult(1, "没有获取到文件", null));
        }
        /// <summary>
        /// 上传批量
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/UploadExcel/DataReaderExcelFile/")]
        public IHttpActionResult DataReaderExcelFile()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //string serverPath = System.Configuration.ConfigurationSettings.AppSettings["serverPath"];
            //string localPath = System.Configuration.ConfigurationSettings.AppSettings["localPath"];
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                System.Web.HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型
                    string fileFilt = ".xlsx";
                    if (fileFilt.IndexOf(fileExt) <= -1)
                    {
                        return Json(JRpcHelper.AjaxResult(1, "上传文件类型错误,允许上传格式.xlsx", null));
                    }
                    DateTime dt = DateTime.Now;
                    string folderPath = "UploadFile\\Import\\";
                    string serverFolderPath = "/Upload/Import/";
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("/") + folderPath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("/") + folderPath);
                    }
                    string fileName = RandomUtils.GenerateOutTradeNo("Excel") + Path.GetExtension(hpf.FileName);
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("/") + folderPath + fileName;
                    hpf.SaveAs(filePath);
                    // string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/1.xlsx";
                    var ImportData = UploadFileBLL.InsertGoodsBaseInfo(filePath, out string Msg);
                    if (ImportData)
                    {
                        return Json(JRpcHelper.AjaxResult(0, "上传成功", null));
                    }
                    else
                    {
                        return Json(JRpcHelper.AjaxResult(1, Msg, null));
                    }
                }
                return Json(JRpcHelper.AjaxResult(1, "没有获取到文件", null));
            }
            return Json(JRpcHelper.AjaxResult(1, "没有获取到文件", null));
        }

        /// <summary>
        /// 商品信息导出Excel 下载模板
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("api/UploadExcel/ExportExcle/")]
        public IHttpActionResult ExportExcle()
        {
            try
            {
                DataTable GoodsSource = new DataTable();
                //DataTable Goods
                string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/商品批量导入格式.xlsx";
                string FileName = RandomUtils.GenerateOutTradeNo("Excel") + ".xlsx";
                string SavPath = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/export/" + FileName;

                DataColumn[] myColumns = { new DataColumn("Status"), new DataColumn("ErrorMessage"), new DataColumn("Index") };
                UploadFileService service = new UploadFileService();
                List<DtoImportExcel> DtList = new List<DtoImportExcel>();
                var SupplierData = service.ExportSupplierDt(path);

                DtList.Add(new DtoImportExcel { dt = SupplierData, SheetNmae = "供应商表" });
                var GoodsGroup = service.ExportGoodsGroupDt(path);

                DtList.Add(new DtoImportExcel { dt = GoodsGroup, SheetNmae = "商品分类表" });
                ExcelHelper.DataTableToExcel(path, DtList, true, SavPath);
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", "/UploadFile/export/" + FileName));
            }
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }

        }

        /// <summary>
        /// 商品供应商价格格式导出Excel 下单模板
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet, Route("api/UploadExcel/ExportGoodsSupplierPrice/")]
        public IHttpActionResult ExportGoodsSupplierPrice()
        {
            try
            {
                UploadFileService service = new UploadFileService();

                string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/商品供应价格批量上传格式.xlsx";
                string FileName = RandomUtils.GenerateOutTradeNo("GoodsSupplierPrice") + ".xlsx";
                string SavPath = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/export/" + FileName;

                List<DtoImportExcel> DtList = new List<DtoImportExcel>();
                var SupplierData = service.ExportSupplierDt(path);

                DtList.Add(new DtoImportExcel { dt = SupplierData, SheetNmae = "供应商表" });
                var GoodsInfo = service.ExportGoodsInfo();

                DtList.Add(new DtoImportExcel { dt = GoodsInfo, SheetNmae = "商品信息" });
                ExcelHelper.DataTableToExcel(path, DtList, true, SavPath);
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", "/UploadFile/export/" + FileName));
            }
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }

        }

        /// <summary>
        /// 商品供应商价格格式导入
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/UploadExcel/ImportGoodsSupplierPrice/")]
        public IHttpActionResult ImportGoodsSupplierPrice()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //string serverPath = System.Configuration.ConfigurationSettings.AppSettings["serverPath"];
            //string localPath = System.Configuration.ConfigurationSettings.AppSettings["localPath"];
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                System.Web.HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型
                    string fileFilt = ".xlsx";
                    if (fileFilt.IndexOf(fileExt) <= -1)
                    {
                        return Json(JRpcHelper.AjaxResult(1, "上传文件类型错误,允许上传格式.xlsx", null));
                    }
                    DateTime dt = DateTime.Now;
                    string folderPath = "UploadFile\\Import\\";
                    string serverFolderPath = "/Upload/Import/";
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("/") + folderPath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("/") + folderPath);
                    }
                    string fileName = RandomUtils.GenerateOutTradeNo("GoodsSupplierPrice") + Path.GetExtension(hpf.FileName);
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("/") + folderPath + fileName;
                    hpf.SaveAs(filePath);
                    // string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/1.xlsx";
                    var ImportData = UploadFileBLL.GoodsSupplierPriceInsertDb(filePath, out string Msg);
                    if (ImportData)
                    {
                        return Json(JRpcHelper.AjaxResult(0, "上传成功", null));
                    }
                    else
                    {
                        return Json(JRpcHelper.AjaxResult(1, Msg, null));
                    }
                }
                return Json(JRpcHelper.AjaxResult(1, "没有获取到文件", null));
            }
            return Json(JRpcHelper.AjaxResult(1, "没有获取到文件", null));
        }


        [HttpPost, Route("api/UploadExcel/gess/")]
        public void gess()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/商品录入格式.xlsx";
            // ExcelHelper.addExcelData(path,0,null);
        }
        [AllowAnonymous]
        [HttpPost, Route("api/UploadExcel/Test/")]
        public void Test()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/汉初文化资料.xlsx";
            var GoodsDT = ExcelHelper.DataReaderExcelFile(path, "sheet1");
            List<string> result = new List<string>();
            using (Entities ctx = new Entities())
                
            {
                //var trans=  ctx.Database.BeginTransaction();
                //for (int i = 0; i < GoodsDT.Rows.Count; i++)
                //{
                //    // result.Add(GoodsDT.Rows[i]["商品名称"].ToString());
                //    string sql = "update le_goods set SpecialOffer=" + GoodsDT.Rows[i]["平台供价"] + " where GoodsName='" + GoodsDT.Rows[i]["商品名称"] + "' ";
                   
                //    ctx.Database.ExecuteSqlCommand(sql);
                //}
                //try
                //{
                //    trans.Commit();
                //    ctx.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    trans.Rollback();
                //}
            }
        //}
          //  string sql = "select * from le_goods_value where serialnumber in ("+string.Join(",", result) +")";
        }
        [AllowAnonymous]
        [HttpPost, Route("api/UploadExcel/UpdateGoodsInfoByExcel/")]
        public IHttpActionResult UpdateGoodsInfoByExcel(string FileName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/"+ FileName + ".xlsx";
            new UploadFileService().UpdateGoodsInfoByExcel(path, out string Msg);
            return null;
        }
    }
}
