webpackJsonp([0],{"1bjU":function(t,o,e){"use strict";var r={name:"headerNav",components:{NavBar:e("Fd2+").d},props:{title:String,showBackArrow:{type:Boolean,default:!0}},methods:{onBack:function(){history.back()}}},a={render:function(){var t=this.$createElement;return(this._self._c||t)("van-nav-bar",{attrs:{title:this.title,"left-text":"","left-arrow":this.showBackArrow},on:{"click-left":this.onBack}})},staticRenderFns:[]};var n=e("VU/8")(r,a,!1,function(t){e("zYFM")},null,null);o.a=n.exports},QhFP:function(t,o,e){"use strict";var r=e("Xxa5"),a=e.n(r),n=e("exGp"),s=e.n(n),c={name:"product-card",props:{product:Object,isCard:{type:Boolean,default:!1},isOrder:{type:Boolean,default:!1}},methods:{handleChange:function(t){var o=this;return s()(a.a.mark(function e(){var r,n;return a.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return(r=t)||(r=1),o.product.GoodsCount=r,o.product.totalPrice=r*o.product.Price,n=[],o.product.GoodsValueList.forEach(function(t){var o={CategoryType:t.CategoryType,GoodsValueID:t.GoodsValueID};n.push(o)}),e.next=8,o.$request.updateCartCount(o.product.GoodsID,o.product.GoodsCount,n);case 8:case"end":return e.stop()}},e,o)}))()}}},u={render:function(){var t=this,o=t.$createElement,e=t._self._c||o;return e("van-cell-group",{staticClass:"additional"},[e("van-card",{staticStyle:{background:"#fff"},attrs:{title:t._f("ellipsis")(t.product.GoodsName,15),num:t.isCard?null:t.product.GoodsCount}},[e("template",{slot:"thumb"},[e("img",{attrs:{src:t.product.GoodsImg}})]),t._v(" "),e("template",{slot:"desc"},[e("div",[t._v(t._s(t.product.SerialNumber))]),t._v(" "),e("div",[t._v(t._s(t.product.specs))])]),t._v(" "),e("template",{slot:"tags"},[e("p",{staticClass:"price"},[t._v("\n\t\t\t\t￥"),e("span",[t._v(t._s(t.product.Price))])]),t._v(" "),t.isCard?e("van-stepper",{attrs:{integer:"",min:1,max:-1!==t.product.Quota?t.product.Quota:1e4,"input-width":"50px"},on:{change:t.handleChange},model:{value:t.product.GoodsCount,callback:function(o){t.$set(t.product,"GoodsCount",o)},expression:"product.GoodsCount"}}):t._e()],1),t._v(" "),e("template",{slot:"num"},[t.isCard?t._e():e("div",[e("span",{staticStyle:{"margin-right":"15px"}},[t._v("x "+t._s(t.product.GoodsCount))]),t._v(" "),t.isOrder?e("span",[t._v("小计:￥"+t._s(t.product.totalPrice))]):t._e()])])],2),t._v(" "),t._t("default")],2)},staticRenderFns:[]};var d=e("VU/8")(c,u,!1,function(t){e("v47g")},null,null);o.a=d.exports},nJow:function(t,o,e){t.exports=e.p+"static/img/notfound.a4473d1.png"},v47g:function(t,o){},zYFM:function(t,o){}});
//# sourceMappingURL=0.640ed4430a2a1f0e15b9.js.map