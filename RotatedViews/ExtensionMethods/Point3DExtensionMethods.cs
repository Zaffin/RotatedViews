using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mastercam.Math;

namespace RotatedViews.ExtensionMethods
{

    public static class Point3DExtensionMethods
    {
        public static double LengthSquared(this Point3D u)
        {
            return Math.Pow(VectorManager.Length(u), 2);
        }

        public static Point3D Project(this Point3D u, Point3D v)
        {
            return VectorManager.Scale(v, VectorManager.Dot(u, v) / v.LengthSquared());
        }

        public static Point3D OrthogonalProject(this Point3D u, Point3D v)
        {
            var projection = u.Project(v);

            return u - projection;
        }

        public static Point3D RotateAboutAxis(this Point3D u, Point3D axis, double angle)
        {
            var angleInRadians = angle * (Math.PI / 180);

            var normalAxis = VectorManager.Normalize(axis);

            var projection = u.Project(axis);

            var orthogonalProjection = u.OrthogonalProject(axis);

            var normalAxisCrossOrthogProjection = VectorManager.Cross(normalAxis, orthogonalProjection);

            var rotatedProjection = VectorManager.Scale(normalAxisCrossOrthogProjection, Math.Sin(angleInRadians)) +
                                    VectorManager.Scale(orthogonalProjection, Math.Cos(angleInRadians));


            return rotatedProjection + projection;
        }

    }
}
