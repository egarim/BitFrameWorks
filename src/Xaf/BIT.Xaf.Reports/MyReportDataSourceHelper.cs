using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base.ReportsV2;
using System;

namespace BIT.Xaf.Reports
{
    public class MyReportDataSourceHelper : ReportDataSourceHelper
    {
        IObjectSpaceProvider objectSpaceProvider;
        public MyReportDataSourceHelper(IObjectSpaceProvider objectSpaceProvider)
            : base(null)
        {
            this.objectSpaceProvider = objectSpaceProvider;
        }
        protected override IReportObjectSpaceProvider CreateReportObjectSpaceProvider()
        {
            return new MyReportObjectSpaceProvider(objectSpaceProvider);
        }
    }
}
