using System.Collections.Generic;

using Mastercam.Support;
using Mastercam.Database;
using Mastercam.IO;

namespace RotatedViews.Services
{
    public class MastercamService : IMastercamService
    {
        public MCView GetCurrentConstructionView()
        {
            return ViewManager.CPlane;
        }

        public List<MCView> GetViews()
        {
            return new List<MCView>(SearchManager.GetViews());
        }
    }
}
