using Mastercam.Math;

namespace RotatedViews.Models
{
    public class RotationAxis
    {
        public Point3D Axis { get; set; }

        public string LinearLabel { get; set; }

        public string RotaryLabel { get; set; }

        public RotationAxis()
        {
            Axis = new Point3D(0, 0, 1);
            LinearLabel = "WorldZ";
            RotaryLabel = "C";
        }

        public RotationAxis(Point3D axis, string linearLabel, string rotaryLabel)
        {
            Axis = axis;
            LinearLabel = linearLabel;
            RotaryLabel = rotaryLabel;
        }
    }
}
