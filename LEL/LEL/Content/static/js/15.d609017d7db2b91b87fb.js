webpackJsonp([15],{Wx55:function(s,e){},tzhQ:function(s,e,a){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var t=a("mvHQ"),r=a.n(t),i=a("Xxa5"),n=a.n(i),c=a("exGp"),o=a.n(c),u=a("Dd8w"),l=a.n(u),g=a("NYxO"),p={name:"MyScan",data:function(){return{isVerified:!1,dialogImageUrl:"",dialogVisible:!1,allowType:["gif","jpg","bmp","jpeg","png"],UsersImage:null,UsersIDImgA:null,UsersIDImgB:null,UsersBusinessImg:null,imgBase:null,UsersImageOrigin:null,UsersIDImgAOrigin:null,UsersIDImgBOrigin:null,UsersBusinessImgOrigin:null}},computed:l()({},Object(g.d)(["userInfo","sysConfig"])),mounted:function(){var s=this;return o()(n.a.mark(function e(){return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,s.loadSysConfig();case 2:s.imgBase=s.sysConfig.content.ShopDomain.Value,s.userInfo.accattach.HeadImage&&(s.UsersImage=s.imgBase+s.userInfo.accattach.HeadImage),s.userInfo.accattach.IDImgA&&(s.UsersIDImgA=s.imgBase+s.userInfo.accattach.IDImgA),s.userInfo.accattach.IDImgB&&(s.UsersIDImgB=s.imgBase+s.userInfo.accattach.IDImgB),s.userInfo.accattach.BusinessImg&&(s.UsersBusinessImg=s.imgBase+s.userInfo.accattach.BusinessImg),s.isVerified=1===s.userInfo.accattach.status;case 8:case"end":return e.stop()}},e,s)}))()},methods:l()({},Object(g.b)(["loadSysConfig"]),Object(g.c)(["SET_USER_LOGIN_INFO"]),{beforeUpload:function(s){var e=!1;this.allowType.forEach(function(a){e=e||s.type==="image/"+a});var a=s.size/1024/1024<2;return e?a||this.$Message.error("上传图片大小不能超过2MB，请重试"):this.$Message.error("只支持gif,jpg,bmp,jpeg,png格式图片，请重试"),e&&a},uploadUsersImage:function(s){var e=this;return o()(n.a.mark(function a(){return n.a.wrap(function(a){for(;;)switch(a.prev=a.next){case 0:return a.next=2,e.uploadImg(s,"UsersImage");case 2:case"end":return a.stop()}},a,e)}))()},uploadUsersIDImgA:function(s){var e=this;return o()(n.a.mark(function a(){return n.a.wrap(function(a){for(;;)switch(a.prev=a.next){case 0:return a.next=2,e.uploadImg(s,"UsersIDImgA");case 2:case"end":return a.stop()}},a,e)}))()},uploadUsersIDImgB:function(s){var e=this;return o()(n.a.mark(function a(){return n.a.wrap(function(a){for(;;)switch(a.prev=a.next){case 0:return a.next=2,e.uploadImg(s,"UsersIDImgB");case 2:case"end":return a.stop()}},a,e)}))()},uploadUsersBusinessImg:function(s){var e=this;return o()(n.a.mark(function a(){return n.a.wrap(function(a){for(;;)switch(a.prev=a.next){case 0:return a.next=2,e.uploadImg(s,"UsersBusinessImg");case 2:case"end":return a.stop()}},a,e)}))()},uploadImg:function(s,e){var a=this;return o()(n.a.mark(function t(){var r;return n.a.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.next=2,a.$request.uploadImg(s.file);case 2:(r=t.sent)?(a.$Message.success("上传成功"),a[""+e]=a.imgBase+r.content,a[e+"Origin"]=r.content):a.$Message.error("上传失败");case 4:case"end":return t.stop()}},t,a)}))()},submitUpdate:function(){var s=this;return o()(n.a.mark(function e(){var a,t;return n.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return(a=(a=JSON.parse(localStorage.getItem("userInfo"))).accattach).Code="",a.Msg="",a.HeadImage=s.UsersImageOrigin,a.IDImgA=s.UsersIDImgAOrigin,a.IDImgB=s.UsersIDImgBOrigin,a.BusinessImg=s.UsersBusinessImgOrigin,e.next=10,s.$request.updateUserInfo(a);case 10:e.sent?((t=s.userInfo).accattach=a,s.SET_USER_LOGIN_INFO(t),localStorage.setItem("userInfo",r()(t)),s.$Message.success("提交成功")):s.$Message.error("提交失败");case 12:case"end":return e.stop()}},e,s)}))()}})},I={render:function(){var s=this,e=s.$createElement,a=s._self._c||e;return a("div",[a("div",{staticClass:"imgBox"},[a("p",{staticClass:"title"},[s._v("店铺门头照")]),s._v(" "),a("el-upload",{staticClass:"avatar-uploader",attrs:{action:"","show-file-list":!1,disabled:s.isVerified,accept:".jpg,.jpeg,.png,.gif,.bmp,.JPG,.JPEG,.PNG,.GIF,.BMP","http-request":s.uploadUsersImage,"before-upload":s.beforeUpload}},[s.UsersImage?a("img",{staticClass:"avatar",attrs:{src:s.UsersImage}}):a("i",{staticClass:"el-icon-plus avatar-uploader-icon"})])],1),s._v(" "),a("div",{staticClass:"imgBox"},[a("p",{staticClass:"title"},[s._v("身份证正面")]),s._v(" "),a("el-upload",{staticClass:"avatar-uploader",attrs:{action:"","show-file-list":!1,disabled:s.isVerified,accept:".jpg,.jpeg,.png,.gif,.bmp,.JPG,.JPEG,.PNG,.GIF,.BMP","http-request":s.uploadUsersIDImgA,"before-upload":s.beforeUpload}},[s.UsersIDImgA?a("img",{staticClass:"avatar",attrs:{src:s.UsersIDImgA}}):a("i",{staticClass:"el-icon-plus avatar-uploader-icon"})])],1),s._v(" "),a("div",{staticClass:"imgBox"},[a("p",{staticClass:"title"},[s._v("身份证反面")]),s._v(" "),a("el-upload",{staticClass:"avatar-uploader",attrs:{action:"","show-file-list":!1,disabled:s.isVerified,accept:".jpg,.jpeg,.png,.gif,.bmp,.JPG,.JPEG,.PNG,.GIF,.BMP","http-request":s.uploadUsersIDImgB,"before-upload":s.beforeUpload}},[s.UsersIDImgB?a("img",{staticClass:"avatar",attrs:{src:s.UsersIDImgB}}):a("i",{staticClass:"el-icon-plus avatar-uploader-icon"})])],1),s._v(" "),a("div",{staticClass:"imgBox"},[a("p",{staticClass:"title"},[s._v("营业执照")]),s._v(" "),a("el-upload",{staticClass:"avatar-uploader",attrs:{action:"","show-file-list":!1,disabled:s.isVerified,accept:".jpg,.jpeg,.png,.gif,.bmp,.JPG,.JPEG,.PNG,.GIF,.BMP","http-request":s.uploadUsersBusinessImg,"before-upload":s.beforeUpload}},[s.UsersBusinessImg?a("img",{staticClass:"avatar",attrs:{src:s.UsersBusinessImg}}):a("i",{staticClass:"el-icon-plus avatar-uploader-icon"})])],1),s._v(" "),a("div",{staticStyle:{clear:"both"}}),s._v(" "),s.isVerified?s._e():a("el-button",{staticStyle:{"margin-top":"30px"},attrs:{size:"small",type:"success"},on:{click:s.submitUpdate}},[s._v("提交审核")]),s._v(" "),s.isVerified?s._e():a("div",{staticStyle:{"margin-top":"20px"}},[a("span",[s._v("提交审核资料后请于左侧填写基本信息")])])],1)},staticRenderFns:[]};var m=a("VU/8")(p,I,!1,function(s){a("Wx55")},"data-v-27aa4eab",null);e.default=m.exports}});
//# sourceMappingURL=15.d609017d7db2b91b87fb.js.map