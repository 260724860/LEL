webpackJsonp([9],{fQzi:function(e,t){},k1yD:function(e,t,r){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var a=r("Xxa5"),n=r.n(a),o=r("exGp"),s=r.n(o),c=r("Dd8w"),i=r.n(c),u=r("NYxO"),l={name:"CheckPhone",data:function(){return{formValidate:{phone:"",checkNum:""},ruleValidate:{phone:[{required:!0,message:"手机号不能为空",trigger:"blur"},{type:"string",pattern:/^1[3|4|5|6|7|8|9][0-9]{9}$/,message:"手机号格式出错",trigger:"blur"}],checkNum:[{required:!0,message:"必须填写验证码",trigger:"blur"},{type:"string",min:6,max:6,message:"验证码长度错误",trigger:"blur"}]},checkNumSent:!1,time:60}},mounted:function(){this.SET_SIGN_UP_STEP(0)},methods:i()({},Object(u.d)(["SET_SIGN_UP_STEP"]),{countDown:function(){var e=this;this.time>0?setTimeout(function(){e.time--,e.countDown()},1e3):(this.time=60,this.checkNumSent=!1)},getCheckNum:function(){var e=this;return s()(n.a.mark(function t(){return n.a.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:if(11!==e.formValidate.phone.length){t.next=7;break}return t.next=3,e.$request.sendSms(e.formValidate.phone);case 3:t.sent?(e.$Message.success({content:"验证码已发送",duration:3,closable:!0}),e.checkNumSent=!0,e.countDown()):e.$Message.error({content:"验证码发送失败",duration:3,closable:!0}),t.next=8;break;case 7:e.$Message.error({content:"请输入正确的手机号",duration:3,closable:!0});case 8:case"end":return t.stop()}},t,e)}))()},handleSubmit:function(){var e,t=this;this.$refs.formData.validate((e=s()(n.a.mark(function e(r){return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:if(!r){e.next=7;break}return e.next=3,t.$request.checkCode(t.formValidate.phone,t.formValidate.checkNum);case 3:e.sent?(t.$router.push({path:"/register/InputInfo",query:{phone:t.formValidate.phone}}),t.SET_SIGN_UP_STEP(1)):t.$Message.error({content:"验证码错误",duration:3,closable:!0}),e.next=8;break;case 7:t.$Message.error({content:"请填写正确的信息",duration:3,closable:!0});case 8:case"end":return e.stop()}},e,t)})),function(t){return e.apply(this,arguments)}))}})},m={render:function(){var e=this,t=e.$createElement,r=e._self._c||t;return r("div",{staticClass:"info-form"},[r("Form",{ref:"formData",attrs:{model:e.formValidate,"label-width":80,rules:e.ruleValidate}},[r("FormItem",{attrs:{label:"手机号",prop:"phone"}},[r("i-input",{attrs:{clearable:"",size:"large",placeholder:"请输入手机号"},model:{value:e.formValidate.phone,callback:function(t){e.$set(e.formValidate,"phone",t)},expression:"formValidate.phone"}})],1),e._v(" "),r("FormItem",{attrs:{label:"验证码",prop:"checkNum"}},[r("i-input",{attrs:{size:"large",placeholder:"请输入验证码"},model:{value:e.formValidate.checkNum,callback:function(t){e.$set(e.formValidate,"checkNum",t)},expression:"formValidate.checkNum"}},[e.checkNumSent?r("Button",{attrs:{slot:"append",disabled:""},slot:"append"},[e._v("重新发送 ("+e._s(e.time)+"s)")]):r("Button",{attrs:{slot:"append"},on:{click:e.getCheckNum},slot:"append"},[e._v("获取验证码")])],1)],1),e._v(" "),r("Button",{attrs:{type:"error",size:"large",long:""},on:{click:e.handleSubmit}},[e._v("验证手机号")])],1)],1)},staticRenderFns:[]};var d=r("VU/8")(l,m,!1,function(e){r("fQzi")},"data-v-ceaca594",null);t.default=d.exports}});
//# sourceMappingURL=9.e0a364d984af7813643d1560476466300.js.map