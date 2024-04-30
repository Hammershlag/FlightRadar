using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.IO;

namespace OOD_24L_01180689.src.console.commands
{
    public class Condition : ICondition
    {

        public enum ConditionType
        {
            EQUAL,
            NOT_EQUAL,
            GREATER,
            LESS,
            GREATER_EQUAL,
            LESS_EQUAL
        }

        public Condition(IComparable left, IComparable right, ConditionType type)
        {
            FieldLeft = left;
            FieldRight = right;
            Type = type;
        }

        public IComparable FieldLeft { get; set; }
        public IComparable FieldRight { get; set; }

        public ConditionType Type { get; set; }

        public bool Check()
        {
            int comparisonResult = FieldLeft.CompareTo(FieldRight);

            switch (Type)
            {
                case ConditionType.EQUAL:
                    return comparisonResult == 0;
                case ConditionType.NOT_EQUAL:
                    return comparisonResult != 0;
                case ConditionType.GREATER:
                    return comparisonResult > 0;
                case ConditionType.LESS:
                    return comparisonResult < 0;
                case ConditionType.GREATER_EQUAL:
                    return comparisonResult >= 0;
                case ConditionType.LESS_EQUAL:
                    return comparisonResult <= 0;
                default:
                    throw new InvalidOperationException("Unknown condition type.");
            }
        }


    }
}
