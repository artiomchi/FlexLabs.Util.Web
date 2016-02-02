﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexLabs.Web.TablePager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;

#line 5 "..\..\TablePager.cshtml"
    using System.Web.Mvc;

#line default
#line hidden
    using System.Web.Mvc.Ajax;

#line 6 "..\..\TablePager.cshtml"
    using System.Web.Mvc.Html;

#line default
#line hidden
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;

#line 4 "..\..\TablePager.cshtml"
    using PagedList;

#line default
#line hidden

    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class TablePager : System.Web.WebPages.HelperPage
    {

#line 7 "..\..\TablePager.cshtml"

        [Obsolete("This method is obsolete. Use Html.Pager() instead")]
        public static HelperResult Pager<T>(IPagedList<T> pagedList, String label = "Page: ")
        {
            if (pagedList.PageCount == 0)
            {
                return null;
            }
            var model = new PagedListData(pagedList.PageNumber, pagedList.PageSize, pagedList.PageCount, pagedList.TotalItemCount);
            return Pager(model, label);
        }

#line default
#line hidden

        [Obsolete("This method is obsolete. Use Html.Pager() instead")]
#line 17 "..\..\TablePager.cshtml"
        public static System.Web.WebPages.HelperResult Pager(PagedListData model, String label)
        {
#line default
#line hidden
            return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 17 "..\..\TablePager.cshtml"



#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "    <div");

                WriteLiteralTo(__razor_helper_writer, " class=\"float-right\"");

                WriteLiteralTo(__razor_helper_writer, ">\r\n        <label>");


#line 19 "..\..\TablePager.cshtml"
                WriteTo(__razor_helper_writer, label);


#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "</label>\r\n        <ul>\r\n");


#line 21 "..\..\TablePager.cshtml"


#line default
#line hidden

#line 21 "..\..\TablePager.cshtml"
                if (!model.CanSeeFirstPage())
                {


#line default
#line hidden
                    WriteLiteralTo(__razor_helper_writer, "            <li>\r\n                <button");

                    WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

                    WriteLiteralTo(__razor_helper_writer, " name=\"page\"");

                    WriteLiteralTo(__razor_helper_writer, " value=\"1\"");

                    WriteLiteralTo(__razor_helper_writer, ">1</button>.\r\n            </li>\r\n");


#line 25 "..\..\TablePager.cshtml"
                }


#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "        ");


#line 26 "..\..\TablePager.cshtml"
                foreach (var page in model.PageRange)
                {
                    if (page == model.PageNumber)
                    {


#line default
#line hidden
                        WriteLiteralTo(__razor_helper_writer, "                <li>");


#line 28 "..\..\TablePager.cshtml"
                        WriteTo(__razor_helper_writer, page);


#line default
#line hidden
                        WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 29 "..\..\TablePager.cshtml"
                    }
                    else {


#line default
#line hidden
                        WriteLiteralTo(__razor_helper_writer, "                <li><button");

                        WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

                        WriteLiteralTo(__razor_helper_writer, " name=\"page\"");

                        WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 1047), Tuple.Create("\"", 1060)

#line 30 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 1055), Tuple.Create<System.Object, System.Int32>(page

#line default
#line hidden
, 1055), false)
                        );

                        WriteLiteralTo(__razor_helper_writer, ">");


#line 30 "..\..\TablePager.cshtml"
                        WriteTo(__razor_helper_writer, page);


#line default
#line hidden
                        WriteLiteralTo(__razor_helper_writer, "</button></li>\r\n");


#line 31 "..\..\TablePager.cshtml"
                    }
                }


#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "        ");


#line 33 "..\..\TablePager.cshtml"
                if (!model.CanSeeLastPage())
                {


#line default
#line hidden
                    WriteLiteralTo(__razor_helper_writer, "            <li><button");

                    WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

                    WriteLiteralTo(__razor_helper_writer, " name=\"page\"");

                    WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 1199), Tuple.Create("\"", 1223)

#line 34 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 1207), Tuple.Create<System.Object, System.Int32>(model.PageCount

#line default
#line hidden
, 1207), false)
                    );

                    WriteLiteralTo(__razor_helper_writer, ">");


#line 34 "..\..\TablePager.cshtml"
                    WriteTo(__razor_helper_writer, model.PageCount);


#line default
#line hidden
                    WriteLiteralTo(__razor_helper_writer, "</button></li>\r\n");


#line 35 "..\..\TablePager.cshtml"
                }


#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "        </ul>\r\n    </div>\r\n");


#line 38 "..\..\TablePager.cshtml"


#line default
#line hidden
            });

#line 38 "..\..\TablePager.cshtml"
        }
#line default
#line hidden

        [Obsolete("This method is obsolete. Use Html.PageSizer() instead")]
#line 40 "..\..\TablePager.cshtml"
        public static System.Web.WebPages.HelperResult PageSizer(System.Web.Mvc.HtmlHelper html, String label = "Page Size:", Int32[] pageSizes = null, Int32? currentSize = null)
        {
#line default
#line hidden
            return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 40 "..\..\TablePager.cshtml"



#line default
#line hidden

#line 41 "..\..\TablePager.cshtml"
                WriteTo(__razor_helper_writer, html.Label("PageSize", label));


#line default
#line hidden

#line 41 "..\..\TablePager.cshtml"



#line default
#line hidden

#line 42 "..\..\TablePager.cshtml"
                WriteTo(__razor_helper_writer, html.DropDownList("PageSize", TableModel.GetPageSizes(pageSizes, currentSize), new { onchange = "$(this).closest('form').submit();" }));


#line default
#line hidden

#line 42 "..\..\TablePager.cshtml"



#line default
#line hidden

#line 43 "..\..\TablePager.cshtml"
                WriteTo(__razor_helper_writer, html.ValidationMessage("PageSize"));


#line default
#line hidden

#line 43 "..\..\TablePager.cshtml"



#line default
#line hidden
            });

#line 44 "..\..\TablePager.cshtml"
        }
#line default
#line hidden

        [Obsolete("This method is obsolete. Use Html.TableHeader() instead")]
#line 46 "..\..\TablePager.cshtml"
        public static System.Web.WebPages.HelperResult Header(IEnumerable<ITableHeader> headers)
        {
#line default
#line hidden
            return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 46 "..\..\TablePager.cshtml"



#line default
#line hidden

#line 47 "..\..\TablePager.cshtml"
                WriteTo(__razor_helper_writer, Header(new[] { headers }));


#line default
#line hidden

#line 47 "..\..\TablePager.cshtml"



#line default
#line hidden
            });

#line 48 "..\..\TablePager.cshtml"
        }
#line default
#line hidden

        [Obsolete("This method is obsolete. Use Html.TableHeader() instead")]
#line 49 "..\..\TablePager.cshtml"
        public static System.Web.WebPages.HelperResult Header(IEnumerable<ITableHeader>[] headersSet)
        {
#line default
#line hidden
            return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 49 "..\..\TablePager.cshtml"



#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "    <thead>\r\n");


#line 51 "..\..\TablePager.cshtml"


#line default
#line hidden

#line 51 "..\..\TablePager.cshtml"
                foreach (var headers in headersSet)
                {


#line default
#line hidden
                    WriteLiteralTo(__razor_helper_writer, "            <tr>\r\n");


#line 53 "..\..\TablePager.cshtml"


#line default
#line hidden

#line 53 "..\..\TablePager.cshtml"
                    foreach (var iheader in headers.Where(h => h.IsRendered))
                    {


#line default
#line hidden
                        WriteLiteralTo(__razor_helper_writer, "                    <th");

                        WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 1956), Tuple.Create("\"", 1981)

#line 54 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 1964), Tuple.Create<System.Object, System.Int32>(iheader.CssClass

#line default
#line hidden
, 1964), false)
                        );

                        WriteAttributeTo(__razor_helper_writer, "colspan", Tuple.Create(" colspan=\"", 1982), Tuple.Create("\"", 2008)

#line 54 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 1992), Tuple.Create<System.Object, System.Int32>(iheader.ColSpan

#line default
#line hidden
, 1992), false)
                        );

                        WriteAttributeTo(__razor_helper_writer, "rowspan", Tuple.Create(" rowspan=\"", 2009), Tuple.Create("\"", 2035)

#line 54 "..\..\TablePager.cshtml"
      , Tuple.Create(Tuple.Create("", 2019), Tuple.Create<System.Object, System.Int32>(iheader.RowSpan

#line default
#line hidden
, 2019), false)
                        );

                        WriteLiteralTo(__razor_helper_writer, ">");


#line 54 "..\..\TablePager.cshtml"

                        var header = iheader as TableHeader;
                        if (iheader is CustomTableHeader)
                        {


#line default
#line hidden

#line 57 "..\..\TablePager.cshtml"
                            WriteTo(__razor_helper_writer, (iheader as CustomTableHeader).Content(null));


#line default
#line hidden

#line 57 "..\..\TablePager.cshtml"

                        }
                        else {
                            if (header.Value != null)
                            {

#line default
#line hidden
                                WriteLiteralTo(__razor_helper_writer, "<button");

                                WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

                                WriteLiteralTo(__razor_helper_writer, " name=\"changeSort\"");

                                WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 2401), Tuple.Create("\"", 2422)

#line 60 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 2409), Tuple.Create<System.Object, System.Int32>(header.Value

#line default
#line hidden
, 2409), false)
                                );

                                WriteAttributeTo(__razor_helper_writer, "title", Tuple.Create(" title=\"", 2423), Tuple.Create("\"", 2446)

#line 60 "..\..\TablePager.cshtml"
                      , Tuple.Create(Tuple.Create("", 2431), Tuple.Create<System.Object, System.Int32>(header.ToolTip

#line default
#line hidden
, 2431), false)
                                );

                                WriteLiteralTo(__razor_helper_writer, ">");


#line 60 "..\..\TablePager.cshtml"
                                WriteTo(__razor_helper_writer, header.Title);


#line default
#line hidden
                                WriteLiteralTo(__razor_helper_writer, "</button>");


#line 60 "..\..\TablePager.cshtml"
                            }
                            else {


#line default
#line hidden

#line 62 "..\..\TablePager.cshtml"
                                WriteTo(__razor_helper_writer, header.Title);


#line default
#line hidden

#line 62 "..\..\TablePager.cshtml"

                            }
                        }


#line default
#line hidden
                        WriteLiteralTo(__razor_helper_writer, "</th>\r\n");


#line 66 "..\..\TablePager.cshtml"
                    }


#line default
#line hidden
                    WriteLiteralTo(__razor_helper_writer, "            </tr>\r\n");


#line 68 "..\..\TablePager.cshtml"
                }


#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "    </thead>\r\n");


#line 70 "..\..\TablePager.cshtml"


#line default
#line hidden
            });

#line 70 "..\..\TablePager.cshtml"
        }
#line default
#line hidden

        [Obsolete("This method is obsolete. Use Html.EnableFormSorting() instead")]
#line 72 "..\..\TablePager.cshtml"
        public static System.Web.WebPages.HelperResult FormHidden(ITableModel model)
        {
#line default
#line hidden
            return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 72 "..\..\TablePager.cshtml"



#line default
#line hidden

#line 73 "..\..\TablePager.cshtml"



#line default
#line hidden
                WriteLiteralTo(__razor_helper_writer, "    <input");

                WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

                WriteLiteralTo(__razor_helper_writer, " name=\"SortBy\"");

                WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 2946), Tuple.Create("\"", 2967)

#line 74 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 2954), Tuple.Create<System.Object, System.Int32>(model.SortBy

#line default
#line hidden
, 2954), false)
                );

                WriteLiteralTo(__razor_helper_writer, " />\r\n");

                WriteLiteralTo(__razor_helper_writer, "    <input");

                WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

                WriteLiteralTo(__razor_helper_writer, " name=\"SortAsc\"");

                WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 3012), Tuple.Create("\"", 3045)

#line 75 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 3020), Tuple.Create<System.Object, System.Int32>(model.SortAsc.ToString()

#line default
#line hidden
, 3020), false)
                );

                WriteLiteralTo(__razor_helper_writer, " />\r\n");


#line 76 "..\..\TablePager.cshtml"
                if (model.FirstItemID.HasValue)
                {


#line default
#line hidden
                    WriteLiteralTo(__razor_helper_writer, "        <input");

                    WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

                    WriteLiteralTo(__razor_helper_writer, " name=\"FirstItemID\"");

                    WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 3137), Tuple.Create("\"", 3163)

#line 77 "..\..\TablePager.cshtml"
, Tuple.Create(Tuple.Create("", 3145), Tuple.Create<System.Object, System.Int32>(model.FirstItemID

#line default
#line hidden
, 3145), false)
                    );

                    WriteLiteralTo(__razor_helper_writer, " />\r\n");


#line 78 "..\..\TablePager.cshtml"
                }


#line default
#line hidden
            });

#line 79 "..\..\TablePager.cshtml"
        }
#line default
#line hidden

    }
}
#pragma warning restore 1591