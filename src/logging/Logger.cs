using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;

namespace OOD_24L_01180689.src.logging;

public abstract class Logger : ILogger
{
    public abstract void Log(string message);

    public virtual void Update(IDUpdateArgs e)
    {
        Log($"Updating from ID {e.ObjectID} to ID {e.NewObjectID}");
        if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out var oldEnt) &&
            !DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out var newEnt))
            Log($"ID Updated from ID {e.ObjectID} to ID {e.NewObjectID} successfully");
        else if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out var oldEnt2) &&
                 DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.NewObjectID, out var newEnt2))
            Log($"Entity with ID {e.NewObjectID} already exists");
        else
            Log("Entity ID update failed");
    }

    public virtual void Update(PositionUpdateArgs e)
    {
        Log($"Updating position of entity with ID {e.ObjectID}");
        if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out var ent))
        {
            if (ent as Flight != null)
            {
                var flight = (Flight)ent;
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

    public virtual void Update(ContactInfoUpdateArgs e)
    {
        Log($"Updating contact info of entity with ID {e.ObjectID}");

        if (DataStorage.GetInstance.GetIDEntityMap().TryGetValue(e.ObjectID, out var ent))
        {
            if (ent as Person != null)
            {
                var person = (Person)ent;
                Log(
                    $"Contact Info updated Email: {person.Email} -> {e.EmailAddress}, Phone Number: {person.Phone} -> {e.PhoneNumber} successfully");
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