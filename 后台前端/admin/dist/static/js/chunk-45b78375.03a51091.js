(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-45b78375"],{1:function(e,a){},"1cd9":function(e,a,t){"use strict";t("5676")},2:function(e,a){},3:function(e,a){},3796:function(e,a,t){"use strict";var n=function(){var e=this,a=e.$createElement,t=e._self._c||a;return t("div",[t("input",{ref:"excel-upload-input",staticClass:"excel-upload-input",attrs:{type:"file",accept:".xlsx, .xls"},on:{change:e.handleClick}}),t("div",{staticClass:"drop",on:{drop:e.handleDrop,dragover:e.handleDragover,dragenter:e.handleDragover}},[e._v(" "+e._s(e.filename)+" "),t("el-button",{staticStyle:{"margin-left":"16px"},attrs:{loading:e.loading,size:"mini",type:"primary"},on:{click:e.handleUpload}},[e._v(" 点击上传 ")])],1)])},r=[],l=(t("b0c0"),t("d3b7"),t("ac1f"),t("00b4"),t("1146")),s=t.n(l),o={props:{beforeUpload:Function,onSuccess:Function},data:function(){return{loading:!1,filename:"拖拽Excel文件在这 或",excelData:{header:null,results:null}}},methods:{generateData:function(e){var a=e.header,t=e.results;this.excelData.header=a,this.excelData.results=t,this.onSuccess&&this.onSuccess(this.excelData)},handleDrop:function(e){if(e.stopPropagation(),e.preventDefault(),!this.loading){var a=e.dataTransfer.files;if(1===a.length){var t=a[0];if(!this.isExcel(t))return this.$message.error("Only supports upload .xlsx, .xls, .csv suffix files"),!1;this.upload(t),e.stopPropagation(),e.preventDefault()}else this.$message.error("Only support uploading one file!")}},handleDragover:function(e){e.stopPropagation(),e.preventDefault(),e.dataTransfer.dropEffect="copy"},handleUpload:function(){this.$refs["excel-upload-input"].click()},handleClick:function(e){var a=e.target.files,t=a[0];t&&this.upload(t)},upload:function(e){if(this.$refs["excel-upload-input"].value=null,this.beforeUpload){var a=this.beforeUpload(e);this.filename=e.name,a&&this.readerData(e)}else this.readerData(e)},readerData:function(e){var a=this;return this.loading=!0,new Promise((function(t,n){var r=new FileReader;r.onload=function(e){var n=e.target.result,r=s.a.read(n,{type:"array"}),l=r.SheetNames[0],o=r.Sheets[l],i=a.getHeaderRow(o),c=s.a.utils.sheet_to_json(o);a.generateData({header:i,results:c}),a.loading=!1,t()},r.readAsArrayBuffer(e)}))},getHeaderRow:function(e){var a,t=[],n=s.a.utils.decode_range(e["!ref"]),r=n.s.r;for(a=n.s.c;a<=n.e.c;++a){var l=e[s.a.utils.encode_cell({c:a,r:r})],o="UNKNOWN "+a;l&&l.t&&(o=s.a.utils.format_cell(l)),t.push(o)}return t},isExcel:function(e){return/\.(xlsx|xls|csv)$/.test(e.name)}}},i=o,c=(t("1cd9"),t("2877")),u=Object(c["a"])(i,n,r,!1,null,"64b85a3f",null);a["a"]=u.exports},5676:function(e,a,t){},a137:function(e,a,t){"use strict";t.r(a);var n=function(){var e=this,a=e.$createElement,t=e._self._c||a;return t("div",{staticClass:"app-container"},[t("upload-excel-component",{attrs:{"on-success":e.handleSuccess,"before-upload":e.beforeUpload}}),t("el-table",{staticStyle:{width:"100%","margin-top":"20px"},attrs:{data:e.tableData,border:"","highlight-current-row":""}},e._l(e.tableHeader,(function(e){return t("el-table-column",{key:e,attrs:{prop:e,label:e}})})),1)],1)},r=[],l=t("3796"),s={name:"UploadExcel",components:{UploadExcelComponent:l["a"]},data:function(){return{tableData:[],tableHeader:[]}},methods:{beforeUpload:function(e){var a=e.size/1024/1024<1;return!!a||(this.$message({message:"Please do not upload files larger than 1m in size.",type:"warning"}),!1)},handleSuccess:function(e){var a=e.results,t=e.header;this.tableData=a,this.tableHeader=t}}},o=s,i=t("2877"),c=Object(i["a"])(o,n,r,!1,null,null,null);a["default"]=c.exports}}]);