#pragma checksum "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d0a47a9ee31f37902c12ccbcddc02289e375267f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuario_VistaUsuarios), @"mvc.1.0.view", @"/Views/Usuario/VistaUsuarios.cshtml")]
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
#line 1 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\_ViewImports.cshtml"
using Tp3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\_ViewImports.cshtml"
using Tp3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0a47a9ee31f37902c12ccbcddc02289e375267f", @"/Views/Usuario/VistaUsuarios.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64ba4ccb3b3f376e7677acca0cce187b5395598e", @"/Views/_ViewImports.cshtml")]
    public class Views_Usuario_VistaUsuarios : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Tp3.Models.ViewModels.UsuarioViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
  
    ViewData["Title"] = "VistaUsuarios";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>VistaUsuarios</h1>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 13 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>            \r\n            <th>\r\n                ");
#nullable restore
#line 19 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.DisplayNameFor(model => model.Rol));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 25 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 28 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 31 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.DisplayFor(modelItem => item.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>            \r\n            <td>\r\n                ");
#nullable restore
#line 34 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.DisplayFor(modelItem => item.Rol));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.ActionLink("Modificar", "Edit", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 38 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
           Write(Html.ActionLink("Borrar", "Delete", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 41 "C:\Users\Caro\source\repos\tp032021-CarOmodeo\Tp3\Tp3\Views\Usuario\VistaUsuarios.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Tp3.Models.ViewModels.UsuarioViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
