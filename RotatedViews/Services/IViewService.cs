using Mastercam.Database;

using RotatedViews.Models;

namespace RotatedViews.Services
{
    public interface IViewService
    {
        void CreateRotatedViews(MCView View, RotationAxis RotationAxis, int Number, double Angle, DistanceType distanceType);
    }
}
