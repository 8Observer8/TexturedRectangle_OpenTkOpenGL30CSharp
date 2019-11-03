
using System;
using System.IO;

namespace TexturedRectangle
{
    static class Logger
    {
        private static string _fileName = "info.txt";

        private static StreamWriter _sw;

        public static void Init()
        {
            _sw = File.AppendText(_fileName);
        }

        public static void Clear()
        {
            // Clear file if it exists
            if (File.Exists(_fileName))
            {
                File.WriteAllText(_fileName, "");
            }
        }

        public static void Print(string message)
        {
            // Save a message to the file
            _sw.WriteLine(message);

            // Show to the console
            Console.WriteLine(message);
        }

        public static void Close()
        {
            _sw.Close();
        }
    }
}
