#pragma checksum "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2dbb065a8b5a2a4c110be41a009e3a623d5ffa5acd00a555c5e19804207074ab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_Import), @"mvc.1.0.view", @"/Views/Student/Import.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\_ViewImports.cshtml"
using SchoolManagementApp

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\_ViewImports.cshtml"
using SchoolManagementApp.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"2dbb065a8b5a2a4c110be41a009e3a623d5ffa5acd00a555c5e19804207074ab", @"/Views/Student/Import.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a0e4525dd096cfb8cfa0e4a6c49db1c78407bc91315b95d0f951e6537df899fe", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Student_Import : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SchoolManagementApp.Models.Student>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
  
    ViewData["Title"] = "批量导入学生信息";

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<h1>批量导入学生信息</h1>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2dbb065a8b5a2a4c110be41a009e3a623d5ffa5acd00a555c5e19804207074ab4441", async() => {
                WriteLiteral("\r\n    ");
                Write(
#nullable restore
#line 10 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
     Html.AntiForgeryToken()

#line default
#line hidden
#nullable disable
                );
                WriteLiteral("\r\n\r\n");
#nullable restore
#line 12 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
     if (ViewData.ModelState != null && ViewData.ModelState.ErrorCount > 0)
    {

#line default
#line hidden
#nullable disable

                WriteLiteral("        <div class=\"alert alert-danger\">\r\n            <strong>导入失败!</strong>\r\n            <ul>\r\n");
#nullable restore
#line 17 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
                 foreach (var error in ViewData.ModelState.Values.SelectMany(v => v.Errors))
                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                    <li>");
                Write(
#nullable restore
#line 19 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
                         error.ErrorMessage

#line default
#line hidden
#nullable disable
                );
                WriteLiteral("</li>\r\n");
#nullable restore
#line 20 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
                }

#line default
#line hidden
#nullable disable

                WriteLiteral("            </ul>\r\n        </div>\r\n");
#nullable restore
#line 23 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Student\Import.cshtml"
    }

#line default
#line hidden
#nullable disable

                WriteLiteral("\r\n    <div class=\"form-group\">\r\n        <label for=\"file\">选择 Excel 文件</label>\r\n        <input type=\"file\" class=\"form-control-file\" id=\"file\" name=\"file\" accept=\".xlsx\">\r\n    </div>\r\n\r\n    <button type=\"submit\" class=\"btn btn-primary\">导入</button>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SchoolManagementApp.Models.Student>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
