webpackJsonp([2],{"886d":function(t,s){},nJow:function(t,s,e){t.exports=e.p+"static/img/notfound.a4473d1.png"},zV51:function(t,s,e){"use strict";Object.defineProperty(s,"__esModule",{value:!0});var o=e("Dd8w"),a=e.n(o),i=e("/KFX"),c=e("bYoP"),n=e("NYxO"),r={name:"GoodsList",components:{Search:i.a,Footer:c.a},data:function(){return{isAction:[!0,!1,!1],isASC:[!0,!0,!0],sortTag:["上架时间","价格","综合"]}},created:function(){},mounted:function(){this.SET_SEARCH_PAGE(1),this.SET_GOODS_ORDER_BY(1)},beforeRouteEnter:function(t,s,e){window.scrollTo(0,0),e()},computed:a()({},Object(n.d)(["searchResult","searchKey","sortKey","searchCategory","searchPage"])),methods:a()({},Object(n.b)(["loadSearchResult"]),Object(n.c)(["SET_GOODS_ORDER_BY","SET_SEARCH_PAGE"]),{orderBy:function(t){var s=void 0;this.isAction[t]?(s=this.sortKey%2>0?this.sortKey+1:this.sortKey-1,this.isASC[t]=s%2>0):(this.isAction=[!1,!1,!1],this.isAction[t]=!0,s=2*t+1),this.SET_SEARCH_PAGE(1),this.SET_GOODS_ORDER_BY(s),this.loadSearchResult([this.searchKey,0,this.searchCategory])},changePage:function(t){this.SET_SEARCH_PAGE(t),this.loadSearchResult([this.searchKey,t-1,this.searchCategory])}})},d={render:function(){var t=this,s=t.$createElement,e=t._self._c||s;return e("div",[e("Search"),t._v(" "),e("div",{staticClass:"container"},[e("div",{staticClass:"bread-crumb"},[e("Breadcrumb",[e("BreadcrumbItem",{attrs:{to:"/"}},[e("Icon",{attrs:{type:"ios-home-outline"}}),t._v("\n\t\t\t\t\t首页\n\t\t\t\t")],1),t._v(" "),e("BreadcrumbItem",[e("Icon",{attrs:{type:"ios-search-outline"}}),t._v("\n\t\t\t\t\t"+t._s(t.searchKey)+"\n\t\t\t\t")],1)],1)],1),t._v(" "),e("div",{staticClass:"goods-box"},[e("div",{staticClass:"goods-list-box"},[e("div",{staticClass:"goods-list-tool"},[e("ul",t._l(t.sortTag,function(s,o){return e("li",{key:o,on:{click:function(s){return t.orderBy(o)}}},[e("span",{class:{"goods-list-tool-active":t.isAction[o]}},[t._v(t._s(s))])])}),0)]),t._v(" "),e("div",{staticClass:"goods-list"},[0===t.searchResult.GoodsModel.length?e("div",{staticClass:"product-not-found-container"},[t._m(0),t._v(" "),e("div",{staticClass:"product-not-found-txt"},[t._v("未找到相关商品")])]):t._e(),t._v(" "),t._l(t.searchResult.GoodsModel,function(s,o){return e("div",{key:o,staticClass:"goods-show-info"},[e("router-link",{attrs:{to:"/goodsDetail?id="+s.GoodsID}},[e("div",{staticClass:"goods-show-img"},[e("img",{attrs:{src:s.Image,width:"220",height:"220"}})]),t._v(" "),e("div",{staticClass:"goods-show-price"},[e("span",[e("span",{staticClass:"seckill-price-icon text-danger"},[t._v("￥")]),t._v(" "),e("span",{staticClass:"seckill-price text-danger"},[t._v(t._s(s.SpecialOffer))])])]),t._v(" "),e("div",{staticClass:"goods-show-detail"},[e("span",[t._v(t._s(s.GoodsName))])]),t._v(" "),e("div",{staticStyle:{"font-size":"12px",display:"flex","justify-content":"flex-end",color:"black"}},[e("span",{staticStyle:{"margin-right":"20px"}},[t._v("月售："+t._s(s.SalesVolumes))]),t._v(" "),e("span",[t._v("总售："+t._s(s.TotalSalesVolumes))])])])],1)})],2)])]),t._v(" "),e("div",{staticClass:"goods-page"},[e("Page",{attrs:{total:t.searchResult.PageCount,"page-size":24,current:t.searchPage,"show-total":""},on:{"update:current":function(s){t.searchPage=s},"on-change":t.changePage}})],1)]),t._v(" "),e("Footer")],1)},staticRenderFns:[function(){var t=this.$createElement,s=this._self._c||t;return s("div",{staticClass:"product-not-found-img"},[s("img",{attrs:{src:e("nJow"),width:"200",height:"200"}})])}]};var l=e("VU/8")(r,d,!1,function(t){e("886d")},"data-v-591a8553",null);s.default=l.exports}});
//# sourceMappingURL=2.1872659193567b392b30.js.map