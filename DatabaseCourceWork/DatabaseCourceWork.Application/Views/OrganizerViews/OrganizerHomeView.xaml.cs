using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
