using System;
using System.Collections.Generic;
using System.Linq;
using OOD_24L_01180689.src.console.commands.command;
using OOD_24L_01180689.src.factories.commandFactories;

namespace OOD_24L_01180689.src.console.commands.parser
{
    public class CommandParser
    {
        private readonly Dictionary<string, CommandFactory> factories = new Dictionary<string, CommandFactory>()
        {
            {"add", new AddCommandFactory()},
            {"display", new DisplayCommandFactory()},
            {"delete", new RemoveCommandFactory()},
            {"update", new UpdateCommandFactory()},
        };

        private readonly ConditionParser conditionParser = new ConditionParser();

        public Command Parse(string command)
        {
            List<string> parts = command.Split(' ').ToList();
            string commandType = parts[0].ToLower();

            if (factories.ContainsKey(commandType))
            {
                CommandFactory factory = factories[commandType];
                parts.RemoveAt(0);

                string object_class = "";
                Dictionary<string, IComparable> key_val_set = null;
                List<string> object_fields = null;
                ConditionsList conditionsList = null;
                List<string> testFromOneField = new List<string>()
                    { "Airport", "Cargo", "Flight", "Crew", "Passenger", "CargoPlane", "PassengerPlane" };

                if (!testFromOneField.Contains(parts[0]))
                {
                    object_fields = parts[0].Split(',').ToList();
                    parts.RemoveAt(0);
                    parts.RemoveAt(0);
                }

                object_class = parts[0];
                parts.RemoveAt(0);



                while(parts.Count > 0){
                    if (parts[0] == "where")
                    {
                        conditionsList = ParseConditionsList(parts.Skip(1).ToArray());
                        break;
                    } else if (parts[0] == "set" || parts[0] == "new")
                    {
                        parts.RemoveAt(0);

                        key_val_set = ParseKeyValSet(parts[0]);
                        parts.RemoveAt(0);
                    }
                }

                return factory.Create(object_class, key_val_set, object_fields, conditionsList);
            }
            else
            {
                ConsoleHandler.DisplayHelp();
            }

            return null;
        }

        private Dictionary<string, IComparable>? ParseKeyValSet(string part)
        {
            // Remove the parentheses and split the string by commas
            string[] pairs = part.Trim('(', ')').Split(';');

            // Initialize the dictionary to store key-value pairs
            Dictionary<string, IComparable> keyValSet = new Dictionary<string, IComparable>();

            // Iterate over each key-value pair
            foreach (string pair in pairs)
            {
                // Split the pair into key and value
                string[] keyValue = pair.Split('=');

                // Extract the key and value
                string key = keyValue[0];
                string value = keyValue[1];

                // Remove any leading or trailing spaces
                key = key.Trim().ToUpper();
                value = value.Trim();

                // Determine the data type of the value and parse it accordingly
                if (ulong.TryParse(value, out ulong ulongValue))
                {
                    keyValSet[key] = ulongValue;
                }
                else if (float.TryParse(value, out float floatValue))
                {
                    keyValSet[key] = floatValue;
                }
                else if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    // Remove the double quotes from the string value
                    string stringValue = value.Trim('"');
                    keyValSet[key] = stringValue;
                }
                else
                {
                    throw new FormatException($"Invalid value format for key '{key}'.");
                }
            }

            return keyValSet;
        }


        private ConditionsList ParseConditionsList(string[] parts)
        {
            if (parts.Length == 0)
            {
                return null;
            }

            string conditionsString = string.Join(" ", parts);

            ConditionParser cp = new ConditionParser();
            return cp.Parse(conditionsString);
        }

    }
}
