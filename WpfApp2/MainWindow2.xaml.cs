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
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using WpfApp2.CustomControls;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private TextEditor codeEditor;
        private ContentControl contentControl;
        private MainLogicGrid mainLogic = new MainLogicGrid();
        private ForBase64 base64 = new ForBase64();
        public MainWindow2()
        {
            InitializeComponent();
            //ContentControl.Content = mainLogic;
            main_logic_grid.Children.Clear();
        }

        // PlaceHolder Code For TextBox.
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


        private void DisplayString_Checked(object sender, RoutedEventArgs e)
        {
            main_logic_grid.Children.Clear();

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
                Height = 40,
                Width = 200,
                BorderBrush = new SolidColorBrush(Color.FromArgb(127,237, 222, 249)) ,
                Background = new SolidColorBrush(Color.FromArgb(255,219, 189, 243)),
                Padding = new Thickness(10)
            };

            TextBox textbox_String = new TextBox
            {
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
            main_logic_grid.Children.Add(stackPanel);
            main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }

        private void ReverseShell_Checked(object sender, RoutedEventArgs e)
        {
            main_logic_grid.Children.Clear();

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

            TextBox textbox_LHOST = new TextBox
            {
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };

            TextBox textbox_LPORT = new TextBox
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

            main_logic_grid.Children.Add(stackPanel3);
            main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AsciiDrawing_Checked(object sender, RoutedEventArgs e)
        {
            main_logic_grid.Children.Clear();

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

            TextBox textbox = new TextBox
            {
                FontSize = 15,
                FontFamily = new FontFamily("Inter"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.White,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(5),
                BorderThickness = new Thickness(0),
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                AcceptsReturn = true,
            };

            TextBoxHelper.SetPlaceholder(textbox, "Enter ASCII Drawing"); // adding the place holder
            border.Child = textbox;
            //stackPanel.Children.Add(displayString);
            //stackPanel.Children.Add(border);
            main_logic_grid.Children.Add(border);
            main_logic_grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            main_logic_grid.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void Video_Click(object sender, RoutedEventArgs e)
        {
            main_logic_grid.Children.Clear();

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                //Background = Brushes.Green,
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

            TextBox textbox_URL = new TextBox
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
            main_logic_grid.Children.Add(stackPanel);
            main_logic_grid.HorizontalAlignment = HorizontalAlignment.Center;
            main_logic_grid.VerticalAlignment = VerticalAlignment.Center;
        }

        private void Compile_Click(object sender, RoutedEventArgs e)
        {
            //int selectedIndex = ScriptsComboBox.SelectedIndex;
            //string message;
            int i = 0;
            string[] data = new string[2];
            StackPanel stackPanel = (StackPanel)main_logic_grid.Children[0];
            foreach (StackPanel item in stackPanel.Children)
            {
                foreach (UIElement elem in item.Children)
                {
                    if (elem is TextBox)
                    {
                        string value = ((TextBox)elem).Text;
                        data[i++] = value;
                        //MessageBox.Show
                    }
                }
            }
        }
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            //int selectedIndex = ScriptsComboBox.SelectedIndex;
            //string message;
            int i = 0;
            string[] data = new string[2];
            StackPanel stackPanel = (StackPanel)main_logic_grid.Children[0];
            foreach (StackPanel item in stackPanel.Children)
            {
                foreach (UIElement elem in item.Children)
                {
                    if (elem is TextBox)
                    {
                        string value = ((TextBox)elem).Text;
                        data[i++] = value;
                    }
                }

            }
        }
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

        private void CustomToggle_Checked(object sender, RoutedEventArgs e)
        {
            mainBorder.Child = null;
            mainBorder.Child = main_logic_grid;
            dropdown.Visibility = Visibility.Visible;
        }

        private void CustomToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            dropdown.Visibility = Visibility.Collapsed;
        }

        private void Editor_Checked(object sender, RoutedEventArgs e)
        {
            main_logic_grid.Children.Clear();

            dropdown2.Visibility = Visibility.Visible;

            codeEditor = new TextEditor
            {
                Name = "CodeEditor",
                ShowLineNumbers = true,
                SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("C++"),
                WordWrap = true,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 13,
                Padding = new Thickness(2),
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

            main_logic_grid.Children.Add(border);
            main_logic_grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            main_logic_grid.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void Editor_Unchecked(object sender, RoutedEventArgs e)
        {
            dropdown2.Visibility = Visibility.Collapsed;
        }

        private void Base64_Checked(object sender, RoutedEventArgs e)
        {
            main_logic_grid.Children.Clear();

            contentControl = new ContentControl();
            mainBorder.Child= contentControl;
            contentControl.Content = base64;

            Button btnOpen = new Button
            {
                //Name = "btnOpen",
                Content = "Open",
                MinWidth = 100,
                MaxHeight = 40,
                //MinHeight = 40,
                Margin = new Thickness(10),
            };

            Button btnSave = new Button
            {
                //Name = "btnOpen",
                Content = "Save",
                MinWidth = 100,
                MaxHeight = 40,
                IsEnabled = false,
                Margin = new Thickness(10),
            };

            Border border = new Border
            {
                CornerRadius = new CornerRadius(5),
                Background = new SolidColorBrush(Color.FromRgb(219, 189, 243)),
                Child = codeEditor,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            //border.Child = btnOpen;
            //border.Child = btnSave;
            //main_logic_grid.Children.Add(border);

            //button_logic_stack.Children.Add(btnOpen);
            //button_logic_stack.Children.Add(btnSave);

            //btnOpen.Click += BtnOpen_Click;
            dropdown3.Visibility = Visibility.Visible;

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Base64_UnChecked(object sender, RoutedEventArgs e)
        {
            dropdown3.Visibility = Visibility.Collapsed;
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
    }
}
