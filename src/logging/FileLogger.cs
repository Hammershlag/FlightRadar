using System;
using System.IO;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;

namespace OOD_24L_01180689.src.logging
{
    internal class FileLogger : Logger
    {
        private string filename = "";

        public FileLogger(string filename)
        {
            this.filename = filename;
        }

        public FileLogger()
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
            string logDirectory = Path.Combine(dir, "data");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            string todayLogFileName = Path.Combine(logDirectory, $"log_{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            if (!File.Exists(todayLogFileName))
            {
                using (StreamWriter writer = File.CreateText(todayLogFileName))
                {
                    writer.WriteLine($"Log created on {DateTime.Now}\n");
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(todayLogFileName))
                {
                    writer.WriteLine($"\nLog updated on {DateTime.Now}\n");
                }
            }


            filename = todayLogFileName;
        }

        public override void Log(string message)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filename))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging: {ex.Message}");
            }
        }
    }
}