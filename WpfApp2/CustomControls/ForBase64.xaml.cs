using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace WpfApp2.CustomControls
{
    /// <summary>
    /// Interaction logic for ForBase64.xaml
    /// </summary>
    public partial class ForBase64 : UserControl
    {
        string base64;
        public ForBase64()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(selectedFile);
                fileLable.Content = selectedFile;
                base64 = Convert.ToBase64String(fileBytes);
                btnSave.IsEnabled = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "script", // Default file name
                DefaultExt = ".txt", // Default file extension
                Filter = "Text Document (*.txt)|.*txt|All files (*.*)|*.*", // Filter options
                FilterIndex = 0,
            };

            // Show the dialog and save the file if the user clicks OK
            if (saveFileDialog.ShowDialog() == true)
            {
                int chunksize = 345;
                string filePath = saveFileDialog.FileName;
                for (int i = 0; i < base64.Length; i += chunksize)
                {
                    if (i + chunksize > base64.Length)
                    {
                        chunksize = base64.Length - i;
                    }
                    string chuncks = base64.Substring(i, chunksize);
                    string textToAppend = $"STRING {chuncks}\n\r";
                    File.AppendAllText(filePath, textToAppend);
                }
            }
        }

        private void main_logic_grid_DragEnter(object sender, DragEventArgs e)
        {
            // Checks if the data contains file drop data
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void main_logic_grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // let's just opening the first file only
                if (files.Length == 1)
                {
                    string selectedFile = files[0];
                    // TODO: performing operations on the file:
                    byte[] fileBytes = File.ReadAllBytes(selectedFile);
                    fileLable.Content = selectedFile;
                    base64 = Convert.ToBase64String(fileBytes);
                    btnSave.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Only One file is allowed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
