using System;
using System.IO;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.logging
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
        }

        public void Log(string message)
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

        public void Update(IDUpdateArgs e)
        {
            Console.WriteLine($"Updating from ID {e.ObjectID} to ID {e.NewObjectID}");
            if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out Entity newEnt) &&
                !DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity oldEnt))
            {
                Console.WriteLine($"ID Update from {e.ObjectID} to {e.NewObjectID} successfully");
            }
            else if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity oldEnt2) &&
                     !DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out Entity newEnt2))
            {
                Console.WriteLine($"Entity update failed");
            }
            else if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity oldEnt3) &&
                     DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out Entity newEnt3))
            {
                Console.WriteLine($"Entity with ID {e.NewObjectID} already exists");
            }
            else if (!DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity oldEnt4))
            {
                Console.WriteLine($"Entity with ID {e.ObjectID} doesn't exist");
            }
        }

        public void Update(PositionUpdateArgs e)
        {
            Console.WriteLine($"Updating position of entity with ID {e.ObjectID}");
            if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity ent))
            {
                if (ent as Flight != null)
                {
                    Flight flight = (Flight)ent;
                    Console.WriteLine(
                        $"Position Update from {flight.Latitude}, {flight.Longitude}, {flight.AMSL} to {e.Latitude}, {e.Longitude}, {e.AMSL}");
                    flight.Latitude = e.Latitude;
                    flight.Longitude = e.Longitude;
                    flight.AMSL = e.AMSL;
                }
                else
                {
                    Console.WriteLine("Not a flight");
                }
            }
            else
            {
                Console.WriteLine("Entity with ID not found");
            }
        }

        public void Update(ContactInfoUpdateArgs e)
        {
            Console.WriteLine($"Updating contact info of entity with ID {e.ObjectID}");

            if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out Entity ent))
            {
                if (ent as Person != null)
                {
                    Person person = (Person)ent;
                    Console.WriteLine(
                        $"Contact Info Update from {person.Email}, {person.Phone} to {e.EmailAddress}, {e.PhoneNumber}");
                    person.Email = e.EmailAddress;
                    person.Phone = e.PhoneNumber;
                }
                else
                {
                    Console.WriteLine("Entity is not a person");
                }
            }
            else
            {
                Console.WriteLine("Entity with ID not found");
            }
        }
    }
}