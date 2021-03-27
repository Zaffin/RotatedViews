using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Mastercam.Math;
using Mastercam.Database;

using RotatedViews.Models;


namespace RotatedViews.Services
{
    public class ViewService : IViewService
    {

        public void CreateRotatedViews(MCView view, ViewAxis selectedAxis, int number, double angle, DistanceType distanceTypep, string viewNameTemplate)
        {
            var rotationAxis = GetRotationAxis(selectedAxis, view.ViewMatrix);

            if (distanceTypep == DistanceType.TotalSweep)
            {
                angle /= number;
                RotateView(view, rotationAxis, number, angle, viewNameTemplate);
            }
            else
            {
                RotateView(view, rotationAxis, number, angle, viewNameTemplate);
            }
        }

        private RotationAxis GetRotationAxis(ViewAxis axis, Matrix3D viewMatrix)
        {
            switch (axis)
            {
                case ViewAxis.XAxis:
                    return new RotationAxis
                    {
                        Axis = viewMatrix.Row1,
                        Label = "X"
                    };

                case ViewAxis.YAxis:
                    return new RotationAxis
                    {
                        Axis = viewMatrix.Row2,
                        Label = "Y"
                    };
                case ViewAxis.ZAxis:
                    return new RotationAxis
                    {
                        Axis = viewMatrix.Row3,
                        Label = "Z"
                    };
                default:
                    return new RotationAxis();
            }
        }

        private void RotateView(MCView view, RotationAxis rotationAxis, int number, double angle, string viewNameTemplate)
        {
            var instance = 1;
            var initialAngle = angle;

            while (number >= instance)
            {
                var rotatedView = new MCView
                {
                    ViewName = BuildViewNameFromTemplate(view.ViewName,
                                                         rotationAxis.Label,
                                                         angle.ToString(),
                                                         viewNameTemplate),

                    ViewOrigin = view.ViewOrigin,
                    ViewMatrix = CreateMatrix(view.ViewMatrix, rotationAxis.Axis, angle)
                };

                rotatedView.Commit();

                instance++;

                angle = initialAngle * instance;
            }
        }

        private string BuildViewNameFromTemplate(string viewName, string axisLabel, string angle, string template)
        {
            var name = string.Empty;

            var replacementMap = new Dictionary<string, string>()
            {
                {@"<NAME>", viewName},
                {@"<AXIS>", axisLabel},
                {@"<ANGLE>", angle}
            };

            var regex = new Regex(string.Join("|", replacementMap.Keys));

            name = regex.Replace(template, m => replacementMap[m.Value]);

            return name;
        }

        private Matrix3D CreateMatrix(Matrix3D matrix, Point3D rotationAxis, double angle)
        {
            Point3D axisOne;
            Point3D axisTwo;
            Point3D axisThree;

            if (matrix.Row1 == rotationAxis)
            {
                axisOne = rotationAxis;

                axisTwo = VectorManager.Rotate(matrix.Row2,  
                                               rotationAxis, 
                                               VectorManager.DegreesToRadians(angle));

                axisThree = VectorManager.Rotate(matrix.Row3,
                                                 rotationAxis,
                                                 VectorManager.DegreesToRadians(angle));
            }
            else if (matrix.Row2 == rotationAxis)
            {
                axisOne = VectorManager.Rotate(matrix.Row1,
                                               rotationAxis,
                                               VectorManager.DegreesToRadians(angle));

                axisTwo = rotationAxis;

                axisThree = VectorManager.Rotate(matrix.Row3,
                                                 rotationAxis,
                                                 VectorManager.DegreesToRadians(angle));
            }
            else
            {
                axisOne = VectorManager.Rotate(matrix.Row1,
                                               rotationAxis,
                                               VectorManager.DegreesToRadians(angle));

                axisTwo = VectorManager.Rotate(matrix.Row2,
                                               rotationAxis,
                                               VectorManager.DegreesToRadians(angle));

                axisThree = rotationAxis;
            }

            return new Matrix3D
            {
                Row1 = VectorManager.Normalize(axisOne),
                Row2 = VectorManager.Normalize(axisTwo),
                Row3 = VectorManager.Normalize(axisThree)
            };
        }
    }
}
