using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO.Ports;
using System.Windows;

namespace WpfApp2.Classes
{
    internal class ArduinoHandler
    {
        private string[] ports = SerialPort.GetPortNames();
        private string sketchpath = "test\\test.ino";
        private string command_compile, command_upload; // Arduino-cli commands to compile and upload the sketch
        private string result;
        public string ComPort { get; set; }

        public ArduinoHandler()
        {
            try
            {
                ComPort = ports[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("No COM PORT found, make sure your board is connected");
                Console.WriteLine(ex);
            }

            command_compile = $@"arduino-cli.exe compile --fqbn arduino:avr:leonardo ""{sketchpath}"" --config-dir .";
            command_upload = $@"arduino-cli.exe upload -p {ComPort} --fqbn arduino:avr:leonardo ""{sketchpath}"" --config-dir .";
        }
        
        public string[] DisplayPorts()
        {
            foreach (string port in ports)
                Console.WriteLine(port);
            return ports;
        }

        public string CompileSketch()
        {
            result = RunCMD(command_compile);
            return result;
        }

        public string UploadSketch()
        {
            //MessageBox.Show(ComPort);
            RunCMD(command_compile);
            result = RunCMD(command_upload);
            return result;
        }

        private string RunCMD(string command)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "CMD",
                Arguments = "/C" + command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            //Console.WriteLine(output);
            if (!string.IsNullOrEmpty(output))
            {
                return output;
            }

            else if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            return null;
        }

    }
}
