webpackJsonp([9],{"7TMx":function(e,r,a){"use strict";Object.defineProperty(r,"__esModule",{value:!0});var t=a("Xxa5"),s=a.n(t),o=a("exGp"),n=a.n(o),i=a("Dd8w"),l=a.n(i),d=a("NYxO"),u={name:"InputInfo",data:function(){var e=this;return{formValidate:{password:"",rePassword:""},ruleValidate:{password:[{required:!0,message:"密码不能为空",trigger:"blur"},{type:"string",min:6,message:"密码长度不能小于6",trigger:"blur"}],rePassword:[{required:!0,message:"请再次输入密码",trigger:"blur"},{validator:function(r,a,t){a!==e.formValidate.password?t(new Error("两次输入的密码不一样")):t()},trigger:"blur"}]}}},mounted:function(){this.SET_SIGN_UP_STEP(1)},methods:l()({},Object(d.c)(["SET_SIGN_UP_STEP"]),{handleSubmit:function(){var e,r=this;this.$refs.formData.validate((e=n()(s.a.mark(function e(a){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:if(!a){e.next=7;break}return e.next=3,r.$request.regist(r.$route.query.phone,r.formValidate.password);case 3:e.sent&&(r.$Message.success("注册成功"),r.SET_SIGN_UP_STEP(2),r.$router.push("/SignUp/signUpDone")),e.next=8;break;case 7:r.$Message.error("请填写正确的信息");case 8:case"end":return e.stop()}},e,r)})),function(r){return e.apply(this,arguments)}))}})},c={render:function(){var e=this,r=e.$createElement,a=e._self._c||r;return a("div",{staticClass:"info-form"},[a("Form",{ref:"formData",attrs:{model:e.formValidate,rules:e.ruleValidate,"label-width":80}},[a("FormItem",{attrs:{label:"密码",prop:"password"}},[a("i-input",{attrs:{type:"password",clearable:"",size:"large",placeholder:"请输入你的密码"},model:{value:e.formValidate.password,callback:function(r){e.$set(e.formValidate,"password",r)},expression:"formValidate.password"}})],1),e._v(" "),a("FormItem",{attrs:{label:"确认密码",prop:"rePassword"}},[a("i-input",{attrs:{type:"password",clearable:"",size:"large",placeholder:"请再次输入你的密码"},model:{value:e.formValidate.rePassword,callback:function(r){e.$set(e.formValidate,"rePassword",r)},expression:"formValidate.rePassword"}})],1),e._v(" "),a("Button",{attrs:{type:"error",size:"large",long:""},on:{click:e.handleSubmit}},[e._v("注册")])],1)],1)},staticRenderFns:[]};var p=a("VU/8")(u,c,!1,function(e){a("vKgO")},"data-v-c3c0336c",null);r.default=p.exports},vKgO:function(e,r){}});
//# sourceMappingURL=9.b397a5e8376648c5a7ed.js.map