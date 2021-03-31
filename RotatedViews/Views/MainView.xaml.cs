using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using RotatedViews.Services;
using RotatedViews.ViewModel;

using GraphicsServiceWrapper;


namespace RotatedViews.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = new MainViewViewModel(new ViewService(), 
                                                     new SettingsService(),
                                                     new GraphicsService());
        }

        private void TitleLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
