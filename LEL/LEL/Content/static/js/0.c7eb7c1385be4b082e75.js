webpackJsonp([0],{B7tt:function(t,e,s){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var i=s("Dd8w"),a=s.n(i),o=s("Xxa5"),n=s.n(o),r=s("exGp"),c=s.n(r),l=s("/KFX"),d=s("bYoP"),u=s("NYxO"),p={name:"ShowGoods",data:function(){return{count:1,maxCount:1e4,imgIndex:0,selectString:[],selectIndex:[],specs:["味道","颜色","尺寸"],serialNumbers:[],serialString:""}},created:function(){var t=this;return c()(n.a.mark(function e(){return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,t.loadGoodsInfo(t.$route.query.id);case 2:t.initSelectString(),t.maxCount=t.goodsInfo.Quota>0?t.goodsInfo.Quota:t.maxCount,t.maxCount=Math.min(t.maxCount,t.goodsInfo.Stock);case 5:case"end":return e.stop()}},e,t)}))()},computed:a()({},Object(u.d)(["goodsInfo","shoppingCart"])),methods:a()({},Object(u.b)(["loadGoodsInfo","loadShoppingCart"]),{initSelectString:function(){for(var t=0;t<this.goodsInfo.ValuesList.length;t++)this.selectString.push(this.goodsInfo.ValuesList[t][0].GoodsValueName),this.serialNumbers.push(this.goodsInfo.ValuesList[t][0].SerialNumber),this.selectIndex[t]=this.goodsInfo.ValuesList[t][0].GoodsValueID;this.serialString=this.serialNumbers.join("-")},select:function(t){var e=this;this.goodsInfo.ValuesList[t].forEach(function(s){s.GoodsValueName===e.selectString[t]&&(e.selectIndex[t]=s.GoodsValueID,e.serialNumbers[t]=s.SerialNumber)}),this.serialString=this.serialNumbers.join("-")},showBigImg:function(t){this.imgIndex=t},addShoppingCartBtn:function(){var t=this;return c()(n.a.mark(function e(){var s,i,a;return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,t.loadShoppingCart();case 2:s=t.selectString.join("-"),i=!1,a=0;case 5:if(!(a<t.shoppingCart.length)){e.next=12;break}if(parseInt(t.shoppingCart[a].GoodsID)!==parseInt(t.goodsInfo.goodsID)||t.shoppingCart[a].specs!==s){e.next=9;break}return i=!0,e.abrupt("break",12);case 9:a++,e.next=5;break;case 12:if(!i){e.next=16;break}t.$Modal.confirm({title:"提示",content:"该商品已在购物车中，是否继续添加？",onOk:function(){var e=c()(n.a.mark(function e(){return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,t.addToCart();case 2:case"end":return e.stop()}},e,t)}));return function(){return e.apply(this,arguments)}}(),onCancel:function(){}}),e.next=18;break;case 16:return e.next=18,t.addToCart();case 18:case"end":return e.stop()}},e,t)}))()},addToCart:function(){var t=this;return c()(n.a.mark(function e(){var s,i,a;return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:for(s=[],i=0;i<t.selectIndex.length;i++)a={CategoryType:i+1,GoodsValueID:t.selectIndex[i]},s.push(a);return e.next=4,t.$request.addCart(t.goodsInfo.goodsID,t.count,s);case 4:e.sent&&t.$Message.success("加入购物车成功");case 6:case"end":return e.stop()}},e,t)}))()}})},v={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",[s("div",{staticClass:"item-detail-show"},[s("div",{staticClass:"item-detail-left"},[s("div",{staticClass:"item-detail-big-img"},[s("img",{attrs:{src:t.goodsInfo.ImgList[t.imgIndex].Src,alt:""}})]),t._v(" "),s("div",{staticClass:"item-detail-img-row"},t._l(t.goodsInfo.ImgList,function(e,i){return s("div",{key:i,staticClass:"item-detail-img-small",on:{mouseover:function(e){return t.showBigImg(i)}}},[s("img",{attrs:{src:e.Src,alt:""}})])}),0)]),t._v(" "),s("div",{staticClass:"item-detail-right"},[s("div",{staticClass:"item-detail-title"},[s("p",[t._v(t._s(t.goodsInfo.GoodsName))])]),t._v(" "),s("div",{staticClass:"item-detail-price-row"},[s("div",{staticClass:"item-price-left"},[s("div",{staticClass:"item-price-row"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("单价")]),t._v(" "),s("span",{staticClass:"item-price"},[t._v("￥"+t._s(t.goodsInfo.SpecialOffer.toFixed(2)))])])])])]),t._v(" "),s("div",{staticClass:"item-select"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("建议零售价")]),t._v(" "),s("span",{staticClass:"item-price-title"},[t._v("￥"+t._s(t.goodsInfo.OriginalPrice.toFixed(2)))])])]),t._v(" "),s("div",{staticClass:"item-select"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("条码")]),t._v(" "),s("span",{staticClass:"item-price-title"},[t._v(t._s(t.serialString))])])]),t._v(" "),s("div",{staticClass:"item-select"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("规格")]),t._v(" "),s("span",{staticClass:"item-price-title"},[t._v(t._s(t.goodsInfo.Specifications))])])]),t._v(" "),s("div",{staticClass:"item-select"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("保质期")]),t._v(" "),s("span",{staticClass:"item-price-title"},[t._v(t._s(t.goodsInfo.ShelfLife)+" 月")])])]),t._v(" "),s("div",{staticClass:"item-select"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("装箱数")]),t._v(" "),s("span",{staticClass:"item-price-title"},[t._v(t._s(t.goodsInfo.PackingNumber))])])]),t._v(" "),s("div",{staticClass:"item-select"},[s("p",[s("span",{staticClass:"item-price-title"},[t._v("整件价")]),t._v(" "),s("span",{staticClass:"item-price-title"},[t._v("￥"+t._s(t.goodsInfo.singlePrice))])])]),t._v(" "),t._l(t.goodsInfo.ValuesList,function(e,i){return s("div",{key:i,staticClass:"item-select"},[s("div",{staticClass:"item-select-title"},[s("p",[t._v(t._s(t.specs[t.goodsInfo.ValuesList[i][0].CategoryType-1]))])]),t._v(" "),s("div",{staticClass:"item-select-column"},[s("div",{staticClass:"item-select-row"},[s("RadioGroup",{attrs:{type:"button"},on:{"on-change":function(e){return t.select(i)}},model:{value:t.selectString[i],callback:function(e){t.$set(t.selectString,i,e)},expression:"selectString[index]"}},t._l(e,function(t,e){return s("Radio",{key:e,attrs:{label:t.GoodsValueName}})}),1)],1)])])}),t._v(" "),s("br"),t._v(" "),s("div",{staticClass:"add-buy-car-box"},[s("div",{staticClass:"add-buy-car"},[s("InputNumber",{attrs:{min:1,max:t.maxCount,size:"large"},model:{value:t.count,callback:function(e){t.count=e},expression:"count"}}),t._v(" "),s("Button",{attrs:{type:"error",size:"large",disabled:t.goodsInfo.Stock<=0},on:{click:t.addShoppingCartBtn}},[t._v("加入购物车")]),t._v(" "),-1!==t.goodsInfo.Quota?s("span",{staticStyle:{"margin-left":"15px","font-size":"14px"}},[t._v("限购："+t._s(t.goodsInfo.Quota)+"件")]):t._e(),t._v(" "),s("span",{staticStyle:{"margin-left":"15px","font-size":"14px"}},[t._v("库存："+t._s(t.goodsInfo.Stock)+"件")])],1)])],2)])])},staticRenderFns:[]};var m=s("VU/8")(p,v,!1,function(t){s("QBhl")},"data-v-d948f83c",null).exports,g={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",[t._m(0),t._v(" "),s("div",{staticClass:"item-protect-container"},[s("div",{staticClass:"item-protect-box"},[s("p",{staticClass:"item-protect-title-box"},[s("Avatar",{staticStyle:{"background-color":"#e4393c"},attrs:{icon:"ios-ribbon"}}),t._v(" "),s("span",{staticClass:"item-protect-title"},[t._v("卖家服务")])],1),t._v(" "),s("p",{staticClass:"item-protect-detail"},[t._v("\n\t\t\t\t高品质敢承诺：7天无理由退货，30天免费换新，质量问题商家承担来回运费换新；如需发票，请在确认收货无误后联系商家开出。（注*发票不随货品一同发出）\n\t\t\t")])]),t._v(" "),s("div",{staticClass:"item-protect-box"},[s("p",{staticClass:"item-protect-title-box"},[s("Avatar",{staticStyle:{"background-color":"#e4393c"},attrs:{icon:"ios-cash"}}),t._v(" "),s("span",{staticClass:"item-protect-title"},[t._v("平台承诺")])],1),t._v(" "),t._m(1)]),t._v(" "),s("div",{staticClass:"item-protect-box"},[s("p",{staticClass:"item-protect-title-box"},[s("Avatar",{staticStyle:{"background-color":"#e4393c"},attrs:{icon:"ios-lock"}}),t._v(" "),s("span",{staticClass:"item-protect-title"},[t._v("正品行货")])],1),t._v(" "),s("p",{staticClass:"item-protect-detail"},[t._v("\n\t\t\t\tBIT商城向您保证所售商品均为正品行货，BIT自营商品开具机打发票或电子发票。\n\t\t\t")])]),t._v(" "),s("div",{staticClass:"item-protect-box"},[s("p",{staticClass:"item-protect-title-box"},[s("Avatar",{staticStyle:{"background-color":"#e4393c"},attrs:{icon:"ios-hammer"}}),t._v(" "),s("span",{staticClass:"item-protect-title"},[t._v("全国联保")])],1),t._v(" "),t._m(2)])])])},staticRenderFns:[function(){var t=this.$createElement,e=this._self._c||t;return e("div",{staticClass:"remarks-title"},[e("span",[this._v("售后保障")])])},function(){var t=this.$createElement,e=this._self._c||t;return e("p",{staticClass:"item-protect-detail"},[this._v("\n\t\t\t\t平台卖家销售并发货的商品，由平台卖家提供发票和相应的售后服务。请您放心购买！"),e("br"),this._v("\n\t\t\t\t注：因厂家会在没有任何提前通知的情况下更改产品包装、产地或者一些附件，本司不能确保客户收到的货物与商城图片、产地、附件说明完全一致。只能确保为原厂正货！并且保证与当时市场上同样主流新品一致。若本商城没有及时更新，请大家谅解！\n\t\t\t")])},function(){var t=this.$createElement,e=this._self._c||t;return e("p",{staticClass:"item-protect-detail"},[this._v("\n\t\t\t\t凭质保证书及BIT商城发票，可享受全国联保服务（奢侈品、钟表除外；奢侈品、钟表由BIT联系保修，享受法定三包售后服务），与您亲临商场选购的商品享受相同的质量保证。BIT商城还为您提供具有竞争力的商品价格和运费政策，请您放心购买！"),e("br"),e("br"),this._v("\n\t\t\t\t注：因厂家会在没有任何提前通知的情况下更改产品包装、产地或者一些附件，本司不能确保客户收到的货物与商城图片、产地、附件说明完全一致。只能确保为原厂正货！并且保证与当时市场上同样主流新品一致。若本商城没有及时更新，请大家谅解！\n\t\t\t")])}]};var f={name:"ShowGoodsDetail",data:function(){return{tagsColor:["blue","green","red","yellow"]}},components:{ShowProductWarranty:s("VU/8")({name:"ShowProductWarranty"},g,!1,function(t){s("JzRw")},"data-v-1c1438f9",null).exports},computed:a()({},Object(u.d)(["goodsInfo"])),methods:{changeHeight:function(){var t=window.getComputedStyle(this.$refs.itemIntroGoods).height;t=parseInt(t.substr(0,t.length-2))+89,this.$refs.itemIntroDetail.style.height=t+"px"}},updated:function(){var t=this;this.$nextTick(function(){setTimeout(t.changeHeight,100),setTimeout(t.changeHeight,1e3),setTimeout(t.changeHeight,3e3),setTimeout(t.changeHeight,5e3),setTimeout(t.changeHeight,1e4),setTimeout(t.changeHeight,15e3),setTimeout(t.changeHeight,2e4),setTimeout(t.changeHeight,25e3),setTimeout(t.changeHeight,3e4),setTimeout(t.changeHeight,5e4)})}},h={render:function(){var t=this.$createElement,e=this._self._c||t;return e("div",[e("div",{staticClass:"item-intro-show"},[e("div",{ref:"itemIntroDetail",staticClass:"item-intro-detail"},[e("div",{staticClass:"item-intro-nav item-tabs"},[e("Tabs",[e("TabPane",{attrs:{label:"商品介绍"}},[e("div",{staticClass:"item-param-container"},[e("span",{staticClass:"item-param-content",staticStyle:{"font-size":"14px","padding-left":"30px",padding:"5px"}},[this._v(this._s(this.goodsInfo.Describe))])]),this._v(" "),e("div",{ref:"itemIntroGoods",staticClass:"item-intro-img"},this._l(this.goodsInfo.ImgList,function(t,s){return e("img",{key:s,attrs:{src:t.Src}})}),0)])],1)],1)])])])},staticRenderFns:[]};var _=s("VU/8")(f,h,!1,function(t){s("VaXF"),s("pV74")},"data-v-9d330790",null).exports,C={name:"GoodsDetail",components:{Search:l.a,ShowGoods:m,ShowGoodsDetail:_,Footer:d.a},data:function(){return{level_1:"",level_2:""}},created:function(){var t=this;return c()(n.a.mark(function e(){return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,t.loadGoodsInfo(t.$route.query.id);case 2:t.initCategory();case 3:case"end":return e.stop()}},e,t)}))()},beforeRouteEnter:function(t,e,s){window.scrollTo(0,0),s()},computed:a()({},Object(u.d)(["goodsInfo","goodsGroupOrigin"])),methods:a()({},Object(u.b)(["loadGoodsInfo"]),{initCategory:function(){for(var t=this.goodsInfo.GoodsGroups_ID,e=0;e<this.goodsGroupOrigin.length;e++)if(this.goodsGroupOrigin[e].ID===t){0===this.goodsGroupOrigin[e].ParentID?(this.level_1=this.goodsGroupOrigin[e].Name,t=null):(this.level_2=this.goodsGroupOrigin[e].Name,t=this.goodsGroupOrigin[e].ParentID);break}if(t)for(var s=0;s<this.goodsGroupOrigin.length;s++)if(this.goodsGroupOrigin[s].ID===t){this.level_1=this.goodsGroupOrigin[s].Name;break}}})},I={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",[s("Search"),t._v(" "),s("div",{staticClass:"shop-item-path"},[s("div",{staticClass:"shop-nav-container"},[s("Breadcrumb",[s("BreadcrumbItem",{attrs:{to:"/"}},[t._v("首页")]),t._v(" "),s("BreadcrumbItem",[t._v(t._s(t.level_1))]),t._v(" "),t.level_2?s("BreadcrumbItem",[t._v(t._s(t.level_2))]):t._e()],1)],1)]),t._v(" "),s("ShowGoods"),t._v(" "),s("ShowGoodsDetail"),t._v(" "),s("Footer")],1)},staticRenderFns:[]};var b=s("VU/8")(C,I,!1,function(t){s("GCdJ")},"data-v-29892894",null);e.default=b.exports},GCdJ:function(t,e){},JzRw:function(t,e){},QBhl:function(t,e){},VaXF:function(t,e){},pV74:function(t,e){}});
//# sourceMappingURL=0.c7eb7c1385be4b082e75.js.map