using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Mastercam.IO;
using Mastercam.Math;
using Mastercam.Support;
using Mastercam.Database;

using RotatedViews.Models;
using RotatedViews.ExtensionMethods;


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

        public MCView GetCurrentConstructionView()
        {
            return ViewManager.CPlane;
        }

        public List<DisplayableView> GetDisplayableViews()
        {
            var views = SearchManager.GetViews();
            var displayableViews = new List<DisplayableView>();

            foreach (var view in views)
            {
                displayableViews.Add(new DisplayableView(view));
            }

            return displayableViews;
        }

        private RotationAxis GetRotationAxis(ViewAxis axis, Matrix3D viewMatrix)
        {
            switch (axis)
            {
                case ViewAxis.XAxis:
                    return new RotationAxis
                    {
                        Axis = viewMatrix.Row1,
                        LinearLabel = "X",
                        RotaryLabel = "A"
                    };

                case ViewAxis.YAxis:
                    return new RotationAxis
                    {
                        Axis = viewMatrix.Row2,
                        LinearLabel = "Y",
                        RotaryLabel = "B"
                    };
                case ViewAxis.ZAxis:
                    return new RotationAxis
                    {
                        Axis = viewMatrix.Row3,
                        LinearLabel = "Z",
                        RotaryLabel = "C"
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
                                                         view.ViewOrigin.ToString(),
                                                         rotationAxis.LinearLabel,
                                                         rotationAxis.RotaryLabel,
                                                         instance.ToString(),
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

        private string BuildViewNameFromTemplate(string viewName, string origin, string linAxisLabel, string rotAxisLabel, string instance, string angle, string template)
        {
            var name = string.Empty;

            var replacementMap = new Dictionary<string, string>()
            {
                {@"<NAME>", viewName},
                {@"<ORIGIN>", origin},
                {@"<LINAXIS>", linAxisLabel},
                {@"<ROTAXIS>", rotAxisLabel},
                {@"<INSTANCE>", instance},
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

                axisTwo = matrix.Row2.RotateAboutAxis(rotationAxis, angle);

                axisThree = VectorManager.Cross(axisOne, axisTwo);
            }
            else if (matrix.Row2 == rotationAxis)
            {
                axisOne = matrix.Row1.RotateAboutAxis(rotationAxis, angle);

                axisTwo = rotationAxis;

                axisThree = VectorManager.Cross(axisOne, axisTwo);
            }
            else
            {
                axisOne = matrix.Row1.RotateAboutAxis(rotationAxis, angle);

                axisThree = rotationAxis;

                axisTwo = VectorManager.Cross(axisThree, axisOne);
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
