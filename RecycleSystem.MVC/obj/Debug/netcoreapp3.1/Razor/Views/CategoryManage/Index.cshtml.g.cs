#pragma checksum "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\CategoryManage\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66bd8eda17626b793071a20fc8eeb7a4e7adcbf4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CategoryManage_Index), @"mvc.1.0.view", @"/Views/CategoryManage/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\_ViewImports.cshtml"
using RecycleSystem.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\_ViewImports.cshtml"
using RecycleSystem.MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66bd8eda17626b793071a20fc8eeb7a4e7adcbf4", @"/Views/CategoryManage/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0f17171c1af4e388c1fbe24a8c8e14fb7d04498", @"/Views/_ViewImports.cshtml")]
    public class Views_CategoryManage_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\CategoryManage\Index.cshtml"
  
    ViewData["Title"] = "类别信息首页";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""layui-row "">

    <div class=""layui-col-md2 layui-col-md-offset5 "" style=""margin-top:20px;""><input type=""text"" id=""txt_Query"" placeholder=""请输入类别名"" class=""layui-input""></div>
    <div class=""layui-col-md1"" style=""margin-top:20px;""> <button class=""layui-btn"" onclick=""btn_search()"">搜索</button></div>

</div>

<table class=""layui-hide"" id=""categoryList"" lay-filter=""categoryList""></table>

<script type=""text/html"" id=""operation"">
    <div class=""layui-row"">
        <button class=""layui-btn layui-btn-sm layui-btn-normal"" lay-event=""Modify""><i class=""layui-icon""></i>修改</button>
    </div>
</script>


<!-- 注意：如果你直接复制所有代码到本地，上述js路径需要改成你本地的 -->
<script type=""text/html"" id=""statusId"">
    <input type=""checkbox"" value=""{{d.categoryId}}"" name=""open"" lay-skin=""switch"" lay-filter=""changeState"" lay-text=""在用|禁用"" {{");
            BeginWriteAttribute("d.delFlag", " d.delFlag =", 933, "", 945, 0);
            EndWriteAttribute();
            WriteLiteral(@"= false ? 'checked' : '' }}>
</script>
<script>
    var table; //放置全局中，方便后续使用
    layui.use('table', function () {
        table = layui.table;
        var form = layui.form;
        table.render({
            elem: '#categoryList'
            , url: '/CategoryManage/GetCategoriesList'
            , defaultToolbar: ['filter', 'exports', 'print', { //自定义头部工具栏右侧图标。如无需自定义，去除该参数即可
                title: '提示'
                , layEvent: 'LAYTABLE_TIPS'
                , icon: 'layui-icon-tips'
            }]
            , parseData: function (res) {
                return {
                    ""code"": 0
                    , ""msg"": """",
                    ""count"": res.count
                    , ""data"": res.data
                    , ""curr"": res.data.length
                }
            }
            , title: '用户数据表'
            , cols: [[
                { type: 'checkbox', fixed: 'left' }
                , { field: 'categoryId', title: '类别编号', width: 80, fixed: 'left', unresize: true, ");
            WriteLiteral(@"sort: false }
                , { field: 'categoryName', title: '类别名', width: 100 }
                , { field: 'currentPrice', title: '价钱', width: 120  }
                , { field: 'category', title: '类型', width: 150,  sort: false }
                , { field: 'unit', title: '单位', width: 50 }
                , { field: 'addTime', title: '添加时间', width: 200 }
                , { field: 'DelFlag', title: '状态', width: 130, sort: false, templet: '#statusId' }
                , { fixed: 'right', title: '操作', width: 180, align: 'center', toolbar: '#operation' }
            ]]
            , page: true
            , limit: 10
            , limits: [10, 20, 25, 30, 35, 40, 45, 50, 100]
        });
        form.on('switch(changeState)', function (obj) {
            //layer.tips(this.value + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
            $.ajax({
                url: ""/CategoryManage/ChangeState"",
                type: ""post"",
                data: {
                    categoryId: th");
            WriteLiteral(@"is.value
                },
                success: function (res) {
                    layer.alert(res);
                    btn_search();
                }
            })
        });
        //操作栏事件
        table.on('tool(categoryList)', function (obj) {
            debugger;
            switch (obj.event) {
                case 'Modify':
                    var id = obj.data.id;
                        layui.use('layer', function () {
                            layer.open({
                                type: 2,
                                title: '修改类别信息',
                                shade: 0.8,
                                maxmin: true, //开启最大化最小化按钮
                                area: ['893px', '600px'],
                                content: '/CategoryManage/Update?id=' + id,
                                end: function () {
                                    btn_search();
                                }
                            });
                   ");
            WriteLiteral(@"     })
                    break;

                //自定义头工具栏右侧图标 - 提示
                case 'LAYTABLE_TIPS':
                    layer.alert('这是工具栏右侧自定义的一个图标按钮');
                    break;
            };
        });
    });
    //查询
    btn_search = function () {
        debugger;
        var selectInfo = $(""#txt_Query"").val();

        //重新加载表格，并把文本框的内容传给后台
        table.reload('categoryList', {
            url: '/CategoryManage/GetCategoriesList',
            page: {
                curr: 1//从第一页开始（很关键）
            },
            where: {
                queryInfo: selectInfo, //需传过去的值
            }
        });
    };
</script>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
