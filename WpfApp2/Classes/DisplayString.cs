using System;
using System.IO;


namespace WpfApp2.Classes
{
    internal class DisplayString
    {
        public string Path = "test";
        public string Text { get; set; }

        public DisplayString(string text)
        {
            Text = text;
        }

        public string DisplayStringSketch()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                string sketch = @"
#include ""Keyboard.h""

void typeKey(int key) {
    Keyboard.press(key);
    delay(50);
    Keyboard.release(key);
}

/* Init function */
void setup() {
    // Begining the Keyboard stream
    Keyboard.begin();

    // Wait 500ms
    delay(500);

    delay(3000);

    Keyboard.press(KEY_LEFT_GUI);
    Keyboard.press('r');
    Keyboard.releaseAll();

    delay(500);

    Keyboard.print(""Notepad"");

    delay(500);

    typeKey(KEY_RETURN);

    delay(750);" +

        $@"
    Keyboard.print(""{Text}"");" +

        @"
    typeKey(KEY_RETURN);

    // Ending stream
    Keyboard.end();
}

/* Unused endless loop */
void loop() {}";

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
