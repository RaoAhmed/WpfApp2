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

namespace WpfApp2.CustomControls
{
    /// <summary>
    /// Interaction logic for MainLogicGrid.xaml
    /// </summary>
    public partial class MainLogicGrid : UserControl
    {
        //private MainWindow2 mainWindow2;
        public MainLogicGrid()
        {
            InitializeComponent();
        }
        public event EventHandler compileBtnClick;
        private void btnCompile_Click(object sender, RoutedEventArgs e)
        {
            compileBtnClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
