using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YTech.Inventory.JayaMesin.Web.Mvc
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                string rpt = Request.QueryString["rpt"];
                string toolbar = Request.QueryString["toolbar"];
                if (toolbar == "0")
                    rv.ShowToolBar = false;

                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = Server.MapPath(string.Format("~/Views/Reports/{0}.rdlc", rpt));
                rv.LocalReport.DataSources.Clear();

                ReportDataSource[] repCol = Session["ReportData"] as ReportDataSource[];
                if (repCol != null)
                {
                    foreach (ReportDataSource d in repCol)
                    {
                        rv.LocalReport.DataSources.Add(d);
                    }
                }

                if (Session["ReportParams"] != null)
                {
                    ReportParameterCollection paramCol = Session["ReportParams"] as ReportParameterCollection;
                    rv.LocalReport.SetParameters(paramCol);
                }

                rv.LocalReport.Refresh();
            }
        }
    }
}