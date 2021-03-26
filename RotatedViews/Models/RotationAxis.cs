using Mastercam.Math;

namespace RotatedViews.Models
{
    public class RotationAxis
    {
        public Point3D Axis { get; set; }

        public string Label { get; set; }

        public RotationAxis()
        {
            Axis = new Point3D(0, 0, 1);
            Label = "WorldZ";
        }

        public RotationAxis(Point3D axis, string label)
        {
            Axis = axis;
            Label = label;
        }
    }
}
