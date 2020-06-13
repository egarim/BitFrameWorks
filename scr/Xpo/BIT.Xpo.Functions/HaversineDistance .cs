using BIT.Data.Xpo.CoreFunctions;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;

namespace BIT.Xpo.Functions
{
    public class HaversineDistance : ICustomFunctionOperatorFormattable, IRegistableFunction
    {

        public HaversineDistance ()
        {

        }

        public string Name => nameof(HaversineDistance);

        public object Evaluate(params object[] operands)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Session session)
        {
            throw new NotImplementedException();
        }

        public string Format(Type providerType, params string[] operands)
        {
            // This example implements the function for MS Access databases only.
            if (providerType == typeof(MSSqlConnectionProvider))
                return string.Format("datepart(\"m\", {0})", operands[0]);
            throw new NotSupportedException(string.Concat(
                "This provider is not supported: ", providerType.Name));
        }

        public bool Register(Session session)
        {
            session.ExecuteNonQuery("");
            return true;
        }

        ///// <summary>
        ///// Returns the distance in miles or kilometers of any two
        ///// latitude / longitude points.
        ///// </summary>
        ///// <param name="pos1">Location 1</param>
        ///// <param name="pos2">Location 2</param>
        ///// <param name="unit">Miles or Kilometers</param>
        ///// <returns>Distance in the requested unit</returns>
        //public double HaversineDistance(ILatLng pos1, ILatLng pos2, DistanceUnit unit)
        //{
        //    double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
        //    var lat = (pos2.Latitude - pos1.Latitude).ToRadians();
        //    var lng = (pos2.Longitude - pos1.Longitude).ToRadians();
        //    var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
        //                  Math.Cos(pos1.Latitude.ToRadians()) * Math.Cos(pos2.Latitude.ToRadians()) *
        //                  Math.Sin(lng / 2) * Math.Sin(lng / 2);
        //    var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
        //    return R * h2;
        //}

        public Type ResultType(params Type[] operands)
        {
            throw new NotImplementedException();
        }
    }
}
