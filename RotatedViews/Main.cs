
namespace RotatedViews
{
    using System.Windows;
    using System.Windows.Interop;

    using Mastercam.App;
    using Mastercam.App.Types;
    using Mastercam.Support.UI;

    using RotatedViews.Views;

    public class Main : NetHook3App
    {
        #region Public Override Methods
   
        /// <summary> The main entry point for your RotatedViews. </summary>
        ///
        /// <param name="param"> System parameter. </param>
        ///
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Run(int param)
        {
            var view = new MainView();
            _ = new ModelessDialogTabs.ModelessDialogTabs(view);

            var windowInteropHelper = new WindowInteropHelper(view)
            {
                Owner = MastercamWindow.GetHandle().Handle
            };

            view.Show();

            return MCamReturn.NoErrors;
        }

        #endregion

    }
}
