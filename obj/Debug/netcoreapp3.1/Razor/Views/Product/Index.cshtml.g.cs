#pragma checksum "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "18a9fc878d4e92268c7269225e0f543ab5722e4f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Index), @"mvc.1.0.view", @"/Views/Product/Index.cshtml")]
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
#line 1 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\_ViewImports.cshtml"
using aspcore_watchshop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\_ViewImports.cshtml"
using aspcore_watchshop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"18a9fc878d4e92268c7269225e0f543ab5722e4f", @"/Views/Product/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bcc9903d68fd33c13bb283de0d0301e562f01f82", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/product.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
  
    ViewData["Title"]=ViewBag.PageTitle;
    var pageCode = ViewBag.PageCode;
    var banner = ViewBag.PageCode < 0 ? "banner-sm" : "banner-lg"; 

#line default
#line hidden
#nullable disable
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "18a9fc878d4e92268c7269225e0f543ab5722e4f3826", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("   \r\n");
            }
            );
            WriteLiteral("<span id=\"page\" data-code=");
#nullable restore
#line 9 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
                     Write(pageCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("></span>\r\n<!--Banner lager-->\r\n<div class=\"container-fluid\">\r\n    <div");
            BeginWriteAttribute("class", " class=\"", 331, "\"", 354, 2);
#nullable restore
#line 12 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
WriteAttributeValue("", 339, banner, 339, 7, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 346, "section", 347, 8, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n        <p class=\"white mb-0\">");
#nullable restore
#line 13 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
                         Write(ViewBag.PageTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n</div>\r\n<!--List product-->\r\n<div class=\"container\">\r\n    <!--Filter-->\r\n");
#nullable restore
#line 19 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
     if (pageCode == -1)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p style=\"padding-left: 1.45em\" >Có ");
#nullable restore
#line 21 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
                                       Write(ViewBag.SearchResult);

#line default
#line hidden
#nullable disable
            WriteLiteral(" sản phẩm phù hợp</p>\r\n");
#nullable restore
#line 22 "C:\Users\TOAN\Documents\Srouce\aspcore-watchshop\Views\Product\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""filter contents mb-4"">
        <div class=""row align-content-center"">
            <div id=""orderby"" class=""d-flex"">
                <a class=""nav-item active"" data-index=""0"" title=""Phổ biến"" >phổ biển</a>
                <a class=""nav-item"" data-index=""1"" title=""Giá cao"">giá cao</a>
                <a class=""nav-item"" data-index=""2"" title=""Giá thấp"">giá thấp</a>
            </div>
            <div class=""dropdown ml-lg-auto pl-2 pl-lg-0"">
                <div class=""select"">
                    <span>Loại dây</span>
                    <i class=""las la-angle-down""></i>
                </div>
                <input id=""filter"" type=""hidden"" name=""filter"">
                <ul class=""dropdown-menu bg-white"">
                    <li data-code=""0"">Tất cả</li>
                    <li data-code=""1"">Dây Da</li>
                    <li data-code=""2"">Dây Kim Loại</li>
                    <li data-code=""3"">Dây Vải</li>
                    <li data-code=""4"">Dây V</li>
                </");
            WriteLiteral(@"ul>
            </div>
        </div>
    </div>
    <!--Products-->
    <div class=""contents"">
        <div id=""product-container"">
            <div class=""loader""><div></div><div></div><div></div><div></div></div>
        </div>
    </div>
    <!--Pagnation-->
    <div class=""pagnation contents section"">
    </div> 
</div>


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
