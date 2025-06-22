using System.Windows;

namespace AutoFox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DropdownButton.Checked += (s, e) => RadioPanel.Visibility = Visibility.Visible;
            //DropdownButton.Unchecked += (s, e) => RadioPanel.Visibility = Visibility.Collapsed;

        }

        // Show only Display String panel
        private void DisplayStringRadio_Checked(object sender, RoutedEventArgs e)
        {
            DisplayStringPanel.Visibility = Visibility.Visible;
            ReverseShellPanel.Visibility = Visibility.Collapsed;
            PrankPanel.Visibility = Visibility.Collapsed;
            VideoPanel.Visibility = Visibility.Collapsed;
            DetailsTextBlock.Text = "📝 Display String script selected.";
        }

        // Show only Reverse Shell panel
        private void ReverseShellRadio_Checked(object sender, RoutedEventArgs e)
        {
            DisplayStringPanel.Visibility = Visibility.Collapsed;
            ReverseShellPanel.Visibility = Visibility.Visible;
            PrankPanel.Visibility = Visibility.Collapsed;
            VideoPanel.Visibility = Visibility.Collapsed;
            DetailsTextBlock.Text = "🛠️ Reverse Shell script selected.";
        }

        // Show only Prank panel
        private void PrankRadio_Checked(object sender, RoutedEventArgs e)
        {
            DisplayStringPanel.Visibility = Visibility.Collapsed;
            ReverseShellPanel.Visibility = Visibility.Collapsed;
            PrankPanel.Visibility = Visibility.Visible;
            VideoPanel.Visibility = Visibility.Collapsed;
            DetailsTextBlock.Text = "😄 Prank script selected.";
        }

        // Show only Video panel
        private void VideoRadio_Checked(object sender, RoutedEventArgs e)
        {
            DisplayStringPanel.Visibility = Visibility.Collapsed;
            ReverseShellPanel.Visibility = Visibility.Collapsed;
            PrankPanel.Visibility = Visibility.Collapsed;
            VideoPanel.Visibility = Visibility.Visible;
            DetailsTextBlock.Text = "🎬 Video script selected.";
        }

        // Compile button click
        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            // You can later modify this to show dynamic content based on selected script
            DetailsTextBlock.Text = "✅ Compilation Successful!\n\nGenerated script has been compiled and is ready to upload.";
        }
    }
}
