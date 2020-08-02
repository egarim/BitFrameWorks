using BIT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Services
{
    //HACK more information here 

    public class GeographicService
    {

        // Define Infinite (Using INT_MAX 
        // caused overflow problems) 
        static int INF = 10000;

        /// <summary>
        /// Given three colinear points p, q, r,  the function checks if point q lies is on line segment 'pr' 
        /// </summary>
        /// <param name="p">Start</param>
        /// <param name="q">Point to check</param>
        /// <param name="r">End</param>
        /// <returns>True if q is part of the segment otherise false</returns>
        public bool IsOnSegment(GeoPoint p, GeoPoint q, GeoPoint r)
        {


         


            if (q.x <= Math.Max(p.x, r.x) &&
                q.x >= Math.Min(p.x, r.x) &&
                q.y <= Math.Max(p.y, r.y) &&
                q.y >= Math.Min(p.y, r.y))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// To find orientation of ordered triplet (p, q, r).
        /// </summary>
        /// <param name="p">GeoPoint</param>
        /// <param name="q">GeoPoint</param>
        /// <param name="r">GeoPoint</param>
        /// <returns>0 --> p, q and r are colinear,1 --> Clockwise, 2 --> Counterclockwise</returns>
        public double GetOrientation(GeoPoint p, GeoPoint q, GeoPoint r)
        {
            //HACK http://mathonline.wikidot.com/collinear-points-and-concurrent-lines

            double val = (q.y - p.y) * (r.x - q.x) -
                    (q.x - p.x) * (r.y - q.y);

            if (val == 0)
            {
                return 0; // colinear 
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

       /// <summary>
       /// Check if 2 lines intercept
       /// </summary>
       /// <param name="p1">Line 1 start</param>
       /// <param name="q1">Line 1 end</param>
       /// <param name="p2">Line 2 start</param>
       /// <param name="q2">Line 2 end</param>
       /// <returns></returns>
        public bool LinesIntersect(GeoPoint p1, GeoPoint q1,
                                GeoPoint p2, GeoPoint q2)
        {


            // The function that returns true if 
            // line segment 'p1q1' and 'p2q2' intersect. 

            // Find the four orientations needed for 
            // general and special cases 
            double o1 = GetOrientation(p1, q1, p2);
            double o2 = GetOrientation(p1, q1, q2);
            double o3 = GetOrientation(p2, q2, p1);
            double o4 = GetOrientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases 
            // p1, q1 and p2 are colinear and 
            // p2 lies on segment p1q1 
            if (o1 == 0 && IsOnSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and 
            // q2 lies on segment p1q1 
            if (o2 == 0 && IsOnSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and 
            // p1 lies on segment p2q2 
            if (o3 == 0 && IsOnSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and 
            // q1 lies on segment p2q2 
            if (o4 == 0 && IsOnSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases 
            return false;
        }





        /// <summary>
        /// Returns true if the point p lies inside the polygon[] with n vertices 
        /// </summary>
        /// <param name="polygon">An array of GeoPoints that represents a polygon</param>
        /// <param name="p">GeoPoint</param>
        /// <returns>True if the point p lies inside the polygon otherwise false</returns>
        public bool IsInsidePolygon(GeoPoint[] polygon, GeoPoint p)
        {



            // There must be at least 3 vertices in polygon[] 
            int n = polygon.Length;
            if (n < 3)
            {
                return false;
            }

            // Create a point for line segment from p to infinite 
            GeoPoint extreme = new GeoPoint(INF, p.y);

            // Count intersections of the above line 
            // with sides of polygon 
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 
                // 'extreme' intersects with the line 
                // segment from 'polygon[i]' to 'polygon[next]' 
                if (LinesIntersect(polygon[i],
                                polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line 
                    // segment 'i-next', then check if it lies 
                    // on segment. If it lies, return true, otherwise false 
                    if (GetOrientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return IsOnSegment(polygon[i], p,
                                        polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise 
            return (count % 2 == 1); // Same as (count%2 == 1) 
        }

        // Driver Code 

    }
}
