using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Xpo;
using System;

namespace BIT.Xaf.Reports
{
    public class ReportHelper
    {

        // register your application types here
        private static void RegisterBOTypes(ITypesInfo typesInfo)
        {
            //TODO ask andres sobre los tipos de los reportes
            //typesInfo.RegisterEntity(typeof(OrdenDeServicio));
            //typesInfo.RegisterEntity(typeof(OrdenDeServicio));
            //typesInfo.RegisterEntity(typeof(ReportDataV2));
        }
        public static XPObjectSpaceProvider CreateObjectSpaceProvider(string connectionString)
        {
            XpoTypesInfoHelper.ForceInitialize();
            ITypesInfo typesInfo = XpoTypesInfoHelper.GetTypesInfo();
            XpoTypeInfoSource xpoTypeInfoSource = XpoTypesInfoHelper.GetXpoTypeInfoSource();
            RegisterBOTypes(typesInfo);
            ConnectionStringDataStoreProvider dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString);
            return new XPObjectSpaceProvider(dataStoreProvider, typesInfo, xpoTypeInfoSource);
        }
    }
}
