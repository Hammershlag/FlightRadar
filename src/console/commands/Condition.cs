﻿namespace OOD_24L_01180689.src.console.commands;

public class Condition
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
}