webpackJsonp([2],{UgCr:function(e,t,n){"use strict";t.b=function(e){return new i.a(function(e){e()})},t.d=function(e){return new i.a(function(e){e()})},t.c=function(e){return new i.a(function(e){e()})},n.d(t,"a",function(){return x});var a=n("Xxa5"),s=n.n(a),r=n("exGp"),o=n.n(r),l=n("//Fk"),i=n.n(l),u=n("vLgD"),c=u.a.get,p=u.a.post,d=u.a.postd;h=o()(s.a.mark(function e(t){var n;return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return n={KeyWords:t},e.next=3,c("Goods/GetGoodsGroupList",n);case 3:return e.abrupt("return",e.sent);case 4:case"end":return e.stop()}},e,this)})),f=o()(s.a.mark(function e(t){var n;return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return n={GoodsID:t},e.next=3,c("Goods/GetGoodDetailedAync",n);case 3:return e.abrupt("return",e.sent);case 4:case"end":return e.stop()}},e,this)}));var h,f,v,m,b,S,g,y,w,k,I,G,x=(v=o()(s.a.mark(function e(t){var n;return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return n={GoodsID:t},e.next=3,p("Goods/UnShelvesGoods",n,n);case 3:return e.abrupt("return",e.sent);case 4:case"end":return e.stop()}},e,this)})),function(e){return v.apply(this,arguments)});m=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,p("Goods/EditGoods","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),b=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,p("Goods/AddGoods","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),S=o()(s.a.mark(function e(t){var n;return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return n={KeyWords:t},e.next=3,c("Goods/GetSupplierList",n);case 3:return e.abrupt("return",e.sent);case 4:case"end":return e.stop()}},e,this)})),g=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,p("Goods/DeleteGoodsImage","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),y=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,d("Goods/AddGoodsImage","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),w=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,d("Goods/AddGoodsValue","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),k=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,p("Goods/DeleteGoodsValue","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),I=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,p("Goods/AddGoodsGroupList","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)})),G=o()(s.a.mark(function e(t){return s.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,p("Goods/EditGoodsGroupList","",t);case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e,this)}))},Ya8g:function(e,t){},gEzv:function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var a=n("woOf"),s=n.n(a),r=n("UgCr"),o=n("//Fk"),l=n.n(o);n("vLgD");function i(e,t){return new l.a(function(e){e({code:200,message:"操作成功",data:[{id:90,productId:26,skuCode:"201806070026001",price:3788,stock:499,lowStock:null,sp1:"金色",sp2:"16G",sp3:null,pic:null,sale:null,promotionPrice:3588,lockStock:-8},{id:91,productId:26,skuCode:"201806070026002",price:3999,stock:500,lowStock:null,sp1:"金色",sp2:"32G",sp3:null,pic:null,sale:null,promotionPrice:3799,lockStock:0},{id:92,productId:26,skuCode:"201806070026003",price:3788,stock:500,lowStock:null,sp1:"银色",sp2:"16G",sp3:null,pic:null,sale:null,promotionPrice:3588,lockStock:0},{id:93,productId:26,skuCode:"201806070026004",price:3999,stock:500,lowStock:null,sp1:"银色",sp2:"32G",sp3:null,pic:null,sale:null,promotionPrice:3799,lockStock:0}]})})}n("o/zv");n("Ya8g");var u={keyword:null,pageNum:1,pageSize:5,publishStatus:null,verifyStatus:null,productSn:null,productCategoryId:null,brandId:null},c={KeyWords:"",Offset:0,Rows:100,IsShelves:"",IsRecommend:"",IsNewGoods:"",IsHot:"",GoodsGroupID:"",SortKey:2,SupplierID:JSON.parse(localStorage.getItem("userInfoS")).SuppliersID,SerialNumber:""},p={name:"productList",data:function(){return{editSkuInfo:{dialogVisible:!1,productId:null,productSn:"",productAttributeCategoryId:null,stockList:[],productAttr:[],keyword:null},SortKey:[{label:"上架时间升序",value:1},{label:"上架时间降序",value:2},{label:"价格升序",value:3},{label:"价格降序",value:4},{label:"默认升序",value:5},{label:"默认降序",value:6}],operates:[{label:"商品上架",value:"publishOn"},{label:"商品下架",value:"publishOff"},{label:"设为推荐",value:"recommendOn"},{label:"取消推荐",value:"recommendOff"},{label:"设为新品",value:"newOn"},{label:"取消新品",value:"newOff"},{label:"转移到分类",value:"transferCategory"},{label:"移入回收站",value:"recycle"}],operateType:null,listQuery:s()({},u),list:null,total:null,listLoading:!0,selectProductCateValue:null,multipleSelection:[],productCateOptions:[],brandOptions:[],publishStatusOptions:[{value:1,label:"上架"},{value:0,label:"下架"}],RecommendStatusOptions:[{value:1,label:"推荐"},{value:0,label:"不推荐"}],NewGoodsStatusOptions:[{value:1,label:"是"},{value:0,label:"否"}],HotStatusOptions:[{value:1,label:"是"},{value:0,label:"否"}],verifyStatusOptions:[{value:1,label:"审核通过"},{value:0,label:"未审核"}],options:s()({},c),pageList:{pages:1}}},created:function(){var e=this.$route.query.IsShelves;(e||0==e)&&(this.options.IsShelves=Number(e)),this.getList(),this.getProductCateList()},watch:{},filters:{filteTime:function(e){return e.indexOf("T")>-1?e.replace(/T/g," "):e},verifyStatusFilter:function(e){return 1===e?"审核通过":"未审核"}},methods:{ChangeOnSelect:function(e){this.options.GoodsGroupID=e[e.length-1]},getProductSkuSp:function(e,t){return 0===t?e.sp1:1===t?e.sp2:e.sp3},getList:function(){var e=this;this.listLoading=!0,this.$request.GetGoodsList(this.options).then(function(t){e.listLoading=!1,e.list=t.content.GoodsModel,e.total=t.content.PageCount})},getProductCateList:function(){var e=this;this.$request.GetGoodsGroupList("").then(function(t){var n=t.content;e.listLoading=!1;var a=n.filter(function(e){return 1==e.Level});a.forEach(function(e){e.children=[];for(var t=0;t<n.length;t++)1!=n[t].Level&&n[t].ParentID==e.ID&&e.children.push(n[t])});for(var s=0;s<a.length;s++){var r=[];if(null!=a[s].children&&a[s].children.length>0)for(var o=0;o<a[s].children.length;o++)r.push({label:a[s].children[o].Name,value:a[s].children[o].ID});e.productCateOptions.push({label:a[s].Name,value:a[s].ID,children:r})}})},handleShowSkuEditDialog:function(e,t){var n,a=this;this.editSkuInfo.dialogVisible=!0,this.editSkuInfo.productId=t.id,this.editSkuInfo.productSn=t.productSn,this.editSkuInfo.productAttributeCategoryId=t.productAttributeCategoryId,this.editSkuInfo.keyword=null,i(t.id,this.editSkuInfo.keyword).then(function(e){a.editSkuInfo.stockList=e.data}),(t.productAttributeCategoryId,n={type:0},new l.a(function(e){var t="";1==n.type?t={code:200,message:"操作成功",data:{total:4,totalPage:1,pageSize:100,list:[{id:45,productAttributeCategoryId:3,name:"屏幕尺寸",selectType:0,inputType:0,inputList:"",sort:0,filterType:0,searchType:0,relatedStatus:0,handAddStatus:0,type:1},{id:46,productAttributeCategoryId:3,name:"网络",selectType:0,inputType:1,inputList:"3G,4G",sort:0,filterType:0,searchType:0,relatedStatus:0,handAddStatus:0,type:1},{id:47,productAttributeCategoryId:3,name:"系统",selectType:0,inputType:1,inputList:"Android,IOS",sort:0,filterType:0,searchType:0,relatedStatus:0,handAddStatus:0,type:1},{id:48,productAttributeCategoryId:3,name:"电池容量",selectType:0,inputType:0,inputList:"",sort:0,filterType:0,searchType:0,relatedStatus:0,handAddStatus:0,type:1}],pageNum:1}}:0==n.type&&(t={code:200,message:"操作成功",data:{total:2,totalPage:1,pageSize:100,list:[{id:43,productAttributeCategoryId:3,name:"颜色",selectType:0,inputType:0,inputList:"",sort:100,filterType:0,searchType:0,relatedStatus:0,handAddStatus:1,type:0},{id:44,productAttributeCategoryId:3,name:"容量",selectType:0,inputType:1,inputList:"16G,32G,64G,128G",sort:0,filterType:0,searchType:0,relatedStatus:0,handAddStatus:0,type:0}],pageNum:1}}),e(t)})).then(function(e){a.editSkuInfo.productAttr=e.data.list})},handleSearchEditSku:function(){var e=this;i(this.editSkuInfo.productId,this.editSkuInfo.keyword).then(function(t){e.editSkuInfo.stockList=t.data})},handleSearchList:function(){this.handleCurrentChange(1)},handleBatchOperate:function(){var e=this;null!=this.operateType?null==this.multipleSelection||this.multipleSelection.length<1?this.$message({message:"请选择要操作的商品",type:"warning",duration:1e3}):this.$confirm("是否要进行该批量操作?","提示",{confirmButtonText:"确定",cancelButtonText:"取消",type:"warning"}).then(function(){for(var t=[],n=0;n<e.multipleSelection.length;n++)t.push(e.multipleSelection[n].id);switch(e.operateType){case e.operates[0].value:e.updatePublishStatus(1,t);break;case e.operates[1].value:e.updatePublishStatus(0,t);break;case e.operates[2].value:e.updateRecommendStatus(1,t);break;case e.operates[3].value:e.updateRecommendStatus(0,t);break;case e.operates[4].value:e.updateNewStatus(1,t);break;case e.operates[5].value:e.updateNewStatus(0,t);break;case e.operates[6].value:break;case e.operates[7].value:e.updateDeleteStatus(1,t)}e.getList()}):this.$message({message:"请选择操作类型",type:"warning",duration:1e3})},handleCurrentChange:function(e){this.pageList.pages=e,this.options.Offset=(e-1)*this.options.Rows,this.getList()},handleSelectionChange:function(e){this.multipleSelection=e},handlePublishStatusChange:function(e,t){var n=this;Object(r.a)(t.GoodsID).then(function(e){"SUCCESS"==e?n.$message({message:"操作成功",showClose:!0,type:"success"}):n.$message({message:"操作失败",showClose:!0,type:"error"})})},handleHotStatusChange:function(e,t){},handleNewStatusChange:function(e,t){var n=[];n.push(t.id),this.updateNewStatus(t.newStatus,n)},handleRecommendStatusChange:function(e,t){var n=[];n.push(t.id),this.updateRecommendStatus(t.recommandStatus,n)},handleResetSearch:function(){this.selectProductCateValue=[],this.options=s()({},c)},handleUpdateProduct:function(e,t){this.$router.push({path:"/pms/ProductDetails",query:{id:t.GoodsID}})},updatePublishStatus:function(e,t){var n=this,a=new URLSearchParams;a.append("ids",t),a.append("publishStatus",e),Object(r.c)(a).then(function(e){n.$message({message:"修改成功",type:"success",duration:1e3})})},updateNewStatus:function(e,t){var n=this,a=new URLSearchParams;a.append("ids",t),a.append("newStatus",e),Object(r.b)(a).then(function(e){n.$message({message:"修改成功",type:"success",duration:1e3})})},updateRecommendStatus:function(e,t){var n=this,a=new URLSearchParams;a.append("ids",t),a.append("recommendStatus",e),Object(r.d)(a).then(function(e){n.$message({message:"修改成功",type:"success",duration:1e3})})},updateDeleteStatus:function(e){function t(t,n){return e.apply(this,arguments)}return t.toString=function(){return e.toString()},t}(function(e,t){var n=this,a=new URLSearchParams;a.append("ids",t),a.append("deleteStatus",e),updateDeleteStatus(a).then(function(e){n.$message({message:"删除成功",type:"success",duration:1e3})}),this.getList()})}},d={render:function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{staticClass:"app-container"},[n("el-card",{staticClass:"filter-container",attrs:{shadow:"never"}},[n("div",[n("i",{staticClass:"el-icon-search"}),e._v(" "),n("span",[e._v("筛选搜索")]),e._v(" "),n("el-button",{staticStyle:{float:"right"},attrs:{type:"primary",size:"small"},on:{click:function(t){e.handleSearchList()}}},[e._v("查询结果")]),e._v(" "),n("el-button",{staticStyle:{float:"right","margin-right":"15px"},attrs:{size:"small"},on:{click:function(t){e.handleResetSearch()}}},[e._v("重置")]),e._v(" "),n("div",{staticStyle:{clear:"both"}})],1),e._v(" "),n("div",{staticStyle:{"margin-top":"15px"}},[n("el-form",{attrs:{inline:!0,model:e.options,size:"small","label-width":"140px"}},[n("el-form-item",{attrs:{label:"输入搜索："}},[n("el-input",{staticStyle:{width:"203px"},attrs:{placeholder:"商品名称"},model:{value:e.options.KeyWords,callback:function(t){e.$set(e.options,"KeyWords",t)},expression:"options.KeyWords"}})],1),e._v(" "),n("el-form-item",{attrs:{label:"商品分类："}},[n("el-cascader",{staticStyle:{width:"203px"},attrs:{clearable:"",options:e.productCateOptions,"change-on-select":""},on:{change:e.ChangeOnSelect},model:{value:e.selectProductCateValue,callback:function(t){e.selectProductCateValue=t},expression:"selectProductCateValue"}})],1),e._v(" "),n("el-form-item",{attrs:{label:"上架状态："}},[n("el-select",{staticStyle:{width:"203px"},attrs:{clearable:""},model:{value:e.options.IsShelves,callback:function(t){e.$set(e.options,"IsShelves",t)},expression:"options.IsShelves"}},e._l(e.publishStatusOptions,function(e){return n("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),n("el-form-item",{attrs:{label:"是否新品："}},[n("el-select",{staticStyle:{width:"203px"},attrs:{placeholder:"全部",clearable:""},model:{value:e.options.IsNewGoods,callback:function(t){e.$set(e.options,"IsNewGoods",t)},expression:"options.IsNewGoods"}},e._l(e.NewGoodsStatusOptions,function(e){return n("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),n("el-form-item",{attrs:{label:"是否热门："}},[n("el-select",{staticStyle:{width:"203px"},attrs:{placeholder:"全部",clearable:""},model:{value:e.options.IsHot,callback:function(t){e.$set(e.options,"IsHot",t)},expression:"options.IsHot"}},e._l(e.HotStatusOptions,function(e){return n("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),n("el-form-item",{attrs:{label:"是否推荐："}},[n("el-select",{staticStyle:{width:"203px"},attrs:{placeholder:"全部",clearable:""},model:{value:e.options.IsRecommend,callback:function(t){e.$set(e.options,"IsRecommend",t)},expression:"options.IsRecommend"}},e._l(e.RecommendStatusOptions,function(e){return n("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),n("el-form-item",{attrs:{label:"排序："}},[n("el-select",{staticStyle:{width:"203px"},attrs:{placeholder:"选择排序"},model:{value:e.options.SortKey,callback:function(t){e.$set(e.options,"SortKey",t)},expression:"options.SortKey"}},e._l(e.SortKey,function(e){return n("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),n("el-form-item",{attrs:{label:"条码："}},[n("el-input",{attrs:{placeholder:"请输入条码"},model:{value:e.options.SerialNumber,callback:function(t){e.$set(e.options,"SerialNumber",t)},expression:"options.SerialNumber"}})],1)],1)],1)]),e._v(" "),n("div",{staticClass:"table-container"},[n("el-table",{directives:[{name:"loading",rawName:"v-loading",value:e.listLoading,expression:"listLoading"}],ref:"productTable",staticStyle:{width:"100%"},attrs:{data:e.list,border:""},on:{"selection-change":e.handleSelectionChange}},[n("el-table-column",{attrs:{label:"上架时间",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(e._s(e._f("filteTime")(t.row.CreateTime)))]}}])}),e._v(" "),n("el-table-column",{attrs:{label:"商品图片",width:"150",align:"center"},scopedSlots:e._u([{key:"default",fn:function(e){return[n("img",{staticStyle:{width:"100%"},attrs:{src:e.row.Image}})]}}])}),e._v(" "),n("el-table-column",{attrs:{label:"商品名称",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[n("p",[e._v(e._s(t.row.GoodsName))])]}}])}),e._v(" "),n("el-table-column",{attrs:{label:"规格",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[n("p",[e._v(e._s(t.row.Specifications))])]}}])}),e._v(" "),n("el-table-column",{attrs:{label:"标签",width:"140",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[n("p",[e._v("\n            上架：\n            "),n("el-switch",{attrs:{"active-value":1,"inactive-value":0,disabled:""},model:{value:t.row.IsShelves,callback:function(n){e.$set(t.row,"IsShelves",n)},expression:"scope.row.IsShelves"}})],1),e._v(" "),n("p",[e._v("\n            热门：\n            "),n("el-switch",{attrs:{"active-value":1,"inactive-value":0,disabled:""},model:{value:t.row.IsHot,callback:function(n){e.$set(t.row,"IsHot",n)},expression:"scope.row.IsHot"}})],1),e._v(" "),n("p",[e._v("\n            新品：\n            "),n("el-switch",{attrs:{disabled:"","active-value":1,"inactive-value":0},model:{value:t.row.IsNewGoods,callback:function(n){e.$set(t.row,"IsNewGoods",n)},expression:"scope.row.IsNewGoods"}})],1),e._v(" "),n("p",[e._v("\n            推荐：\n            "),n("el-switch",{attrs:{disabled:"","active-value":1,"inactive-value":0},model:{value:t.row.IsRecommend,callback:function(n){e.$set(t.row,"IsRecommend",n)},expression:"scope.row.IsRecommend"}})],1)]}}])}),e._v(" "),n("el-table-column",{attrs:{label:"排序",width:"100",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[e._v(e._s(t.row.Sort))]}}])}),e._v(" "),n("el-table-column",{attrs:{label:"操作",width:"160",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[n("p",[n("el-button",{attrs:{size:"mini"},on:{click:function(n){e.handleUpdateProduct(t.$index,t.row)}}},[e._v("查看")])],1)]}}])})],1)],1),e._v(" "),n("div",{staticClass:"pagination-container"},[n("el-pagination",{attrs:{background:"",layout:"total,prev, pager, next,jumper,sizes","page-size":e.options.Rows,"current-page":e.pageList.pages,"page-sizes":[100,200,300,400],total:e.total},on:{"current-change":e.handleCurrentChange,"update:currentPage":function(t){e.$set(e.pageList,"pages",t)}}})],1)],1)},staticRenderFns:[]};var h=n("VU/8")(p,d,!1,function(e){n("trUA")},"data-v-a83fa634",null);t.default=h.exports},"o/zv":function(e,t,n){(function(e){function n(e,t){for(var n=0,a=e.length-1;a>=0;a--){var s=e[a];"."===s?e.splice(a,1):".."===s?(e.splice(a,1),n++):n&&(e.splice(a,1),n--)}if(t)for(;n--;n)e.unshift("..");return e}var a=/^(\/?|)([\s\S]*?)((?:\.{1,2}|[^\/]+?|)(\.[^.\/]*|))(?:[\/]*)$/,s=function(e){return a.exec(e).slice(1)};function r(e,t){if(e.filter)return e.filter(t);for(var n=[],a=0;a<e.length;a++)t(e[a],a,e)&&n.push(e[a]);return n}t.resolve=function(){for(var t="",a=!1,s=arguments.length-1;s>=-1&&!a;s--){var o=s>=0?arguments[s]:e.cwd();if("string"!=typeof o)throw new TypeError("Arguments to path.resolve must be strings");o&&(t=o+"/"+t,a="/"===o.charAt(0))}return t=n(r(t.split("/"),function(e){return!!e}),!a).join("/"),(a?"/":"")+t||"."},t.normalize=function(e){var a=t.isAbsolute(e),s="/"===o(e,-1);return(e=n(r(e.split("/"),function(e){return!!e}),!a).join("/"))||a||(e="."),e&&s&&(e+="/"),(a?"/":"")+e},t.isAbsolute=function(e){return"/"===e.charAt(0)},t.join=function(){var e=Array.prototype.slice.call(arguments,0);return t.normalize(r(e,function(e,t){if("string"!=typeof e)throw new TypeError("Arguments to path.join must be strings");return e}).join("/"))},t.relative=function(e,n){function a(e){for(var t=0;t<e.length&&""===e[t];t++);for(var n=e.length-1;n>=0&&""===e[n];n--);return t>n?[]:e.slice(t,n-t+1)}e=t.resolve(e).substr(1),n=t.resolve(n).substr(1);for(var s=a(e.split("/")),r=a(n.split("/")),o=Math.min(s.length,r.length),l=o,i=0;i<o;i++)if(s[i]!==r[i]){l=i;break}var u=[];for(i=l;i<s.length;i++)u.push("..");return(u=u.concat(r.slice(l))).join("/")},t.sep="/",t.delimiter=":",t.dirname=function(e){var t=s(e),n=t[0],a=t[1];return n||a?(a&&(a=a.substr(0,a.length-1)),n+a):"."},t.basename=function(e,t){var n=s(e)[2];return t&&n.substr(-1*t.length)===t&&(n=n.substr(0,n.length-t.length)),n},t.extname=function(e){return s(e)[3]};var o="b"==="ab".substr(-1)?function(e,t,n){return e.substr(t,n)}:function(e,t,n){return t<0&&(t=e.length+t),e.substr(t,n)}}).call(t,n("W2nU"))},trUA:function(e,t){}});
//# sourceMappingURL=2.3f116099ff42aabdc8351560476466300.js.map