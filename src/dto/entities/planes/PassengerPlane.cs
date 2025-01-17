﻿using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.planes;

public class PassengerPlane : Plane, IReportable
{
    public PassengerPlane() : base("Wrong", ulong.MaxValue, "Wrong", "Wrong", "Wrong")
    {
    }

    public PassengerPlane(string type, ulong id, string serial, string countryISO, string model,
        ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) :
        base(type, id, serial, countryISO, model)
    {
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }

    public ushort FirstClassSize { get; set; }
    public ushort BusinessClassSize { get; set; }
    public ushort EconomyClassSize { get; set; }


    public void Accept(INewsVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override string ToString()
    {
        return
            $"PassengerPlane: {Type} {ID} {Serial} {CountryISO} {Model} {FirstClassSize} {BusinessClassSize} {EconomyClassSize}";
    }

    public override bool TryParse(Entity input, out Entity output)
    {
        if (input as PassengerPlane != null)
        {
            output = (PassengerPlane)input;
            return true;
        }

        output = default(PassengerPlane);
        return false;
    }

    protected override void InitializeFieldGetters()
    {
        base.InitializeFieldGetters();
        fieldGetters["SERIAL"] = () => Serial;
        fieldGetters["COUNTRYISO"] = () => CountryISO;
        fieldGetters["MODEL"] = () => Model;
        fieldGetters["FIRSTCLASSSIZE"] = () => FirstClassSize;
        fieldGetters["BUSINESSCLASSSIZE"] = () => BusinessClassSize;
        fieldGetters["ECONOMYCLASSSIZE"] = () => EconomyClassSize;
    }

    protected override void InitializeFieldSetters()
    {
        base.InitializeFieldSetters();
        fieldSetters["TYPE"] = value => Type = "PP";
        fieldSetters["SERIAL"] = value => Serial = value == null ? "Uninitialized" : (string)value;
        fieldSetters["COUNTRYISO"] = value => CountryISO = value == null ? "Uninitialized" : (string)value;
        fieldSetters["MODEL"] = value => Model = value == null ? "Uninitialized" : (string)value;
        fieldSetters["FIRSTCLASSSIZE"] = value => FirstClassSize = value == null ? ushort.MaxValue : (ushort)value;
        fieldSetters["BUSINESSCLASSSIZE"] =
            value => BusinessClassSize = value == null ? ushort.MaxValue : (ushort)value;
        fieldSetters["ECONOMYCLASSSIZE"] = value => EconomyClassSize = value == null ? ushort.MaxValue : (ushort)value;
    }
}