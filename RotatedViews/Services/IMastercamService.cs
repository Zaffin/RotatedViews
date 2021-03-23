using System.Collections.Generic;

using Mastercam.Database;


namespace RotatedViews.Services
{
    public interface IMastercamService
    {
        MCView GetCurrentConstructionView();

        List<MCView> GetViews();
    }
}
