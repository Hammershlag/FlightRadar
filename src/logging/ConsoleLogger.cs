using System;
using System.IO;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.logging
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger()
        {
        }

        public override void Log(string message)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now}: {message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging: {ex.Message}");
            }
        }
    }
}