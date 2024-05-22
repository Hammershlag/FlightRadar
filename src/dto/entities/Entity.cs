using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.entityFactories;
using OOD_24L_01180689.src.factories.entityFactories.airports;
using OOD_24L_01180689.src.factories.entityFactories.cargo;
using OOD_24L_01180689.src.factories.entityFactories.flights;
using OOD_24L_01180689.src.factories.entityFactories.people;
using OOD_24L_01180689.src.factories.entityFactories.planes;

namespace OOD_24L_01180689.src.dto.entities;

public abstract class Entity
{
    private static readonly Dictionary<Condition.ConditionType, Func<IComparable, IComparable, bool>>
        comparisonFunctions =
            new()
            {
                { Condition.ConditionType.LESS_EQUAL, (left, right) => left.CompareTo(right) <= 0 },
                { Condition.ConditionType.GREATER_EQUAL, (left, right) => left.CompareTo(right) >= 0 },
                { Condition.ConditionType.EQUAL, (left, right) => left.CompareTo(right) == 0 },
                { Condition.ConditionType.NOT_EQUAL, (left, right) => left.CompareTo(right) != 0 },
                { Condition.ConditionType.LESS, (left, right) => left.CompareTo(right) < 0 },
                { Condition.ConditionType.GREATER, (left, right) => left.CompareTo(right) > 0 }
            };

    public static Dictionary<string, EntityFactory> entityFactories = new()
    {
        { "Airport", new AirportFactory() },
        { "Cargo", new CargoFactory() },
        { "Flight", new FlightFactory() },
        { "Crew", new CrewFactory() },
        { "Passenger", new PassengerFactory() },
        { "CargoPlane", new CargoPlaneFactory() },
        { "PassengerPlane", new PassengerPlaneFactory() }
    };

    public Dictionary<string, Func<IComparable>> fieldGetters = new();
    public Dictionary<string, Action<IComparable>> fieldSetters = new();

    protected Entity(string type, ulong ID)
    {
        InitializeFieldGetters();
        InitializeFieldSetters();
        this.ID = ID;
        Type = type;
    }

    public string Type { get; set; }
    public ulong ID { get; set; }

    public ulong getID()
    {
        return ID;
    }

    public override string ToString()
    {
        return $"Entity: {Type} {ID}";
    }

    public bool CheckCondition(Condition condition)
    {
        Func<IComparable> leftGetter;
        Func<IComparable> rightGetter;

        Func<IComparable, IComparable> fieldValueFunc = value => value;

        if (!fieldGetters.TryGetValue(condition.FieldLeft.ToString().ToUpper(), out leftGetter))
            leftGetter = () => fieldValueFunc(condition.FieldLeft);

        if (!fieldGetters.TryGetValue(condition.FieldRight.ToString().ToUpper(), out rightGetter))
            rightGetter = () => fieldValueFunc(condition.FieldRight);

        var left = leftGetter();
        var right = rightGetter();

        Func<IComparable, IComparable, bool> comparisonFunction;
        if (!comparisonFunctions.TryGetValue(condition.Type, out comparisonFunction)) return false;

        try
        {
            return comparisonFunction(left, right);
        }
        catch (ArgumentException e)
        {
            return false;
        }
    }

    public string GetFieldValue(string field_name)
    {
        Func<IComparable> getter;
        if (fieldGetters.TryGetValue(field_name.ToUpper(), out getter))
        {
            var value = getter();
            return value?.ToString() ?? string.Empty;
        }

        return "No field";
    }

    public abstract bool TryParse(Entity input, out Entity output);

    protected virtual void InitializeFieldGetters()
    {
        fieldGetters["ID"] = () => ID;
        fieldGetters["TYPE"] = () => Type;
    }

    protected virtual void InitializeFieldSetters()
    {
        fieldSetters["ID"] = value =>
        {
            var prev = ID;
            ID = value == null || DataStorage.GetInstance.GetIDEntityMap()
                .TryGetValue((ulong)value, out var ignore)
                ? DataStorage.GetInstance.MaxID() + 1
                : (ulong)value;
            if (prev == null) return;
            foreach (var flight in DataStorage.GetInstance.GetFlights()) flight.UpdateIDs(prev, ID);
        };
    }
}