using System;
using System.IO;
using System.Linq;

namespace WpfApp2.Classes
{
    internal class AsciiArt
    {
        public string Path = "test";
        public string Ascii { get; set; }

        public AsciiArt (string ascii)
        {
            Ascii = ascii;
        }

        //private string Formater(string rawText)
        //{
        //    var formattedLines = rawText
        //        .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
        //        .Select(line => $"\"{line} \\n\"");

        //    // Combine the lines into a single string
        //    string formattedText = string.Join(Environment.NewLine, formattedLines);
        //    return formattedText;
        //}

        public string AsciiArtSketch()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                //string formatedText = Formater(Text);
                string sketch = "#include \"Keyboard.h\"\r\n\r\n" +
                    "void setup()\r\n" +
                    "{" +
                    //"   String powershellCommand =R\"(powershell $path=\"$env:tmp\"; ni $path\\test.exe -Force > $null; for($i=3;$i;$i--){$sc=(New-Object -ComObject WScript.Shell).CreateShortcut(\\\"$env:USERPROFILE\\\\Desktop\\\\test$i.lnk\\\");$sc.TargetPath=\\\"$path\\test.exe\\\";$sc.Save();} notepad;)\";" +
                    "   delay (1000);\r\n" +
                    "   Keyboard.begin();\r\n\r\n" +
                    "   Keyboard.press(KEY_LEFT_GUI);\r\n" +
                    "   Keyboard.press('r');\r\n" +
                    "   Keyboard.releaseAll();\r\n" +
                    "   delay(500);\r\n" +
                    //"   Keyboard.println(powershellCommand);\r\n" +
                    "   Keyboard.println(\"notepad\");\r\n" +
                    "   delay(3000);\r\n" +
                    "   Keyboard.press(KEY_LEFT_ALT);\r\n" +
                    "   Keyboard.press(' ');\r\n" +
                    "   Keyboard.releaseAll();\r\n" +
                    "   delay(20);\r\n" +
                    "   Keyboard.println(\"x\");\r\n" +
                    "   for (int i=0; i<3; i++)\r\n" +
                    "       {\r\n" +
                    "       Keyboard.press(KEY_LEFT_CTRL);\r\n" +
                    "       Keyboard.press(KEY_KP_MINUS);\r\n" +
                    "       Keyboard.releaseAll();\r\n}" +
                    $"   Keyboard.println(F(R\"({Ascii})\"));\r\n" +
                    "   Keyboard.end();\r\n" +
                    "}" +
                    "   void loop() {}";
                File.WriteAllText(@"test\test.ino", sketch);
                //return "Here is your Ascii Art 😍\r\n" +
                //    $"{formatedText} ";
                return null;
            }
            catch(Exception ex)
            {
                return "Oops! Looks likes you have an error 😮" +
                    $"{ex}";
            }
            
        }
    }
}
