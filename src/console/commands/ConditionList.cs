using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands;

public class ConditionsList
{
    public enum Conjunctions
    {
        AND,
        OR
    }

    private static readonly Dictionary<Conjunctions, Func<bool, bool, bool>> LogicalOperations = new()
    {
        { Conjunctions.AND, (a, b) => a && b },
        { Conjunctions.OR, (a, b) => a || b }
    };

    public ConditionsList()
    {
        Conditions = new List<Condition>();
        AndOrs = new List<Conjunctions>();
    }

    public List<Condition> Conditions { get; set; }
    public List<Conjunctions> AndOrs { get; set; }

    public void AddCondition(Condition condition)
    {
        Conditions.Add(condition);
    }

    public bool Check(Entity ent)
    {
        if (Conditions.Count == 0) return true;

        var result = ent.CheckCondition(Conditions[0]);

        if (Conditions.Count == 1) return result;

        result = Conditions.Skip(1)
            .Zip(AndOrs, (condition, conjunction) => (Condition: condition, Conjunction: conjunction))
            .Aggregate(result, (currentResult, pair) =>
            {
                var conditionResult = ent.CheckCondition(pair.Condition);
                return LogicalOperations[pair.Conjunction](currentResult, conditionResult);
            });

        return result;
    }
}