using Mastercam.Math;
using Mastercam.Database;

using RotatedViews.DataTypes;

namespace RotatedViews.Services
{
    public class ViewService : IViewService
    {
        public void CreateRotatedViews(MCView view, RotationAxis rotationAxis, int number, double angle, bool isTotalSweep)
        {
            if (isTotalSweep)
            {
                angle /= number;
                RotateView(view, rotationAxis, number, angle);
            }
            else
            {
                RotateView(view, rotationAxis, number, angle);
            }
        }

        private void RotateView(MCView view, RotationAxis rotationAxis, int number, double angle)
        {
            var instance = 1;
            var initialAngle = angle;

            while (number >= instance)
            {
                var rotatedView = new MCView
                {
                    ViewName = $"{view.ViewName} about {rotationAxis.Label} by {angle}deg.",
                    ViewOrigin = view.ViewOrigin,
                    ViewMatrix = CreateMatrix(view, rotationAxis.Axis, angle)
                };

                rotatedView.Commit();

                instance++;

                angle = initialAngle * instance;
            }
        }

        private Matrix3D CreateMatrix(MCView view, Point3D rotationAxis, double angle)
        {
            Point3D axisOne;
            Point3D axisTwo;
            Point3D axisThree;

            if (view.ViewMatrix.Row1 == rotationAxis)
            {
                axisOne = rotationAxis;

                axisTwo = VectorManager.Rotate(view.ViewMatrix.Row2, 
                                               view.ViewOrigin, 
                                               rotationAxis, 
                                               VectorManager.DegreesToRadians(angle));

                axisThree = VectorManager.Rotate(view.ViewMatrix.Row3,
                                                 view.ViewOrigin,
                                                 rotationAxis,
                                                 VectorManager.DegreesToRadians(angle));

            }
            else if (view.ViewMatrix.Row2 == rotationAxis)
            {
                axisOne = VectorManager.Rotate(view.ViewMatrix.Row1,
                                               view.ViewOrigin,
                                               rotationAxis,
                                               VectorManager.DegreesToRadians(angle));

                axisTwo = rotationAxis;

                axisThree = VectorManager.Rotate(view.ViewMatrix.Row3,
                                                 view.ViewOrigin,
                                                 rotationAxis,
                                                 VectorManager.DegreesToRadians(angle));

            }
            else
            {
                axisOne = VectorManager.Rotate(view.ViewMatrix.Row1,
                                               view.ViewOrigin,
                                               rotationAxis,
                                               VectorManager.DegreesToRadians(angle));

                axisTwo = VectorManager.Rotate(view.ViewMatrix.Row2,
                                               view.ViewOrigin,
                                               rotationAxis,
                                               VectorManager.DegreesToRadians(angle));

                axisThree = rotationAxis;


            }

            return new Matrix3D
            {
                Row1 = axisOne,
                Row2 = axisTwo,
                Row3 = axisThree
            };
        }
    }
}
