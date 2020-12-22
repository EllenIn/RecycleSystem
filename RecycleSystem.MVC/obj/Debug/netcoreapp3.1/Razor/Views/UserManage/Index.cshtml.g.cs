#pragma checksum "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\UserManage\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e57456bd6ab43f4e61d45b52e67358b635e5cfd0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserManage_Index), @"mvc.1.0.view", @"/Views/UserManage/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e57456bd6ab43f4e61d45b52e67358b635e5cfd0", @"/Views/UserManage/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0f17171c1af4e388c1fbe24a8c8e14fb7d04498", @"/Views/_ViewImports.cshtml")]
    public class Views_UserManage_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\Senkuu\RecycleSystem\RecycleSystem.MVC\Views\UserManage\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""layui-row "">

    <div class=""layui-col-md2 layui-col-md-offset5 "" style=""margin-top:20px;""><input type=""text"" id=""txt_Query"" placeholder=""请输入用户名"" class=""layui-input""></div>
    <div class=""layui-col-md1"" style=""margin-top:20px;""> <button class=""layui-btn"" onclick=""btn_search()"">搜索</button></div>

</div>

<table class=""layui-hide"" id=""userList"" lay-filter=""userList""></table>

<script type=""text/html"" id=""toolbarDemo"">
    <div class=""layui-btn-container"">
        <button class=""layui-btn layui-btn-sm"" onclick=""btn_add()""><i class=""layui-icon""></i>添加用户信息</button>
        <button class=""layui-btn layui-btn-sm"" lay-event=""update""><i class=""layui-icon""></i>修改选中信息</button>
");
            WriteLiteral(@"        <button class=""layui-btn layui-btn-sm layui-btn-warm"" lay-event=""repwd""><i class=""layui-icon""></i>重置密码</button>
        <button class=""layui-btn layui-btn-sm   layui-btn-danger"" lay-event=""checkrole""><i class=""layui-icon""></i>分配角色</button>
    </div>
</script>

<script type=""text/html"" id=""barDemo"">
    <a class=""layui-btn layui-btn-xs"" lay-event=""edit"">编辑</a>
    <a class=""layui-btn layui-btn-danger layui-btn-xs"" lay-event=""del"">删除</a>
</script>

<!-- 注意：如果你直接复制所有代码到本地，上述js路径需要改成你本地的 -->
<script type=""text/html"" id=""userState"">
    <input type=""checkbox"" value=""{{d.userId}}"" name=""open"" lay-skin=""switch"" lay-filter=""changeState"" lay-text=""在用|禁用"" {{");
            BeginWriteAttribute("d.delFlag", " d.delFlag =", 1591, "", 1603, 0);
            EndWriteAttribute();
            WriteLiteral(@"= false ? 'checked' : '' }}>
</script>
<script>
    var table; //放置全局中，方便后续使用
    layui.use('table', function () {
        table = layui.table;
        var form = layui.form;
        table.render({
            elem: '#userList'
            , url: '/UserManage/GetUserListJson'
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
                , { field: 'id', title: 'ID', wid");
            WriteLiteral(@"th: 80, fixed: 'left', unresize: true, sort: false }
                , { field: 'userId', title: '用户账号', width: 100, edit: 'text' }
                , { field: 'userName', title: '用户名', width: 120, edit: 'text' }
                , { field: 'tel', title: '电话号码', width: 150, edit: 'text', sort: false }
                , { field: 'email', title: '邮箱', width: 250 }
                , { field: 'roleName', title: '角色', width: 350 }
                , { field: 'departmentName', title: '所属部门', width: 180 }
                , {
                    field: 'gender', title: '性别', width: 80, sort: false, templet: function (res) {
                        if (res.gender == false) {
                            return ""女""
                        } else {

                            return ""男""
                        }
                    }
                }
                , { field: 'addTime', title: '添加时间', width: 200 }
                , { field: 'delFlag', title: '用户状态', width: 130, sort: false, templet: '#u");
            WriteLiteral(@"serState' }
            ]]
            , page: true
            , limit: 10
            , limits: [10, 20, 25, 30, 35, 40, 45, 50, 100]
        });
        //监听状态操作
        form.on('switch(changeState)', function (obj) {
            //layer.tips(this.value + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
            $.ajax({
                url: ""/UserManage/ChangeState"",
                type: ""post"",
                data: {
                    id: this.value
                },
                success: function (res) {
                    layer.alert(res);
                    btn_search();
                }
            })
        });
        //头工具栏事件
        table.on('toolbar(userList)', function (obj) {
            debugger;
            var checkStatus = table.checkStatus(obj.config.id);
            switch (obj.event) {
                case 'repwd':
                    var Id = [];
                    var data = checkStatus.data;
                    if (data.length == null");
            WriteLiteral(@") {
                        return layer.alert(""请选中条目！"");
                    }
                    for (var i = 0; i < data.length; i++) {
                        Id.push(data[i]['id'])
                    };

                    $.ajax({
                        url: ""/UserManage/Repwd"",
                        type: ""post"",
                        data: {
                            Id: Id
                        },
                        success: function (res) {
                            layer.alert(res);
                            btn_search();
                        }
                    });
                    break;
                case 'update':
                    debugger;
                    var data = checkStatus.data;
                    if (data.length == 1) {
                        var id = data[0].id;
                        layui.use('layer', function () {
                            layer.open({
                                type: 2,
                      ");
            WriteLiteral(@"          title: '修改用户信息',
                                shade: 0.8,
                                maxmin: true, //开启最大化最小化按钮
                                area: ['893px', '600px'],
                                content: '/UserManage/Update?Id=' + id,
                                end: function () {
                                    btn_search();
                                }
                            });
                        })
                    } else {
                        return layer.alert(""必须选中一条且只能选中一条！"")
                    }
                    
                    break;
                //case 'delete':
                //    var data = checkStatus.data[0].id;
                //    if (confirm(""确定删除吗？"")) {
                //        $.ajax({
                //            url: ""/User/Del"",
                //            type: ""post"",
                //            data: { id: data },
                //            success: function (res) {
                /");
            WriteLiteral(@"/                if (res == ""1001"") {
                //                    layer.alert(""删除成功！"");
                //                    btn_search();
                //                } else {
                //                    layer.alert(""删除失败！"");
                //                    btn_search();
                //                }

                //            }
                //        })
                //    }

                //    break;
                case 'checkrole':

                    layui.use('layer', function () {
                        layer.open({
                            type: 2,
                            title: '修改用户信息',
                            shade: 0.8,
                            maxmin: true, //开启最大化最小化按钮
                            area: ['893px', '600px'],
                            content: '/User/AddRoleForUser?userId=' + data,
                            end: function () {
                                btn_search();
                 ");
            WriteLiteral(@"           }
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
        debugger;
        var selectInfo = $(""#txt_Query"").val();

        //重新加载表格，并把文本框的内容传给后台
        table.reload('userList', {
            url: '/UserManage/GetUserListJson',
            page: {
                curr: 1//从第一页开始（很关键）
            },
            where: {
                queryInfo: selectInfo, //需传过去的值
            }
        });
    };
    btn_add = function () {
        debugger;
        layui.use('layer', function () {
            layer.open({
                type: 2,
                title: '添加用户',
                shade: 0.8,
                maxmin: true, //开启最大化最小化按钮
                area: ['893px', '600px'],
                content: '/User");
            WriteLiteral("Manage/AddUserPage\'\r\n            });\r\n        })\r\n    };\r\n</script>\r\n\r\n");
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
