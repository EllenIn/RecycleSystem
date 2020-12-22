#pragma checksum "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\RoleManage\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e692627f430e344f088acc0e2a858b4ef1129261"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RoleManage_Index), @"mvc.1.0.view", @"/Views/RoleManage/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e692627f430e344f088acc0e2a858b4ef1129261", @"/Views/RoleManage/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0f17171c1af4e388c1fbe24a8c8e14fb7d04498", @"/Views/_ViewImports.cshtml")]
    public class Views_RoleManage_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\RoleManage\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""layui-row "">

    <div class=""layui-col-md2 layui-col-md-offset5 "" style=""margin-top:20px;""><input type=""text"" id=""txt_Query"" placeholder=""请输入角色名"" class=""layui-input""></div>
    <div class=""layui-col-md1"" style=""margin-top:20px;""> <button class=""layui-btn"" onclick=""btn_search()"">搜索</button></div>

</div>

<table class=""layui-hide"" id=""roleDataTable"" lay-filter=""roleDataTable""></table>

<script type=""text/html"" id=""toolbarDemo"">
    <div class=""layui-btn-container"">
        <button class=""layui-btn layui-btn-sm"" onclick=""btn_add()""><i class=""layui-icon""></i>添加角色信息</button>
        <button class=""layui-btn layui-btn-sm"" lay-event=""update""><i class=""layui-icon""></i>修改角色信息</button>
");
            WriteLiteral(@"        <button class=""layui-btn layui-btn-sm   layui-btn-danger"" lay-event=""checkrole""><i class=""layui-icon""></i>为角色分配权限</button>
    </div>
</script>


<!-- 注意：如果你直接复制所有代码到本地，上述js路径需要改成你本地的 -->
<script type=""text/html"" id=""roleState"">
    <input type=""checkbox"" value=""{{d.roleId}}"" name=""open"" lay-skin=""switch"" lay-filter=""changeState"" lay-text=""在用|禁用"" {{");
            BeginWriteAttribute("d.delFlag", " d.delFlag =", 1262, "", 1274, 0);
            EndWriteAttribute();
            WriteLiteral(@"= false ? 'checked' : '' }}>
</script>

<script>
    var table; //放置全局中，方便后续使用
    layui.use('table', function () {
        table = layui.table;
        var form = layui.form;
        table.render({
            elem: '#roleDataTable'
            , url: '/RoleManage/GetRoleInfo'
            , toolbar: '#toolbarDemo' //开启头部工具栏，并为其绑定左侧模板
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
                , { field: 'roleId', title: '角色ID', width: 80, fixed: 'left");
            WriteLiteral(@"', unresize: true, sort: false }
                , { field: 'roleName', title: '角色名称', width: 120 }
                , { field: 'description', title: '角色描述', width: 350 }
                , { field: 'delFlag', title: '角色状态', width: 130, sort: false, templet: '#roleState' }
                , { field: 'addTime', title: '添加时间', width: 200 }
            ]]
            , page: true
            , limit: 5
            , limits: [5, 10, 20, 25, 30, 35, 40, 45, 50, 100]
        });
        //监听状态操作
        form.on('switch(changeState)', function (obj) {
            //layer.tips(this.value + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
            $.ajax({
                url: ""/RoleManage/ChangeState"",
                type: ""post"",
                data: {
                    roleId: this.value
                },
                success: function (res) {
                    layer.alert(res);
                }
            })
        });
        //头工具栏事件
        table.on('toolbar(roleDataTable)', function (obj) {
");
            WriteLiteral(@"            debugger;
            var checkStatus = table.checkStatus(obj.config.id);
            switch (obj.event) {
                case 'update':
                    var data = checkStatus.data;
                    if (data.length == 1) {
                        var id = data[0].id;
                        layui.use('layer', function () {
                            layer.open({
                                type: 2,
                                title: '修改角色信息',
                                shade: 0.8,
                                maxmin: true, //开启最大化最小化按钮
                                area: ['893px', '600px'],
                                content: '/RoleManage/Update?id=' + id,
                                end: function () {
                                    btn_search();
                                }
                            });
                        })
                    } else {
                        layer.alert(""请选中且只能选中一条数据！"");
                    }
           ");
            WriteLiteral(@"       
                    break;
                case 'checkrole':
                    var data = checkStatus.data[0].roleId;
                    layui.use('layer', function () {
                        layer.open({
                            type: 2,
                            title: '分配权限',
                            shade: 0.8,
                            maxmin: true, //开启最大化最小化按钮
                            area: ['893px', '600px'],
                            content: '/Power/AttributePowerPage?roleId=' + data,
                            end: function () {
                                btn_search();
                            }
                        });
                    })
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
        var selectInfo = $(""#txt_Query"").val();

        //重新加载表格");
            WriteLiteral(@"，并把文本框的内容传给后台
        table.reload('roleDataTable', {
            url: '/RoleManage/GetRoleInfo',
            page: {
                curr: 1//从第一页开始（很关键）
            },
            where: {
                queryInfo: selectInfo, //需传过去的值
            }
        });
    };
    btn_add = function () {
        layui.use('layer', function () {
            layer.open({
                type: 2,
                title: '添加角色',
                shade: 0.8,
                maxmin: true, //开启最大化最小化按钮
                area: ['893px', '600px'],
                content: '/RoleManage/AddRole',
                end: function () {
                    btn_search();
                }
            });
        })
    };
</script>");
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
