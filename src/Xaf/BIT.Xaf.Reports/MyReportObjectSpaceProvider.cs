using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Base.ReportsV2;
using System;

namespace BIT.Xaf.Reports
{
    public class MyReportObjectSpaceProvider : IReportObjectSpaceProvider, IObjectSpaceCreator
    {
        IObjectSpaceProvider objectSpaceProvider;
        IObjectSpace objectSpace;
        public MyReportObjectSpaceProvider(IObjectSpaceProvider objectSpaceProvider)
        {
            this.objectSpaceProvider = objectSpaceProvider;
        }
        public void DisposeObjectSpaces()
        {
            if (objectSpace != null)
            {
                objectSpace.Dispose();
                objectSpace = null;
            }
        }
        public IObjectSpace GetObjectSpace(Type type)
        {
            if (objectSpace == null)
            {
                objectSpace = objectSpaceProvider.CreateObjectSpace();
            }
            return objectSpace;
        }
        public IObjectSpace CreateObjectSpace(Type type)
        {
            return objectSpaceProvider.CreateObjectSpace();
        }
    }
}
