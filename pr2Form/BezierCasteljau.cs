using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Written by mohammad_284499 */
namespace pr2Form
{
    internal class BezierCasteljau
    {

        // Generate the Bezier curve points using the De Casteljau algorithm
        public PointF[] GenerateBezierCurve(PointF[] controlPoints, int steps)
        {
            PointF[] curvePoints = new PointF[steps + 1]; // Array to hold the curve points
            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps; // Calculate the parameter t
                curvePoints[i] = DeCasteljau(controlPoints, t); // Compute the curve point for parameter t
            }
            return curvePoints; // Return the array of curve points
        }

        // Compute a single point on the Bezier curve using the De Casteljau algorithm
        private PointF DeCasteljau(PointF[] controlPoints, float t)
        {
            PointF[] temp = new PointF[controlPoints.Length]; // Temporary array to hold intermediate points
            Array.Copy(controlPoints, temp, controlPoints.Length); // Copy control points to temporary array
            int n = temp.Length; // Number of control points
            for (int r = 1; r < n; r++)
            {
                for (int i = 0; i < n - r; i++)
                {
                    // Interpolate between points to compute new points
                    float x = (1 - t) * temp[i].X + t * temp[i + 1].X;
                    float y = (1 - t) * temp[i].Y + t * temp[i + 1].Y;
                    temp[i] = new PointF(x, y); // Store the interpolated point
                }
            }
            return temp[0]; // Return the final computed point on the curve
        }
    }
}
/* Written by mohammad_284499 */