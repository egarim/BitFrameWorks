--SELECT TOP 1 *, ( 3960 * acos( cos( radians( @custLat ) ) *
--  cos( radians( Lat ) ) * cos( radians(  Lng  ) - radians( @custLng ) ) +
--  sin( radians( @custLat ) ) * sin( radians(  Lat  ) ) ) ) AS Distance
--FROM Offices
--ORDER BY Distance ASC


IF OBJECT_ID (N'dbo.HaversineDistance', N'FN') IS NOT NULL  
    DROP FUNCTION HaversineDistance;  
GO  
CREATE FUNCTION dbo.HaversineDistance(@custLat float,@custLng float,@Lat float,@Lng float,@Distance float)  
RETURNS int   
AS   
-- Returns the stock level for the product.  
BEGIN  
    DECLARE @ret float;  
  
  --SELECT TOP 1 *, ( 3960 * acos( cos( radians( @custLat ) ) *
  --cos( radians( Lat ) ) * cos( radians(  Lng  ) - radians( @custLng ) ) +
  --sin( radians( @custLat ) ) * sin( radians(  Lat  ) ) ) ) AS Distance


 RETURN   ( 3960 * acos( cos( radians( @custLat ) ) *
  cos( radians( @Lat ) ) * cos( radians(  @Lng  ) - radians( @custLng ) ) +
  sin( radians( @custLat ) ) * sin( radians(  @Lat  ) ) ) ) 

   -- RETURN @ret;  
END;


--/// <summary>
--/// Returns the distance in miles or kilometers of any two
--/// latitude / longitude points.
--/// </summary>
--/// <param name="pos1">Location 1</param>
--/// <param name="pos2">Location 2</param>
--/// <param name="unit">Miles or Kilometers</param>
--/// <returns>Distance in the requested unit</returns>
--public double HaversineDistance(LatLng pos1, LatLng pos2, DistanceUnit unit)
--{
--    double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
--    var lat = (pos2.Latitude - pos1.Latitude).ToRadians();
--    var lng = (pos2.Longitude - pos1.Longitude).ToRadians();
--    var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
--                  Math.Cos(pos1.Latitude.ToRadians()) * Math.Cos(pos2.Latitude.ToRadians()) *
--                  Math.Sin(lng / 2) * Math.Sin(lng / 2);
--    var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
--    return R * h2;
--}