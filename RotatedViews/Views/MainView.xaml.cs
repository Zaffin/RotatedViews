using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RotatedViews.Services;
using RotatedViews.ViewModel;


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
            this.DataContext = new MainViewViewModel(new MastercamService(), new ViewService());
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
