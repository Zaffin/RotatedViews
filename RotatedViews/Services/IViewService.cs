using System.Collections.Generic;

using Mastercam.Math;
using Mastercam.Database;

using RotatedViews.Models;

namespace RotatedViews.Services
{
    public interface IViewService
    {
        void CreateRotatedViews(MCView View, ViewAxis selectedAxis, int Number, double Angle, DistanceType distanceType, string planeNameTemplate, bool useExistingWorkoffset);

        MCView GetCurrentConstructionView();

        List<DisplayableView> GetDisplayableViews();
    }
}
