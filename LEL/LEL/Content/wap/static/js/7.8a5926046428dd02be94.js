webpackJsonp([7],{S9zk:function(t,e){},"a7B+":function(t,e,s){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var d=s("Dd8w"),n=s.n(d),i=s("Fd2+"),a=s("1bjU"),r=s("NYxO"),o={name:"AddressList",components:{headerNav:a.a,AddressList:i.b},data:function(){return{}},computed:n()({},Object(r.d)(["address"])),created:function(){this.loadAddress()},methods:n()({},Object(r.b)(["loadAddress"]),{onAdd:function(){this.$router.push("/addressDetail")},onEdit:function(t,e){this.$router.push("/addressDetail?index="+e)},onSelect:function(t,e){this.$router.push("/addressDetail?index="+e)}})},c={render:function(){var t=this.$createElement,e=this._self._c||t;return e("div",[e("headerNav",{attrs:{title:"地址列表"}}),this._v(" "),e("van-address-list",{staticClass:"hideSelect",attrs:{list:this.address},on:{add:this.onAdd,edit:this.onEdit,select:this.onSelect}})],1)},staticRenderFns:[]};var u=s("VU/8")(o,c,!1,function(t){s("S9zk")},"data-v-eab1f082",null);e.default=u.exports}});
//# sourceMappingURL=7.8a5926046428dd02be94.js.map