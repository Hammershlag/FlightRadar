using System.Text;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands.command;

public class DisplayCommand : Command
{
    private readonly Dictionary<string, int> field_max_len = new();
    private readonly List<Entity> valid_entities = new();
    public ConditionsList conditionsList = new();
    public List<string> object_fields = new();


    public DisplayCommand(string object_class, List<string> object_fields, ConditionsList conditionsList)
    {
        this.object_class = object_class;
        this.object_fields = object_fields;
        this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
    }

    public override bool Execute()
    {
        PreProcess();
        CreateValidEntities();
        CalculateMaxLength();
        PrintHeader();
        PrintValidEntities();

        return true;
    }

    private void CalculateMaxLength()
    {
        foreach (var field in object_fields) field_max_len[field] = field.Length;
        foreach (var field in object_fields)
        foreach (var valid in valid_entities)
        {
            var value = valid.GetFieldValue(field);
            var valueLength = value?.Length ?? 0;

            if (!field_max_len.ContainsKey(field) || field_max_len[field] < valueLength)
                field_max_len[field] = valueLength;
        }
    }

    private void PreProcess()
    {
        if (object_fields.Count == 1 && object_fields[0] == "*")
            object_fields = Entity.entityFactories[object_class].Create().fieldGetters.Keys.ToList();
    }

    private void PrintValidEntities()
    {
        foreach (var valid in valid_entities)
        {
            var sb = new StringBuilder();
            foreach (var field in object_fields)
            {
                var value = valid.GetFieldValue(field);
                var padding = (field_max_len[field] - (value?.Length ?? 0)) / 2;

                sb.Append(new string(' ', padding * 2));

                if ((field_max_len[field] - (value?.Length ?? 0)) % 2 != 0)
                    sb.Append(' ');

                sb.Append(value);

                sb.Append("  |  ");
            }

            Console.WriteLine(sb.ToString());
        }
    }


    private void CreateValidEntities()
    {
        var def = Entity.entityFactories[object_class].Create();
        foreach (var obj in DataStorage.GetInstance.GetIDEntityMap().Values)
            if (def.TryParse(obj, out var output))
            {
                if (!conditionsList.Check(output)) continue;
                valid_entities.Add(output);
            }
    }

    private void PrintHeader()
    {
        var headerBuilder = new StringBuilder();
        var separatorBuilder = new StringBuilder();

        foreach (var field in object_fields)
        {
            var padding = 1;

            headerBuilder.Append(field);
            headerBuilder.Append(new string(' ',
                field_max_len[field] - field.Length));
            headerBuilder.Append("  |  ");
        }

        separatorBuilder.Append('-', headerBuilder.Length - 2);


        for (var i = 0; i < headerBuilder.Length && i < separatorBuilder.Length; i++)
            if (headerBuilder[i] == '|')
                separatorBuilder[i] = '+';

        Console.WriteLine(headerBuilder.ToString());
        Console.WriteLine(separatorBuilder.ToString());
    }
}