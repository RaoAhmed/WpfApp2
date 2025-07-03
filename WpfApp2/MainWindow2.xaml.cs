using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using AutoFox;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using WpfApp2.Classes;
using WpfApp2.CustomControls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private DispatcherTimer timer;
        private TextEditor codeEditor;
        //private ContentControl contentControl;
        private MainLogicGrid mainLogic = new MainLogicGrid();
        private ForBase64 base64 = new ForBase64();
        private TextBox textbox_String, textbox_LHOST, textbox_LPORT, textbox_Ascii, textbox_URL;
        public MainWindow2()
        {
            InitializeComponent();
            ContentControl.Content = mainLogic;
            mainLogic.main_logic_grid.Children.Clear();
            mainLogic.compileBtnClick += MainLogic_compileBtnClick;
            mainLogic.uploadBtnClick += MainLogic_uploadBtnClick;
        }
        public static class TextBoxHelper
        {
            public static readonly DependencyProperty PlaceholderProperty =
                DependencyProperty.RegisterAttached(
                    "Placeholder",
                    typeof(string),
                    typeof(TextBoxHelper),
                    new FrameworkPropertyMetadata(null, OnPlaceholderChanged));

            public static string GetPlaceholder(DependencyObject obj) =>
                (string)obj.GetValue(PlaceholderProperty);

            public static void SetPlaceholder(DependencyObject obj, string value) =>
                obj.SetValue(PlaceholderProperty, value);

            private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is TextBox textBox)
                {
                    textBox.Loaded += (s, args) => ShowPlaceholder(textBox);
                    textBox.TextChanged += (s, args) => ShowPlaceholder(textBox);
                }
            }

            private static void ShowPlaceholder(TextBox textBox)
            {

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Foreground = Brushes.Gray;
                    textBox.Text = GetPlaceholder(textBox);
                    textBox.GotFocus += RemovePlaceholder;
                }
            }

            private static void RemovePlaceholder(object sender, RoutedEventArgs e)
            {
                if (sender is TextBox textBox && textBox.Text == GetPlaceholder(textBox))
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.Black;
                    textBox.GotFocus -= RemovePlaceholder;
                }
            }
        }
        
        

        #region Upload Button
        private async void MainLogic_uploadBtnClick(object sender, EventArgs e)
        {
            var loadingDialog = new LoadingDialog();
            loadingDialog.Show();

            await Task.Run(() =>
            {
                Thread.Sleep(3000); // Simulate work
            });

            Output_Window.Text = "";
            string message = "";
            ArduinoHandler arduinoHandler = new ArduinoHandler();
            if (DisplayString.IsChecked == true)
            {
                DisplayString displayString = new DisplayString(textbox_String.Text);
                message = displayString.DisplayStringSketch();
            }
            else if (ReverseShell.IsChecked == true)
            {
                //Output_Window.Text = textbox_LHOST.Text + "\n\r" + textbox_LPORT.Text;
                ReverseShell reverseShell = new ReverseShell(textbox_LHOST.Text, textbox_LPORT.Text);
                message = reverseShell.ReverseShellSketch();
            }
            else if (AsciiDrawing.IsChecked == true)
            {
                //Output_Window.Text = textbox_Ascii.Text;
                AsciiArt asciiArt = new AsciiArt(textbox_Ascii.Text);
                message = asciiArt.AsciiArtSketch();
            }
            else if (Video.IsChecked == true)
            {
                //Output_Window.Text = textbox_URL.Text;
                PlayVideo playVideo = new PlayVideo(textbox_URL.Text);
                message = playVideo.PlayVideoSketch();
            }

            else if (SD_Card.IsChecked == true)
            {
                SDCard sDCard = new SDCard();
                message = sDCard.SDCardSketch();
            }

            else if (Editor.IsChecked == true)
            {
                Output_Window.Text = codeEditor.Text;
                string Path = "test";
                try
                {
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }
                    File.WriteAllText(@"test\test.ino", codeEditor.Text);
                }
                catch (Exception ex)
                {
                    Output_Window.Text = $"Failed SuccessFully 😁: {ex}";
                }

            }
            message = arduinoHandler.CompileSketch();
            Output_Window.Text = message;
            message = arduinoHandler.UploadSketch();
            Output_Window.Text = message;
            loadingDialog.Close();
        }
        #endregion
        // PlaceHolder Code For TextBox.

        #region Compile Button
        private async void MainLogic_compileBtnClick(object sender, EventArgs e)
        {
            var loadingDialog = new LoadingDialog();
            loadingDialog.Show();
            
            await Task.Run(() =>
            {
                Thread.Sleep(3000); // Simulate work
            });

            Output_Window.Text = "";
            string message ="";
            ArduinoHandler arduinoHandler = new ArduinoHandler();
            if (DisplayString.IsChecked == true)
            {
                DisplayString displayString = new DisplayString(textbox_String.Text);
                message = displayString.DisplayStringSketch();
            }
            else if (ReverseShell.IsChecked == true)
            {
                //Output_Window.Text = textbox_LHOST.Text + "\n\r" + textbox_LPORT.Text;
                ReverseShell reverseShell = new ReverseShell(textbox_LHOST.Text, textbox_LPORT.Text);
                message = reverseShell.ReverseShellSketch();
            }
            else if (AsciiDrawing.IsChecked == true)
            {
                //Output_Window.Text = textbox_Ascii.Text;
                AsciiArt asciiArt = new AsciiArt(textbox_Ascii.Text);
                message = asciiArt.AsciiArtSketch();
            }
            else if (Video.IsChecked == true)
            {
                //Output_Window.Text = textbox_URL.Text;
                PlayVideo playVideo = new PlayVideo(textbox_URL.Text);
                message = playVideo.PlayVideoSketch();
            }

            else if (SD_Card.IsChecked == true)
            {
                SDCard sDCard = new SDCard();
                message = sDCard.SDCardSketch();
            }

            else if (Editor.IsChecked == true)
            {
                Output_Window.Text = codeEditor.Text;
                string Path = "test";
                try
                {
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }
                    File.WriteAllText(@"test\test.ino", codeEditor.Text);
                }
                catch (Exception ex)
                {
                    Output_Window.Text = $"Failed SuccessFully 😁: {ex}";
                }

            }
            Output_Window.Text = message;
            message = arduinoHandler.CompileSketch();
            Output_Window.Text = message;

            loadingDialog.Close();
            //switch (selectedIndex)
            //{
            //    case 0:
            //        DisplayString displayString = new DisplayString(data[0]);
            //        message = displayString.DisplayStringSketch();
            //        break;
            //    case 1:
            //        ReverseShell reverseShell = new ReverseShell(data[0], data[1]);
            //        message = reverseShell.ReverseShellSketch();
            //        break;
            //    case 2:
            //        AsciiArt asciiArt = new AsciiArt(data[0]);
            //        message = asciiArt.AsciiArtSketch();
            //        break;
            //    case 3:
            //        PlayVideo playVideo = new PlayVideo(data[0]);
            //        message = playVideo.PlayVideoSketch();
            //        break;
            //}
            //message = arduinoHandler.CompileSketch();
        }
        #endregion

        #region Scripts
        private void DisplayString_Checked(object sender, RoutedEventArgs e)
        {
            mainLogic.main_logic_grid.Children.Clear();

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            Label displayString = new Label
            {
                FontSize = 15,
                Foreground = Brushes.Black,
                //Background = Brushes.Red,
                FontFamily = new FontFamily("Inter"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 50, 0),
                Content = "String:"
            };

            Border border = new Border
            {
                CornerRadius = new CornerRadius(10),
                Height = 40,
                Width = 200,
                BorderBrush = new SolidColorBrush(Color.FromArgb(127,237, 222, 249)),
                Background = new SolidColorBrush(Color.FromArgb(255,219, 189, 243)),
                Padding = new Thickness(10)
            };

            textbox_String = new TextBox
            {
                Name = "tb_DisplayString",
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };

            border.Child = textbox_String;
            stackPanel.Children.Add(displayString);
            stackPanel.Children.Add(border);
            mainLogic.main_logic_grid.Children.Add(stackPanel);
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }

        private void ReverseShell_Checked(object sender, RoutedEventArgs e)
        {
            mainLogic.main_logic_grid.Children.Clear();

            StackPanel stackPanel1 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0,0,0,5),
                //Background = Brushes.Green,
            };

            StackPanel stackPanel2 = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Label lable_LHOST = new Label
            {
                Content = "LHOST",
                FontSize = 15,
                Foreground = Brushes.Black,
                //Background = Brushes.Red,
                FontFamily = new FontFamily("Inter"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 50, 0),
            };

            Label lable_LPORT = new Label
            {
                Content = "LPORT",
                FontSize = 15,
                Foreground = Brushes.Black,
                //Background = Brushes.Red,
                FontFamily = new FontFamily("Inter"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 50, 0),
            };

            textbox_LHOST = new TextBox
            {
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };

            textbox_LPORT = new TextBox
            {
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };

            Border border_LHOST = new Border
            {
                CornerRadius = new CornerRadius(10),
                Height = 40,
                Width = 200,
                BorderBrush = new SolidColorBrush(Color.FromArgb(127, 237, 222, 249)),
                Background = new SolidColorBrush(Color.FromArgb(255, 219, 189, 243)),
                Padding = new Thickness(10),
                Child = textbox_LHOST,
            };
            Border border_LPORT = new Border
            {
                CornerRadius = new CornerRadius(10),
                Height = 40,
                Width = 200,
                BorderBrush = new SolidColorBrush(Color.FromArgb(127, 237, 222, 249)),
                Background = new SolidColorBrush(Color.FromArgb(255, 219, 189, 243)),
                Padding = new Thickness(10),
                Child = textbox_LPORT,
            };

            //lable.Content = "LHOST";
            //border.Child = textbox_LHOST;
            stackPanel1.Children.Add(lable_LHOST);
            stackPanel1.Children.Add(border_LHOST);

            //lable.Content = "LPORT";
            //border.Child = textbox_LPORT;

            stackPanel2.Children.Add(lable_LPORT);
            stackPanel2.Children.Add(border_LPORT);

            StackPanel stackPanel3 = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            stackPanel3.Children.Add(stackPanel1);
            stackPanel3.Children.Add(stackPanel2);
            
            mainLogic.main_logic_grid.Children.Add(stackPanel3);
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AsciiDrawing_Checked(object sender, RoutedEventArgs e)
        {
            mainLogic.main_logic_grid.Children.Clear();

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                //Background = Brushes.Green,
            };

            Label displayString = new Label
            {
                FontSize = 15,
                Foreground = Brushes.Black,
                //Background = Brushes.Red,
                FontFamily = new FontFamily("Inter"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 50, 0),
                Content = "String:"
            };

            Border border = new Border
            {
                CornerRadius = new CornerRadius(10),
                //Height = 40,
                //Width = 200,
                BorderBrush = new SolidColorBrush(Color.FromArgb(127, 237, 222, 249)),
                Background = new SolidColorBrush(Color.FromArgb(255, 219, 189, 243)),
                Padding = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            textbox_Ascii = new TextBox
            {
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.White,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(10),
                BorderThickness = new Thickness(0),
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                AcceptsReturn = true,
            };

            TextBoxHelper.SetPlaceholder(textbox_Ascii, "Enter ASCII Drawing"); // adding the place holder
            border.Child = textbox_Ascii;
            //stackPanel.Children.Add(displayString);
            //stackPanel.Children.Add(border);
            mainLogic.main_logic_grid.Children.Add(border);
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void Video_Click(object sender, RoutedEventArgs e)
        {
            mainLogic.main_logic_grid.Children.Clear();

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            Label URL = new Label
            {
                FontSize = 15,
                Foreground = Brushes.Black,
                //Background = Brushes.Red,
                FontFamily = new FontFamily("Inter"),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 50, 0),
                Content = "URL:"
            };

            Border border = new Border
            {
                CornerRadius = new CornerRadius(10),
                Height = 40,
                Width = 200,
                BorderBrush = new SolidColorBrush(Color.FromArgb(127, 237, 222, 249)),
                Background = new SolidColorBrush(Color.FromArgb(255, 219, 189, 243)),
                Padding = new Thickness(10)
            };

            textbox_URL = new TextBox
            {
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };

            border.Child = textbox_URL;
            stackPanel.Children.Add(URL);
            stackPanel.Children.Add(border);
            mainLogic.main_logic_grid.Children.Add(stackPanel);
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }

        #region SD_Card
        private void SD_Card_Click(object sender, RoutedEventArgs e)
        {
            mainLogic.main_logic_grid.Children.Clear();
            mainLogic.main_logic_grid.Children.Add(new Label
            {
                FontFamily = new FontFamily("Inter"),
                Content = "Script is ready to be compiled",
            });
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }
        #endregion

        #region Data Exfiltration
        private void Data_Exfiltration_Click(object sender, RoutedEventArgs e)
        {
            mainLogic.main_logic_grid.Children.Clear();
            mainLogic.main_logic_grid.Children.Add(new Label
            {
                FontFamily = new FontFamily("Inter"),
                Content = "Script is ready to be compiled",
            });
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }
        #endregion

        #endregion

        private void Expander_click_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            if (expander != null)
            {
                if (!(expander.IsExpanded))
                {
                    expander.VerticalAlignment = VerticalAlignment.Top;
                }
            }
        }

        private void Expander_click_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            if (expander != null)
            {
                if ((expander.IsExpanded))
                {
                    expander.VerticalAlignment = VerticalAlignment.Stretch;
                    expander.HorizontalAlignment = HorizontalAlignment.Stretch;
                }
            }
        }

        private void Scripts_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = mainLogic;
            dropdown.Visibility = Visibility.Visible;
            Editor.IsChecked = false;
        }

        private void Scripts_Unchecked(object sender, RoutedEventArgs e)
        {
            dropdown.Visibility = Visibility.Collapsed;
        }

        private void Editor_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = mainLogic;
            mainLogic.main_logic_grid.Children.Clear();
            dropdown2.Visibility = Visibility.Visible;
            Scripts.IsChecked = Base64.IsChecked = false;
            DisplayString.IsChecked = ReverseShell.IsChecked = Video.IsChecked = AsciiDrawing.IsChecked = false;

            codeEditor = new TextEditor
            {
                Name = "CodeEditor",
                ShowLineNumbers = true,
                SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("C++"),
                WordWrap = true,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 13,
                Padding = new Thickness(4),
                Foreground = Brushes.Black,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(7)
            };

            Border border = new Border
            {
                CornerRadius = new CornerRadius(5),
                Background = new SolidColorBrush(Color.FromRgb(219, 189, 243)),
                Child = codeEditor,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            mainLogic.main_logic_grid.Children.Add(border);
            mainLogic.main_logic_grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            mainLogic.main_logic_grid.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void Editor_Unchecked(object sender, RoutedEventArgs e)
        {
            dropdown2.Visibility = Visibility.Collapsed;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            // Capture the user’s response
            MessageBoxResult result = MessageBox.Show("Your unsaved Work will be lost", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                // Proceed with deletion
                codeEditor.Clear();
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                // Do something with the file
                foreach (string line in File.ReadLines(selectedFile))
                {
                    codeEditor.Text += line + '\n';
                }
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "MyFile", // Default file name
                DefaultExt = ".ino", // Default file extension
                Filter = "Text Document (*.txt)|.*txt|Script file (*.ino)|*.ino|All files (*.*)|*.*", // Filter options
                FilterIndex = 2,
            };

            // Show the dialog and save the file if the user clicks OK
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, codeEditor.Text);
            }
        }


        private void Base64_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = base64;
            Scripts.IsChecked = Editor.IsChecked = false;
            DisplayString.IsChecked = ReverseShell.IsChecked = Video.IsChecked = AsciiDrawing.IsChecked = false;

            //dropdown3.Visibility = Visibility.Visible;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Base64_UnChecked(object sender, RoutedEventArgs e)
        {
            dropdown3.Visibility = Visibility.Collapsed;
        }
    }
}
