webpackJsonp([11],{MRDV:function(t,e,a){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var s={name:"Pay",components:{Footer:a("bYoP").a},data:function(){return{state:[{Img:"static/img/pay/pay_success.png",content:"您已成功下单，感谢您的购买！"},{Img:"static/img/pay/pay_error.png",content:"下单失败，请重新下单"}]}},created:function(){this.res=this.$route.query.result},methods:{goShop:function(){this.$router.push("/index")}}},n={render:function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[a("div",{staticClass:"pay-container"},[a("div",{staticClass:"pay_center"},[a("img",{attrs:{src:t.state[t.res].Img,alt:""}})]),t._v(" "),a("div",{staticClass:"pay_center pay_title"},[t._v(t._s(t.state[t.res].content))]),t._v(" "),a("div",{staticClass:"pay_center"},[a("button",{staticClass:"pay_go_shop",on:{click:t.goShop}},[t._v("继续购物")])])]),t._v(" "),a("Footer")],1)},staticRenderFns:[]};var r=a("VU/8")(s,n,!1,function(t){a("Vwmv")},"data-v-af23d5ae",null);e.default=r.exports},Vwmv:function(t,e){}});
//# sourceMappingURL=11.daf410b2ce95c3a0e92e.js.map