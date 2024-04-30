using OOD_24L_01180689.src.console.commands;
using ReactiveUI;

namespace OOD_24L_01180689.src.dto.entities
{
    public abstract class Entity
    {
        protected Dictionary<string, Func<IComparable>> fieldGetters = new Dictionary<string, Func<IComparable>>();
        private static readonly Dictionary<Condition.ConditionType, Func<IComparable, IComparable, bool>> comparisonFunctions =
            new Dictionary<Condition.ConditionType, Func<IComparable, IComparable, bool>>()
            {
                { Condition.ConditionType.LESS_EQUAL, (left, right) => left.CompareTo(right) <= 0 },
                { Condition.ConditionType.GREATER_EQUAL, (left, right) => left.CompareTo(right) >= 0 },
                { Condition.ConditionType.EQUAL, (left, right) => left.CompareTo(right) == 0 },
                { Condition.ConditionType.NOT_EQUAL, (left, right) => left.CompareTo(right) != 0 },
                { Condition.ConditionType.LESS, (left, right) => left.CompareTo(right) < 0 },
                { Condition.ConditionType.GREATER, (left, right) => left.CompareTo(right) > 0 }
            };



        public string Type { get; set; }
        public ulong ID { get; set; }

        protected Entity(string type, ulong ID)
        {
            InitializeFieldGetters();
            this.ID = ID;
            Type = type;
        }

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

            Func<IComparable, IComparable> fieldValueFunc = (value) => value;

            if (!fieldGetters.TryGetValue(condition.FieldLeft.ToString(), out leftGetter))
            {
                leftGetter = () => fieldValueFunc(condition.FieldLeft);
            }

            if (!fieldGetters.TryGetValue(condition.FieldRight.ToString(), out rightGetter))
            {
                rightGetter = () => fieldValueFunc(condition.FieldRight);
            }

            IComparable left = leftGetter();
            IComparable right = rightGetter();

            Func<IComparable, IComparable, bool> comparisonFunction;
            if (!comparisonFunctions.TryGetValue(condition.Type, out comparisonFunction))
            {
                return false;
            }

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
                IComparable value = getter();
                return value?.ToString() ?? string.Empty;
            }
            else
            {
                return "No field";
            }
        }

        protected abstract void InitializeFieldGetters();
    }
}