using System;
using System.IO;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;

namespace OOD_24L_01180689.src.logging
{
    internal class FileLogger : ILogger
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

        public void Log(string message)
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

        public void Update(IDUpdateArgs e)
        {
            Log($"Updating from ID {e.ObjectID} to ID {e.NewObjectID}");
            if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity oldEnt) &&
                !DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out Entity newEnt))
            {
                Log($"ID Updated from ID {e.ObjectID} to ID {e.NewObjectID} successfully");
            }
            else if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity oldEnt2) &&
                     DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out Entity newEnt2))
            {
                Log($"Entity with ID {e.NewObjectID} already exists");
            }
            else
            {
                Log($"Entity ID update failed");
            }
        }

        public void Update(PositionUpdateArgs e)
        {
            Log($"Updating position of entity with ID {e.ObjectID}");
            if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity ent))
            {
                if (ent as Flight != null)
                {
                    Flight flight = (Flight)ent;
                    Log(
                        $"Position updated Lat: {flight.Latitude} -> {e.Latitude}, Lon: {flight.Longitude} -> {e.Longitude}, AMSL: {flight.AMSL} -> {e.AMSL} successfully");

                }
                else
                {
                    Log("Not a flight");
                }
            }
            else
            {
                Log("Entity with ID not found");
            }
        }

        public void Update(ContactInfoUpdateArgs e)
        {
            Log($"Updating contact info of entity with ID {e.ObjectID}");

            if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity ent))
            {
                if (ent as Person != null)
                {
                    Person person = (Person)ent;
                    Log(
                        $"Contact Info updated Email: {person.Email} ->, {e.EmailAddress}, Phone Number: {person.Phone} {e.PhoneNumber} successfully");

                }
                else
                {
                    Log("Entity is not a person");
                }
            }
            else
            {
                Log("Entity with ID not found");
            }
        }
    }
}