#pragma checksum "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "29f90f61d399bd3770c3a0896b4f3dcb330b6c8cd83753038d1dff62a6b89e27"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Courses_Index), @"mvc.1.0.view", @"/Views/Courses/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"29f90f61d399bd3770c3a0896b4f3dcb330b6c8cd83753038d1dff62a6b89e27", @"/Views/Courses/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a0e4525dd096cfb8cfa0e4a6c49db1c78407bc91315b95d0f951e6537df899fe", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Courses_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SchoolManagementApp.Models.Course>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-primary me-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
  
    ViewData["Title"] = "课程管理";

#line default
#line hidden
#nullable disable

            WriteLiteral("<div class=\"container mt-4\">\r\n    <div class=\"d-flex justify-content-between align-items-center mb-4\">\r\n        <h1 class=\"display-5 fw-bold text-primary\">\r\n            <i class=\"fas fa-book me-2\"></i>");
            Write(
#nullable restore
#line 8 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                             ViewData["Title"]

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n        </h1>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29f90f61d399bd3770c3a0896b4f3dcb330b6c8cd83753038d1dff62a6b89e276106", async() => {
                WriteLiteral("\r\n            <i class=\"fas fa-plus-circle me-1\"></i>添加新课程\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
    <div class=""card shadow-sm"">
        <div class=""card-body"">
            <div class=""table-responsive"">
                <table class=""table table-hover align-middle"">
                    <thead class=""table-light"">
                        <tr>
                            <th>课程编号</th>
                            <th>课程名</th>
                            <th>开课院系</th>
                            <th>学分</th>
                            <th class=""text-center"">操作</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 28 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                         foreach (var course in Model)
                        {

#line default
#line hidden
#nullable disable

            WriteLiteral("                            <tr>\r\n                                <td>");
            Write(
#nullable restore
#line 31 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                     course.CourseCode

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                                <td>");
            Write(
#nullable restore
#line 32 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                     course.CourseName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                                <td>");
            Write(
#nullable restore
#line 33 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                     course.Department

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                                <td>");
            Write(
#nullable restore
#line 34 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                     course.Credit

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                                <td class=\"text-center\">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29f90f61d399bd3770c3a0896b4f3dcb330b6c8cd83753038d1dff62a6b89e279856", async() => {
                WriteLiteral("编辑");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#nullable restore
#line 36 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                                                        course.CourseCode

#line default
#line hidden
#nullable disable
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29f90f61d399bd3770c3a0896b4f3dcb330b6c8cd83753038d1dff62a6b89e2712235", async() => {
                WriteLiteral("删除");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#nullable restore
#line 37 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                                                                          course.CourseCode

#line default
#line hidden
#nullable disable
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 40 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Courses\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable

            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div> ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SchoolManagementApp.Models.Course>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
