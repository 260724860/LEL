webpackJsonp([6],{"D+bJ":function(t,e){},qFXM:function(t,e,s){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var r=s("Xxa5"),i=s.n(r),n=s("exGp"),a=s.n(n),o=s("Dd8w"),c=s.n(o),l=s("NYxO"),d={name:"MyorderDetails",data:function(){return{columns:[{title:"图片",key:"img",width:80,render:function(t,e){return t("div",[t("img",{attrs:{src:e.row.GoodsImg,width:40,height:40}})])},align:"center"},{type:"html",title:"条码",width:250,key:"SerialNumber",align:"center"},{title:"商品",key:"GoodsName",align:"center"},{title:"属性",width:350,key:"specs",align:"center"},{title:"下单数量",width:100,key:"GoodsCount",align:"center"},{title:"实发数量",width:100,key:"DeliverCount",align:"center"},{title:"金额",width:150,key:"price",align:"center"},{title:"小计",width:150,key:"totalPrice",align:"center"}]}},computed:c()({},Object(l.d)(["orderDetails"]),{totalCount:{get:function(){var t=0;return this.orderDetails.content.forEach(function(e){t+=e.DeliverCount}),t}},totalPrice:{get:function(){var t=0;return this.orderDetails.content.forEach(function(e){t+=e.DeliverCount*e.SpecialOffer}),t}}}),methods:{goBack:function(){this.$parent.hideOrderDetails()}}},u={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",[s("div",{staticClass:"DetailsNav"},[s("i",{staticClass:"el-icon-back",on:{click:t.goBack}})]),t._v(" "),s("div",{staticStyle:{"font-size":"14px","margin-bottom":"10px"}},[s("span",{staticStyle:{"font-size":"15px","font-weight":"bold"}},[t._v("订单编号：")]),t._v(t._s(t.orderDetails.OrderNO)+"\n\t")]),t._v(" "),t.orderDetails.expressType?s("div",{staticStyle:{"font-size":"14px","margin-bottom":"10px"}},[s("span",{staticStyle:{"font-size":"15px","font-weight":"bold"}},[t._v("取货方式：")]),t._v(t._s(t.orderDetails.expressType)+"\n\t")]):t._e(),t._v(" "),t.orderDetails.expressType?s("div",{staticStyle:{"font-size":"14px","margin-bottom":"10px"}},[s("span",{staticStyle:{"font-size":"15px","font-weight":"bold"}},[t._v("取货时间：")]),t._v(t._s(t.orderDetails.pickUpTime)+"\n\t")]):t._e(),t._v(" "),s("div",{staticStyle:{"font-size":"14px","margin-bottom":"10px"}},[s("span",{staticStyle:{"font-size":"15px","font-weight":"bold"}},[t._v("地址：")]),t._v(t._s(t.orderDetails.address)+"\n\t")]),t._v(" "),s("div",{staticStyle:{"font-size":"14px","margin-bottom":"10px"}},[s("span",{staticStyle:{"font-size":"15px","font-weight":"bold"}},[t._v("备注信息：")]),t._v(t._s(t.orderDetails.remark)+"\n\t")]),t._v(" "),s("Table",{attrs:{border:"",columns:t.columns,data:t.orderDetails.content,size:"large"}}),t._v(" "),s("div",{staticClass:"go-to"},[s("span",{staticStyle:{"font-size":"16px","margin-right":"30px"}},[t._v("商品总量："+t._s(t.totalCount))]),t._v(" "),s("span",{staticStyle:{"font-size":"16px"}},[t._v("商品总价：￥"+t._s(t.totalPrice))])])],1)},staticRenderFns:[]};var h={name:"myOrder",components:{MyorderDetails:s("VU/8")(d,u,!1,function(t){s("wh4V")},"data-v-4ef5ca3c",null).exports},data:function(){var t=this;return{pageID:1,isDetailsShow:!1,columns:[{title:"订单号",key:"Out_Trade_No",width:250,align:"center"},{title:"商品",key:"title",align:"center"},{title:"金额",width:150,key:"Money",align:"center"},{title:"下单时间",width:200,key:"CreateTime",align:"center"},{title:"订单类型",width:150,key:"OrderType",align:"center"},{title:"操作",key:"readMore",align:"center",width:200,render:function(e,s){return 0===t.state&&0===t.switchxz?e("div",[e("Button",{props:{type:"primary"},style:{marginRight:"5px"},on:{click:function(){t.showOrderDetails(s.row)}}},"详情"),e("Button",{props:{type:"error"},on:{click:function(){t.cancelOrder(s.row)}}},"取消")]):e("div",[e("Button",{props:{type:"primary"},style:{marginRight:"5px"},on:{click:function(){t.showOrderDetails(s.row)}}},"详情")])}}],state:0,orders:[[{content:"未处理",status:0},{content:"待接单",status:3},{content:"已接单",status:4}],[{content:"已完成",status:1},{content:"已取消",status:5}]],switchxz:0}},beforeRouteUpdate:function(t,e,s){s(),this.state=this.$route.params.state-1,this.switchxz=0,this.pageID=1,this.isDetailsShow=!1,this.loadOrderResult([this.pageID-1,this.orders[this.state][this.switchxz].status,this.userInfo.accattach.UserID])},computed:c()({},Object(l.d)(["orderResult"])),mounted:function(){this.userInfo=JSON.parse(localStorage.getItem("userInfo")),this.pageID=1,this.loadOrderResult([this.pageID-1,this.orders[this.state][this.switchxz].status,this.userInfo.accattach.UserID])},methods:c()({},Object(l.b)(["loadOrderResult","loadOrderDetails"]),{changePage:function(t){this.pageID=t,this.loadOrderResult([t-1,this.orders[this.state][this.switchxz].status,this.userInfo.accattach.UserID])},switchTab:function(t){this.pageID=1,this.switchxz=t,this.loadOrderResult([this.pageID-1,this.orders[this.state][this.switchxz].status,this.userInfo.accattach.UserID])},showOrderDetails:function(t){var e=this;return a()(i.a.mark(function s(){var r,n,a;return i.a.wrap(function(s){for(;;)switch(s.prev=s.next){case 0:r=t.RcName+" "+t.RcPhone+" "+t.RcAddr,n=void 0,n="订货单"===t.OrderType?1===t.ExpressType?"快递物流":"自提":null,(a=t.PickupTime||"")&&(a=a.replace("T"," ")),e.loadOrderDetails([t.Out_Trade_No,t.remark,r,n,a]),e.isDetailsShow=!0;case 7:case"end":return s.stop()}},s,e)}))()},hideOrderDetails:function(){var t=this;return a()(i.a.mark(function e(){return i.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:t.isDetailsShow=!1;case 1:case"end":return e.stop()}},e,t)}))()},cancelOrder:function(t){var e,s=this;this.$Modal.confirm({title:"提醒",content:"确定取消这个订单？",onOk:(e=a()(i.a.mark(function e(){return i.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,s.$request.cancelOrder(t.Out_Trade_No);case 2:e.sent&&(s.pageID=1,s.loadOrderResult([s.pageID-1,s.orders[s.state][s.switchxz].status,s.userInfo.accattach.UserID]),s.$Message.success("取消订单成功"));case 4:case"end":return e.stop()}},e,s)})),function(){return e.apply(this,arguments)}),onCancel:function(){s.$Message.info("订单未取消")}})}})},p={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",[t.isDetailsShow?s("MyorderDetails"):s("div",[s("div",{staticClass:"MyOrder_header"},t._l(t.orders[t.state],function(e,r){return s("div",{key:r,class:{xz:r===t.switchxz},on:{click:function(e){return t.switchTab(r)}}},[t._v(t._s(e.content)+"\n\t\t\t")])}),0),t._v(" "),s("Table",{attrs:{border:"",columns:t.columns,data:t.orderResult.content,size:"large","no-data-text":"你还没有订单，快点去购物吧"}}),t._v(" "),s("div",{staticClass:"page-size"},[s("Page",{attrs:{total:t.orderResult.accattach,"page-size":10,current:t.pageID,"show-total":""},on:{"update:current":function(e){t.pageID=e},"on-change":t.changePage}})],1)],1)],1)},staticRenderFns:[]};var f=s("VU/8")(h,p,!1,function(t){s("D+bJ")},"data-v-1c64d136",null);e.default=f.exports},wh4V:function(t,e){}});
//# sourceMappingURL=6.ded7a6ceee47ce2171ff.js.map