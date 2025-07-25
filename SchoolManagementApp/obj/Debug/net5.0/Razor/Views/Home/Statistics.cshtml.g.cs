#pragma checksum "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "c10874e554b920ab14cf94aec1c89cd1b231c966a309ce826934d6344b88ef9f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Statistics), @"mvc.1.0.view", @"/Views/Home/Statistics.cshtml")]
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

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
 using Newtonsoft.Json

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"c10874e554b920ab14cf94aec1c89cd1b231c966a309ce826934d6344b88ef9f", @"/Views/Home/Statistics.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a0e4525dd096cfb8cfa0e4a6c49db1c78407bc91315b95d0f951e6537df899fe", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Statistics : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SchoolManagementApp.Controllers.HomeController.SchoolStatisticsViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Statistics", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mb-3 d-flex align-items-center gap-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
  
    ViewData["Title"] = "班级与全校成绩统计";
    var classNames = Model.ClassStats.Select(c => c.ClassName).ToList();
    var avgScores = Model.ClassStats.Select(c => c.AverageScore).ToList();
    var maxScores = Model.ClassStats.Select(c => c.MaxScore).ToList();
    var minScores = Model.ClassStats.Select(c => c.MinScore).ToList();
    var passRates = Model.ClassStats.Select(c => (c.PassRate * 100).ToString("F1")).ToList();
    var excellentRates = Model.ClassStats.Select(c => (c.ExcellentRate * 100).ToString("F1")).ToList();
    var scoreRanges = Model.ScoreRanges;
    var allScores = Model.AllScores;
    var selectedClassId = Context.Request.Query["classId"].ToString();

#line default
#line hidden
#nullable disable

            WriteLiteral("<div class=\"container py-5\">\r\n    <h2 class=\"mb-4 text-primary\">成绩统计分析</h2>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c10874e554b920ab14cf94aec1c89cd1b231c966a309ce826934d6344b88ef9f6076", async() => {
                WriteLiteral("\r\n        <label for=\"classId\" class=\"form-label mb-0 me-2\">选择班级：</label>\r\n        <select id=\"classId\" name=\"classId\" class=\"form-select w-auto\" onchange=\"this.form.submit()\">\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c10874e554b920ab14cf94aec1c89cd1b231c966a309ce826934d6344b88ef9f6562", async() => {
                    WriteLiteral("全校");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 21 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
             foreach (var item in (IEnumerable<SelectListItem>)ViewBag.AllClasses)
            {
                if (selectedClassId == item.Value)
                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c10874e554b920ab14cf94aec1c89cd1b231c966a309ce826934d6344b88ef9f8215", async() => {
                    Write(
#nullable restore
#line 25 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                                          item.Text

#line default
#line hidden
#nullable disable
                    );
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
                WriteLiteral(
#nullable restore
#line 25 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                    item.Value

#line default
#line hidden
#nullable disable
                );
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 26 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c10874e554b920ab14cf94aec1c89cd1b231c966a309ce826934d6344b88ef9f10875", async() => {
                    Write(
#nullable restore
#line 29 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                                 item.Text

#line default
#line hidden
#nullable disable
                    );
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
                WriteLiteral(
#nullable restore
#line 29 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                    item.Value

#line default
#line hidden
#nullable disable
                );
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 30 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                }
            }

#line default
#line hidden
#nullable disable

                WriteLiteral("        </select>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    <div class=""mb-3"">
        <button class=""btn btn-primary me-2"" onclick=""showTab('school')"">全校统计</button>
        <button class=""btn btn-outline-primary"" onclick=""showTab('class')"">班级对比</button>
    </div>
    <div id=""schoolTab"">
        <div class=""row mb-4"">
            <div class=""col-md-6"">
                <canvas id=""schoolPie"" height=""200""></canvas>
            </div>
            <div class=""col-md-6"">
                <canvas id=""schoolBar"" height=""200""></canvas>
            </div>
        </div>
        <div class=""row mb-4"">
            <div class=""col-md-6"">
                <canvas id=""schoolHistogram"" height=""200""></canvas>
            </div>
        </div>
    </div>
    <div id=""classTab"" style=""display:none;"">
        <div class=""row mb-4"">
            <div class=""col-md-12"">
                <canvas id=""classBar"" height=""120""></canvas>
            </div>
        </div>
        <div class=""row mb-4"">
            <div class=""col-md-6"">
                <canvas id=""c");
            WriteLiteral(@"lassPassBar"" height=""120""></canvas>
            </div>
            <div class=""col-md-6"">
                <canvas id=""classExcellentBar"" height=""120""></canvas>
            </div>
        </div>
    </div>
</div>
<script src=""https://cdn.jsdelivr.net/npm/chart.js""></script>
<script>
    window.showTab = function(tab) {
        document.getElementById('schoolTab').style.display = tab === 'school' ? '' : 'none';
        document.getElementById('classTab').style.display = tab === 'class' ? '' : 'none';
    }
    // 全校饼图（分数段分布）
    const pieData = {
        labels: ");
            Write(
#nullable restore
#line 77 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                 Html.Raw(JsonConvert.SerializeObject(scoreRanges.Keys))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(",\r\n        datasets: [{\r\n            data: ");
            Write(
#nullable restore
#line 79 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                   Html.Raw(JsonConvert.SerializeObject(scoreRanges.Values))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@",
            backgroundColor: [
                'rgba(54, 162, 235, 0.7)',
                'rgba(75, 192, 192, 0.7)',
                'rgba(255, 206, 86, 0.7)',
                'rgba(255, 159, 64, 0.7)',
                'rgba(255, 99, 132, 0.7)'
            ]
        }]
    };
    new Chart(document.getElementById('schoolPie').getContext('2d'), {
        type: 'pie',
        data: pieData,
        options: { plugins: { title: { display: true, text: '全校分数段分布' } } }
    });
    // 全校柱状图（及格率、优秀率）
    new Chart(document.getElementById('schoolBar').getContext('2d'), {
        type: 'bar',
        data: {
            labels: ['及格率', '优秀率'],
            datasets: [{
                label: '比例(%)',
                data: [");
            Write(
#nullable restore
#line 101 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                        Model.SchoolPassRate

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" * 100, ");
            Write(
#nullable restore
#line 101 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                                     Model.SchoolExcellentRate

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@" * 100],
                backgroundColor: ['rgba(54, 162, 235, 0.7)', 'rgba(255, 206, 86, 0.7)']
            }]
        },
        options: { plugins: { title: { display: true, text: '全校及格率/优秀率' } }, scales: { y: { beginAtZero: true, max: 100 } } }
    });
    // 全校直方图（成绩分布）
    const allScores = ");
            Write(
#nullable restore
#line 108 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                       Html.Raw(JsonConvert.SerializeObject(allScores))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@";
    function getHistogramBins(scores, binSize = 10) {
        const bins = Array(10).fill(0);
        scores.forEach(s => {
            let idx = Math.floor(s / binSize);
            if (idx >= bins.length) idx = bins.length - 1;
            bins[idx]++;
        });
        return bins;
    }
    const bins = getHistogramBins(allScores);
    new Chart(document.getElementById('schoolHistogram').getContext('2d'), {
        type: 'bar',
        data: {
            labels: ['0-9','10-19','20-29','30-39','40-49','50-59','60-69','70-79','80-89','90-100'],
            datasets: [{
                label: '人数',
                data: bins,
                backgroundColor: 'rgba(153, 102, 255, 0.7)'
            }]
        },
        options: { plugins: { title: { display: true, text: '全校成绩直方图' } }, scales: { y: { beginAtZero: true } } }
    });
    // 各班级平均分柱状图
    new Chart(document.getElementById('classBar').getContext('2d'), {
        type: 'bar',
        data: {
            labels: ");
            Write(
#nullable restore
#line 135 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                     Html.Raw(JsonConvert.SerializeObject(classNames))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(",\r\n            datasets: [\r\n                { label: \'平均分\', data: ");
            Write(
#nullable restore
#line 137 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                       Html.Raw(JsonConvert.SerializeObject(avgScores))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(", backgroundColor: \'rgba(54, 162, 235, 0.7)\' },\r\n                { label: \'最高分\', data: ");
            Write(
#nullable restore
#line 138 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                       Html.Raw(JsonConvert.SerializeObject(maxScores))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(", backgroundColor: \'rgba(255, 206, 86, 0.7)\' },\r\n                { label: \'最低分\', data: ");
            Write(
#nullable restore
#line 139 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                       Html.Raw(JsonConvert.SerializeObject(minScores))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@", backgroundColor: 'rgba(255, 99, 132, 0.7)' }
            ]
        },
        options: { plugins: { title: { display: true, text: '各班成绩对比' } }, scales: { y: { beginAtZero: true, max: 100 } } }
    });
    // 各班级及格率柱状图
    new Chart(document.getElementById('classPassBar').getContext('2d'), {
        type: 'bar',
        data: {
            labels: ");
            Write(
#nullable restore
#line 148 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                     Html.Raw(JsonConvert.SerializeObject(classNames))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(",\r\n            datasets: [{ label: \'及格率(%)\', data: ");
            Write(
#nullable restore
#line 149 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                                 Html.Raw(JsonConvert.SerializeObject(passRates))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@", backgroundColor: 'rgba(54, 162, 235, 0.7)' }]
        },
        options: { plugins: { title: { display: true, text: '各班及格率' } }, scales: { y: { beginAtZero: true, max: 100 } } }
    });
    // 各班级优秀率柱状图
    new Chart(document.getElementById('classExcellentBar').getContext('2d'), {
        type: 'bar',
        data: {
            labels: ");
            Write(
#nullable restore
#line 157 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                     Html.Raw(JsonConvert.SerializeObject(classNames))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(",\r\n            datasets: [{ label: \'优秀率(%)\', data: ");
            Write(
#nullable restore
#line 158 "C:\Users\19786\Desktop\新建文件夹 (2)\SchoolManagementApp\SchoolManagementApp\Views\Home\Statistics.cshtml"
                                                 Html.Raw(JsonConvert.SerializeObject(excellentRates))

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(", backgroundColor: \'rgba(255, 206, 86, 0.7)\' }]\r\n        },\r\n        options: { plugins: { title: { display: true, text: \'各班优秀率\' } }, scales: { y: { beginAtZero: true, max: 100 } } }\r\n    });\r\n</script> ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SchoolManagementApp.Controllers.HomeController.SchoolStatisticsViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
