using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DataHandlingService
    {
        public bool CleanUpDuplication()
        {
            using (Entities ctx=new Entities())
            {
                var Trans = ctx.Database.BeginTransaction();
                var DuplictionGoodsSql= "drop temporary table if exists tmp_table; CREATE TEMPORARY TABLE tmp_table select goodsname,GoodsID,isshelves from le_goods where goodsname in (select  goodsname from le_goods a group by goodsname having  count(*) > 1 order by goodsname ) and isshelves = 0 and goodsid in (select goodsid from le_orders_lines)";


                string DeleteRelationSql = "delete from le_goods_img where goodsid in (select goodsid from tmp_table );" +
               " delete from le_cart_goodsvalue where GoodsValueID in (select GoodsValueID from  le_goods_value where goodsid in (select goodsid from tmp_table)); " +
               " delete from le_shop_cart where goodsid in (select goodsid from tmp_table ); " +
               "  " +
               " delete from le_goods_suppliers where goodsid  in (select goodsid from tmp_table); " +
               " delete from le_goods_log where goodsid in (select goodsid from tmp_table); ";
                //  " delete from le_goods where goodsid in (select goodsid from tmp_table); ";

                var kkk = ctx.Database.ExecuteSqlCommand(DuplictionGoodsSql);
                var DuplictionGoods = ctx.Database.SqlQuery<Goodsinfo>("select * from tmp_table").ToArray();
               // ctx.Database.ExecuteSqlCommand(DeleteRelationSql);
                foreach (var item in DuplictionGoods)
                {
                    var SelectTrueGoods = ctx.Database.SqlQuery<GoodsGoodsValue>("select a.GoodsID,b.GoodsValueID from le_goods a  inner join le_goods_value b on a.GoodsID = b.goodsid where goodsname = '"+ item .goodsname+ "' and isshelves = 1").FirstOrDefault();
                    if (SelectTrueGoods == null)
                    {
                       //throw new Exception("查询不到正确的商品");
                    }
                    else
                    {
                        var updateOrderLinesql = ctx.Database.ExecuteSqlCommand("update le_orderline_goodsvalue a inner join le_orders_lines b on a.orderlineid=b.orderslinesid set a.GoodsValueid='" + SelectTrueGoods.GoodsValueid + "' ,b.GoodsID='" + SelectTrueGoods.goodsid + "' where b.GoodsID=" + item.goodsid + " and updatetime<'2019-08-20 14:48:52'");
                       // var k = ctx.Database.ExecuteSqlCommand(" delete from le_goods_value where goodsid in (select goodsid from le_goods where goodsid="+item.goodsid+");");
                        
                       // var path= ctx.Database.ExecuteSqlCommand("delete from le_goods where goodsid ="+ item .goodsid+ "");
                    }
                   
                    
                }
               
               // var DeleteGoodsSql = ctx.Database.ExecuteSqlCommand("delete from le_goods where goodsid in( select goodsid from tmp_table)");
                try
                {
                    Trans.Commit();
                    ctx.SaveChanges();
                }
                catch(Exception ex)
                {
                    Trans.Rollback();
                }
              
            }
            return false;
        }

        private class Goodsinfo
        {
            public string goodsid { get; set; }
            public string goodsname { get; set; }
        }
        private class GoodsGoodsValue
        {
            public string goodsid { get; set; }
            public string GoodsValueid { get; set; }
        }
    }
}
