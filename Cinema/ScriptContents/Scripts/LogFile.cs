using System;
using System.IO;

namespace Scripts
{
    static public class LogFile
    {
        static public string LogPath { private set; get; } = $"log.txt";

        static LogFile()
        {
            if (File.Exists(LogPath))
            {
                using (StreamWriter streamWriter = File.AppendText(LogPath))
                {
                    streamWriter.WriteLine();
                }
            }
        }

        static public void Log(string TextInfo, string TypeInfo)
        {
            if (!File.Exists(LogPath))
            {
                using (StreamWriter streamWriter = File.CreateText(LogPath))
                {
                    streamWriter.WriteLine($"[{DateTime.UtcNow}]=>[{TypeInfo}]: {TextInfo}");
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(LogPath))
                {
                    streamWriter.WriteLine($"[{DateTime.UtcNow}]=>[{TypeInfo}]: {TextInfo}");
                }
            }
        }
    }
}
