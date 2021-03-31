using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mastercam.Database;
using Mastercam.Math;

namespace RotatedViews.Models
{
    public class DisplayableView
    {
        public MCView Data { get; set; }

        public int DisplayID { get; set; }

        public bool IsDisplayed { get; set; }

        public DisplayableView(MCView view)
        {
            Data = view;
            DisplayID = 0;
            IsDisplayed = false;
        }

        public void Display(Func<Matrix3D, Point3D, int> displayView)
        {
            IsDisplayed = true;
            DisplayID = displayView(Data.ViewMatrix, Data.ViewOrigin);
        }

        public void Erase(Action<int> eraseView)
        {
            eraseView(DisplayID);
            IsDisplayed = false;
        }
    }
}
