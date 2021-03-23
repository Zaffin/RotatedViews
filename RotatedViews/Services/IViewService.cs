using Mastercam.Database;

using RotatedViews.DataTypes;

namespace RotatedViews.Services
{
    public interface IViewService
    {
        void CreateRotatedViews(MCView View, RotationAxis RotationAxis, int Number, double Angle, bool IsTotalSweep);
    }
}
