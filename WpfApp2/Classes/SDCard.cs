using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp2.Classes
{
    internal class SDCard
    {
        public string Path = "test";

        public string SDCardSketch()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                string sketch =
"#include <SPI.h>\n" +
"#include <SD.h>\n" +
"#include <string.h>\n" +
"#include \"Keyboard.h\"\n\n" +
"File myFile;\n" +
"boolean first = true;\n" +
"String DEFAULT_FILE_NAME = \"script.txt\";\n\n" +
"void setup() {\n" +
"  if (!SD.begin(4)) {\n" +
"    return;\n" +
"  }\n\n" +
"  myFile = SD.open(DEFAULT_FILE_NAME);\n" +
"  if (myFile) {\n" +
"    Keyboard.begin();\n\n" +
"    String line = \"\";\n" +
"    while (myFile.available()) {\n" +
"      char m = myFile.read();\n" +
"      if (m == '\\n'){\n" +
"        Line(line);\n" +
"        line = \"\";\n" +
"      }\n" +
"      else if((int) m != 13)\n" +
"      {\n" +
"        line += m;\n" +
"      }\n" +
"    }\n" +
"    Line(line);\n" +
"    myFile.close();\n" +
"  }\n" +
"  Keyboard.end();\n" +
"}\n\n" +
"void Line(String l)\n" +
"{\n" +
"  int space_1 = l.indexOf(\" \");\n" +
"  if (space_1 == -1)\n" +
"  {\n" +
"    Press(l);\n" +
"  }\n" +
"  else if (l.substring(0,space_1) == \"STRING\")\n" +
"  {\n" +
"    Keyboard.print(l.substring(space_1 + 1));\n" +
"  }\n" +
"  else if (l.substring(0,space_1) == \"DELAY\")\n" +
"  {\n" +
"    int delaytime = l.substring(space_1 + 1).toInt();\n" +
"    delay(delaytime);\n" +
"  }\n" +
"  else if(l.substring(0,space_1) == \"REM\"){}\n" +
"  else\n" +
"  {\n" +
"    String remain = l;\n" +
"    while(remain.length() > 0)\n" +
"    {\n" +
"      int latest_space = remain.indexOf(\" \");\n" +
"      if (latest_space == -1)\n" +
"      {\n" +
"        Press(remain);\n" +
"        remain = \"\";\n" +
"      }\n" +
"      else\n" +
"      {\n" +
"        Press(remain.substring(0, latest_space));\n" +
"        remain = remain.substring(latest_space + 1);\n" +
"      }\n" +
"      delay(5);\n" +
"    }\n" +
"  }\n" +
"  Keyboard.releaseAll();\n" +
"}\n\n" +
"void Press(String b)\n" +
"{\n" +
"  if(b.length() == 1)\n" +
"  {\n" +
"    char c = b[0];\n" +
"    Keyboard.press(c);\n" +
"  }\n" +
"  else if (b.equals(\"ENTER\"))\n" +
"  {\n" +
"    Serial.print(\"Before Enter\");\n" +
"    Keyboard.press(KEY_RETURN);\n" +
"    Serial.print(\"After Enter\");\n" +
"  }\n" +
"  else if (b.equals(\"CTRL\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_LEFT_CTRL);\n" +
"  }\n" +
"  else if (b.equals(\"SHIFT\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_LEFT_SHIFT);\n" +
"  }\n" +
"  else if (b.equals(\"ALT\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_LEFT_ALT);\n" +
"  }\n" +
"  else if (b.equals(\"GUI\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_LEFT_GUI);\n" +
"  }\n" +
"  else if (b.equals(\"UP\") || b.equals(\"UPARROW\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_UP_ARROW);\n" +
"  }\n" +
"  else if (b.equals(\"DOWN\") || b.equals(\"DOWNARROW\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_DOWN_ARROW);\n" +
"  }\n" +
"  else if (b.equals(\"LEFT\") || b.equals(\"LEFTARROW\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_LEFT_ARROW);\n" +
"  }\n" +
"  else if (b.equals(\"RIGHT\") || b.equals(\"RIGHTARROW\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_RIGHT_ARROW);\n" +
"  }\n" +
"  else if (b.equals(\"DELETE\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_DELETE);\n" +
"  }\n" +
"  else if (b.equals(\"PAGEUP\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_PAGE_UP);\n" +
"  }\n" +
"  else if (b.equals(\"PAGEDOWN\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_PAGE_DOWN);\n" +
"  }\n" +
"  else if (b.equals(\"HOME\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_HOME);\n" +
"  }\n" +
"  else if (b.equals(\"ESC\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_ESC);\n" +
"  }\n" +
"  else if (b.equals(\"INSERT\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_INSERT);\n" +
"  }\n" +
"  else if (b.equals(\"TAB\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_TAB);\n" +
"  }\n" +
"  else if (b.equals(\"END\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_END);\n" +
"  }\n" +
"  else if (b.equals(\"CAPSLOCK\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_CAPS_LOCK);\n" +
"  }\n" +
"  else if (b.equals(\"F1\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F1);\n" +
"  }\n" +
"  else if (b.equals(\"F2\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F2);\n" +
"  }\n" +
"  else if (b.equals(\"F3\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F3);\n" +
"  }\n" +
"  else if (b.equals(\"F4\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F4);\n" +
"  }\n" +
"  else if (b.equals(\"F5\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F5);\n" +
"  }\n" +
"  else if (b.equals(\"F6\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F6);\n" +
"  }\n" +
"  else if (b.equals(\"F7\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F7);\n" +
"  }\n" +
"  else if (b.equals(\"F8\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F8);\n" +
"  }\n" +
"  else if (b.equals(\"F9\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F9);\n" +
"  }\n" +
"  else if (b.equals(\"F10\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F10);\n" +
"  }\n" +
"  else if (b.equals(\"F11\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F11);\n" +
"  }\n" +
"  else if (b.equals(\"F12\"))\n" +
"  {\n" +
"    Keyboard.press(KEY_F12);\n" +
"  }\n" +
"  else if (b.equals(\"SPACE\"))\n" +
"  {\n" +
"    Keyboard.press(' ');\n" +
"  }\n" +
"  Serial.print(\"JOB DONE\");\n" +
"}\n";

                File.WriteAllText(@"test\test.ino", sketch);
                //Console.WriteLine("done");

                return "Script Is Ready to be compiled";
            }
            catch (Exception ex)
            {
                return $"Error: Script Building Failed{ex}";
            }

        }
    }
}
