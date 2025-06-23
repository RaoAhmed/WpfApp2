using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Classes
{
    internal class PlayVideo
    {
        public string Path = "test";
        public string Url { get; set; }

        public PlayVideo(string url)
        {
            Url = url;
        }

        public string PlayVideoSketch()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                string sketch = "#include \"Keyboard.h\"\r\n\r\n" +
                    "void typeKey(int key) {\r\n" +
                    "  Keyboard.press(key);\r\n" +
                    "  delay(50);\r\n" +
                    "  Keyboard.release(key);\r\n}\r\n\r\n" +
                    "void setup() {\r\n" +
                    "  Keyboard.begin();\r\n" +
                    "  delay(500);\r\n" +
                    "  delay(3000);\r\n" +
                    "  Keyboard.press(KEY_LEFT_GUI);\r\n" +
                    "  Keyboard.press('r');\r\n" +
                    "  Keyboard.releaseAll();\r\n" +
                    "  delay(500);\r\n" +
                    $"  Keyboard.print(\"{Url}\");\r\n" +
                    "  delay(500);\r\n" +
                    "  typeKey(KEY_RETURN);\r\n" +
                    "  Keyboard.end();\r\n}\r\n\r\n" +
                    "void loop() {}";

                File.WriteAllText(@"test\test.ino", sketch);
                Console.WriteLine("done");

                return "Done Doubtfully 😎";
            }
            catch (Exception ex)
            {
                return $"Failed SuccessFully 😁: {ex}";
            }

        }
    }

}
