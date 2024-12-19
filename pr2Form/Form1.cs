using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/* Written by mohammad_284499 */
namespace pr2Form
{
    public partial class Form1 : Form
    {
        private PointF[] controlPoints = new PointF[100]; // Array to store control points
        private int pointCount = 0; // Count of current control points
        private bool isDrawing = false; // Flag to indicate drawing state
        private BezierCasteljau bezier = new BezierCasteljau(); // Instance of the BezierCasteljau class

        public Form1()
        {
            InitializeComponent(); // Initialize the form components
            pictureBox.MouseDown += PictureBox_MouseDown; // Attach event handler for mouse down event on pictureBox
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            isDrawing = true; // Enable drawing mode
            pointCount = 0; // Reset control points count
            pictureBox.Invalidate(); // Redraw the pictureBox
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isDrawing = false; // Disable drawing mode
            pointCount = 0; // Clear the control points
            pictureBox.Invalidate(); // Redraw the pictureBox
        }

        private void btnDeCasteljau_Click(object sender, EventArgs e)
        {
            if (pointCount < 2) return; // Exit if there are fewer than 2 control points

            Graphics graphics = pictureBox.CreateGraphics(); // Get graphics object for pictureBox
            graphics.Clear(Color.Black); // Clear the pictureBox

            // Draw the frame around all control points
            DrawBoundingBox(graphics);

            // Draw each control point
            for (int i = 0; i < pointCount; i++)
            {
                PointF point = controlPoints[i]; // Get the current control point
                graphics.FillEllipse(Brushes.Yellow, point.X - 3, point.Y - 3, 6, 6); // Draw the control point
                graphics.DrawString($"({point.X}, {point.Y})", DefaultFont, Brushes.White, point.X + 5, point.Y + 5); // Label the point
            }

            // Draw the Bezier curves from highest degree to first degree
            for (int degree = pointCount - 1; degree > 0; degree--)
            {
                PointF[] subPoints = new PointF[degree + 1]; // Create array for subset of control points
                Array.Copy(controlPoints, subPoints, degree + 1); // Copy subset of control points
                PointF[] curvePoints = bezier.GenerateBezierCurve(subPoints, 100); // Generate the Bezier curve points
                DrawCurve(curvePoints); // Draw the Bezier curve
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDrawing || pointCount >= controlPoints.Length) return; // Exit if not drawing or array is full

            PointF point = new PointF(e.X, e.Y); // Create a new control point from mouse coordinates
            controlPoints[pointCount] = point; // Add the control point to the array
            pointCount++; // Increment the control points count

            // Draw the new control point
            using (Graphics g = pictureBox.CreateGraphics())
            {
                g.FillEllipse(Brushes.Red, point.X - 3, point.Y - 3, 6, 6); // Draw the control point
                g.DrawString($"({point.X}, {point.Y})", DefaultFont, Brushes.Blue, point.X + 5, point.Y + 5); // Label the point
            }
        }

        private void DrawBoundingBox(Graphics graphics)
        {
            if (pointCount == 0) return; // Exit if no control points

            // Calculate bounding box coordinates
            float minX = controlPoints.Take(pointCount).Min(p => p.X);
            float minY = controlPoints.Take(pointCount).Min(p => p.Y);
            float maxX = controlPoints.Take(pointCount).Max(p => p.X);
            float maxY = controlPoints.Take(pointCount).Max(p => p.Y);

            // Draw bounding box
            graphics.DrawRectangle(Pens.Red, minX - 5, minY - 5, maxX - minX + 10, maxY - minY + 10);
        }

        private void DrawCurve(PointF[] curvePoints)
        {
            using (Graphics g = pictureBox.CreateGraphics())
            {
                // Draw the lines connecting the curve points
                for (int i = 1; i < curvePoints.Length; i++)
                {
                    g.DrawLine(Pens.Red, curvePoints[i - 1], curvePoints[i]);
                }
            }
        }
    }
}

/* Written by mohammad_284499 */
