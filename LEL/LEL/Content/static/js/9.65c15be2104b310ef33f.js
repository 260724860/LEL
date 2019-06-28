webpackJsonp([9],{WpMG:function(e,a){},sePl:function(e,a,r){"use strict";Object.defineProperty(a,"__esModule",{value:!0});var s=r("Xxa5"),t=r.n(s),o=r("mvHQ"),i=r.n(o),d=r("exGp"),l=r.n(d),n=r("Dd8w"),m=r.n(n),u=r("NYxO"),c={name:"UserInfo",data:function(){var e=this;return{formData:{isVerified:!1,shopName:"",ownerName:"",phone:"",address:"",idCardNo:"",businessNo:"",CarNumber:""},ruleInline:{shopName:[{required:!0,message:"请输入店铺名称",trigger:"blur"}],ownerName:[{required:!0,message:"请输入负责人姓名",trigger:"blur"}],address:[{required:!0,message:"请输入店铺地址",trigger:"blur"}],phone:[{required:!0,message:"手机号不能为空",trigger:"blur"},{type:"string",pattern:/^1[3|4|5|7|8|9][0-9]{9}$/,message:"手机号格式出错",trigger:"blur"}],idCardNo:[{required:!0,message:"请输入身份证号码",trigger:"blur"}],businessNo:[{required:!0,message:"请输入营业执照号码",trigger:"blur"}],CarNumber:[{required:!1}]},formValidate:{oldPassword:"",password:"",rePassword:""},ruleValidate:{oldPassword:[{required:!0,message:"旧密码不能为空",trigger:"blur"},{type:"string",min:6,message:"密码长度不能小于6",trigger:"blur"}],password:[{required:!0,message:"新密码不能为空",trigger:"blur"},{type:"string",min:6,message:"密码长度不能小于6",trigger:"blur"}],rePassword:[{required:!0,message:"请再次输入新密码",trigger:"blur"},{validator:function(a,r,s){r!==e.formValidate.password?s(new Error("两次输入的密码不一样")):s()},trigger:"blur"}]}}},computed:m()({},Object(u.d)(["userInfo"])),mounted:function(){this.formData.shopName=this.userInfo.accattach.NickName,this.formData.ownerName=this.userInfo.accattach.TrueName,this.formData.phone=this.userInfo.accattach.Mobile,this.formData.address=this.userInfo.accattach.Address,this.formData.idCardNo=this.userInfo.accattach.IDCardNo,this.formData.businessNo=this.userInfo.accattach.BusinessNo,this.formData.CarNumber=this.userInfo.accattach.CarNumber,this.isVerified=1===this.userInfo.accattach.status},methods:m()({},Object(u.c)(["SET_USER_LOGIN_INFO"]),{submitInfo:function(){var e,a=this;this.$refs.formInfo.validate((e=l()(t.a.mark(function e(r){var s,o;return t.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:if(!r){e.next=18;break}return(s=(s=JSON.parse(localStorage.getItem("userInfo"))).accattach).Code="",s.Msg="",s.NickName=a.formData.shopName,s.TrueName=a.formData.ownerName,s.Mobile=a.formData.phone,s.Address=a.formData.address,s.IDCardNo=a.formData.idCardNo,s.BusinessNo=a.formData.businessNo,s.CarNumber=a.formData.CarNumber,e.next=14,a.$request.updateUserInfo(s);case 14:e.sent?((o=a.userInfo).accattach=s,a.SET_USER_LOGIN_INFO(o),localStorage.setItem("userInfo",i()(o)),a.$Message.success("修改成功")):a.$Message.error("修改失败"),e.next=19;break;case 18:a.$Message.error("请填写正确的信息");case 19:case"end":return e.stop()}},e,a)})),function(a){return e.apply(this,arguments)}))},submitPwd:function(){var e,a=this;this.$refs.formPwd.validate((e=l()(t.a.mark(function e(r){var s;return t.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:if(!r){e.next=9;break}return s=(s=JSON.parse(localStorage.getItem("userInfo"))).content,e.next=5,a.$request.updatePwd(s.userName,a.formValidate.oldPassword,a.formValidate.password);case 5:e.sent?a.$Message.success("修改成功"):a.$Message.error("修改失败"),e.next=10;break;case 9:a.$Message.error("请填写正确的信息");case 10:case"end":return e.stop()}},e,a)})),function(a){return e.apply(this,arguments)}))}})},f={render:function(){var e=this,a=e.$createElement,r=e._self._c||a;return r("div",[r("div",{staticClass:"add-container"},[e._m(0),e._v(" "),r("div",{staticClass:"add-box"},[r("Form",{ref:"formInfo",attrs:{model:e.formData,"label-position":"left","label-width":100,rules:e.ruleInline}},[r("FormItem",{attrs:{label:"店铺名称",prop:"shopName"}},[r("i-input",{attrs:{disabled:e.isVerified,size:"large"},model:{value:e.formData.shopName,callback:function(a){e.$set(e.formData,"shopName",a)},expression:"formData.shopName"}})],1),e._v(" "),r("FormItem",{attrs:{label:"负责人",prop:"ownerName"}},[r("i-input",{attrs:{disabled:e.isVerified,size:"large"},model:{value:e.formData.ownerName,callback:function(a){e.$set(e.formData,"ownerName",a)},expression:"formData.ownerName"}})],1),e._v(" "),r("FormItem",{attrs:{label:"联系电话",prop:"phone"}},[r("i-input",{attrs:{disabled:e.isVerified,size:"large"},model:{value:e.formData.phone,callback:function(a){e.$set(e.formData,"phone",a)},expression:"formData.phone"}})],1),e._v(" "),r("FormItem",{attrs:{label:"店铺地址",prop:"address"}},[r("i-input",{attrs:{disabled:e.isVerified,size:"large"},model:{value:e.formData.address,callback:function(a){e.$set(e.formData,"address",a)},expression:"formData.address"}})],1),e._v(" "),r("FormItem",{attrs:{label:"身份证号",prop:"idCardNo"}},[r("i-input",{attrs:{disabled:e.isVerified,size:"large"},model:{value:e.formData.idCardNo,callback:function(a){e.$set(e.formData,"idCardNo",a)},expression:"formData.idCardNo"}})],1),e._v(" "),r("FormItem",{attrs:{label:"营业执照号",prop:"businessNo"}},[r("i-input",{attrs:{disabled:e.isVerified,size:"large"},model:{value:e.formData.businessNo,callback:function(a){e.$set(e.formData,"businessNo",a)},expression:"formData.businessNo"}})],1),e._v(" "),r("FormItem",{attrs:{label:"取货车牌",prop:"CarNumber"}},[r("i-input",{attrs:{size:"large"},model:{value:e.formData.CarNumber,callback:function(a){e.$set(e.formData,"CarNumber",a)},expression:"formData.CarNumber"}})],1)],1)],1),e._v(" "),r("div",{staticClass:"add-submit"},[r("Button",{attrs:{type:"primary"},on:{click:e.submitInfo}},[e._v("确认修改")])],1)]),e._v(" "),r("div",{staticClass:"add-container"},[e._m(1),e._v(" "),r("div",{staticClass:"add-box"},[r("Form",{ref:"formPwd",attrs:{model:e.formValidate,rules:e.ruleValidate,"label-position":"left","label-width":100}},[r("FormItem",{attrs:{label:"旧密码",prop:"oldPassword"}},[r("i-input",{attrs:{type:"password",clearable:"",size:"large",placeholder:"请输入旧密码"},model:{value:e.formValidate.oldPassword,callback:function(a){e.$set(e.formValidate,"oldPassword",a)},expression:"formValidate.oldPassword"}})],1),e._v(" "),r("FormItem",{attrs:{label:"新密码",prop:"password"}},[r("i-input",{attrs:{type:"password",clearable:"",size:"large",placeholder:"请输入新密码"},model:{value:e.formValidate.password,callback:function(a){e.$set(e.formValidate,"password",a)},expression:"formValidate.password"}})],1),e._v(" "),r("FormItem",{attrs:{label:"确认新密码",prop:"rePassword"}},[r("i-input",{attrs:{type:"password",clearable:"",size:"large",placeholder:"请再次输入新密码"},model:{value:e.formValidate.rePassword,callback:function(a){e.$set(e.formValidate,"rePassword",a)},expression:"formValidate.rePassword"}})],1)],1)],1),e._v(" "),r("div",{staticClass:"add-submit"},[r("Button",{attrs:{type:"primary"},on:{click:e.submitPwd}},[e._v("确认修改")])],1)])])},staticRenderFns:[function(){var e=this.$createElement,a=this._self._c||e;return a("div",{staticClass:"add-title"},[a("h1",[this._v("修改资料")])])},function(){var e=this.$createElement,a=this._self._c||e;return a("div",{staticClass:"add-title"},[a("h1",[this._v("修改密码")])])}]};var p=r("VU/8")(c,f,!1,function(e){r("WpMG")},"data-v-b590a6e8",null);a.default=p.exports}});
//# sourceMappingURL=9.65c15be2104b310ef33f.js.map