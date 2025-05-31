using System.Windows.Controls;
using System.Windows.Input;

namespace DatabaseCourceWork.DesktopApplication.Views.OrganizerViews
{
    /// <summary>
    /// Interaction logic for OrganizerHomeView.xaml
    /// </summary>
    public partial class OrganizerHomeView : UserControl
    {
        public OrganizerHomeView()
        {
            InitializeComponent();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
